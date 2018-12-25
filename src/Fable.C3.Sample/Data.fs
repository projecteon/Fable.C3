module Fable.C3.Sample.Data

open System
open Fable
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.JS
open C3

let primitiveFloat (value: float): U3<string, bool, float> option =
  Some (U3.Case3 value)

let optionalPrimitiveValue (value: U3<string, bool, float>) =
  Some value

let row1: PrimitiveArray =
  [|Some (!^ "data1"); primitiveFloat 30.0; primitiveFloat 200.0; primitiveFloat 100.0; primitiveFloat 400.0; primitiveFloat 150.0; primitiveFloat 250.0 |]

let row2: PrimitiveArray =
  [|Some (!^ "data2"); primitiveFloat 130.0; primitiveFloat 100.0; primitiveFloat 140.0; primitiveFloat 200.0; primitiveFloat 150.0; primitiveFloat 50.0 |]


let BarData: Data = {
  columns = Some (ResizeArray [| row1; row2 |])
  ``type`` = Some "bar"
}

// let Data: Data =
//   columns = [
//     Some ["data1", 30, 200, 100, 400, 150, 250]
//     Some ["data2", 130, 100, 140, 200, 150, 50]
//   ]
//   type = 'bar'
