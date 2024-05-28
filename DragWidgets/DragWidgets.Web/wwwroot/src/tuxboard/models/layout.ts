import { LayoutRowCollection } from "./layoutRowCollection";
import { WidgetPlacement } from "../widget/widgetPlacement";
import { LayoutRow } from "./layoutRow";
import { Column } from "./column";
import { defaultLayoutRowSelector } from "../common";

export class Layout {

    private layoutRows = new LayoutRowCollection(this.parent);

    selector = defaultLayoutRowSelector;

    constructor(
        private readonly parent: HTMLDivElement
    ) { }

    getLayoutRows = () => {
        if (this.layoutRows) {
            return this.layoutRows.getLayoutRows();
        }
        throw new Error("No layout rows were found.");
    }

    public getFirstLayoutRow = () => {
        if (this.layoutRows) {
            return this.layoutRows.getLayoutRows()[0];
        }
        return null;
    }

    public getWidgetPlacements = () => {
        const widgets: WidgetPlacement[] = [];
        const rows = this.getLayoutRows();
        rows.map((row: LayoutRow) => {
            const columns = row.getColumns();
            columns.map((col: Column) => {
                const items = col.getWidgetCollection().getWidgets();
                widgets.push(...items);
            });
        });
        return widgets;
    }
}
