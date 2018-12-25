module Fable.C3
open System
open Fable.Core
open Fable.Import.JS
open Fable.Import.Browser

let [<Import("*","C3")>] c3 : IExports = jsNative

type [<AllowNullLiteral>] IExports =
    abstract generate: config: unit -> unit
