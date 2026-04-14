import * as Bootstrap from "bootstrap";
export class BaseDialog {
    constructor(selector) {
        this.selector = selector;
        this.dialogBodySelector = ".modal-body";
        this.getDialogInstance = () => Bootstrap.Modal.getOrCreateInstance(this.getDialog());
        this.getDom = () => this.getDialog();
        this.getDialog = () => document.querySelector(this.selector);
        this.showDialog = () => this.getDialogInstance().show();
        this.hideDialog = () => this.getDialogInstance().hide();
    }
}
