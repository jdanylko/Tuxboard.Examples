import { TuxbarButton } from "./TuxbarButton";
import { Tuxbar } from "./Tuxbar";
import { defaultAddWidgetDialogSelector } from "../common";
import { AddWidgetDialog } from "../dialog/addWidget/AddWidgetDialog";

export class AddWidgetButton extends TuxbarButton {

    private dialog: AddWidgetDialog = null;

    constructor(tb: Tuxbar, sel: string) {

        super(tb, sel);

        const element = this.getDom();
        element?.removeEventListener("click", this.onClick, false);
        element?.addEventListener("click", this.onClick, false);
    }

    onClick = (ev: MouseEvent) => {

        if (!this.dialog) {
            this.dialog = new AddWidgetDialog(
                defaultAddWidgetDialogSelector,
                this.tuxBar.getTuxboard());
        }

        if (this.dialog) {
            this.dialog.getDom().removeEventListener("hide.bs.modal", () => this.refresh(this.dialog), false);
            this.dialog.getDom().addEventListener("hide.bs.modal", () => this.refresh(this.dialog), false);

            this.dialog.showDialog();
        }
    };

    refresh = (dialog: AddWidgetDialog) => {
        if (dialog.allowRefresh) {
            this.tuxBar.getTuxboard().refresh();
        }
    };

    getDom = () => this.tuxBar.getDom().querySelector(this.selector);
}
