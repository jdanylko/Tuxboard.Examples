import { WidgetPlacement } from "./widgetPlacement";
import { defaultWidgetSelector, dataId } from "../common";

export class WidgetCollection {

    private selector = defaultWidgetSelector;

    private collection = Array.from(this.parent.querySelectorAll<HTMLDivElement>(this.selector));

    constructor(
        private readonly parent: HTMLDivElement,
        private readonly columnIndex: number,
        private readonly layoutRowId: string
    ) { }

    getWidgetSelector = () => this.selector;

    getWidgets = () => this.collection.map((element, index) => this.createWidget(element, index));

    getWidgetProperties = (widget: WidgetPlacement) => widget.getProperties();

    createWidget = (element: HTMLElement, index: number) => {
        const id = element.getAttribute(dataId);
        return new WidgetPlacement(this.parent, id, index, this.columnIndex);
        //widget.setIndex(index);
        //widget.setColumnIndex(this.columnIndex);

        // return widget;
    }
}