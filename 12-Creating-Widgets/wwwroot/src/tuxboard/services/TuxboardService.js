import { BaseService } from "./BaseService";
export class TuxboardService extends BaseService {
    constructor(debugParam = false) {
        super(debugParam);
        this.tuxWidgetPlacementUrl = "?handler=SaveWidgetPosition";
        this.tuxRefreshUrl = "?handler=Refresh";
        this.tuxSimpleLayoutDialogUrl = "?handler=SimpleLayoutDialog";
        this.tuxSaveSimpleLayoutUrl = "?handler=SaveSimpleLayout";
        this.tuxAdvancedLayoutDialogUrl = "?handler=AdvancedLayoutDialog";
        this.tuxGetLayoutTypeUrl = "?handler=GetLayoutType";
        this.tuxSaveAdvancedLayoutUrl = "?handler=SaveAdvancedLayout";
        this.tuxCanDeleteLayoutRowUrl = "?handler=CanDeleteLayoutRow";
        this.tuxAddWidgetDialogUrl = "?handler=AddWidgetsDialog";
        this.tuxAddWidgetUrl = "?handler=AddWidget";
        this.tuxRemoveWidgetUrl = "?handler=RemoveWidget";
        this.tuxSetWidgetStateUrl = "?handler=SetWidgetState";
        this.saveWidgetPlacement = (ev, dragInfo) => {
            const postData = {
                PlacementId: dragInfo.placementId,
                PreviousLayoutRowId: dragInfo.previousLayoutRowId,
                PreviousColumn: dragInfo.previousColumnIndex,
                CurrentLayoutRowId: dragInfo.currentLayoutRowId,
                CurrentColumn: dragInfo.currentColumnIndex,
                PlacementList: dragInfo.placementList,
            };
            const request = new Request(this.tuxWidgetPlacementUrl, {
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
        };
        this.refresh = () => {
            const request = new Request(this.tuxRefreshUrl, {
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
        };
        this.getSimpleLayoutDialog = () => {
            const request = new Request(this.tuxSimpleLayoutDialogUrl, {
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
        };
        this.saveSimpleLayout = (layoutRowId, newLayoutTypeId) => {
            const postData = {
                LayoutRowId: layoutRowId,
                LayoutTypeId: newLayoutTypeId
            };
            const request = new Request(this.tuxSaveSimpleLayoutUrl, {
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
        };
        this.getAdvancedLayoutDialog = () => {
            const request = new Request(this.tuxAdvancedLayoutDialogUrl, {
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
        };
        this.getLayoutType = (typeId) => {
            var postData = {
                id: typeId
            };
            const request = new Request(this.tuxGetLayoutTypeUrl, {
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
        };
        this.saveAdvancedLayout = (model) => {
            var postData = {
                TabId: model.TabId,
                LayoutList: model.LayoutList
            };
            const request = new Request(this.tuxSaveAdvancedLayoutUrl, {
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
        };
        this.canDeleteLayoutRow = (tabId, layoutRowId) => {
            var postData = {
                tabId: tabId,
                layoutRowId: layoutRowId
            };
            const request = new Request(this.tuxCanDeleteLayoutRowUrl, {
                method: "post",
                body: JSON.stringify(postData),
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': this.getToken(),
                }
            });
            return fetch(request);
        };
        this.getAddWidgetDialog = () => {
            const request = new Request(this.tuxAddWidgetDialogUrl, {
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
        };
        this.addWidget = (widgetId) => {
            var postData = {
                WidgetId: widgetId
            };
            const request = new Request(this.tuxAddWidgetUrl, {
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
        };
        this.removeWidget = (widgetId) => {
            var postData = {
                WidgetId: widgetId
            };
            const request = new Request(this.tuxRemoveWidgetUrl, {
                method: "post",
                body: JSON.stringify(postData),
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': this.getToken(),
                }
            });
            return fetch(request);
        };
        this.setWidgetState = (widgetPlacementId, collapsed) => {
            var postData = {
                WidgetPlacementId: widgetPlacementId,
                Collapsed: collapsed
            };
            const request = new Request(this.tuxSetWidgetStateUrl, {
                method: "post",
                body: JSON.stringify(postData),
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': this.getToken(),
                }
            });
            return fetch(request);
        };
    }
}
