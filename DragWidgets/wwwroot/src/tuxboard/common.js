import { PlacementItem } from "./dto/PlacementItem";
// Default selectors
export const defaultDashboardSelector = ".dashboard";
export const defaultTabSelector = ".dashboard-tab";
export const defaultLayoutRowSelector = ".layout-row";
export const defaultColumnSelector = ".column";
export const defaultWidgetSelector = ".card";
export const defaultWidgetHeaderSelector = ".card-header";
export const defaultWidgetTitleSelector = ".card-title";
export const defaultWidgetBodySelector = ".card-body";
export const defaultGeneralOverlaySelector = ".overlay";
export const defaultLoadingSelector = ".loading-status";
export const defaultWidgetSettingsSelector = ".widget-settings";
export const defaultWidgetSettingsCancelButtonSelector = ".settings-cancel";
export const defaultWidgetSettingsSaveButtonSelector = ".settings-save";
export const defaultWidgetSettingInputsSelector = ".setting-value";
/* Widget toolbar */
export const defaultWidgetToolbarSelector = ".card-tools";
/* Widget toolbar buttons */
export const defaultWidgetRemoveWidgetSelector = ".remove-widget";
// Tuxbar selectors (comment out if you don't want a tuxbar)
export const defaultTuxbarSelector = ".tuxbar";
// Buttons
export const defaultRefreshButton = "#refresh-button";
///////////////// Dialogs (comment out what you don't want to use) /////////////////////
// Add Widget Dialog
export const defaultAddWidgetButton = "#widget-button";
export const defaultAddWidgetDialogSelector = "#widget-dialog";
export const defaultWidgetTabGroupSelector = ".widget-tabs";
export const defaultWidgetListItemSelector = "a.widget-item";
export const defaultAddWidgetButtonSelector = ".add-widget";
export const defaultWidgetSelectionSelector = ".selected";
// Change Layout Dialog
export const defaultChangeLayoutButton = "#layout-button";
export const defaultChangeLayoutDialogSelector = "#layout-dialog";
export const defaultLayoutListSelector = ".layout-list";
export const defaultLayoutItemSelector = "li";
export const defaultSaveLayoutButtonSelector = ".save-layout";
// Change Row Layout Dialog
export const defaultChangeRowLayoutButton = "#row-layout-button";
export const defaultChangeRowLayoutDialogSelector = "#row-layout-dialog";
export const defaultDeleteRowLayoutButtonSelector = ".row-layout-delete-button";
export const defaultLayoutRowListSelector = ".row-layout-list";
export const defaultLayoutRowItemSelector = ".row-layout-item";
export const defaultLayoutRowPlaceholderSelector = ".placeholder";
export const defaultDropdownLayoutTypesSelector = ".row-layout-types";
export const defaultLayoutRowTypeSelector = ".row-layout-types option";
export const defaultLayoutRowItemHandleSelector = ".handle";
export const defaultLayoutRowMessageSelector = "#row-layout-message";
export const defaultSaveRowLayoutButtonSelector = ".save-row-layout";
// Default attributes
export const dataId = "data-id";
export const isStaticAttribute = "data-static";
export const collapsedToggleSelector = "collapsed";
// Common functions
export function getDataId(elem) { return elem.getAttribute(dataId); }
export function isHidden(elem) { return elem.hasAttribute("hidden"); }
export function isDisabled(elem) { return elem.hasAttribute("disabled"); }
export function noPeriod(id) { return id.startsWith(".") ? id.replace(".", "") : id; }
export function getDomWidget(id) {
    return document.querySelector(`[${dataId}='${id}']`);
}
export function getParent(element, classToSearch) {
    return getClosestByClass(element, classToSearch);
}
export function getClosestByClass(element, classToSearch) {
    while (element) {
        if (element.classList.contains(classToSearch)) {
            return element;
        }
        element = element.parentElement;
    }
}
export function isWidget(target) {
    return target && target.classList.contains(noPeriod(defaultWidgetHeaderSelector));
}
export function isColumn(target) {
    return target && target.classList.contains(noPeriod(defaultColumnSelector));
}
export function isWidgetOrColumn(target) {
    return isWidget(target) || isColumn(target);
}
export function getColumnByPlacement(placementId) {
    const placement = getDomWidget(placementId);
    return getClosestByClass(placement, noPeriod(defaultColumnSelector));
}
export function getWidgetIndex(dragInfo, placementId) {
    const column = getColumnByPlacement(placementId);
    const columnWidgets = column.querySelectorAll(defaultWidgetSelector);
    return Array.from(columnWidgets)
        .findIndex((widget) => {
        return widget.getAttribute(dataId) === placementId;
    });
}
export function getColumnIndexByDragInfo(placementId) {
    const column = getColumnByPlacement(placementId);
    return Array.from(column.parentElement.querySelectorAll(defaultColumnSelector))
        .findIndex((column) => column.querySelector(`[${dataId}='${placementId}']`) != null);
}
export function getWidgetSnapshot(dragInfo, tab) {
    const widgetList = Array.from(tab.getDom().querySelectorAll(defaultWidgetSelector));
    return widgetList.map((elem) => {
        const placementId = getDataId(elem);
        const rowTemplate = getClosestByClass(elem, noPeriod(defaultLayoutRowSelector));
        const widgetIndex = getWidgetIndex(dragInfo, placementId);
        const columnIndex = getColumnIndexByDragInfo(placementId);
        const isStatic = elem.getAttribute(isStaticAttribute) === "true";
        return new PlacementItem(placementId, widgetIndex, rowTemplate.getAttribute(dataId), columnIndex, isStatic);
    });
}
export function isBefore(el1, el2) {
    if (el2.parentNode === el1.parentNode) {
        let cur;
        for (cur = el1.previousSibling; cur; cur = cur.previousSibling) {
            if (cur === el2) {
                return true;
            }
        }
    }
    return false;
}
export function isRowLayoutListItem(elem) {
    return elem && elem.tagName && elem.tagName.toLowerCase() === 'li'
        && elem.classList.contains(noPeriod(defaultLayoutRowItemSelector));
}
export function clearNodes(node) {
    while (node.firstChild) {
        node.firstChild.remove();
    }
}
export function createFromHtml(htmlString) {
    const div = document.createElement("div");
    div.innerHTML = htmlString.trim();
    return Array.from(div.children);
}
export function createNodesFromHtml(htmlString) {
    return document.createRange().createContextualFragment(htmlString);
}
export function ready(fn) {
    if (document.readyState !== 'loading') {
        fn();
    }
    else {
        document.addEventListener('DOMContentLoaded', fn);
    }
}
export function hideElement(elem) {
    elem.setAttribute('hidden', '');
}
export function showElement(elem) {
    elem.removeAttribute('hidden');
}
