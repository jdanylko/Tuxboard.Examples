import { defaultLayoutItemSelector, defaultLayoutListSelector, defaultSaveLayoutButtonSelector } from "../../common";
import { BaseDialog } from "../BaseDialog";
export class SimpleLayoutDialog extends BaseDialog {
    constructor(selector, tuxboard) {
        super(selector);
        this.tuxboard = tuxboard;
        this.initialize = () => {
            this.getDom().addEventListener('shown.bs.modal', () => this.loadDialog());
        };
        this.getService = () => this.tuxboard.getService();
        this.getSaveLayoutButton = () => this.getDom().querySelector(defaultSaveLayoutButtonSelector);
        this.getLayoutList = () => this.getDom().querySelector(defaultLayoutListSelector);
        this.getLayoutItems = () => this.getLayoutList().querySelectorAll(defaultLayoutItemSelector);
        this.loadDialog = () => {
            this.getService().getSimpleLayoutDialog()
                .then((data) => {
                this.getDom().querySelector('.modal-body').innerHTML = data;
                this.attachEvents();
            });
        };
        this.getSelected = () => this.getDom().querySelector("li.selected");
        this.getSelectedId = () => this.getSelected().getAttribute('data-id');
        this.getLayoutRowId = () => {
            const tab = this.tuxboard.getTab();
            const layoutRows = this.tuxboard.getLayoutRowCollection(tab);
            return layoutRows[0].getLayoutRowId();
        };
        this.clearSelected = () => {
            Array.from(this.getLayoutItems()).forEach((item) => {
                item.classList.remove("selected");
            });
        };
        this.attachEvents = () => {
            const items = this.getLayoutItems();
            Array.from(items).forEach((item) => {
                item === null || item === void 0 ? void 0 : item.removeEventListener('click', () => { this.listItemOnClick(item); });
                item === null || item === void 0 ? void 0 : item.addEventListener('click', () => { this.listItemOnClick(item); });
            });
            const saveButton = this.getSaveLayoutButton();
            saveButton === null || saveButton === void 0 ? void 0 : saveButton.removeEventListener("click", this.saveLayoutClick);
            saveButton === null || saveButton === void 0 ? void 0 : saveButton.addEventListener("click", this.saveLayoutClick);
        };
        this.listItemOnClick = (item) => {
            this.clearSelected();
            item.classList.add("selected");
        };
        this.saveLayoutClick = (ev) => {
            ev.preventDefault();
            this.saveLayout();
        };
        this.saveLayout = () => {
            const layoutRowId = this.getLayoutRowId();
            this.getService().saveSimpleLayout(layoutRowId, this.getSelectedId())
                .then((data) => {
                this.dashboardData = data;
                this.hideDialog();
            });
        };
        this.initialize();
    }
}
