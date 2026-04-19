import { dataIdAttribute, defaultAddButtonSelector, defaultWidgetListItemSelector, defaultWidgetSelectionSelector, noPeriod } from "../../common";
import { BaseDialog } from "../BaseDialog";
export class AddWidgetDialog extends BaseDialog {
    constructor(selector, tuxboard) {
        super(selector);
        this.tuxboard = tuxboard;
        this.allowRefresh = false;
        this.initialize = () => {
            this.getDom().addEventListener('shown.bs.modal', () => this.loadDialog());
        };
        this.getService = () => this.tuxboard.getService();
        this.getAddWidgetButton = () => this.getDom().querySelector(defaultAddButtonSelector);
        this.getWidgetItems = () => this.getDom().querySelectorAll(defaultWidgetListItemSelector);
        this.loadDialog = () => {
            this.getService().getAddWidgetDialog()
                .then((data) => {
                this.getDom().querySelector('.modal-body').innerHTML = data;
                this.attachEvents();
            });
        };
        this.getSelected = () => this.getDom().querySelector("li" + defaultWidgetSelectionSelector);
        this.getSelectedId = () => this.getSelected().getAttribute(dataIdAttribute);
        this.clearSelected = () => {
            Array.from(this.getWidgetItems()).forEach((item) => {
                item.classList.remove(noPeriod(defaultWidgetSelectionSelector));
            });
        };
        this.attachEvents = () => {
            const items = this.getWidgetItems();
            Array.from(items).forEach((item) => {
                item.removeEventListener('click', () => { this.listItemOnClick(item); });
                item.addEventListener('click', () => { this.listItemOnClick(item); });
            });
            const addButton = this.getAddWidgetButton();
            addButton === null || addButton === void 0 ? void 0 : addButton.removeEventListener("click", this.addWidgetToLayout, false);
            addButton === null || addButton === void 0 ? void 0 : addButton.addEventListener("click", this.addWidgetToLayout, false);
        };
        this.listItemOnClick = (item) => {
            this.clearSelected();
            item.classList.add(noPeriod(defaultWidgetSelectionSelector));
        };
        this.addWidgetToLayout = (ev) => {
            ev.preventDefault();
            ev.stopImmediatePropagation();
            this.getService().addWidget(this.getSelectedId())
                .then(() => {
                this.allowRefresh = true;
                this.hideDialog();
            });
        };
        this.initialize();
    }
}
