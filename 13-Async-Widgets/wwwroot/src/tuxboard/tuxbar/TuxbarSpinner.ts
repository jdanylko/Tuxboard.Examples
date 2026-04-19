import { hideElement, showElement } from "../common";
import { Tuxbar } from "./Tuxbar";
import { TuxbarControl } from "./TuxbarControl";

export class TuxbarSpinner extends TuxbarControl {
    constructor(tuxBar: Tuxbar, selector: string) {
        super(tuxBar, selector);
    }

    public getDom = () => this.tuxBar.getDom().querySelector(this.selector);
    public show = () => { showElement(this.getDom()); }
    public hide = () => { hideElement(this.getDom()); }
}
