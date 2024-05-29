import { ready } from "./tuxboard/common";
import { Tuxboard } from "./tuxboard/tuxboard";

ready(() => {
    const dashboard = new Tuxboard();
    dashboard.initialize();
})
