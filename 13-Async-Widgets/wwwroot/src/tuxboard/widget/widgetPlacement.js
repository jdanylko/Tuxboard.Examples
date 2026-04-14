import { collapsedToggleSelector, dataIdAttribute, defaultGeneralOverlaySelector, defaultLoadingSelector, defaultWidgetBodySelector, defaultWidgetSelector, hideElement, staticAttribute, showElement, defaultWidgetHeaderSelector, defaultwidgetErrorTemplate } from "../common";
import { WidgetProperties } from "../dto/WidgetProperties";
export class WidgetPlacement {
    constructor(parent, placementId = "", index = 0, columnIndex = 0) {
        this.parent = parent;
        this.placementId = placementId;
        this.index = index;
        this.columnIndex = columnIndex;
        this.selector = defaultWidgetSelector;
        this.widgetOverlaySelector = defaultGeneralOverlaySelector + defaultLoadingSelector;
        this.getSelectorWithPlacementId = (placementId) => `[${this.getAttributeName()}='${placementId}']`;
        this.getSelector = () => `${this.selector}[${this.getAttributeName()}='${this.placementId}']`;
        this.getDom = () => document.querySelector(this.getSelector());
        this.getOverlay = () => this.getDom().querySelector(defaultGeneralOverlaySelector);
        // ** Added **
        this.getHeader = () => this.getDom().querySelector(defaultWidgetHeaderSelector);
        // ** Added **
        this.isLoading = () => this.getDom().querySelector(this.widgetOverlaySelector).classList.contains("show");
        this.isCollapsed = () => this.getDom().classList.contains(collapsedToggleSelector);
        this.isStatic = () => this.getDom().getAttribute(staticAttribute) === "true";
        this.getBody = () => this.getDom().querySelector(defaultWidgetBodySelector);
        // ** Added **
        this.showLoader = () => {
            this.hideHeader();
            this.hideBody();
            this.showOverlay();
        };
        // ** Added **
        this.hideLoader = () => {
            this.showHeader();
            this.showBody();
            this.hideOverlay();
        };
        // ** Added **
        this.showHeader = () => {
            const header = this.getHeader();
            if (header) {
                header.classList.remove('d-none');
                header.classList.add('d-flex');
            }
        };
        // ** Added **
        this.showError = () => {
            this.getBody().innerHTML = defaultwidgetErrorTemplate;
        };
        // ** Added **
        this.hideHeader = () => {
            const header = this.getHeader();
            if (header) {
                header.classList.remove('d-flex');
                header.classList.add('d-none');
            }
        };
        this.hideBody = () => {
            const body = this.getBody();
            if (body) {
                hideElement(body);
            }
        };
        this.showBody = () => {
            const body = this.getBody();
            if (body) {
                showElement(body);
            }
        };
        this.showOverlay = () => {
            const overlay = this.getOverlay();
            if (overlay) {
                showElement(overlay);
            }
        };
        this.hideOverlay = () => {
            const overlay = this.getOverlay();
            if (overlay) {
                hideElement(overlay);
            }
        };
        this.getAttributeName = () => dataIdAttribute;
        this.getPlacementId = () => this.placementId;
        this.getProperties = () => new WidgetProperties(this.placementId, this.columnIndex, this.index, this.parent.getAttribute(this.getAttributeName()));
    }
}
