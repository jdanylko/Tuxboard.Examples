import { dataIdAttribute, defaultTabSelector } from "../common";
import { Layout } from "./layout";

export class Tab {

    private layout: Layout;

    private selector = defaultTabSelector;

    private tab = this.parent.querySelector(this.selector) as HTMLDivElement;

    constructor(private readonly parent: HTMLDivElement
    ) { }

    getDom = () => this.tab;
    getLayout = () => new Layout(this.getDom());
    getCurrentTab = () => this.tab;
    getCurrentTabId = () => this.getCurrentTab().getAttribute(dataIdAttribute);

    public getLayoutRows = () => new Layout(this.getDom()).getLayoutRows();
}
