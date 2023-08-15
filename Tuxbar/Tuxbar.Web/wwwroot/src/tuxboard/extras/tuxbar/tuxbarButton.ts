import { ITuxbarControl } from "./ITuxbarControl";
import { Tuxbar } from "./tuxbar";

export class TuxbarButton implements ITuxbarControl {

    constructor(
        protected readonly tuxBar: Tuxbar,
        protected selector: string) {
    }

    getDom() {
        throw new Error("Requires a getDom() method.");
    }
}