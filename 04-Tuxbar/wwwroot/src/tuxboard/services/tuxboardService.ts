import { DragWidgetInfo } from "../dto/dragWidgetInfo";
import { BaseService } from "./BaseService";

export class TuxboardService extends BaseService {

    private tuxWidgetPlacementUrl: string = "?handler=SaveWidgetPosition";
    //private tuxRefreshUrl: string = "?handler=Refresh";

    constructor(debugParam: boolean = false) {
        super(debugParam);
    }

    public saveWidgetPlacement = (ev: Event, dragInfo: DragWidgetInfo) => {

        const postData = {
            PlacementId: dragInfo.placementId,
            PreviousLayoutRowId: dragInfo.previousLayoutRowId,
            PreviousColumn: dragInfo.previousColumnIndex,
            CurrentLayoutRowId: dragInfo.currentLayoutRowId,
            CurrentColumn: dragInfo.currentColumnIndex,
            PlacementList: dragInfo.placementList,
        };

        const request = new Request(this.tuxWidgetPlacementUrl,
            {
                method: "post",
                body: JSON.stringify(postData),
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': this.getToken(),
                }
            });

        return fetch(request)
            .then(this.validateResponse)
            .catch(this.logError);
    }

    private tuxRefreshUrl: string = "?handler=Refresh";
    public refresh = () => {

        const request = new Request(this.tuxRefreshUrl,
            {
                method: "post",
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': this.getToken(),
                }
            });

        return fetch(request)
            .then(this.validateResponse)
            .then(this.readResponseAsText)
            .catch(this.logError);
    }

}
