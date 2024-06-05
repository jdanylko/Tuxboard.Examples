import { TuxbarButton } from "./TuxbarButton";
import { Tuxbar } from "./Tuxbar";
import { defaultTuxbarMessageSelector } from "../common";
import { TuxbarMessage } from "./TuxbarMessage";

export class RefreshButton extends TuxbarButton {

    constructor(tb: Tuxbar, sel: string) {

        super(tb, sel);

        const element = this.getDom();
        if (element) {
            element.removeEventListener("click", this.onClick, false);
            element.addEventListener("click", this.onClick, false);
        }
    }

    onClick = (ev: MouseEvent) => {
        const message = this.tuxBar.get(defaultTuxbarMessageSelector) as TuxbarMessage;
        if (message) {
            message.setMessage("It's clicked.", false);
        }
    };

    getDom = () => this.tuxBar.getDom().querySelector(this.selector);
}
