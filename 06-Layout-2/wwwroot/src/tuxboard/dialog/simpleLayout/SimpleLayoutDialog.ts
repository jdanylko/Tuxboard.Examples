import { defaultLayoutItemSelector, defaultLayoutListSelector, defaultSaveLayoutButtonSelector } from "../../common";
import { Tuxboard } from "../../tuxboard";
import { BaseDialog } from "../BaseDialog";

export class SimpleLayoutDialog extends BaseDialog {

    dashboardData: string;

    constructor(
        selector: string,
        private tuxboard: Tuxboard) {
        super(selector);
        this.initialize();
    }

    initialize = () => {
        this.getDom().addEventListener('shown.bs.modal',
            () => this.loadDialog());
    }

    getService = () => this.tuxboard.getService();

    public getSaveLayoutButton = () => this.getDom().querySelector(defaultSaveLayoutButtonSelector) as HTMLButtonElement;
    public getLayoutList = () => this.getDom().querySelector(defaultLayoutListSelector);
    public getLayoutItems = () => this.getLayoutList().querySelectorAll(defaultLayoutItemSelector);

    private loadDialog = () => {
        this.getService().getSimpleLayoutDialog()
            .then((data: string) => {
                this.getDom().querySelector('.modal-body').innerHTML = data;
                this.attachEvents();
            });
    }

    public getSelected = () => this.getDom().querySelector("li.selected");
    public getSelectedId = () => this.getSelected().getAttribute('data-id');

    public getLayoutRowId = () => {
        const tab = this.tuxboard.getTab();
        const layoutRows = this.tuxboard.getLayoutRowCollection(tab);
        return layoutRows[0].getLayoutRowId();
    }

    public clearSelected = () => {
        Array.from(this.getLayoutItems()).forEach((item: HTMLLIElement) => {
            item.classList.remove("selected");
        })
    }

    public attachEvents = () => {
        const items = this.getLayoutItems();
        Array.from(items).forEach((item: HTMLLIElement) => {
            item?.removeEventListener('click', () => { this.listItemOnClick(item); });
            item?.addEventListener('click', () => { this.listItemOnClick(item); });
        })

        const saveButton = this.getSaveLayoutButton();
        saveButton?.removeEventListener("click", this.saveLayoutClick);
        saveButton?.addEventListener("click", this.saveLayoutClick);
    }

    public listItemOnClick = (item: HTMLLIElement) => {
        this.clearSelected();
        item.classList.add("selected");
    }

    public saveLayoutClick = (ev: Event) => {
        ev.preventDefault();
        this.saveLayout();
    }

    private saveLayout = () => {
        const layoutRowId = this.getLayoutRowId();
        this.getService().saveSimpleLayout(layoutRowId, this.getSelectedId())
            .then((data: string) => {
                this.dashboardData = data;
                this.hideDialog();
            })
    }
}
