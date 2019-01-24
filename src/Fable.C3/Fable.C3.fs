namespace Fable.C3

open Fable.Core
open Fable.Import.Browser

[<AutoOpen>]
module Defintiion =
  [<StringEnum>]
  [<RequireQualifiedAccess>]
  type ChartType =
    | Line
    | Spline
    | Step
    | Area
    | [<CompiledName("area-spline")>] AreaSpline
    | [<CompiledName("area-step")>] AreaStep
    | Bar
    | Scatter
    | Pie
    | Donut
    | Gauge

  type ArrayOrString =
    U2<ResizeArray<string>, string>

  type [<AllowNullLiteral>] TargetIds =
    abstract ids: ArrayOrString with get, set

  type PrimitiveArray =
    U3<string, bool, float> option []

  type Data = {
    columns: ResizeArray<PrimitiveArray> option
    ``type``: ChartType option
  }

  type XAxisConfiguration = {
    ``type``: string option
    categories: ResizeArray<string> option
  }

  type Axis = {
      /// Switch x and y axis position.
      rotated: bool option
      x: XAxisConfiguration
  }

  type ChartConfiguration = {
    bindto: U2<string, HTMLElement> option
    data: Data
    axis: Axis option
  }

  type ChartAPILoadArgs = {
    columns: ResizeArray<PrimitiveArray> option
    ``type``: ChartType option
    unload: U3<string, bool, float> array
  }

  type ChartAPIResizeSize = {
    width: float option
    height: float option
  }

  type [<AllowNullLiteral>] ChartAPI =
    abstract load: args: ChartAPILoadArgs -> unit
    abstract unload: unit -> unit
    abstract destroy: unit -> unit
    abstract element: HTMLElement
    abstract transform: ``type``: string * ?targetIds: ArrayOrString -> unit
    abstract resize: ?size: ChartAPIResizeSize -> unit
    abstract flush: unit -> unit

  type [<AllowNullLiteral>] IExports =
    abstract generate: config: ChartConfiguration -> ChartAPI

  let [<Import("*","C3")>] c3 : IExports = jsNative
