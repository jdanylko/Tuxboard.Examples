import { TuxbarButton } from "./TuxbarButton";
import { Tuxbar } from "./Tuxbar";
import { defaultAdvancedLayoutDialogSelector } from "../common";
import { AdvancedLayoutDialog } from "../dialog/advancedLayout/AdvancedLayoutDialog";

export class AdvancedLayoutButton extends TuxbarButton {

    constructor(tb: Tuxbar, sel: string) {

        super(tb, sel);

        const element = this.getDom();
        element?.removeEventListener("click", this.onClick, false);
        element?.addEventListener("click", this.onClick, false);
    }

    onClick = (ev: MouseEvent) => {

        const dialog = new AdvancedLayoutDialog(
            defaultAdvancedLayoutDialogSelector,
            this.tuxBar.getTuxboard());

        if (dialog) {
            dialog.getDom().removeEventListener("hide.bs.modal", () => this.hideAdvancedLayout(dialog), false);
            dialog.getDom().addEventListener("hide.bs.modal", () => this.hideAdvancedLayout(dialog), false);

            dialog.showDialog();
        }
    }

    hideAdvancedLayout = (dialog: AdvancedLayoutDialog) => {
        this.tuxBar.getTuxboard().updateDashboard(dialog.dashboardData);
    }

    getDom = () => this.tuxBar.getDom().querySelector(this.selector);
}


