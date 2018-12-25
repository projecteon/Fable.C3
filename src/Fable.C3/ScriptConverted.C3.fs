// ts2fable 0.6.1
module rec Base.C3
open System
open Fable.Core
open Fable.Import.JS
open Fable.Import.Browser


type [<AllowNullLiteral>] IExports =
    abstract generate: config: ChartConfiguration -> ChartAPI

type PrimitiveArray =
    Array<U3<string, bool, float> option>

type [<AllowNullLiteral>] FormatFunction =
    [<Emit "$0($1...)">] abstract Invoke: v: obj option * id: string * i: float * j: float -> unit

type [<AllowNullLiteral>] TargetIds =
    abstract ids: ArrayOrString with get, set

type ArrayOrString =
    U2<ResizeArray<string>, string>

[<RequireQualifiedAccess; CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module ArrayOrString =
    let ofStringArray v: ArrayOrString = v |> U2.Case1
    let isStringArray (v: ArrayOrString) = match v with U2.Case1 _ -> true | _ -> false
    let asStringArray (v: ArrayOrString) = match v with U2.Case1 o -> Some o | _ -> None
    let ofString v: ArrayOrString = v |> U2.Case2
    let isString (v: ArrayOrString) = match v with U2.Case2 _ -> true | _ -> false
    let asString (v: ArrayOrString) = match v with U2.Case2 o -> Some o | _ -> None

type [<AllowNullLiteral>] ChartConfiguration =
    /// The CSS selector or the element which the chart will be set to. D3 selection object can be specified. If other chart is set already, it will be replaced with the new one (only one chart
    /// can be set in one element).
    /// If this option is not specified, the chart will be generated but not be set. Instead, we can access the element by chart.element and set it by ourselves.
    /// Note: When chart is not binded, c3 starts observing if chart.element is binded by MutationObserver. In this case, polyfill is required in IE9 and IE10 becuase they do not support
    /// MutationObserver. On the other hand, if chart always will be binded, polyfill will not be required because MutationObserver will never be called.
    abstract bindto: U3<string, HTMLElement, D3.Selection<obj option, obj option, obj option, obj option>> option with get, set
    abstract size: obj option with get, set
    abstract padding: obj option with get, set
    abstract resize: obj option with get, set
    abstract color: obj option with get, set
    abstract interaction: obj option with get, set
    abstract transition: obj option with get, set
    /// Set a callback to execute when the chart is initialized.
    abstract oninit: unit -> unit
    /// Set a callback which is executed when the chart is rendered. Basically, this callback will be called in each time when the chart is redrawed.
    abstract onrendered: unit -> unit
    /// Set a callback to execute when mouse enters the chart.
    abstract onmouseover: unit -> unit
    /// Set a callback to execute when mouse leaves the chart.
    abstract onmouseout: unit -> unit
    /// Set a callback to execute when user resizes the screen.
    abstract onresize: unit -> unit
    /// Set a callback to execute when screen resize finished.
    abstract onresized: unit -> unit
    abstract data: Data with get, set
    abstract axis: Axis option with get, set
    abstract grid: Grid option with get, set
    /// Show rectangles inside the chart.
    /// This option accepts array including object that has axis, start, end and class. The keys start, end and class are optional.
    /// axis must be x, y or y2. start and end should be the value where regions start and end. If not specified, the edge values will be used. If timeseries x axis, date string, Date object and
    /// unixtime integer can be used. If class is set, the region element will have it as class.
    abstract regions: ResizeArray<RegionOptions> option with get, set
    abstract legend: LegendOptions option with get, set
    abstract tooltip: TooltipOptions option with get, set
    abstract subchart: SubchartOptions option with get, set
    abstract zoom: ZoomOptions option with get, set
    abstract point: PointOptions option with get, set
    abstract line: obj option with get, set
    abstract area: obj option with get, set
    abstract bar: obj option with get, set
    abstract pie: obj option with get, set
    abstract donut: obj option with get, set
    abstract gauge: obj option with get, set
    abstract spline: obj option with get, set

type [<AllowNullLiteral>] Data =
    /// Load a CSV or JSON file from a URL. Note that this will not work if loading via the "file://" protocol as most browsers with block XMLHTTPRequests.
    abstract url: string option with get, set
    /// Parse a JSON object for data.
    abstract json: obj option with get, set
    /// Load data from a multidimensional array, with the first element containing the data names, the following containing related data in that order.
    abstract rows: ResizeArray<PrimitiveArray> option with get, set
    abstract columns: ResizeArray<PrimitiveArray> option with get, set
    /// Used if loading JSON via data.url
    abstract mimeType: string option with get, set
    /// Choose which JSON object keys correspond to desired data.
    abstract keys: obj option with get, set
    /// Specify the key of x values in the data.
    /// We can show the data with non-index x values by this option. This option is required when the type of x axis is timeseries. If this option is set on category axis, the values of the data
    /// on the key will be used for category names.
    abstract x: string option with get, set
    /// Specify the keys of the x values for each data.
    /// This option can be used if we want to show the data that has different x values.
    abstract xs: obj option with get, set
    /// Set a format to parse string specifed as x.
    /// Default is %Y-%m-%d
    abstract xFormat: string option with get, set
    /// Set custom data name.
    abstract names: obj option with get, set
    /// Set custom data class.
    /// If this option is specified, the element g for the data has an additional class that has the prefix c3-target- (e.g. c3-target-additional-data1-class).
    abstract classes: obj option with get, set
    /// Set groups for the data for stacking.
    abstract groups: ResizeArray<ResizeArray<string>> option with get, set
    /// Set y axis the data related to. y and y2 can be used.
    abstract axes: obj option with get, set
    /// Set chart type at once.
    /// If this option is specified, the type will be applied to every data. This setting can be overwritten by data.types.
    /// Available Values: line, spline, step, area, area-spline, area-step, bar, scatter, pie, donut, gauge
    abstract ``type``: string option with get, set
    /// Set chart type for each data.
    /// This setting overwrites data.type setting.
    abstract types: obj option with get, set
    /// Show labels on each data points or set formatter function for data labels.
    /// The formatter function receives 4 arguments such as v, id, i, j and it must return a string that will be shown as the label. The arguments are:
    /// - v is the value of the data point where the label is shown.
    /// - id is the id of the data where the label is shown.
    /// - i is the index of the data point where the label is shown.
    /// - j is the sub index of the data point where the label is shown.
    /// Formatter function can be defined for each data by specifying as an object and D3 formatter function can be set (e.g. d3.format('$'))
    abstract labels: U3<bool, obj, obj> option with get, set
    /// Define the order of the data.
    /// This option changes the order of stacking the data and pieces of pie/donut. If null specified, it will be the order the data loaded. If function specified, it will be used to sort the data
    /// and it will recieve the data as argument.
    /// Available Values: desc, asc, function (data1, data2) { ... }, null
    abstract order: U2<string, (ResizeArray<string> -> unit)> option with get, set
    /// Define regions for each data.
    /// The values must be an array for each data and it should include an object that has start, end, style. If start is not set, the start will be the first data point. If end is not set, the
    /// end will be the last data point.
    /// Currently this option supports only line chart and dashed style. If this option specified, the line will be dashed only in the regions.
    abstract regions: obj option with get, set
    /// Set color converter function.
    /// This option should a function and the specified function receives color (e.g. '#ff0000') and d that has data parameters like id, value, index, etc. And it must return a string that
    /// represents color (e.g. '#00ff00').
    abstract color: color: string * d: obj option -> U2<string, D3.RGBColor>
    /// Set color for each data.
    abstract colors: obj option with get, set
    /// Hide each data when the chart appears.
    /// If true specified, all of data will be hidden. If multiple ids specified as an array, those will be hidden.
    abstract hide: U2<bool, ResizeArray<string>> option with get, set
    /// Set text displayed when empty data.
    abstract empty: obj option with get, set
    abstract selection: obj option with get, set
    /// Set a callback for click event on each data point.
    /// This callback will be called when each data point clicked and will receive d and element as the arguments.
    /// - d is the data clicked and element is the element clicked. In this callback, this will be the Chart object.
    abstract onclick: d: obj option * element: obj option -> unit
    /// Set a callback for mouseover event on each data point.
    /// This callback will be called when mouse cursor moves onto each data point and will receive d as the argument.
    /// - d is the data where mouse cursor moves onto. In this callback, this will be the Chart object.
    abstract onmouseover: d: obj option * ?element: obj option -> unit
    /// Set a callback for mouseout event on each data point.
    /// This callback will be called when mouse cursor moves out each data point and will receive d as the argument.
    /// - d is the data where mouse cursor moves out. In this callback, this will be the Chart object.
    abstract onmouseout: d: obj option * ?element: obj option -> unit
    abstract onselected: d: obj option * ?element: obj option -> unit
    abstract onunselected: d: obj option * ?element: obj option -> unit

type [<AllowNullLiteral>] Axis =
    /// Switch x and y axis position.
    abstract rotated: bool option with get, set
    abstract x: XAxisConfiguration option with get, set
    abstract y: YAxisConfiguration option with get, set
    abstract y2: YAxisConfiguration option with get, set

type [<AllowNullLiteral>] XAxisConfiguration =
    /// Show or hide x axis.
    abstract show: bool option with get, set
    /// Set type of x axis (timeseries, category, indexed)
    abstract ``type``: string option with get, set
    /// Set how to treat the timezone of x values.
    /// If true, treat x value as localtime. If false, convert to UTC internally.
    abstract localtime: bool option with get, set
    /// Set category names on category axis.
    /// This must be an array that includes category names in string. If category names are included in the date by data.x option, this is not required.
    abstract categories: ResizeArray<string> option with get, set
    abstract tick: XTickConfiguration option with get, set
    /// Set max value of x axis range.
    abstract max: U3<string, float, DateTime> option with get, set
    /// Set min value of x axis range.
    abstract min: U3<string, float, DateTime> option with get, set
    /// Set padding for x axis.
    /// If this option is set, the range of x axis will increase/decrease according to the values. If no padding is needed in the ragen of x axis, 0 should be set. On category axis, this option
    /// will be ignored.
    abstract padding: obj option with get, set
    /// Set height of x axis.
    /// The height of x axis can be set manually by this option. If you need more space for x axis, please use this option for that. The unit is pixel.
    abstract height: float option with get, set
    /// Set default extent for subchart and zoom. This can be an array or function that returns an array.
    abstract extent: U2<ResizeArray<float>, (unit -> ResizeArray<float>)> option with get, set
    /// Set label on x axis.
    /// You can set x axis label and change its position by this option. string and object can be passed and we can change the poisiton by passing object that has position key. Available position
    /// differs according to the axis direction (vertical or horizontal). If string set, the position will be the default.
    /// Valid horizontal positions: inner-right (Default), inner-center, inner-left, outer-right, outer-center, outer-left
    /// Valid vertical positions: inner-top, inner-middle, inner-bottom, outer-top, outer-middle, outer-bottom
    abstract label: U2<string, obj> option with get, set

type [<AllowNullLiteral>] YAxisConfiguration =
    /// Show or hide y axis.
    abstract show: bool option with get, set
    /// Show y axis inside of the chart.
    abstract inner: bool option with get, set
    /// Set max value of y axis.
    abstract max: float option with get, set
    /// Set min value of y axis.
    abstract min: float option with get, set
    /// Change the direction of y axis.
    /// If true set, the direction will be from the top to the bottom.
    abstract inverted: bool option with get, set
    /// Set center value of y axis.
    abstract center: float option with get, set
    /// Set label on y axis. This option works in the same way as axis.x.label.
    /// Valid horizontal positions: inner-right (Default), inner-center, inner-left, outer-right, outer-center, outer-left
    /// Valid vertical positions: inner-top, inner-middle, inner-bottom, outer-top, outer-middle, outer-bottom
    abstract label: U2<string, obj> option with get, set
    abstract tick: YTickConfiguration option with get, set
    /// Set padding for y axis.
    /// You can set padding for y axis to create more space on the edge of the axis. This option accepts object and it can include top and bottom. top, bottom will be treated as pixels.
    abstract padding: obj option with get, set
    /// Set default range of y axis. This option set the default value for y axis when there is no data on init.
    abstract ``default``: ResizeArray<float> option with get, set

type [<AllowNullLiteral>] XTickConfiguration =
    /// Centerise ticks on category axis
    abstract centered: bool option with get, set
    /// A function to format tick value. Format string is also available for timeseries data.
    abstract format: U2<string, (U2<float, DateTime> -> U2<string, float>)> option with get, set
    /// Setting for culling ticks.
    /// If true is set, the ticks will be culled, then only limitted tick text will be shown. This option does not hide the tick lines. If false is set, all of ticks will be shown.
    abstract culling: U2<bool, CullingConfiguration> option with get, set
    /// The number of x axis ticks to show.
    /// This option hides tick lines together with tick text. If this option is used on timeseries axis, the ticks position will be determined precisely and not nicely positioned (e.g. it will
    /// have rough second value).
    abstract count: float option with get, set
    /// Fit x axis ticks.
    /// If true set, the ticks will be positioned nicely. If false set, the ticks will be positioned according to x value of the data points.
    abstract fit: bool option with get, set
    /// Set the x values of ticks manually.
    /// If this option is provided, the position of the ticks will be determined based on those values. This option works with timeseries data and the x values will be parsed accoding to the type
    /// of the value and data.xFormat option.
    abstract values: U2<ResizeArray<float>, ResizeArray<string>> option with get, set
    /// Rotate x axis tick text. If you set negative value, it will rotate to opposite direction.
    abstract rotate: float option with get, set
    /// Show x axis outer tick.
    abstract outer: bool option with get, set
    /// Set width of x axis tick.
    abstract width: float option with get, set
    abstract multiline: bool option with get, set

type [<AllowNullLiteral>] YTickConfiguration =
    /// Set formatter for y axis tick text.
    /// This option accepts d3.format object as well as a function you define.
    abstract format: x: float -> string
    /// Show or hide outer tick.
    abstract outer: bool option with get, set
    /// Set the y values of ticks manually.
    abstract values: ResizeArray<float> option with get, set
    /// The number of y axis ticks to show.
    /// The position of the ticks will be calculated precisely, so the values on the ticks will not be rounded nicely. In the case, axis.y.tick.format or axis.y.tick.values will be helpful.
    abstract count: float option with get, set

type [<AllowNullLiteral>] CullingConfiguration =
    /// The number of tick texts will be adjusted to less than this value.
    abstract max: float with get, set

type [<AllowNullLiteral>] Grid =
    abstract x: obj option with get, set
    abstract y: obj option with get, set

type [<AllowNullLiteral>] LineOptions =
    abstract value: U3<string, float, DateTime> with get, set
    abstract text: string option with get, set
    abstract axis: string option with get, set
    abstract position: string option with get, set
    abstract ``class``: string option with get, set

type [<AllowNullLiteral>] RegionOptions =
    abstract axis: string option with get, set
    abstract start: U3<string, float, DateTime> option with get, set
    abstract ``end``: U3<string, float, DateTime> option with get, set
    abstract ``class``: string option with get, set

type [<AllowNullLiteral>] LegendOptions =
    /// Show or hide legend.
    abstract show: bool option with get, set
    /// Hide legend
    /// If true given, all legend will be hidden. If string or array given, only the legend that has the id will be hidden.
    abstract hide: U2<bool, ArrayOrString> option with get, set
    /// Change the position of legend.
    /// Currently bottom, right and inset are supported.
    abstract position: string option with get, set
    /// Change inset legend attributes.
    /// This option accepts object that has the keys anchor, x, y and step.
    /// anchor decides the position of the legend. These anchors are available: top-left, top-right, bottom-left, bottom-right
    /// x and y set the position of the legend based on the anchor.
    /// step defines the max step the lagend has (e.g. If 2 set and legend has 3 legend item, the legend 2 columns).
    abstract inset: obj option with get, set
    /// Padding between legend elements.
    abstract padding: float option with get, set
    abstract item: obj option with get, set

type [<AllowNullLiteral>] TooltipOptions =
    /// Show or hide tooltip.
    abstract show: bool option with get, set
    /// Set if tooltip is grouped or not for the data points.
    abstract grouped: bool option with get, set
    abstract format: obj option with get, set
    /// Set custom position for the tooltip. This option can be used to modify the tooltip position by returning object that has top and left.
    abstract position: data: obj option * width: float * height: float * element: obj option -> obj
    /// Set custom HTML for the tooltip.
    /// Specified function receives data, defaultTitleFormat, defaultValueFormat and color of the data point to show. If tooltip.grouped is true, data includes multiple data points.
    abstract contents: data: obj option * defaultTitleFormat: string * defaultValueFormat: string * color: obj option -> string
    /// Set tooltip values order
    /// Available Values: desc, asc, any[], function (data1, data2) { ... }, null
    abstract order: U3<string, ResizeArray<obj option>, (obj option -> obj option -> float)> option with get, set

type [<AllowNullLiteral>] SubchartOptions =
    /// Show sub chart on the bottom of the chart.
    abstract show: bool option with get, set
    abstract size: obj option with get, set
    /// Set callback for brush event.
    /// Specified function receives the current zoomed x domain.
    abstract onbrush: domain: obj option -> unit

type [<AllowNullLiteral>] ZoomOptions =
    /// Enable zooming.
    abstract enabled: bool option with get, set
    /// Set interaction type for zooming
    abstract ``type``: U2<string, string> option with get, set
    /// Enable to rescale after zooming. If true set, y domain will be updated according to the zoomed region.
    abstract rescale: bool option with get, set
    /// Change zoom extent.
    abstract extent: float * float option with get, set
    /// Set callback that is called when the chart is zooming. Specified function receives the zoomed domain.
    abstract onzoom: domain: obj option -> unit
    /// Set callback that is called when zooming starts. Specified function receives the zoom event.
    abstract onzoomstart: ``event``: Event -> unit
    /// Set callback that is called when zooming ends. Specified function receives the zoomed domain.
    abstract onzoomend: domain: obj option -> unit

type [<AllowNullLiteral>] PointOptions =
    /// Whether to show each point in line.
    abstract show: bool option with get, set
    /// The radius size of each point.
    abstract r: U2<float, (obj option -> float)> option with get, set
    abstract focus: obj option with get, set
    abstract select: obj option with get, set

type [<AllowNullLiteral>] ChartAPI =
    /// This API highlights specified targets and fade out the others.
    /// You can specify multiple targets by giving an array that includes id as String. If no argument is given, all of targets will be highlighted.
    abstract focus: ?targetIds: ArrayOrString -> unit
    /// This API fades out specified targets and reverts the others.
    /// You can specify multiple targets by giving an array that includes id as String. If no argument is given, all of targets will be faded out.
    abstract defocus: ?targetIds: ArrayOrString -> unit
    /// This API reverts specified targets.
    /// You can specify multiple targets by giving an array that includes id as String. If no argument is given, all of targets will be reverted.
    abstract revert: ?targetIds: ArrayOrString -> unit
    /// This API shows specified targets.
    /// You can specify multiple targets by giving an array that includes id as String. If no argument is given, all of targets will be shown.
    /// If withLegend is set true, legend will be shown together with the specified data.
    abstract show: ?targetIds: ArrayOrString * ?options: ChartAPIShowOptions -> unit
    /// This API hides specified targets.
    /// You can specify multiple targets by giving an array that includes id as String. If no argument is given, all of targets will be hidden.
    /// If withLegend is set true, legend will be hidden together with the specified data.
    abstract hide: ?targetIds: ArrayOrString * ?options: ChartAPIHideOptions -> unit
    /// This API toggles (shows or hides) specified targets.
    /// You can specify multiple targets by giving an array that includes id as String. If no argument is given, all of targets will be toggles.
    /// If withLegend is set true, legend will be toggled together with the specified data.
    abstract toggle: ?targetIds: ArrayOrString * ?options: ChartAPIToggleOptions -> unit
    /// Load data to the chart.
    /// If url, json, rows and columns given, the data will be loaded. If data that has the same target id is given, the chart will be updated. Otherwise, new target will be added.
    /// If classes given, the classes specifed by data.classes will be updated. classes must be Object that has target id as keys.
    /// If categories given, the categories specifed by axis.x.categories or data.x will be updated. categories must be Array.
    /// If axes given, the axes specifed by data.axes will be updated. axes must be Object that has target id as keys.
    /// If colors given, the colors specifed by data.colors will be updated. colors must be Object that has target id as keys.
    /// If type or types given, the type of targets will be updated. type must be String and types must be Object.
    /// If unload given, data will be unloaded before loading new data. If true given, all of data will be unloaded. If target ids given as String or Array, specified targets will be unloaded.
    /// If done given, the specified function will be called after data loded.
    /// NOTE: unload should be used if some data needs to be unloaded simultaneously. If you call unload API soon after/before load instead of unload param, chart will not be rendered properly
    /// because of cancel of animation.
    /// NOTE: done will be called after data loaded, but it's not after rendering. It's because rendering will finish after some transition and there is some time lag between loading and rendering.
    abstract load: args: ChartAPILoadArgs -> unit
    /// Unload data to the chart.
    /// You can specify multiple targets by giving an array that includes id as String. If no argument is given, all of targets will be toggles.
    /// If ids given, the data that has specified target id will be unloaded. ids should be String or Array. If ids is not specified, all data will be unloaded.
    /// If done given, the specified function will be called after data loded.
    /// NOTE: If you call load API soon after/before unload, unload param of load should be used. Otherwise chart will not be rendered properly because of cancel of animation.
    /// NOTE: done will be called after data loaded, but it's not after rendering. It's because rendering will finish after some transition and there is some time lag between loading and rendering.
    abstract unload: ?targetIds: TargetIds * ?``done``: (unit -> obj option) -> obj option
    /// Flow data to the chart. By this API, you can append new data points to the chart.
    /// If json, rows and columns given, the data will be loaded. If data that has the same target id is given, the chart will be appended. Otherwise, new target will be added. One of these is
    /// required when calling. If json specified, keys is required as well as data.json
    /// If to is given, the lower x edge will move to that point. If not given, the lower x edge will move by the number of given data points.
    /// If length is given, the lower x edge will move by the number of this argument.
    /// If duration is given, the duration of the transition will be specified value. If not given, transition.duration will be used as default.
    /// If done is given, the specified function will be called when flow ends.
    abstract flow: args: ChartAPIFlowArgs -> unit
    /// <summary>Change data point state to selected. By this API, you can select data points. To use this API, data.selection.enabled needs to be set true.</summary>
    /// <param name="ids">Specify target ids to be selected. If this argument is not given, all targets will be the candidate.</param>
    /// <param name="indices">Specify indices to be selected. If this argument is not given, all data points will be the candidate.</param>
    /// <param name="resetOthers">If this argument is set true, the data points that are not specified by ids, indices will be unselected.</param>
    abstract select: ?ids: ResizeArray<string> * ?indices: ResizeArray<float> * ?resetOthers: bool -> unit
    /// <summary>Change data point state to unselected. By this API, you can unselect data points. To use this API, data.selection.enabled needs to be set true.</summary>
    /// <param name="ids">Specify target ids to be unselected. If this argument is not given, all targets will be the candidate.</param>
    /// <param name="indices">Specify indices to be unselected. If this argument is not given, all data points will be the candidate.</param>
    abstract unselect: ?ids: ResizeArray<string> * ?indices: ResizeArray<float> -> unit
    /// <summary>Get selected data points. By this API, you can get selected data points information. To use this API, data.selection.enabled needs to be set true.</summary>
    /// <param name="targetId">You can filter the result by giving target id that you want to get. If not given, all of data points will be returned.</param>
    abstract selected: ?targetId: string -> Data
    /// <summary>Change the type of the chart.</summary>
    /// <param name="type">Specify the type to be transformed. The types listed in data.type can be used.</param>
    /// <param name="targetIds">Specify targets to be transformed. If not given, all targets will be the candidate.</param>
    abstract transform: ``type``: string * ?targetIds: ArrayOrString -> unit
    /// <summary>Update groups for the targets.</summary>
    /// <param name="groups">This argument needs to be an Array that includes one or more Array that includes target ids to be grouped.</param>
    abstract groups: groups: ResizeArray<ResizeArray<string>> -> unit
    abstract xgrids: GridOperations with get, set
    abstract ygrids: GridOperations with get, set
    abstract regions: obj with get, set
    abstract data: obj with get, set
    /// <summary>Gets and sets the value a category</summary>
    /// <param name="index">Index of the category to get or set</param>
    /// <param name="category">: Value of the category to update</param>
    abstract category: index: float * ?category: string -> string
    /// <summary>Get and set the categories</summary>
    /// <param name="categories">: Value of the categories to update</param>
    abstract categories: ?categories: ResizeArray<string> -> ResizeArray<string>
    /// Get the color for the specified targetId
    abstract color: targetId: string -> string
    /// <summary>Get and set x values for the chart.</summary>
    /// <param name="x">If x is given, x values of every target will be updated. If no argument is given, current x values will be returned as an Object whose keys are the target ids.</param>
    abstract x: ?x: PrimitiveArray -> PrimitiveArray
    /// Get and set x values for the chart.
    abstract xs: ?xs: ChartAPIXs -> obj
    abstract axis: obj with get, set
    abstract legend: obj with get, set
    abstract zoom: obj with get, set
    /// Unzoom to the original domain.
    abstract unzoom: unit -> unit
    /// <summary>Resize the chart. If no size is specified it will resize to fit.</summary>
    /// <param name="size">This argument should include width and height in pixels.</param>
    abstract resize: ?size: ChartAPIResizeSize -> unit
    /// Force to redraw.
    abstract flush: unit -> unit
    /// Reset the chart object and remove element and events completely.
    abstract destroy: unit -> unit

type [<AllowNullLiteral>] ChartAPIShowOptions =
    abstract withLegend: bool with get, set

type [<AllowNullLiteral>] ChartAPIHideOptions =
    abstract withLegend: bool with get, set

type [<AllowNullLiteral>] ChartAPIToggleOptions =
    abstract withLegend: bool with get, set

type [<AllowNullLiteral>] ChartAPILoadArgs =
    abstract url: string option with get, set
    abstract json: obj option with get, set
    abstract keys: obj option with get, set
    abstract rows: ResizeArray<PrimitiveArray> option with get, set
    abstract columns: ResizeArray<PrimitiveArray> option with get, set
    abstract xs: obj option with get, set
    abstract names: obj option with get, set
    abstract classes: obj option with get, set
    abstract categories: ResizeArray<string> option with get, set
    abstract axes: obj option with get, set
    abstract colors: obj option with get, set
    abstract ``type``: string option with get, set
    abstract types: obj option with get, set
    abstract unload: U2<bool, ArrayOrString> option with get, set
    abstract ``done``: unit -> obj option

type [<AllowNullLiteral>] ChartAPIFlowArgs =
    abstract json: obj option with get, set
    abstract keys: obj option with get, set
    abstract rows: ResizeArray<PrimitiveArray> option with get, set
    abstract columns: ResizeArray<PrimitiveArray> option with get, set
    abstract ``to``: obj option with get, set
    abstract length: float option with get, set
    abstract duration: float option with get, set
    abstract ``done``: unit -> obj option

type [<AllowNullLiteral>] ChartAPIXs =
    [<Emit "$0[$1]{{=$2}}">] abstract Item: key: string -> PrimitiveArray with get, set

type [<AllowNullLiteral>] ChartAPIResizeSize =
    abstract width: float option with get, set
    abstract height: float option with get, set

type [<AllowNullLiteral>] GridOperations =
    /// <summary>Update the x/y grid lines.</summary>
    /// <param name="grids">X/Y grid lines will be replaced with this argument. The format of this argument is the same as grid.x.lines or grid.y.lines.</param>
    [<Emit "$0($1...)">] abstract Invoke: grids: ResizeArray<obj option> -> unit
    /// <summary>Add x/y grid lines. This API adds new x/y grid lines instead of replacing like xgrids.</summary>
    /// <param name="grids">New x/y grid lines will be added. The format of this argument is the same as grid.x.lines or grid.y.lines and it's possible to give an Object if only one line will be added.</param>
    abstract add: grids: U2<ResizeArray<obj option>, obj> -> unit
    /// <summary>Remove x/y grid lines. This API removes x/y grid lines.</summary>
    /// <param name="args">This argument should include value or class. If value is given, the x/y grid lines that have specified x/y value will be removed. If class is given, the x/y grid lines that
    /// have specified class will be removed. If args is not given, all of x/y grid lines will be removed.</param>
    abstract remove: ?args: GridOperationsRemoveArgs -> unit

type [<AllowNullLiteral>] GridOperationsRemoveArgs =
    abstract ``class``: string option with get, set
    abstract value: U2<float, string> option with get, set
