import { PlacementItem } from "./dto/PlacementItem";
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
export function getDataId(elem) { return elem.getAttribute(dataIdAttribute); }
export function isHidden(elem) { return elem.hasAttribute("hidden"); }
export function isDisabled(elem) { return elem.hasAttribute("disabled"); }
export function isStatic(elem) { return elem.hasAttribute(staticAttribute); }
export function noPeriod(id) { return id.startsWith(".") ? id.replace(".", "") : id; }
export function getDomWidget(id) { return document.querySelector(`[${dataIdAttribute}='${id}']`); }
export function getParent(element, classToSearch) { return getClosestByClass(element, classToSearch); }
export function isWidget(target) { return target && target.classList.contains(noPeriod(defaultWidgetHeaderSelector)); }
export function isColumn(target) { return target && target.classList.contains(noPeriod(defaultColumnSelector)); }
export function isWidgetOrColumn(target) { return isWidget(target) || isColumn(target); }
export function createNodesFromHtml(htmlString) { return document.createRange().createContextualFragment(htmlString); }
export function hideElement(elem) { elem.setAttribute('hidden', ''); }
export function showElement(elem) { elem.removeAttribute('hidden'); }
export function getColumnByPlacement(placementId) { return getClosestByClass(getDomWidget(placementId), noPeriod(defaultColumnSelector)); }
export function getClosestByClass(element, classToSearch) {
    while (element) {
        if (element.classList.contains(classToSearch)) {
            return element;
        }
        element = element.parentElement;
    }
}
export function getWidgetIndex(dragInfo, placementId) {
    const column = getColumnByPlacement(placementId);
    const columnWidgets = column.querySelectorAll(defaultWidgetSelector);
    return Array.from(columnWidgets)
        .findIndex((widget) => {
        return widget.getAttribute(dataIdAttribute) === placementId;
    });
}
export function getColumnIndexByDragInfo(placementId) {
    const column = getColumnByPlacement(placementId);
    return Array.from(column.parentElement.querySelectorAll(defaultColumnSelector))
        .findIndex((column) => column.querySelector(`[${dataIdAttribute}='${placementId}']`) != null);
}
export function getWidgetSnapshot(dragInfo, tab) {
    const widgetList = Array.from(tab.getDom().querySelectorAll(defaultWidgetSelector));
    return widgetList.map((elem) => {
        const placementId = getDataId(elem);
        const rowTemplate = getClosestByClass(elem, noPeriod(defaultLayoutRowSelector));
        const widgetIndex = getWidgetIndex(dragInfo, placementId);
        const columnIndex = getColumnIndexByDragInfo(placementId);
        return new PlacementItem(placementId, widgetIndex, rowTemplate.getAttribute(dataIdAttribute), columnIndex, isStatic(elem));
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
export function ready(fn) {
    if (document.readyState !== 'loading') {
        fn();
    }
    else {
        document.addEventListener('DOMContentLoaded', fn);
    }
}
