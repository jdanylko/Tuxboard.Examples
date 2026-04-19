import { TuxbarButton } from "./TuxbarButton";
import { defaultSimpleLayoutDialogSelector } from "../common";
import { SimpleLayoutDialog } from "../dialog/simpleLayout/simpleLayoutDialog";
export class SimpleLayoutButton extends TuxbarButton {
    constructor(tb, sel) {
        super(tb, sel);
        this.dialog = null;
        this.onClick = (ev) => {
            if (!this.dialog) {
                this.dialog = new SimpleLayoutDialog(defaultSimpleLayoutDialogSelector, this.tuxBar.getTuxboard());
            }
            if (this.dialog) {
                this.dialog.getDom().removeEventListener("hide.bs.modal", () => this.hideSimpleLayout(this.dialog), false);
                this.dialog.getDom().addEventListener("hide.bs.modal", () => this.hideSimpleLayout(this.dialog), false);
                this.dialog.showDialog();
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
