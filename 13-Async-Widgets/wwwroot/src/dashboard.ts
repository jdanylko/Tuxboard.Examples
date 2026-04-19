import { defaultTuxbarSelector, ready } from "./tuxboard/common";
import { Tuxbar } from "./tuxboard/tuxbar/Tuxbar";
import { Tuxboard } from "./tuxboard/tuxboard";

ready(() => {
    const dashboard = new Tuxboard();
    dashboard.initialize();

    const tuxbar = new Tuxbar(dashboard, defaultTuxbarSelector);
    tuxbar.initialize();
})
