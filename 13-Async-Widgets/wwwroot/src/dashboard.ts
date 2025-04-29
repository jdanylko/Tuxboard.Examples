import { defaultTuxbarSelector, ready } from "./tuxboard/common";
import { Tuxbar } from "./tuxboard/tuxbar/Tuxbar";
import { Tuxboard } from "./tuxboard/tuxboard";

ready(async () => {
    const dashboard = new Tuxboard();
    await dashboard.initialize();

    const tuxbar = new Tuxbar(dashboard, defaultTuxbarSelector);
    tuxbar.initialize();
})
