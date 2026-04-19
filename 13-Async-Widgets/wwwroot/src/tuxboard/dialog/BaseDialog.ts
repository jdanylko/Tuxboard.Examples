import * as Bootstrap from "bootstrap";

export class BaseDialog {

    protected dialogBodySelector = ".modal-body";

    constructor(protected selector: string) { }

    public getDialogInstance = () => Bootstrap.Modal.getOrCreateInstance(this.getDialog());
    public getDom = () => this.getDialog();
    public getDialog = () => document.querySelector(this.selector);

    showDialog = () => this.getDialogInstance().show();
    hideDialog = () => this.getDialogInstance().hide();
}