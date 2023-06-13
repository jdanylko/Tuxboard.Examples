import { dataId, defaultLayoutRowSelector } from "../common";
import { LayoutRow } from "./LayoutRow";
export class LayoutRowCollection {
    constructor(parent) {
        this.parent = parent;
        this.selector = defaultLayoutRowSelector;
        this.layoutRows = this.parent.querySelectorAll(this.selector);
        this.getLayoutRows = () => Array.from(this.layoutRows)
            .map((element, index) => this.createLayoutRow(element, index));
        this.createLayoutRow = (element, index) => {
            const id = element.getAttribute(dataId);
            const row = new LayoutRow(this.parent, id);
            row.setIndex(index);
            return row;
        };
    }
}
