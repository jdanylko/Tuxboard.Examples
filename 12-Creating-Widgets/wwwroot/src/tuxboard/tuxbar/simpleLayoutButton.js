import { TuxbarButton } from "./TuxbarButton";
import { defaultSimpleLayoutDialogSelector } from "../common";
import { SimpleLayoutDialog } from "../dialog/simpleLayout/simpleLayoutDialog";
export class SimpleLayoutButton extends TuxbarButton {
    constructor(tb, sel) {
        super(tb, sel);
        this.onClick = (ev) => {
            const dialog = new SimpleLayoutDialog(defaultSimpleLayoutDialogSelector, this.tuxBar.getTuxboard());
            if (dialog) {
                dialog.getDom().removeEventListener("hide.bs.modal", () => this.hideSimpleLayout(dialog), false);
                dialog.getDom().addEventListener("hide.bs.modal", () => this.hideSimpleLayout(dialog), false);
                dialog.showDialog();
            }
        };
        this.hideSimpleLayout = (dialog) => {
            this.tuxBar.getTuxboard().updateDashboard(dialog.dashboardData);
        };
        this.getDom = () => this.tuxBar.getDom().querySelector(this.selector);
        const element = this.getDom();
        element === null || element === void 0 ? void 0 : element.removeEventListener("click", this.onClick, false);
        element === null || element === void 0 ? void 0 : element.addEventListener("click", this.onClick, false);
    }
}
