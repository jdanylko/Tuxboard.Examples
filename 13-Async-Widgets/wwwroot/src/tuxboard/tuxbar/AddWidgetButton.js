import { TuxbarButton } from "./TuxbarButton";
import { defaultAddWidgetDialogSelector } from "../common";
import { AddWidgetDialog } from "../dialog/addWidget/AddWidgetDialog";
export class AddWidgetButton extends TuxbarButton {
    constructor(tb, sel) {
        super(tb, sel);
        this.dialog = null;
        this.onClick = (ev) => {
            if (!this.dialog) {
                this.dialog = new AddWidgetDialog(defaultAddWidgetDialogSelector, this.tuxBar.getTuxboard());
            }
            if (this.dialog) {
                this.dialog.getDom().removeEventListener("hide.bs.modal", () => this.refresh(this.dialog), false);
                this.dialog.getDom().addEventListener("hide.bs.modal", () => this.refresh(this.dialog), false);
                this.dialog.showDialog();
            }
        };
        this.refresh = (dialog) => {
            if (dialog.allowRefresh) {
                this.tuxBar.getTuxboard().refresh();
            }
        };
        this.getDom = () => this.tuxBar.getDom().querySelector(this.selector);
        const element = this.getDom();
        element === null || element === void 0 ? void 0 : element.removeEventListener("click", this.onClick, false);
        element === null || element === void 0 ? void 0 : element.addEventListener("click", this.onClick, false);
    }
}
