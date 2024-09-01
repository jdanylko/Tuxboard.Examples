import { dataIdAttribute, defaultTabSelector } from "../common";
import { Layout } from "./layout";
export class Tab {
    constructor(parent) {
        this.parent = parent;
        this.selector = defaultTabSelector;
        this.tab = this.parent.querySelector(this.selector);
        this.getDom = () => this.tab;
        this.getLayout = () => new Layout(this.getDom());
        this.getCurrentTab = () => this.tab;
        this.getCurrentTabId = () => this.getCurrentTab().getAttribute(dataIdAttribute);
        this.getLayoutRows = () => new Layout(this.getDom()).getLayoutRows();
    }
}
