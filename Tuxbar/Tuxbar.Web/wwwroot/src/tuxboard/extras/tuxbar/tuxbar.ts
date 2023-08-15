import { Tuxboard } from "../../tuxboard";
import { ITuxbarControl } from "./ITuxbarControl";
import { RefreshButton } from "./RefreshButton";
import { TuxbarMessage } from "./TuxbarMessage";

export class Tuxbar {

    private defaultTuxbarSelector: string = ".tuxbar";

    private controls: ITuxbarControl[] = [];

    constructor(
        readonly tuxboard: Tuxboard,
        selector: string = null) {
        this.defaultTuxbarSelector = selector || this.defaultTuxbarSelector;

        this.controls.push(new RefreshButton(this));
        this.controls.push(new TuxbarMessage(this));
    }

    public getDom() {
        return document.querySelector(this.defaultTuxbarSelector) as HTMLElement;
    }

    public getTuxboard() {
        return this.tuxboard;
    }
}