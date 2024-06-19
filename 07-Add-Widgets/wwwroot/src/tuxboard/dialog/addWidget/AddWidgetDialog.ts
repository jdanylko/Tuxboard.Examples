import { dataIdAttribute, defaultAddButtonSelector, defaultLayoutItemSelector, defaultLayoutListSelector, defaultSaveLayoutButtonSelector, defaultWidgetListItemSelector, defaultWidgetSelectionSelector, noPeriod } from "../../common";
import { Tuxboard } from "../../tuxboard";
import { BaseDialog } from "../BaseDialog";

export class AddWidgetDialog extends BaseDialog {

    allowRefresh: boolean = false;

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

    public getAddWidgetButton = () => this.getDom().querySelector(defaultAddButtonSelector) as HTMLButtonElement;
    public getWidgetItems = () => this.getDom().querySelectorAll(defaultWidgetListItemSelector);

    private loadDialog = () => {
        this.getService().getAddWidgetDialog()
            .then((data: string) => {
                this.getDom().querySelector('.modal-body').innerHTML = data;
                this.attachEvents();
            });
    }

    public getSelected = () => this.getDom().querySelector("li" + defaultWidgetSelectionSelector);
    public getSelectedId = () => this.getSelected().getAttribute(dataIdAttribute);

    public clearSelected = () => {
        Array.from(this.getWidgetItems()).forEach((item: HTMLLIElement) => {
            item.classList.remove("active");
        })
    }

    public attachEvents = () => {
        const items = this.getWidgetItems();
        Array.from(items).forEach((item: HTMLLIElement) => {
            item?.removeEventListener('click', () => { this.listItemOnClick(item); });
            item?.addEventListener('click', () => { this.listItemOnClick(item); });
        })

        const saveButton = this.getAddWidgetButton();
        saveButton?.removeEventListener("click", this.saveLayoutClick);
        saveButton?.addEventListener("click", this.saveLayoutClick);
    }

    public listItemOnClick = (item: HTMLLIElement) => {
        this.clearSelected();
        item.classList.add(noPeriod(defaultWidgetSelectionSelector));
    }

    public saveLayoutClick = (ev: Event) => {
        ev.preventDefault();
        this.addWidgetToLayout();
    }

    private addWidgetToLayout = () => {
        this.getService().addWidget(this.getSelectedId())
            .then( () => {
                this.allowRefresh = true;
                this.hideDialog();
            })
    }
}
