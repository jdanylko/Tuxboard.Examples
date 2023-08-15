import { BaseService } from "../core/baseService";

export class TuxboardService extends BaseService {

    private baseApi: string = "/api/";

    private tuxRefreshUrl: string = this.baseApi + "tuxboard/";
    private tuxWidgetUrl: string = this.baseApi+"widget/{id}";

    constructor(debugParam: boolean = false) {
        super(debugParam);
    }

    public refreshService() {

        const request = new Request(this.tuxRefreshUrl,
            { method: "get" });

        return fetch(request)
            .then(this.validateResponse)
            .then(this.readResponseAsJson)
            .catch(this.logError);
    }

    public widgetService() {

        const request = new Request(this.tuxWidgetUrl,
            { method: "get" });

        return fetch(request)
            .then(this.validateResponse)
            .then(this.readResponseAsJson)
            .catch(this.logError);
    }

}