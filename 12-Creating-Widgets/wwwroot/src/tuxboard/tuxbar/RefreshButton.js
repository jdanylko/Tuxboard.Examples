import { TuxbarButton } from "./TuxbarButton";
import { defaultTuxbarSpinnerSelector } from "../common";
export class RefreshButton extends TuxbarButton {
    constructor(tb, sel) {
        super(tb, sel);
        this.onClick = (ev) => {
            const spinner = this.tuxBar.get(defaultTuxbarSpinnerSelector);
            spinner === null || spinner === void 0 ? void 0 : spinner.show();
            try {
                this.tuxBar.getTuxboard().refresh();
            }
            finally {
                spinner === null || spinner === void 0 ? void 0 : spinner.hide();
            }
        };
        this.getDom = () => this.tuxBar.getDom().querySelector(this.selector);
        const element = this.getDom();
        element === null || element === void 0 ? void 0 : element.removeEventListener("click", this.onClick, false);
        element === null || element === void 0 ? void 0 : element.addEventListener("click", this.onClick, false);
    }
}
