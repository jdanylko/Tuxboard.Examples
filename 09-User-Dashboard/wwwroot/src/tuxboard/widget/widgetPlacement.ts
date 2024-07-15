
import {
    collapsedToggleSelector,
    dataIdAttribute,
    defaultGeneralOverlaySelector,
    defaultLoadingSelector,
    defaultWidgetBodySelector,
    defaultWidgetSelector,
    hideElement,
    staticAttribute,
    showElement
} from "../common";
import { WidgetProperties } from "../dto/WidgetProperties";


export class WidgetPlacement {

    private selector = defaultWidgetSelector;

    private widgetOverlaySelector = defaultGeneralOverlaySelector + defaultLoadingSelector;

    constructor(
        private parent: HTMLDivElement,
        private placementId: string = "",
        private index: number = 0,
        private columnIndex: number = 0
    ) { }

    getSelectorWithPlacementId = (placementId: string) => `[${this.getAttributeName()}='${placementId}']`;


    getSelector = () => `${this.selector}[${this.getAttributeName()}='${this.placementId}']`;

    getDom = (): HTMLElement => document.querySelector(this.getSelector());
    getOverlay = (): HTMLElement => this.getDom().querySelector(this.widgetOverlaySelector);

    isCollapsed = () => this.getDom().classList.contains(collapsedToggleSelector);
    isStatic = () => this.getDom().getAttribute(staticAttribute) === "true";
    getBody = () => this.getDom().querySelector(defaultWidgetBodySelector);

    hideBody = () => {
        const body = this.getBody();
        if (body) {
            hideElement(body);
        }
    }

    showBody = () => {
        const body = this.getBody();
        if (body) {
            showElement(body);
        }
    }

    showOverlay = () => {
        const overlay = this.getOverlay();
        if (overlay) {
            showElement(overlay);
        }
    }

    hideOverlay = () => {
        const overlay = this.getOverlay();
        if (overlay) {
            hideElement(overlay);
        }
    }

    public getAttributeName = () => dataIdAttribute;

    public getPlacementId = (): string => this.placementId;

    public getProperties = () => new WidgetProperties(
        this.placementId,
        this.columnIndex,
        this.index,
        this.parent.getAttribute(this.getAttributeName())
    );
}
