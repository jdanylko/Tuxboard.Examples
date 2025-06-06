import { dataIdAttribute, defaultLayoutRowSelector } from "../common";
import { LayoutRow } from "./LayoutRow";

export class LayoutRowCollection {

    private selector = defaultLayoutRowSelector;

    private layoutRows = this.parent.querySelectorAll<HTMLDivElement>(this.selector);

    constructor(
        private readonly parent: HTMLDivElement
    ) { }

    getLayoutRows = () => Array.from(this.layoutRows)
        .map((element, index) => this.createLayoutRow(element, index));

    createLayoutRow = (element: HTMLDivElement, index: number) => {
        const id = element.getAttribute(dataIdAttribute);
        const row = new LayoutRow(this.parent, id);
        row.setIndex(index);
        return row;
    }
}
