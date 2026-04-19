import { TuxbarButton } from "./TuxbarButton";
import { Tuxbar } from "./Tuxbar";
import { defaultSimpleLayoutDialogSelector } from "../common";
import { SimpleLayoutDialog } from "../dialog/simpleLayout/simpleLayoutDialog";

export class SimpleLayoutButton extends TuxbarButton {

    private dialog: SimpleLayoutDialog = null;
    constructor(tb: Tuxbar, sel: string) {

        super(tb, sel);

        const element = this.getDom();
        element?.removeEventListener("click", this.onClick, false);
        element?.addEventListener("click", this.onClick, false);
    }

    onClick = (ev: MouseEvent) => {

        if (!this.dialog) {
            this.dialog = new SimpleLayoutDialog(
                defaultSimpleLayoutDialogSelector,
                this.tuxBar.getTuxboard());
        }

        if (this.dialog) {
            this.dialog.getDom().removeEventListener("hide.bs.modal", () => this.hideSimpleLayout(this.dialog), false);
            this.dialog.getDom().addEventListener("hide.bs.modal", () => this.hideSimpleLayout(this.dialog), false);

            this.dialog.showDialog();
        }
    }

    hideSimpleLayout = (dialog: SimpleLayoutDialog) => {
        this.tuxBar.getTuxboard().updateDashboard(dialog.dashboardData);
    }

    getDom = () => this.tuxBar.getDom().querySelector(this.selector);
}
