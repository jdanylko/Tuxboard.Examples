import { defaultColumnSelector } from "../common";
import { Column } from "./Column";

export class ColumnCollection {

    private selector = defaultColumnSelector;

    private columns = Array.from(this.parent.querySelectorAll<HTMLDivElement>(this.selector));

    constructor(
        private readonly parent: HTMLDivElement,
        private readonly layoutRowId: string
    ) { }

    fromLayoutRow = () =>
        this.columns.map(
            (element: HTMLDivElement, index: number) => this.toColumn(element, index)
        );

    toColumn = (element: HTMLDivElement, index: number) => {
        const column = new Column(element);
        column.setIndex(index);
        column.setLayoutRowId(this.layoutRowId);
        return column;
    }
}