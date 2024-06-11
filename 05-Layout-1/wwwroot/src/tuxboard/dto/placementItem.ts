export class PlacementItem {
    constructor(
        public placementId: string,
        public index: number,
        public layoutRowId: string,
        public columnIndex: number,
        public isStatic: boolean = false) { }
}
