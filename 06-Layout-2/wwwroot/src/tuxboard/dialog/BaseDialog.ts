import * as bootstrap from "bootstrap";
import { hideElement, showElement } from "../common";

export class BaseDialog {

    protected dialogBodySelector = ".modal-body";
    protected generalOverlaySelector = ".overlay";
    protected loadingSelector = ".loading-status";
    protected dialogOverlaySelector = this.generalOverlaySelector + this.loadingSelector;

    constructor(protected selector: string) { }

    // private getBackdrops = () => Array.from(document.querySelectorAll(".modal-backdrop"));

    public getDialogInstance = () => bootstrap.Modal.getOrCreateInstance(this.getDialog());
    public getDialog = () => document.querySelector(this.selector);
    public getDom = () => document.querySelector(this.selector);
    public getOverlay = () => this.getDom().querySelector(this.generalOverlaySelector);

    public showOverlay = () => {
        const overlay = this.getOverlay();
        if (overlay && overlay.hasAttribute("hidden")) {
            showElement(overlay);
        }
    }

    public hideOverlay = () => {
        const overlay = this.getOverlay();
        if (overlay && !overlay.hasAttribute("hidden")) {
            hideElement(overlay);
        }
    }
}