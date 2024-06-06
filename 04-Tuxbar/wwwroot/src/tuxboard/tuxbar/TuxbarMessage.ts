import { Tuxbar } from "./Tuxbar";
import { TuxbarControl } from "./TuxbarControl";

export class TuxbarMessage extends TuxbarControl {
    constructor(tuxBar: Tuxbar, selector: string) {
        super(tuxBar, selector);
    }

    public getDom = () => this.tuxBar.getDom().querySelector(this.selector);

    public setMessage = (message: string, fade: boolean) => {
        const control = this.getDom();

        if (control) {
            control.innerHTML = message;
        }
    };

    public clearMessage = () => {
        const control = this.getDom();
        if (control) {
            control.innerHTML = "";
        }
    };
}
