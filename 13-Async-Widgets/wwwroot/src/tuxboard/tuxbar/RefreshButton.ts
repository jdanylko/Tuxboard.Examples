import { TuxbarButton } from "./TuxbarButton";
import { Tuxbar } from "./Tuxbar";
import { TuxbarSpinner } from "./TuxbarSpinner";
import { defaultTuxbarSpinnerSelector } from "../common";

export class RefreshButton extends TuxbarButton {

    constructor(tb: Tuxbar, sel: string) {

        super(tb, sel);

        const element = this.getDom();
        element?.removeEventListener("click", this.onClick, false);
        element?.addEventListener("click", this.onClick, false);
    }

    onClick = (ev: MouseEvent) => {

        const spinner = this.tuxBar.get(defaultTuxbarSpinnerSelector) as TuxbarSpinner;
        spinner?.show();

        try
        {
            this.tuxBar.getTuxboard().refresh();
        }
        finally
        {
            spinner?.hide();
        }
    }

    getDom = () => this.tuxBar.getDom().querySelector(this.selector);
}
