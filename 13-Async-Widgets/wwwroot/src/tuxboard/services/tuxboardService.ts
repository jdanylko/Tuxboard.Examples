import { LayoutModel } from "../dialog/advancedLayout/LayoutModel";
import { DragWidgetInfo } from "../dto/dragWidgetInfo";
import { BaseService } from "./BaseService";

export class TuxboardService extends BaseService {

    private tuxWidgetPlacementUrl: string = "?handler=SaveWidgetPosition";
    private tuxRefreshUrl: string = "?handler=Refresh";

    private tuxSimpleLayoutDialogUrl: string = "?handler=SimpleLayoutDialog";
    private tuxSaveSimpleLayoutUrl: string = "?handler=SaveSimpleLayout";

    private tuxAdvancedLayoutDialogUrl: string = "?handler=AdvancedLayoutDialog";
    private tuxGetLayoutTypeUrl: string = "?handler=GetLayoutType";
    private tuxSaveAdvancedLayoutUrl: string = "?handler=SaveAdvancedLayout";
    private tuxCanDeleteLayoutRowUrl: string = "?handler=CanDeleteLayoutRow";

    private tuxAddWidgetDialogUrl: string = "?handler=AddWidgetsDialog";
    private tuxAddWidgetUrl: string = "?handler=AddWidget";
    private tuxRemoveWidgetUrl: string = "?handler=RemoveWidget";

    private tuxSetWidgetStateUrl: string = "?handler=SetWidgetState";

    private tuxGetWidgetUrl: string = "?handler=GetWidget";

    constructor(debugParam: boolean = false) {
        super(debugParam);
    }

    public saveWidgetPlacement = async (ev: Event, dragInfo: DragWidgetInfo) => {

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

        try {
            const response = await fetch(request);
            return this.validateResponse(response);
        } catch (err) {
            this.logError(err);
        }
    }

    /* Added */
    public refresh = async () => {

        const request = new Request(this.tuxRefreshUrl,
            {
                method: "post",
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': this.getToken(),
                }
            });

        try {
            const response = await fetch(request);
            this.validateResponse(response);
            return response.text();
        } catch (err) {
            this.logError(err);
        }
    }

    public getSimpleLayoutDialog = async () => {

        const request = new Request(this.tuxSimpleLayoutDialogUrl,
            {
                method: "post",
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': this.getToken(),
                }
            });

        try {
            const response = await fetch(request);
            this.validateResponse(response);
            return response.text();
        } catch (err) {
            this.logError(err);
        }
    }

    public saveSimpleLayout = async (layoutRowId: string, newLayoutTypeId) => {

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

        try {
            const response = await fetch(request);
            this.validateResponse(response);
            return response.text();
        } catch (err) {
            this.logError(err);
        }
    }

    public getAdvancedLayoutDialog = async () => {

        const request = new Request(this.tuxAdvancedLayoutDialogUrl,
            {
                method: "post",
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': this.getToken(),
                }
            });

        try {
            const response = await fetch(request);
            this.validateResponse(response);
            return response.text();
        } catch (err) {
            this.logError(err);
        }
    }

    public getLayoutType = async (typeId: number) => {

        var postData = {
            id: typeId
        }

        const request = new Request(this.tuxGetLayoutTypeUrl,
            {
                method: "post",
                body: JSON.stringify(postData),
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': this.getToken(),
                }
            });

        try {
            const response = await fetch(request);
            this.validateResponse(response);
            return response.text();
        } catch (err) {
            this.logError(err);
        }
    }

    public saveAdvancedLayout = async (model: LayoutModel) => {

        var postData =
        {
            TabId: model.TabId,
            LayoutList: model.LayoutList
        }

        const request = new Request(this.tuxSaveAdvancedLayoutUrl,
            {
                method: "post",
                body: JSON.stringify(postData),
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': this.getToken(),
                }
            });

        try {
            const response = await fetch(request);
            this.validateResponse(response);
            return response.text();
        } catch (err) {
            this.logError(err);
        }
    }

    public canDeleteLayoutRow = async (tabId: string, layoutRowId: string) => {

        var postData =
        {
            tabId: tabId,
            layoutRowId: layoutRowId
        }

        const request = new Request(this.tuxCanDeleteLayoutRowUrl,
            {
                method: "post",
                body: JSON.stringify(postData),
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': this.getToken(),
                }
            });

        return fetch(request);
    }

    public getAddWidgetDialog = async () => {

        const request = new Request(this.tuxAddWidgetDialogUrl,
            {
                method: "post",
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': this.getToken(),
                }
            });

        try {
            const response = await fetch(request);
            this.validateResponse(response);
            return response.text();
        } catch (err) {
            this.logError(err);
        }
    }

    public addWidget = async (widgetId:string) => {

        var postData = {
            WidgetId: widgetId
        };

        const request = new Request(this.tuxAddWidgetUrl,
            {
                method: "post",
                body: JSON.stringify(postData),
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': this.getToken(),
                }
            });

        try {
            const response = await fetch(request);
            this.validateResponse(response);
            return response.text();
        } catch (err) {
            this.logError(err);
        }
    }

    public removeWidget = (widgetId:string) => {

        var postData = {
            WidgetId: widgetId
        };

        const request = new Request(this.tuxRemoveWidgetUrl,
            {
                method: "post",
                body: JSON.stringify(postData),
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': this.getToken(),
                }
            });

        return fetch(request)
    }

    public setWidgetState = (widgetPlacementId: string, collapsed: boolean) => {

        var postData = {
            WidgetPlacementId: widgetPlacementId,
            Collapsed: collapsed
        };

        const request = new Request(this.tuxSetWidgetStateUrl,
            {
                method: "post",
                body: JSON.stringify(postData),
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': this.getToken(),
                }
            });

        return fetch(request)
    }

    public getWidget = async (widgetPlacementId: string, collapsed: boolean) => {

        var postData = {
            WidgetPlacementId: widgetPlacementId,
            Collapsed: collapsed
        };

        const request = new Request(this.tuxGetWidgetUrl,
            {
                method: "post",
                body: JSON.stringify(postData),
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': this.getToken(),
                }
            });

        const response = await fetch(request);
        this.validateResponse(response);
        return response.text();
    }
}
