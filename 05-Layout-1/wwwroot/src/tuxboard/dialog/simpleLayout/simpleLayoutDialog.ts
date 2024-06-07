import { defaultLayoutItemSelector, defaultLayoutListSelector, defaultSaveLayoutButtonSelector, defaultSimpleLayoutDialogSelector } from "../../common";
import { Tuxboard } from "../../tuxboard";
import { BaseDialog } from "../BaseDialog";

export class SimpleLayoutDialog extends BaseDialog {

    public resultMessage;

    constructor(private tuxboard: Tuxboard) {
        super(defaultSimpleLayoutDialogSelector);
    }

    public getSaveLayoutButton = () => this.getDom().querySelector(defaultSaveLayoutButtonSelector) as HTMLButtonElement;
    public layoutListExists = () => !!this.getDom().querySelector(defaultLayoutListSelector);
    public getLayoutList = () => this.getDom().querySelector(defaultLayoutListSelector);
    public getLayoutItems = () => this.getLayoutList().querySelectorAll(defaultLayoutItemSelector);

    public getSelected = () => this.getDom().querySelector("li.selected");
    public getSelectedId = () => this.getSelected().getAttribute('data-id');

    public getLayoutRowId = () => {
        const tab = this.tuxboard.getTab();
        const layoutRows = this.tuxboard.getLayoutRowCollection(tab);
        return layoutRows[0].getLayoutRowId();
    }

    public setLayoutDialog = (layoutData) => {
        if (!layoutData) return;

        const body: Node = createNodesFromHtml(layoutData);
        const modalBody: HTMLElement = this.getDom().querySelector(this.dialogBodySelector);
        if (modalBody) {
            clearNodes(modalBody);
            modalBody.appendChild(body);
            if (this.layoutListExists()) {
                this.attachEvents();
            }
        }
    };

    public clearSelected = () => {
        Array.from(this.getLayoutItems()).forEach((item: HTMLLIElement) => {
            item.classList.remove("selected");
        })
    }

    public attachEvents = () => {
        const items = this.getLayoutItems();
        Array.from(items).forEach((item: HTMLLIElement) => {
            item.onclick = () => {
                this.clearSelected();
                item.classList.add("selected");
            }
        })

        const saveButton = this.getSaveLayoutButton();
        saveButton?.addEventListener("click", (ev: Event) => {
            ev.preventDefault();
            this.saveLayout();
        });
    }

    private saveLayout = () => {
        const layoutRowId = this.getLayoutRowId();
        this._service.saveLayoutType(layoutRowId, this.getSelectedId())
            .then((data) => {
                this.resultMessage = data;
                this.hideDialog();
            })
    }
}