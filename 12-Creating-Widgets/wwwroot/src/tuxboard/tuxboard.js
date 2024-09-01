import * as bootstrap from 'bootstrap';
import { dataIdAttribute, defaultColumnSelector, defaultDashboardSelector, defaultDropdownInWidgetHeaderSelector, defaultWidgetHeaderSelector, defaultWidgetStateSelector, defaultWidgetRemoveWidgetSelector, defaultWidgetSelector, getClosestByClass, getDataId, getWidgetSnapshot, isWidget, isWidgetOrColumn, noPeriod, collapsedToggleSelector } from "./common";
import { DragWidgetInfo } from "./dto/dragWidgetInfo";
import { Tab } from "./models/tab";
import { TuxboardService } from "./services/TuxboardService";
import { WidgetCollection } from "./widget/widgetCollection";
export class Tuxboard {
    constructor() {
        this.selector = defaultDashboardSelector;
        this.dashboard = document.querySelector(this.selector);
        this.service = new TuxboardService();
        this.getDashboardId = () => getDataId(this.dashboard);
        this.getTab = () => new Tab(this.dashboard);
        this.getColumns = (layoutRow) => layoutRow.getColumns();
        this.getWidget = (id) => document.querySelector(`${defaultWidgetSelector}[${dataIdAttribute}='${id}']`);
        this.initialize = () => {
            this.attachWidgetToolbarEvents();
            this.attachDragAndDropEvents();
        };
        this.getWidgetsByTab = (tab) => tab.getLayout().getWidgetPlacements();
        this.getAllColumns = (tab = null) => {
            const currentTab = tab || this.getTab();
            const layoutRows = currentTab.getLayoutRows();
            let columns = [];
            Array.from(layoutRows)
                .map((rowItem) => {
                var items = this.getColumns(rowItem);
                columns.push(...items);
            });
            return columns;
        };
        this.getLayoutRowCollection = (tab = null) => {
            const currentTab = tab || this.getTab();
            return Array.from(currentTab.getLayoutRows());
        };
        this.getWidgets = (tab = null) => {
            const currentTab = tab || this.getTab();
            let widgets = [];
            currentTab.getLayoutRows()
                .map((layoutRowItem) => {
                this.getColumns(layoutRowItem)
                    .map((column, index) => {
                    const layoutRowId = layoutRowItem.getLayoutRowId();
                    const widgetList = new WidgetCollection(column.getDom(), index, layoutRowId)
                        .getWidgets();
                    if (widgetList.length > 0) {
                        widgets.push(widgetList);
                    }
                });
            });
            return widgets;
        };
        this.getService = () => this.service;
        this.refresh = () => {
            this.service.refresh()
                .then((data) => {
                this.updateDashboard(data);
            });
        };
        this.updateDashboard = (data) => {
            if (data) {
                document.querySelector(defaultDashboardSelector).innerHTML = data;
                this.attachWidgetToolbarEvents();
                this.attachDragAndDropEvents();
            }
        };
        /* Widget Toolbar Events */
        this.attachWidgetToolbarEvents = () => {
            this.dashboard.querySelectorAll(defaultWidgetRemoveWidgetSelector)
                .forEach((item) => {
                item.addEventListener('click', (ev) => this.removeWidget(ev));
            });
            // Grab all dropdown-toggles from inside a widget's header and build them.
            document.querySelectorAll(defaultDropdownInWidgetHeaderSelector)
                .forEach((item) => {
                item.addEventListener('click', () => bootstrap.Dropdown.getOrCreateInstance(item).toggle());
            });
            // Grab all mimimize/maximize buttons and assign onClicks
            document.querySelectorAll(defaultWidgetStateSelector)
                .forEach((item) => {
                item.addEventListener('click', (ev) => { this.setWidgetState(ev); });
            });
        };
        this.setWidgetState = (ev) => {
            const target = ev.target;
            const widget = getClosestByClass(target, noPeriod(defaultWidgetSelector));
            const widgetState = this.getWidgetState(widget);
            widgetState.collapsed = !widgetState.collapsed;
            this.service.setWidgetState(widgetState.id, widgetState.collapsed)
                .then(response => {
                if (response.ok) {
                    this.updateWidgetState(widgetState.id, widgetState.collapsed);
                }
                return response;
            });
        };
        this.updateWidgetState = (id, collapsed) => {
            const widget = this.getWidget(id);
            const widgetStateButton = widget.querySelector(defaultWidgetStateSelector);
            const i = widgetStateButton.querySelector('i');
            if (collapsed) {
                widget.classList.add(collapsedToggleSelector);
                widgetStateButton.setAttribute('title', 'Restore');
                i.setAttribute('class', 'fa-regular fa-window-maximize');
            }
            else {
                widget.classList.remove(collapsedToggleSelector);
                widgetStateButton.setAttribute('title', 'Minimize');
                i.setAttribute('class', 'fa-solid fa-window-minimize');
            }
        };
        this.getWidgetState = (widget) => {
            return {
                id: widget.getAttribute(dataIdAttribute),
                collapsed: widget.classList.contains(collapsedToggleSelector)
            };
        };
        this.removeWidget = (ev) => {
            ev.preventDefault();
            const target = ev.target;
            const widget = getClosestByClass(target, noPeriod(defaultWidgetSelector));
            const widgetId = widget.getAttribute(dataIdAttribute);
            this.service.removeWidget(widgetId)
                .then(response => {
                if (response.ok) {
                    widget.remove();
                }
                return response;
            });
        };
        ////////////////////
        // Drag and Drop
        ////////////////////
        this.attachDragAndDropEvents = () => {
            const columns = this.getAllColumns(this.getTab());
            for (const column of columns) {
                column.getDom().addEventListener("dragstart", (ev) => {
                    this.dragStart(ev, column);
                }, false);
                column.getDom().addEventListener("dragover", this.dragover, false);
                column.getDom().addEventListener("dragenter", this.dragenter, false);
                column.getDom().addEventListener("dragleave", this.dragLeave, false);
                column.getDom().addEventListener("drop", (ev) => { this.drop(ev); }, false);
                column.getDom().addEventListener("dragend", (ev) => { this.dragEnd(ev); }, false);
            }
        };
        this.dragStart = (ev, column) => {
            if (ev.stopPropagation)
                ev.stopPropagation();
            ev.dataTransfer.effectAllowed = 'move';
            const elem = ev.target;
            this.dragInfo = new DragWidgetInfo(elem.getAttribute(dataIdAttribute), column.getIndex(), column.layoutRowId, column.getIndex(), column.layoutRowId);
            ev.dataTransfer.setData('text', JSON.stringify(this.dragInfo));
        };
        this.dragover = (ev) => {
            if (ev.preventDefault)
                ev.preventDefault();
            if (ev.stopPropagation)
                ev.stopPropagation();
            ev.dataTransfer.dropEffect = 'move';
            const target = ev.target;
            return isWidgetOrColumn(target);
        };
        this.dragenter = (ev) => {
            if (ev.preventDefault)
                ev.preventDefault();
            if (ev.stopPropagation)
                ev.stopPropagation();
            const target = ev.target;
            if (isWidgetOrColumn(target))
                target.classList.add('over');
        };
        this.dragLeave = (ev) => {
            if (ev.preventDefault)
                ev.preventDefault();
            if (ev.stopPropagation)
                ev.stopPropagation();
            const target = ev.target;
            if (isWidgetOrColumn(target))
                target.classList.remove("over");
        };
        this.drop = (ev) => {
            if (ev.preventDefault)
                ev.preventDefault();
            if (ev.stopPropagation)
                ev.stopPropagation();
            const targetElement = ev.target; // .column or .card-header
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
                const closestWidget = getClosestByClass(targetElement, defaultWidgetSelector);
                if (closestWidget) {
                    targetElement.insertBefore(draggedWidget, closestWidget);
                }
                else {
                    targetElement.append(draggedWidget);
                }
            }
        };
        this.dragEnd = (ev) => {
            if (ev.preventDefault)
                ev.preventDefault();
            if (ev.stopPropagation)
                ev.stopPropagation();
            this.dashboard.querySelectorAll(defaultColumnSelector)
                .forEach((elem) => elem.classList.remove("over"));
            this.dashboard.querySelectorAll(defaultWidgetHeaderSelector)
                .forEach((elem) => elem.classList.remove("over"));
            const id = this.dragInfo.placementId;
            this.dragInfo.placementList = getWidgetSnapshot(this.dragInfo, this.getTab());
            const selected = this.dragInfo.placementList
                .filter((elem) => elem.placementId === id);
            if (selected && selected.length > 0) {
                this.dragInfo.currentLayoutRowId = selected[0].layoutRowId;
                this.dragInfo.currentColumnIndex = selected[0].columnIndex;
            }
            this.service.saveWidgetPlacement(ev, this.dragInfo)
                .then((result) => console.log("Saved."));
            ev.dataTransfer.clearData();
        };
    }
}
