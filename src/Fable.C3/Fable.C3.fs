module Fable.C3
open System
open Fable.Core
open Fable.Import.JS
open Fable.Import.Browser

type ArrayOrString =
  U2<ResizeArray<string>, string>

type [<AllowNullLiteral>] TargetIds =
  abstract ids: ArrayOrString with get, set

type PrimitiveArray =
  U3<string, bool, float> option []

type Data = {
  columns: ResizeArray<PrimitiveArray> option
  ``type``: string option
}

type ChartConfiguration = {
  bindto: U2<string, HTMLElement> option
  data: Data
}

type ChartAPILoadArgs = {
  columns: ResizeArray<PrimitiveArray> option
  rows: ResizeArray<PrimitiveArray> option
  ``type``: string option
}

type [<AllowNullLiteral>] ChartAPI =
  abstract load: args: ChartAPILoadArgs -> unit
  abstract unload: ?targetIds: TargetIds * ?``done``: (unit -> obj option) -> obj option
  abstract destroy: unit -> unit
  abstract element: HTMLElement

type [<AllowNullLiteral>] IExports =
  abstract generate: config: ChartConfiguration -> ChartAPI

let [<Import("*","C3")>] c3 : IExports = jsNative
