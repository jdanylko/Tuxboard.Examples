import { defaultAddWidgetButton, defaultAdvancedLayoutButton, defaultSimpleLayoutButton, defaultTuxbarMessageSelector, defaultTuxbarRefreshButton, defaultTuxbarSpinnerSelector } from "../common";
import { Tuxboard } from "../tuxboard";
import { ITuxbarControl } from "./ITuxbarControl";
import { RefreshButton } from "./RefreshButton";
import { TuxbarMessage } from "./TuxbarMessage";
import { TuxbarSpinner } from "./TuxbarSpinner";

import { SimpleLayoutButton } from "./simpleLayoutButton";
import { AdvancedLayoutButton } from "./AdvancedLayoutButton";
import { AddWidgetButton } from "./AddWidgetButton";

export class Tuxbar {

    private controls: ITuxbarControl[] = [];

    constructor(private readonly tuxboard: Tuxboard, private readonly selector: string) { }

    public getDom = () => document.querySelector(this.selector) as HTMLDivElement;

    public getTuxboard = () => this.tuxboard;

    public getTuxboardService = () => this.getTuxboard().getService();

    public get = (selector: string) => {
        return this.controls.find(ctl => ctl.selector === selector);
    }

    public initialize = () => {
        this.controls.push(new RefreshButton(this, defaultTuxbarRefreshButton));
        this.controls.push(new AddWidgetButton(this, defaultAddWidgetButton));

        this.controls.push(new SimpleLayoutButton(this, defaultSimpleLayoutButton));
        this.controls.push(new AdvancedLayoutButton(this, defaultAdvancedLayoutButton));

        this.controls.push(new TuxbarMessage(this, defaultTuxbarMessageSelector));

        this.controls.push(new TuxbarSpinner(this, defaultTuxbarSpinnerSelector));
    }
}

