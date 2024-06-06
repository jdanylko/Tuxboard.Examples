import { dataId, defaultColumnSelector } from "../common";
import { WidgetCollection } from "../widget/widgetCollection";

export class Column {

    index: number;
    layoutRowId: string;
    private selector = defaultColumnSelector;

    constructor(
        private readonly parent: HTMLDivElement
    ) { }

    getDom = () => this.parent;
    getAttributeName = () => dataId;
    getIndex = () => this.index;
    setIndex = (value: number) => this.index = value;
    setLayoutRowId = (value: string) => this.layoutRowId = value;

    getSelector = () => this.selector;

    getColumnSelector = () => `${this.selector}:nth-child(${this.index + 1})`;

    public getWidgetCollection = () => new WidgetCollection(this.parent, this.index, this.layoutRowId);
}