import { WidgetPlacement } from "./widgetPlacement";
import { defaultWidgetSelector, dataIdAttribute } from "../common";
export class WidgetCollection {
    constructor(parent, columnIndex, layoutRowId) {
        this.parent = parent;
        this.columnIndex = columnIndex;
        this.layoutRowId = layoutRowId;
        this.selector = defaultWidgetSelector;
        this.collection = Array.from(this.parent.querySelectorAll(this.selector));
        this.getWidgetSelector = () => this.selector;
        this.getWidgets = () => this.collection.map((element, index) => this.createWidget(element, index));
        this.getWidgetProperties = (widget) => widget.getProperties();
        this.createWidget = (element, index) => {
            const id = element.getAttribute(dataIdAttribute);
            return new WidgetPlacement(this.parent, id, index, this.columnIndex);
            //widget.setIndex(index);
            //widget.setColumnIndex(this.columnIndex);
            // return widget;
        };
    }
}
