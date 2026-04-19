import { PlacementItem } from "./dto/PlacementItem";
import { DragWidgetInfo } from "./dto/dragWidgetInfo";
import { Tab } from "./models/tab";

// Default selectors
export const defaultDashboardSelector = ".dashboard";

export const defaultTabSelector = ".dashboard-tab";
export const defaultLayoutRowSelector = ".layout-row";
export const defaultColumnSelector = ".column";

// Widgets
export const defaultWidgetSelector = ".card";
export const defaultWidgetHeaderSelector = ".card-header";
export const defaultWidgetTitleSelector = ".card-title";
export const defaultWidgetBodySelector = ".card-body";
export const defaultGeneralOverlaySelector = ".overlay";
export const defaultLoadingSelector = ".loading-status";
export const defaultWidgetErrorMessage = 'Could not load widget';
export const defaultwidgetErrorTemplate = `<h6 class="text-center">${defaultWidgetErrorMessage}</h6>`;

// Widget Settings
export const defaultWidgetSettingsSelector = ".widget-settings";
export const defaultWidgetSettingsCancelButtonSelector = ".settings-cancel";
export const defaultWidgetSettingsSaveButtonSelector = ".settings-save";
export const defaultWidgetSettingInputsSelector = ".setting-value";

/* Widget toolbar */
export const defaultWidgetToolbarSelector = ".widget-toolbar";
export const defaultWidgetDropdownSelector = ".dropdown-toggle";
export const defaultDropdownInWidgetHeaderSelector = `${defaultWidgetHeaderSelector} ${defaultWidgetDropdownSelector}`;

    /* Widget toolbar buttons */
    export const defaultWidgetRemoveWidgetSelector = ".remove-widget";
    export const defaultWidgetStateSelector = ".widget-state";
    



// Tuxbar selectors (comment out if you don't want a tuxbar)
export const defaultTuxbarSelector = ".tuxbar";

// Tuxbar Controls
export const defaultTuxbarRefreshButton = "#refresh-button";
export const defaultTuxbarMessageSelector = "#tuxbar-status";
export const defaultTuxbarSpinnerSelector = "#tuxbar-spinner";


///////////////// Dialogs (comment out what you don't want to use) /////////////////////

// Add Widget Dialog
export const defaultAddWidgetButton = "#add-widget-button";

export const defaultAddWidgetDialogSelector = "#add-widget-dialog";
export const defaultWidgetTabGroupSelector = "button.nav-link";
export const defaultWidgetListItemSelector = "li.list-group-item";
export const defaultAddButtonSelector = ".add-widget";
export const defaultWidgetSelectionSelector = ".active";

// Simple Layout Dialog
export const defaultSimpleLayoutButton = "#layout-button";

export const defaultSimpleLayoutDialogSelector = "#layout-dialog";
export const defaultLayoutListSelector = ".layout-list";
export const defaultLayoutItemSelector = "li";
export const defaultSaveLayoutButtonSelector = ".save-layout";

// Advanced Layout Dialog
export const defaultAdvancedLayoutButton = "#advanced-layout-button";

export const defaultAdvancedLayoutDialogSelector = "#advanced-layout-dialog";

export const defaultDropdownLayoutTypesSelector = ".row-layout-types";
export const defaultDropdownLayoutRowTypeSelector = `${defaultDropdownLayoutTypesSelector} option`;

export const defaultLayoutRowListSelector = ".row-layout-list";

export const defaultLayoutRowItemHandleSelector = ".handle";
export const defaultLayoutRowItemSelector = ".row-layout-item";
export const defaultLayoutRowPlaceholderSelector = ".placeholder";

export const defaultDeleteRowLayoutButtonSelector = ".row-layout-delete-button";
export const defaultSaveAdvancedLayoutButtonSelector = ".save-advanced-layout";


// Default attributes
export const dataIdAttribute = "data-id";
export const staticAttribute = "data-static";
export const collapsedToggleSelector = "collapsed";


// Common functions
export function getDataId(elem: HTMLElement)   { return elem.getAttribute(dataIdAttribute) }
export function isHidden(elem: HTMLElement)    { return elem.hasAttribute("hidden") }
export function isDisabled(elem: HTMLElement)  { return elem.hasAttribute("disabled") }
export function isStatic(elem: HTMLElement)    { return elem.hasAttribute(staticAttribute) }
export function noPeriod(id: string)           { return id.startsWith(".") ? id.replace(".", "") : id }
export function getDomWidget(id: string)       { return document.querySelector<HTMLDivElement>(`[${dataIdAttribute}='${id}']`); }
export function getParent(element: HTMLElement, classToSearch: string) { return getClosestByClass(element, classToSearch); }
export function isWidget(target: HTMLElement)  { return target && target.classList.contains(noPeriod(defaultWidgetHeaderSelector)) }
export function isColumn(target: HTMLElement)  { return target && target.classList.contains(noPeriod(defaultColumnSelector)) }
export function isWidgetOrColumn(target: HTMLElement) { return isWidget(target) || isColumn(target) }
export function createNodesFromHtml(htmlString: string) { return document.createRange().createContextualFragment(htmlString); }
export function hideElement(elem: Element) { elem.setAttribute('hidden', '') }
export function showElement(elem: Element) { elem.removeAttribute('hidden') }
export function getColumnByPlacement(placementId: string) { return getClosestByClass(getDomWidget(placementId), noPeriod(defaultColumnSelector)) }

export function getClosestByClass(element: HTMLElement, classToSearch: string) {
    while (element) {
        if (element.classList.contains(classToSearch)) {
            return element;
        }
        element = element.parentElement;
    }
}

export function getWidgetIndex(dragInfo: DragWidgetInfo, placementId: string) {
    const column = getColumnByPlacement(placementId)
    const columnWidgets = column.querySelectorAll(defaultWidgetSelector)
    return Array.from(columnWidgets)
        .findIndex((widget: HTMLElement) => {
            return widget.getAttribute(dataIdAttribute) === placementId
        });
}

export function getColumnIndexByDragInfo(placementId: string) {
    const column = getColumnByPlacement(placementId)
    return Array.from(column.parentElement.querySelectorAll(defaultColumnSelector))
        .findIndex((column: HTMLElement) =>
            column.querySelector(`[${dataIdAttribute}='${placementId}']`) != null)
}

export function getWidgetSnapshot(dragInfo: DragWidgetInfo, tab: Tab) {
    const widgetList = Array.from(tab.getDom().querySelectorAll(defaultWidgetSelector))
    return widgetList.map((elem: HTMLElement) => {
        const placementId = getDataId(elem)
        const rowTemplate = getClosestByClass(elem, noPeriod(defaultLayoutRowSelector))
        const widgetIndex = getWidgetIndex(dragInfo, placementId)
        const columnIndex = getColumnIndexByDragInfo(placementId)
        return new PlacementItem(
            placementId,
            widgetIndex,
            rowTemplate.getAttribute(dataIdAttribute),
            columnIndex,
            isStatic(elem)
        )
    });
}

export function isBefore(el1: HTMLElement, el2: HTMLElement) {
    if (el2.parentNode === el1.parentNode) {
        let cur
        for (cur = el1.previousSibling; cur; cur = cur.previousSibling) {
            if (cur === el2) {
                return true
            }
        }
    }
    return false;
}

export function isRowLayoutListItem(elem: HTMLElement) {
    return elem && elem.tagName && elem.tagName.toLowerCase() === 'li'
        && elem.classList.contains(noPeriod(defaultLayoutRowItemSelector));
}

export function clearNodes(node: Element) {
    while (node.firstChild) {
        node.firstChild.remove();
    }
}

export function createFromHtml(htmlString: string) {
    const div = document.createElement("div");
    div.innerHTML = htmlString.trim();
    return Array.from(div.children);
}

export function ready(fn) {
    if (document.readyState !== 'loading') {
        fn();
    } else {
        document.addEventListener('DOMContentLoaded', fn);
    }
}
