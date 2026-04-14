import { TuxbarButton } from "./TuxbarButton";
import { defaultAdvancedLayoutDialogSelector } from "../common";
import { AdvancedLayoutDialog } from "../dialog/advancedLayout/AdvancedLayoutDialog";
export class AdvancedLayoutButton extends TuxbarButton {
    constructor(tb, sel) {
        super(tb, sel);
        this.dialog = null;
        this.onClick = (ev) => {
            if (!this.dialog) {
                this.dialog = new AdvancedLayoutDialog(defaultAdvancedLayoutDialogSelector, this.tuxBar.getTuxboard());
            }
            if (this.dialog) {
                this.dialog.getDom().removeEventListener("hide.bs.modal", () => this.hideAdvancedLayout(this.dialog), false);
                this.dialog.getDom().addEventListener("hide.bs.modal", () => this.hideAdvancedLayout(this.dialog), false);
                this.dialog.showDialog();
            }
        };
        this.hideAdvancedLayout = (dialog) => {
            this.tuxBar.getTuxboard().updateDashboard(dialog.dashboardData);
        };
        this.getDom = () => this.tuxBar.getDom().querySelector(this.selector);
        const element = this.getDom();
        element === null || element === void 0 ? void 0 : element.removeEventListener("click", this.onClick, false);
        element === null || element === void 0 ? void 0 : element.addEventListener("click", this.onClick, false);
    }
}
