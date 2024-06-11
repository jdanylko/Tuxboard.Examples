import { DragWidgetInfo } from "../dto/dragWidgetInfo";
import { BaseService } from "./BaseService";

export class TuxboardService extends BaseService {

    private tuxWidgetPlacementUrl: string    = "?handler=SaveWidgetPosition";
    private tuxRefreshUrl: string            = "?handler=Refresh";
    private tuxSimpleLayoutDialogUrl: string = "?handler=SimpleLayoutDialog";
    private tuxSaveSimpleLayoutUrl: string   = "?handler=SaveSimpleLayout";

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

    public getSimpleLayoutDialog = () => {

        const request = new Request(this.tuxSimpleLayoutDialogUrl,
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

    public saveSimpleLayout = (layoutRowId:string, newLayoutTypeId) => {

        const postData = {
            LayoutRowId: layoutRowId,
            LayoutTypeId: newLayoutTypeId
        };
        const request = new Request(this.tuxSaveSimpleLayoutUrl,
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
            .then(this.readResponseAsText)
            .catch(this.logError);
    }

}
