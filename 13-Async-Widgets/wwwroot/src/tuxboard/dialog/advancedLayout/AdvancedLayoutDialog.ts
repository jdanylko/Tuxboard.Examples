import { createNodesFromHtml, dataIdAttribute, defaultDeleteRowLayoutButtonSelector, defaultDropdownLayoutRowTypeSelector, defaultDropdownLayoutTypesSelector, defaultLayoutItemSelector, defaultLayoutListSelector, defaultLayoutRowItemHandleSelector, defaultLayoutRowItemSelector, defaultLayoutRowListSelector, defaultLayoutRowPlaceholderSelector, defaultSaveAdvancedLayoutButtonSelector, defaultSaveLayoutButtonSelector, defaultSimpleLayoutDialogSelector, hideElement, showElement } from "../../common";
import { Tuxboard } from "../../tuxboard";
import { BaseDialog } from "../BaseDialog";
import { LayoutItem } from "./LayoutItem";
import { LayoutModel } from "./LayoutModel";

export class AdvancedLayoutDialog extends BaseDialog {

    dashboardData: string;

    private draggingElement;
    private dragX: number = 0;
    private dragY: number = 0;
    private placeHolder: HTMLLIElement;
    private isDraggingStarted: boolean = false;

    constructor(
        selector: string,
        private tuxboard: Tuxboard)
    {
        super(selector);
        this.initialize();
    }

    initialize = () => {
        this.getDom().addEventListener('shown.bs.modal',
            () => this.loadDialog());
    }

    private loadDialog = () => {
        this.getService().getAdvancedLayoutDialog()
            .then((data: string) => {
                this.getDom().querySelector('.modal-body').innerHTML = data;
                this.attachEvents();
                this.clearErrorMessage();
            });
    }

    getService = () => this.tuxboard.getService();

    public getDropdownTypes          = () => this.getDialog().querySelector(defaultDropdownLayoutTypesSelector);
    public getDropdownItems          = () => this.getLayoutList().querySelectorAll(defaultDropdownLayoutRowTypeSelector);

    public getLayoutList             = () => this.getDialog().querySelector(defaultLayoutRowListSelector);

    public getLayoutListItems        = () => this.getLayoutList().querySelectorAll(defaultLayoutRowItemSelector);
    public getLayoutListPlaceholders = () => this.getLayoutList().querySelectorAll(defaultLayoutRowPlaceholderSelector);
    public getLayoutListHandle       = () => this.getDialog().querySelector(defaultLayoutRowItemHandleSelector);

    public getDeleteButtons          = () => this.getDialog().querySelectorAll(defaultDeleteRowLayoutButtonSelector);
    public getSaveLayoutButton       = () => this.getDialog().querySelector(defaultSaveAdvancedLayoutButtonSelector) as HTMLButtonElement;
    public canDelete                 = () => this.getLayoutList().querySelectorAll('li').length >= 2;

    public getErrorContainer         = () => this.getDom().querySelector(".error-container") as HTMLSpanElement;
    public getErrorMessage           = () => this.getErrorContainer().querySelector(".error-message") as HTMLElement;

    public attachEvents = () => {

        this.initLayoutDragAndDrop();

        const deleteButtons = this.getDeleteButtons();
        this.attachDeleteEvents(deleteButtons);

        const dropdown = this.getDropdownTypes() as HTMLSelectElement;
        dropdown.onchange = () => {
            const selected = +dropdown.selectedOptions[0].value;
            dropdown.value = "0"; // reset dropdown
            this.getService().getLayoutType(selected)
                .then((data: string) => this.addNewRow(data))
        };

        const saveButton = this.getSaveLayoutButton();
        saveButton?.removeEventListener('click', this.saveLayout);
        saveButton?.addEventListener('click', this.saveLayout);
    };

    private getLayoutModel = () => {
        const tabId = this.tuxboard.getTab().getCurrentTabId();
        const layoutData = new Array<LayoutItem>();
        [].forEach.call(this.getLayoutListItems(), (liItem: HTMLLIElement, index: number) => {
            const rowTypeId = +liItem.getAttribute('data-row-type');
            let id = liItem.getAttribute(dataIdAttribute);
            if (!id) id = '0';
            layoutData.push(new LayoutItem(index, id, rowTypeId));
        });

        return new LayoutModel(layoutData, tabId);
    }

    private saveLayout = () => {
        const model = this.getLayoutModel();
        this.getService().saveAdvancedLayout(model)
            .then((data:string) => {
                this.tuxboard.updateDashboard(data);
                this.hideDialog();
            });
    }

    deleteRowEvent = (ev: Event) => {
        const row = (ev.target as HTMLElement).closest('li');
        const tabId = this.tuxboard.getTab().getCurrentTabId();
        
        this.clearErrorMessage();

        if (row && this.canDelete()) {
            var rowId = row.getAttribute(dataIdAttribute);
            this.getService().canDeleteLayoutRow(tabId, rowId)
                .then(response => {
                    if (response.ok) {
                        this.clearErrorMessage();
                        row.remove();
                    }
                    return response;
                })
                .then(response => response.json())
                .then(data => {
                    if (data && data.message !== "") {
                        this.setErrorMessage(data.message);
                    }
                })
        }
    }

    setErrorMessage = (message: string) => {
        const container = this.getErrorContainer();
        if (container) {
            const msg = this.getErrorMessage();
            if (msg) {
                msg.innerHTML = message;
                showElement(container);
            }
        }
    }

    clearErrorMessage = () => {
        const container = this.getErrorContainer();
        if (container) {
            hideElement(container);
        }
    }

    attachDeleteEvents = (buttons: NodeListOf<Element>) => {
        [].forEach.call(buttons,
            (item: HTMLElement) => {
                item.removeEventListener('click', this.deleteRowEvent, false);
                item.addEventListener('click', this.deleteRowEvent, false);
            }
        );
    }

    public addNewRow = (data: string) => {
        if (!data) return;

        const layoutList = this.getLayoutList();
        if (!layoutList) return;

        layoutList.append(createNodesFromHtml(data));
        this.initLayoutDragAndDrop();
        const lastRow = layoutList.querySelector("li:last-child") as HTMLLIElement;
        if (!lastRow) return;

        const buttons = lastRow.querySelectorAll(defaultDeleteRowLayoutButtonSelector);
        this.attachDeleteEvents(buttons);
    }

    /******** 
    DragDrop
    ***/
    public handleMouseDown = (ev: MouseEvent) => {
        const dragHandle = ev.target as Element;
        this.draggingElement = dragHandle.closest('li') as HTMLLIElement;

        const rect = this.draggingElement.getBoundingClientRect();
        this.dragX = ev.pageX - rect.left;
        this.dragY = ev.pageY - rect.top;

        document.addEventListener('mousemove', this.handleMouseMove)
        document.addEventListener('mouseup', this.handleMouseUp)
    }

    public handleMouseUp = (ev: MouseEvent) => {

        this.placeHolder && this.placeHolder.parentNode.removeChild(this.placeHolder);
        this.isDraggingStarted = false;

        this.draggingElement.style.removeProperty('top');
        this.draggingElement.style.removeProperty('left');
        this.draggingElement.style.removeProperty('position');
        this.draggingElement.style.width = "100%";

        this.dragX = null;
        this.dragY = null;
        this.draggingElement = null;

        document.removeEventListener('mousemove', this.handleMouseMove);
        document.removeEventListener('mouseup', this.handleMouseUp);
    }

    public handleMouseMove = (ev: MouseEvent) => {

        const draggingRect = this.draggingElement.getBoundingClientRect();
        const parentRect = this.getLayoutList().getBoundingClientRect();

        if (!this.isDraggingStarted) {
            this.isDraggingStarted = true;

            if (!this.placeHolder) {
                this.placeHolder = this.createPlaceholder();
            }
            this.draggingElement.parentNode.insertBefore(
                this.placeHolder,
                this.draggingElement.nextSibling
            );

            this.placeHolder.style.height = `${draggingRect.height}px`;
        }

        this.draggingElement.style.position = 'absolute';
        this.draggingElement.style.top = `${(ev.pageY - this.dragY) / 2}px`;
        this.draggingElement.style.left = parentRect.left;
        this.draggingElement.style.width = "75%";

        const previousElement = this.draggingElement.previousElementSibling;
        const nextElement = this.placeHolder.nextElementSibling;

        if (previousElement && this.isAbove(this.draggingElement, previousElement)) {
            this.swap(this.placeHolder, this.draggingElement);
            this.swap(this.placeHolder, previousElement);
        }

        if (nextElement && this.isAbove(nextElement, this.draggingElement)) {
            this.swap(nextElement, this.placeHolder);
            this.swap(nextElement, this.draggingElement);
        }
    }

    public swap = (nodeA, nodeB) => {
        const parentA = nodeA.parentNode;
        const siblingA = nodeA.nextSibling === nodeB
            ? nodeA
            : nodeA.nextSibling;

        nodeB.parentNode.insertBefore(nodeA, nodeB);

        parentA.insertBefore(nodeB, siblingA);
    }

    public isAbove = (nodeA, nodeB) => {
        const rectA = nodeA.getBoundingClientRect();
        const rectB = nodeB.getBoundingClientRect();
        return rectA.top + rectA.height / 2 < rectB.top + rectB.height / 2;
    }

    public createPlaceholder = (): HTMLLIElement => {
        const placeholder = document.createElement('li');
        placeholder.classList.add('placeholder');
        placeholder.innerHTML = '&nbsp;';
        return placeholder;
    }

    public initLayoutDragAndDrop = () => {

        const liList = this.getLayoutListItems();

        [].forEach.call(liList,
            (liItem: HTMLElement) => {
                const handle = liItem.querySelector(defaultLayoutRowItemHandleSelector);
                handle.addEventListener("mousedown", (me: MouseEvent) => this.handleMouseDown(me), false);
            }
        );
    }

}

