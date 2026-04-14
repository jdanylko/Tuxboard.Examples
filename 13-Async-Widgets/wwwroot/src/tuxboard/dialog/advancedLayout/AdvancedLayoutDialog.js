import { createNodesFromHtml, dataIdAttribute, defaultDeleteRowLayoutButtonSelector, defaultDropdownLayoutRowTypeSelector, defaultDropdownLayoutTypesSelector, defaultLayoutRowItemHandleSelector, defaultLayoutRowItemSelector, defaultLayoutRowListSelector, defaultLayoutRowPlaceholderSelector, defaultSaveAdvancedLayoutButtonSelector, hideElement, showElement } from "../../common";
import { BaseDialog } from "../BaseDialog";
import { LayoutItem } from "./LayoutItem";
import { LayoutModel } from "./LayoutModel";
export class AdvancedLayoutDialog extends BaseDialog {
    constructor(selector, tuxboard) {
        super(selector);
        this.tuxboard = tuxboard;
        this.dragX = 0;
        this.dragY = 0;
        this.isDraggingStarted = false;
        this.initialize = () => {
            this.getDom().addEventListener('shown.bs.modal', () => this.loadDialog());
        };
        this.loadDialog = () => {
            this.getService().getAdvancedLayoutDialog()
                .then((data) => {
                this.getDom().querySelector('.modal-body').innerHTML = data;
                this.attachEvents();
                this.clearErrorMessage();
            });
        };
        this.getService = () => this.tuxboard.getService();
        this.getDropdownTypes = () => this.getDialog().querySelector(defaultDropdownLayoutTypesSelector);
        this.getDropdownItems = () => this.getLayoutList().querySelectorAll(defaultDropdownLayoutRowTypeSelector);
        this.getLayoutList = () => this.getDialog().querySelector(defaultLayoutRowListSelector);
        this.getLayoutListItems = () => this.getLayoutList().querySelectorAll(defaultLayoutRowItemSelector);
        this.getLayoutListPlaceholders = () => this.getLayoutList().querySelectorAll(defaultLayoutRowPlaceholderSelector);
        this.getLayoutListHandle = () => this.getDialog().querySelector(defaultLayoutRowItemHandleSelector);
        this.getDeleteButtons = () => this.getDialog().querySelectorAll(defaultDeleteRowLayoutButtonSelector);
        this.getSaveLayoutButton = () => this.getDialog().querySelector(defaultSaveAdvancedLayoutButtonSelector);
        this.canDelete = () => this.getLayoutList().querySelectorAll('li').length >= 2;
        this.getErrorContainer = () => this.getDom().querySelector(".error-container");
        this.getErrorMessage = () => this.getErrorContainer().querySelector(".error-message");
        this.attachEvents = () => {
            this.initLayoutDragAndDrop();
            const deleteButtons = this.getDeleteButtons();
            this.attachDeleteEvents(deleteButtons);
            const dropdown = this.getDropdownTypes();
            dropdown.onchange = () => {
                const selected = +dropdown.selectedOptions[0].value;
                dropdown.value = "0"; // reset dropdown
                this.getService().getLayoutType(selected)
                    .then((data) => this.addNewRow(data));
            };
            const saveButton = this.getSaveLayoutButton();
            saveButton === null || saveButton === void 0 ? void 0 : saveButton.removeEventListener('click', this.saveLayout);
            saveButton === null || saveButton === void 0 ? void 0 : saveButton.addEventListener('click', this.saveLayout);
        };
        this.getLayoutModel = () => {
            const tabId = this.tuxboard.getTab().getCurrentTabId();
            const layoutData = new Array();
            [].forEach.call(this.getLayoutListItems(), (liItem, index) => {
                const rowTypeId = +liItem.getAttribute('data-row-type');
                let id = liItem.getAttribute(dataIdAttribute);
                if (!id)
                    id = '0';
                layoutData.push(new LayoutItem(index, id, rowTypeId));
            });
            return new LayoutModel(layoutData, tabId);
        };
        this.saveLayout = () => {
            const model = this.getLayoutModel();
            this.getService().saveAdvancedLayout(model)
                .then((data) => {
                this.tuxboard.updateDashboard(data);
                this.hideDialog();
            });
        };
        this.deleteRowEvent = (ev) => {
            const row = ev.target.closest('li');
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
                });
            }
        };
        this.setErrorMessage = (message) => {
            const container = this.getErrorContainer();
            if (container) {
                const msg = this.getErrorMessage();
                if (msg) {
                    msg.innerHTML = message;
                    showElement(container);
                }
            }
        };
        this.clearErrorMessage = () => {
            const container = this.getErrorContainer();
            if (container) {
                hideElement(container);
            }
        };
        this.attachDeleteEvents = (buttons) => {
            [].forEach.call(buttons, (item) => {
                item.removeEventListener('click', this.deleteRowEvent, false);
                item.addEventListener('click', this.deleteRowEvent, false);
            });
        };
        this.addNewRow = (data) => {
            if (!data)
                return;
            const layoutList = this.getLayoutList();
            if (!layoutList)
                return;
            layoutList.append(createNodesFromHtml(data));
            this.initLayoutDragAndDrop();
            const lastRow = layoutList.querySelector("li:last-child");
            if (!lastRow)
                return;
            const buttons = lastRow.querySelectorAll(defaultDeleteRowLayoutButtonSelector);
            this.attachDeleteEvents(buttons);
        };
        /********
        DragDrop
        ***/
        this.handleMouseDown = (ev) => {
            const dragHandle = ev.target;
            this.draggingElement = dragHandle.closest('li');
            const rect = this.draggingElement.getBoundingClientRect();
            this.dragX = ev.pageX - rect.left;
            this.dragY = ev.pageY - rect.top;
            document.addEventListener('mousemove', this.handleMouseMove);
            document.addEventListener('mouseup', this.handleMouseUp);
        };
        this.handleMouseUp = (ev) => {
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
        };
        this.handleMouseMove = (ev) => {
            const draggingRect = this.draggingElement.getBoundingClientRect();
            const parentRect = this.getLayoutList().getBoundingClientRect();
            if (!this.isDraggingStarted) {
                this.isDraggingStarted = true;
                if (!this.placeHolder) {
                    this.placeHolder = this.createPlaceholder();
                }
                this.draggingElement.parentNode.insertBefore(this.placeHolder, this.draggingElement.nextSibling);
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
        };
        this.swap = (nodeA, nodeB) => {
            const parentA = nodeA.parentNode;
            const siblingA = nodeA.nextSibling === nodeB
                ? nodeA
                : nodeA.nextSibling;
            nodeB.parentNode.insertBefore(nodeA, nodeB);
            parentA.insertBefore(nodeB, siblingA);
        };
        this.isAbove = (nodeA, nodeB) => {
            const rectA = nodeA.getBoundingClientRect();
            const rectB = nodeB.getBoundingClientRect();
            return rectA.top + rectA.height / 2 < rectB.top + rectB.height / 2;
        };
        this.createPlaceholder = () => {
            const placeholder = document.createElement('li');
            placeholder.classList.add('placeholder');
            placeholder.innerHTML = '&nbsp;';
            return placeholder;
        };
        this.initLayoutDragAndDrop = () => {
            const liList = this.getLayoutListItems();
            [].forEach.call(liList, (liItem) => {
                const handle = liItem.querySelector(defaultLayoutRowItemHandleSelector);
                handle.addEventListener("mousedown", (me) => this.handleMouseDown(me), false);
            });
        };
        this.initialize();
    }
}
