import * as bootstrap from 'bootstrap';
import {
    closestByClass,
    dataIdAttribute,
    defaultColumnSelector,
    defaultDashboardSelector,
    defaultDropdownInWidgetHeaderSelector,
    defaultWidgetHeaderSelector,
    defaultWidgetRemoveWidgetSelector,
    defaultWidgetSelector,
    getClosestByClass,
    getDataId,
    getWidgetSnapshot,
    isWidget,
    isWidgetOrColumn,
    noPeriod
} from "./common";
import { PlacementItem } from "./dto/PlacementItem";
import { DragWidgetInfo } from "./dto/dragWidgetInfo";
import { Column } from "./models/Column";
import { LayoutRow } from "./models/LayoutRow";
import { Tab } from "./models/tab";
import { TuxboardService } from "./services/TuxboardService";
import { WidgetCollection } from "./widget/widgetCollection";

export class Tuxboard {

    private dragInfo: DragWidgetInfo;

    private selector = defaultDashboardSelector;
    private dashboard = document.querySelector<HTMLDivElement>(this.selector);

    private service: TuxboardService = new TuxboardService();

    getDashboardId = (): string => getDataId(this.dashboard);

    getTab = () => new Tab(this.dashboard);

    getColumns = (layoutRow: LayoutRow) => layoutRow.getColumns();

    public initialize = () => {
        this.attachWidgetToolbarEvents();
        this.attachDragAndDropEvents();
    }

    public getWidgetsByTab = (tab: Tab) => tab.getLayout().getWidgetPlacements();

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
        const currentTab = tab || this.getTab();
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
                            .getWidgets();
                        if (widgetList.length > 0) {
                            widgets.push(widgetList);
                        }
                    })
            })
        return widgets;
    }

    getService = () => this.service;

    refresh = () => {
        this.service.refresh()
            .then((data:string) => {
                this.updateDashboard(data);
            })
    }

    updateDashboard = (data: string) => {
        if (data) {
            document.querySelector(defaultDashboardSelector).innerHTML = data;
            this.attachWidgetToolbarEvents();
            this.attachDragAndDropEvents();
        }
    }

    /* Widget Toolbar Events */

    attachWidgetToolbarEvents = () => {

        this.dashboard.querySelectorAll(defaultWidgetRemoveWidgetSelector)
            .forEach((item: HTMLButtonElement) => {
                item.addEventListener('click', (ev: Event) => this.removeWidget(ev))
            });

        // Grab all dropdown-toggles from inside a widget's header and build them.
        const dropdowns = Array.from(document.querySelectorAll(defaultDropdownInWidgetHeaderSelector));
        [...dropdowns].map(element => new bootstrap.Dropdown(element));
    }

    removeWidget = (ev: Event) => {
        ev.preventDefault();
        const target = ev.target as HTMLElement;
        const widget = closestByClass(target, noPeriod(defaultWidgetSelector)) as HTMLDivElement;
        const widgetId = widget.getAttribute(dataIdAttribute);
        this.service.removeWidget(widgetId)
            .then(response => {
                if (response.ok) {
                    widget.remove();
                }
                return response;
            });
    }

    ////////////////////
    // Drag and Drop
    ////////////////////

    attachDragAndDropEvents = () => {
        const columns = this.getAllColumns(this.getTab());
        for (const column of columns) {
            column.getDom().addEventListener("dragstart", (ev: DragEvent) => {
                this.dragStart(ev, column)
            }, false);
            column.getDom().addEventListener("dragover", this.dragover, false);
            column.getDom().addEventListener("dragenter", this.dragenter, false);
            column.getDom().addEventListener("dragleave", this.dragLeave, false);
            column.getDom().addEventListener("drop", (ev: DragEvent) => { this.drop(ev) }, false);
            column.getDom().addEventListener("dragend", (ev: DragEvent) => { this.dragEnd(ev); }, false);
        }
    }

    dragStart = (ev: DragEvent, column: Column) => {

        if (ev.stopPropagation) ev.stopPropagation();

        ev.dataTransfer.effectAllowed = 'move';

        const elem = ev.target as HTMLElement;

        this.dragInfo = new DragWidgetInfo(
            elem.getAttribute(dataIdAttribute),
            column.getIndex(),
            column.layoutRowId,
            column.getIndex(),
            column.layoutRowId);

        ev.dataTransfer.setData('text', JSON.stringify(this.dragInfo));
    }

    dragover = (ev: DragEvent) => {
        if (ev.preventDefault) ev.preventDefault();
        if (ev.stopPropagation) ev.stopPropagation();

        ev.dataTransfer.dropEffect = 'move';

        const target = ev.target as HTMLElement;

        return isWidgetOrColumn(target);
    }

    dragenter = (ev: DragEvent) => {
        if (ev.preventDefault) ev.preventDefault();
        if (ev.stopPropagation) ev.stopPropagation();

        const target = ev.target as HTMLElement;
        if (isWidgetOrColumn(target)) target.classList.add('over');
    }

    dragLeave = (ev: DragEvent) => {
        if (ev.preventDefault) ev.preventDefault();
        if (ev.stopPropagation) ev.stopPropagation();

        const target = ev.target as HTMLElement;
        if (isWidgetOrColumn(target)) target.classList.remove("over");
    }

    drop = (ev: DragEvent) => {
        if (ev.preventDefault) ev.preventDefault();
        if (ev.stopPropagation) ev.stopPropagation();

        const targetElement = ev.target as HTMLElement; // .column or .card-header

        this.dragInfo = JSON.parse(ev.dataTransfer.getData("text"));

        const draggedWidget = document.querySelector(`[${dataIdAttribute}='${this.dragInfo.placementId}'`);

        if (isWidget(targetElement)) {
            const widget = getClosestByClass(targetElement, noPeriod(defaultWidgetSelector));
            const column = getClosestByClass(targetElement, noPeriod(defaultColumnSelector));
            if (column && widget) {
                column.insertBefore(draggedWidget, widget);
            }
        }
        else if (targetElement.classList.contains(defaultColumnSelector.substr(1))) {
            const closestWidget = getClosestByClass(targetElement,
                defaultWidgetSelector);
            if (closestWidget) {
                targetElement.insertBefore(draggedWidget, closestWidget);
            } else {
                targetElement.append(draggedWidget);
            }
        }
    }

    dragEnd = (ev: DragEvent) => {
        if (ev.preventDefault) ev.preventDefault();
        if (ev.stopPropagation) ev.stopPropagation();

        this.dashboard.querySelectorAll(defaultColumnSelector)
            .forEach((elem: HTMLElement) => elem.classList.remove("over"));
        this.dashboard.querySelectorAll(defaultWidgetHeaderSelector)
            .forEach((elem: HTMLElement) => elem.classList.remove("over"));

        const id = this.dragInfo.placementId;

        this.dragInfo.placementList = getWidgetSnapshot(this.dragInfo, this.getTab());

        const selected = this.dragInfo.placementList
            .filter((elem: PlacementItem) => elem.placementId === id);
        if (selected && selected.length > 0) {
            this.dragInfo.currentLayoutRowId = selected[0].layoutRowId;
            this.dragInfo.currentColumnIndex = selected[0].columnIndex;
        }

        this.service.saveWidgetPlacement(ev, this.dragInfo)
            .then((result) => console.log("Saved."));

        ev.dataTransfer.clearData();
    }
}
