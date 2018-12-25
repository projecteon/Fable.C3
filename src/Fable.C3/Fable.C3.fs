module Fable.C3
open System
open Fable.Core
open Fable.Import.JS
open Fable.Import.Browser

let [<Import("*","C3")>] c3 : IExports = jsNative

type [<AllowNullLiteral>] IExports =
  abstract generate: config: unit -> unit

type ArrayOrString =
  U2<ResizeArray<string>, string>

type [<AllowNullLiteral>] TargetIds =
  abstract ids: ArrayOrString with get, set

type PrimitiveArray =
  Array<U3<string, bool, float> option>

type [<AllowNullLiteral>] Data =
  abstract columns: ResizeArray<PrimitiveArray> option with get, set
  abstract ``type``: string option with get, set

type [<AllowNullLiteral>] ChartConfiguration =
  abstract bindto: U2<string, HTMLElement> option with get, set
  abstract data: Data with get, set

type [<AllowNullLiteral>] ChartAPILoadArgs =
  abstract columns: ResizeArray<PrimitiveArray> option with get, set


type [<AllowNullLiteral>] ChartAPI =
  abstract load: args: ChartAPILoadArgs -> unit
  abstract unload: ?targetIds: TargetIds * ?``done``: (unit -> obj option) -> obj option
  abstract destroy: unit -> unit
