import { LayoutRowCollection } from "./layoutRowCollection";
import { defaultLayoutRowSelector } from "../common";
export class Layout {
    constructor(parent) {
        this.parent = parent;
        this.layoutRows = new LayoutRowCollection(this.parent);
        this.selector = defaultLayoutRowSelector;
        this.getLayoutRows = () => {
            if (this.layoutRows) {
                return this.layoutRows.getLayoutRows();
            }
            throw new Error("No layout rows were found.");
        };
        this.getFirstLayoutRow = () => {
            if (this.layoutRows) {
                return this.layoutRows.getLayoutRows()[0];
            }
            return null;
        };
        this.getWidgetPlacements = () => {
            const widgets = [];
            const rows = this.getLayoutRows();
            rows.map((row) => {
                const columns = row.getColumns();
                columns.map((col) => {
                    const items = col.getWidgetCollection().getWidgets();
                    widgets.push(...items);
                });
            });
            return widgets;
        };
    }
}
