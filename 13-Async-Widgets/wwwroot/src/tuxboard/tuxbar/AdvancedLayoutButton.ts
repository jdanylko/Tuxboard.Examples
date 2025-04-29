import { TuxbarButton } from "./TuxbarButton";
import { Tuxbar } from "./Tuxbar";
import { defaultAdvancedLayoutDialogSelector } from "../common";
import { AdvancedLayoutDialog } from "../dialog/advancedLayout/AdvancedLayoutDialog";

export class AdvancedLayoutButton extends TuxbarButton {

    private dialog: AdvancedLayoutDialog = null;

    constructor(tb: Tuxbar, sel: string) {

        super(tb, sel);

        const element = this.getDom();
        element?.removeEventListener("click", this.onClick, false);
        element?.addEventListener("click", this.onClick, false);
    }

    onClick = (ev: MouseEvent) => {

        if (!this.dialog) {
            this.dialog = new AdvancedLayoutDialog(
                defaultAdvancedLayoutDialogSelector,
                this.tuxBar.getTuxboard());
        }

        if (this.dialog) {
            this.dialog.getDom().removeEventListener("hide.bs.modal", () => this.hideAdvancedLayout(this.dialog), false);
            this.dialog.getDom().addEventListener("hide.bs.modal", () => this.hideAdvancedLayout(this.dialog), false);

            this.dialog.showDialog();
        }
    }

    hideAdvancedLayout = (dialog: AdvancedLayoutDialog) => {
        this.tuxBar.getTuxboard().updateDashboard(dialog.dashboardData);
    }

    getDom = () => this.tuxBar.getDom().querySelector(this.selector);
}


