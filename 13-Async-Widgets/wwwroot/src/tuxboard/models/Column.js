import { dataIdAttribute, defaultColumnSelector } from "../common";
import { WidgetCollection } from "../widget/widgetCollection";
export class Column {
    constructor(parent) {
        this.parent = parent;
        this.selector = defaultColumnSelector;
        this.getDom = () => this.parent;
        this.getAttributeName = () => dataIdAttribute;
        this.getIndex = () => this.index;
        this.setIndex = (value) => this.index = value;
        this.setLayoutRowId = (value) => this.layoutRowId = value;
        this.getSelector = () => this.selector;
        this.getColumnSelector = () => `${this.selector}:nth-child(${this.index + 1})`;
        this.getWidgetCollection = () => new WidgetCollection(this.parent, this.index, this.layoutRowId);
    }
}
