﻿import { TuxbarButton } from "./TuxbarButton";
import { Tuxbar } from "./Tuxbar";
import { defaultSimpleLayoutDialogSelector } from "../common";
import { SimpleLayoutDialog } from "../dialog/simpleLayout/simpleLayoutDialog";

export class SimpleLayoutButton extends TuxbarButton {

    constructor(tb: Tuxbar, sel: string) {

        super(tb, sel);

        const element = this.getDom();
        element?.removeEventListener("click", this.onClick, false);
        element?.addEventListener("click", this.onClick, false);
    }

    onClick = (ev: MouseEvent) => {

        const dialog = new SimpleLayoutDialog(
            defaultSimpleLayoutDialogSelector,
            this.tuxBar.getTuxboard());

        if (dialog) {
            dialog.getDom().removeEventListener("hide.bs.modal", () => this.hideSimpleLayout(dialog), false);
            dialog.getDom().addEventListener("hide.bs.modal", () => this.hideSimpleLayout(dialog), false);

            dialog.showDialog();
        }
    }

    hideSimpleLayout = (dialog: SimpleLayoutDialog) => {
        this.tuxBar.getTuxboard().updateDashboard(dialog.dashboardData);
    }

    getDom = () => this.tuxBar.getDom().querySelector(this.selector);
}
