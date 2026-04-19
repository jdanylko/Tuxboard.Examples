import { dataIdAttribute, defaultLayoutRowSelector } from "../common";
import { ColumnCollection } from "./columnCollection";

export class LayoutRow {
    private layoutRowSelector: string;

    private readonly id: string;
    private index: number;
    private layoutType: number;
    private readonly selector = defaultLayoutRowSelector;

    private layoutRow;

    constructor(
        private readonly parent: HTMLDivElement,
        private readonly layoutRowId: string
    ) {
        this.id = layoutRowId;
    }

    getDom = () => {
        if (!this.layoutRow) {
            this.layoutRow = this.parent.querySelector(this.getSelector()) as HTMLDivElement;
        }
        return this.layoutRow;
    }

    getColumns = () => new ColumnCollection(this.getDom(), this.id).fromLayoutRow();
    getLayoutRowId = () => this.id;
    getAttributeName = () => dataIdAttribute;
    setIndex = (value: number) => this.index = value;
    getSelector = () => `${this.selector}[${this.getAttributeName()}='${this.id}']`;
}