import { clearNodes, createFromHtml, defaultDashboardSelector, getDataId } from "./common";
import { Column } from "./models/Column";
import { LayoutRow } from "./models/LayoutRow";
import { Tab } from "./models/tab";
import { TuxboardService } from "./services/tuxboardService";
import { WidgetCollection } from "./widget/widgetCollection";
import { WidgetPlacement } from "./widget/widgetPlacement";

export class Tuxboard {

    private selector = defaultDashboardSelector
    private dashboard = document.querySelector<HTMLDivElement>(this.selector)

    private service = new TuxboardService();

    getDom = () => document.querySelector(this.selector);

    getDashboardId = (): string => getDataId(this.dashboard);

    getTab = () => new Tab(this.dashboard);

    getColumns = (layoutRow: LayoutRow) => layoutRow.getColumns();

    public getWidgetsByTab = (tab: Tab) => tab.getLayout().getWidgetPlacements();

    public async refresh() {
        await this.service.refreshService()
            .then((data: string) => {
                const db = this.getDom();
                if (db) {
                    clearNodes(db);
                    const nodes = createFromHtml(data);
                    nodes.forEach((node) => db.insertAdjacentElement("beforeend", node)); // Layout Rows
                }
            })
            .catch((err) => console.log(err));

        this.updateAllWidgets();
    }

    public updateAllWidgets() {
        const widgetPlacements = this.getWidgetsByTab(this.getTab());
        this.updateWidgets(widgetPlacements);
    }

    public updateWidgets(widgets: WidgetPlacement[]) {
        Array.from(widgets).map((placement: WidgetPlacement) => {
            placement.update();
        });
    }

    getAllColumns = (tab: Tab = null): Column[] => {
        const currentTab = tab || this.getTab()
        const layoutRows = currentTab.getLayoutRows()
        let columns = []
        Array.from(layoutRows)
            .map((rowItem: LayoutRow) => {
                var items = this.getColumns(rowItem)
                columns.push(...items)
            });
        return columns
    }

    getLayoutRowCollection = (tab: Tab = null): LayoutRow[] => {
        const currentTab = tab || this.getTab()
        return Array.from(currentTab.getLayoutRows())
    }

    getWidgets = (tab: Tab = null) => {
        const currentTab = tab || this.getTab()

        let widgets = []
        currentTab.getLayoutRows()
            .map((layoutRowItem: LayoutRow) => {
                this.getColumns(layoutRowItem)
                    .map((column: Column, index: number) => {
                        const layoutRowId = layoutRowItem.getLayoutRowId();
                        const widgetList = new WidgetCollection(column.getDom(), index, layoutRowId)
                            .getWidgets()
                        if (widgetList.length > 0) {
                            widgets.push(widgetList)
                        }
                    })
            })
        return widgets
    }
}
