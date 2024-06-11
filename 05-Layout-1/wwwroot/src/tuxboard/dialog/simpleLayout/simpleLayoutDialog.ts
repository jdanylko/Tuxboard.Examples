import { defaultLayoutItemSelector, defaultLayoutListSelector, defaultSaveLayoutButtonSelector, defaultSimpleLayoutDialogSelector } from "../../common";
import { Tuxboard } from "../../tuxboard";
import { BaseDialog } from "../BaseDialog";

export class SimpleLayoutDialog extends BaseDialog {
    constructor(
        selector: string,
        private selected: string,
        private tuxboard: Tuxboard)
    {
        super(selector);
        this.initialize();
    }

    initialize = () => {
        this.getDom().addEventListener('shown.bs.modal',
            () => this.loadDialog());
    }

    getService = () => this.tuxboard.getService();

    public getSaveLayoutButton = () => this.getDom().querySelector(defaultSaveLayoutButtonSelector) as HTMLButtonElement;
    public layoutListExists = () => !!this.getDom().querySelector(defaultLayoutListSelector);
    public getLayoutList = () => this.getDom().querySelector(defaultLayoutListSelector);
    public getLayoutItems = () => this.getLayoutList().querySelectorAll(defaultLayoutItemSelector);

    private loadDialog = () => {
        this.getService().getSimpleLayoutDialog()
            .then((data:string) => {
                this.getDom().querySelector('.modal-body').innerHTML = data;
                this.attachEvents();
            });
    }

    //selectLayout = (ev: Event) => {
    //    const item = ev.target as HTMLLIElement;
    //    const id = item.getAttribute('data-id');
    //    this.selected = id;
    //    this.getService().selectGeneralTerm(specId, id)
    //        .then(data => {
    //            this.selected = new SelectedGeneralTerm(data.title, data.template);
    //            dialog.hide();
    //        });
    //}

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
        this.getService().saveSimpleLayout(layoutRowId, this.getSelectedId())
            .then((data) => {
                // this.resultMessage = data;
                this.hideDialog();
            })
    }


    showDialog = () => this.getDialogInstance().show();
    hideDialog = () => this.getDialogInstance().hide();
}