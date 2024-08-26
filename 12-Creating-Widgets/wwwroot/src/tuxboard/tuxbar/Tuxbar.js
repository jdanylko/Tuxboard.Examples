import { defaultAddWidgetButton, defaultAdvancedLayoutButton, defaultSimpleLayoutButton, defaultTuxbarMessageSelector, defaultTuxbarRefreshButton, defaultTuxbarSpinnerSelector } from "../common";
import { RefreshButton } from "./RefreshButton";
import { TuxbarMessage } from "./TuxbarMessage";
import { TuxbarSpinner } from "./TuxbarSpinner";
import { SimpleLayoutButton } from "./simpleLayoutButton";
import { AdvancedLayoutButton } from "./AdvancedLayoutButton";
import { AddWidgetButton } from "./AddWidgetButton";
export class Tuxbar {
    constructor(tuxboard, selector) {
        this.tuxboard = tuxboard;
        this.selector = selector;
        this.controls = [];
        this.getDom = () => document.querySelector(this.selector);
        this.getTuxboard = () => this.tuxboard;
        this.getTuxboardService = () => this.getTuxboard().getService();
        this.get = (selector) => {
            return this.controls.find(ctl => ctl.selector === selector);
        };
        this.initialize = () => {
            this.controls.push(new RefreshButton(this, defaultTuxbarRefreshButton));
            this.controls.push(new AddWidgetButton(this, defaultAddWidgetButton));
            this.controls.push(new SimpleLayoutButton(this, defaultSimpleLayoutButton));
            this.controls.push(new AdvancedLayoutButton(this, defaultAdvancedLayoutButton));
            this.controls.push(new TuxbarMessage(this, defaultTuxbarMessageSelector));
            this.controls.push(new TuxbarSpinner(this, defaultTuxbarSpinnerSelector));
        };
    }
}
