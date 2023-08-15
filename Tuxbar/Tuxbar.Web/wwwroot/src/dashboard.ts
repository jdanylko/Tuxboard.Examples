import { ready } from "./tuxboard/common";
import { Tuxbar } from "./tuxboard/extras/tuxbar/tuxbar";
import { Tuxboard } from "./tuxboard/tuxboard";

ready(() => {
    const dashboard = new Tuxboard();
    const tuxbar = new Tuxbar(dashboard);
})