import { ITuxbarControl } from "./ITuxbarControl";
import { Tuxbar } from "./Tuxbar";

export class TuxbarControl implements ITuxbarControl {

    constructor(public readonly tuxBar: Tuxbar, public readonly selector: string) {  }
}

