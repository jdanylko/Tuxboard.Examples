import { hideElement, showElement } from "../common";
import { TuxbarControl } from "./TuxbarControl";
export class TuxbarSpinner extends TuxbarControl {
    constructor(tuxBar, selector) {
        super(tuxBar, selector);
        this.getDom = () => this.tuxBar.getDom().querySelector(this.selector);
        this.show = () => { showElement(this.getDom()); };
        this.hide = () => { hideElement(this.getDom()); };
    }
}
