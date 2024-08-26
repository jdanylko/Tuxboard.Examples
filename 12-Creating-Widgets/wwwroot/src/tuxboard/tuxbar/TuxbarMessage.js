import { TuxbarControl } from "./TuxbarControl";
export class TuxbarMessage extends TuxbarControl {
    constructor(tuxBar, selector) {
        super(tuxBar, selector);
        this.getDom = () => this.tuxBar.getDom().querySelector(this.selector);
        this.setMessage = (message, fade) => {
            const control = this.getDom();
            if (control) {
                control.innerHTML = message;
            }
        };
        this.clearMessage = () => {
            const control = this.getDom();
            if (control) {
                control.innerHTML = "";
            }
        };
    }
}
