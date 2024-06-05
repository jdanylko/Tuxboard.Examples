import { defaultTuxbarMessageSelector, defaultTuxbarRefreshButton } from "../common";
import { Tuxboard } from "../tuxboard";
import { ITuxbarControl } from "./ITuxbarControl";
import { RefreshButton } from "./RefreshButton";
import { TuxbarControl } from "./TuxbarControl";
import { TuxbarMessage } from "./TuxbarMessage";

export class Tuxbar {

    private controls: ITuxbarControl[] = [];

    constructor(private readonly tuxboard: Tuxboard, private readonly selector: string) { }

    public getDom = () => document.querySelector(this.selector) as HTMLDivElement;

    public getTuxboard = () => this.tuxboard;

    public getTuxboardService = () => this.getTuxboard().getService();

    public initialize = () => {
        this.controls.push(new RefreshButton(this, defaultTuxbarRefreshButton));
        this.controls.push(new TuxbarMessage(this, defaultTuxbarMessageSelector));
    }

    public get = (selector: string) => {
        return this.controls.find(ctl => ctl.selector === selector);
    }
}

