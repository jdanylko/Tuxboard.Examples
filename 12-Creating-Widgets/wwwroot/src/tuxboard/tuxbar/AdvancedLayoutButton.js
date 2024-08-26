import { TuxbarButton } from "./TuxbarButton";
import { defaultAdvancedLayoutDialogSelector } from "../common";
import { AdvancedLayoutDialog } from "../dialog/advancedLayout/AdvancedLayoutDialog";
export class AdvancedLayoutButton extends TuxbarButton {
    constructor(tb, sel) {
        super(tb, sel);
        this.onClick = (ev) => {
            const dialog = new AdvancedLayoutDialog(defaultAdvancedLayoutDialogSelector, this.tuxBar.getTuxboard());
            if (dialog) {
                dialog.getDom().removeEventListener("hide.bs.modal", () => this.hideAdvancedLayout(dialog), false);
                dialog.getDom().addEventListener("hide.bs.modal", () => this.hideAdvancedLayout(dialog), false);
                dialog.showDialog();
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
