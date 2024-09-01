import { TuxbarButton } from "./TuxbarButton";
import { defaultAddWidgetDialogSelector } from "../common";
import { AddWidgetDialog } from "../dialog/addWidget/AddWidgetDialog";
export class AddWidgetButton extends TuxbarButton {
    constructor(tb, sel) {
        super(tb, sel);
        this.onClick = (ev) => {
            const dialog = new AddWidgetDialog(defaultAddWidgetDialogSelector, this.tuxBar.getTuxboard());
            if (dialog) {
                dialog.getDom().removeEventListener("hide.bs.modal", () => this.refresh(dialog), false);
                dialog.getDom().addEventListener("hide.bs.modal", () => this.refresh(dialog), false);
                dialog.showDialog();
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
