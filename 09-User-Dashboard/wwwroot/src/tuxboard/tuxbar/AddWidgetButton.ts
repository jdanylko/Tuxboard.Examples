import { TuxbarButton } from "./TuxbarButton";
import { Tuxbar } from "./Tuxbar";
import { defaultAddWidgetDialogSelector } from "../common";
import { AddWidgetDialog } from "../dialog/addWidget/AddWidgetDialog";

export class AddWidgetButton extends TuxbarButton {

    constructor(tb: Tuxbar, sel: string) {

        super(tb, sel);

        const element = this.getDom();
        element?.removeEventListener("click", this.onClick, false);
        element?.addEventListener("click", this.onClick, false);
    }

    onClick = (ev: MouseEvent) => {

        const dialog = new AddWidgetDialog(
            defaultAddWidgetDialogSelector,
            this.tuxBar.getTuxboard());

        if (dialog) {
            dialog.getDom().removeEventListener("hide.bs.modal", () => this.refresh(dialog), false);
            dialog.getDom().addEventListener("hide.bs.modal", () => this.refresh(dialog), false);

            dialog.showDialog();
        }
    };

    refresh = (dialog: AddWidgetDialog) => {
        if (dialog.allowRefresh) {
            this.tuxBar.getTuxboard().refresh();
        }
    };

    getDom = () => this.tuxBar.getDom().querySelector(this.selector);
}
