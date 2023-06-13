import { dataId, defaultLayoutRowSelector } from "../common";
import { ColumnCollection } from "./columnCollection";
export class LayoutRow {
    constructor(parent, layoutRowId) {
        this.parent = parent;
        this.layoutRowId = layoutRowId;
        this.selector = defaultLayoutRowSelector;
        this.getDom = () => {
            if (!this.layoutRow) {
                this.layoutRow = this.parent.querySelector(this.getSelector());
            }
            return this.layoutRow;
        };
        this.getColumns = () => new ColumnCollection(this.getDom(), this.id).fromLayoutRow();
        this.getLayoutRowId = () => this.id;
        this.getAttributeName = () => dataId;
        this.setIndex = (value) => this.index = value;
        this.getSelector = () => `${this.selector}[${this.getAttributeName()}='${this.id}']`;
        this.id = layoutRowId;
    }
}
