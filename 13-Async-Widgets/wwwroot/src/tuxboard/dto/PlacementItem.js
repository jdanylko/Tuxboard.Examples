export class PlacementItem {
    constructor(placementId, index, layoutRowId, columnIndex, isStatic = false) {
        this.placementId = placementId;
        this.index = index;
        this.layoutRowId = layoutRowId;
        this.columnIndex = columnIndex;
        this.isStatic = isStatic;
    }
}
