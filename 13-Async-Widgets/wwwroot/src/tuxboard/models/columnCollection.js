import { defaultColumnSelector } from "../common";
import { Column } from "./Column";
export class ColumnCollection {
    constructor(parent, layoutRowId) {
        this.parent = parent;
        this.layoutRowId = layoutRowId;
        this.selector = defaultColumnSelector;
        this.columns = Array.from(this.parent.querySelectorAll(this.selector));
        this.fromLayoutRow = () => this.columns.map((element, index) => this.toColumn(element, index));
        this.toColumn = (element, index) => {
            const column = new Column(element);
            column.setIndex(index);
            column.setLayoutRowId(this.layoutRowId);
            return column;
        };
    }
}
