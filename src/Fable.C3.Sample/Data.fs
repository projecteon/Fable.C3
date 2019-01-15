module Fable.C3.Sample.Data

open Fable.Core
open Fable.Core.JsInterop
open Fable.C3

let primitiveFloat (value: float): U3<string, bool, float> option =
  Some (U3.Case3 value)

let optionalPrimitiveValue (value: U3<string, bool, float>) =
  Some value

let row1: PrimitiveArray =
  [|Some (!^ "data1"); primitiveFloat 30.0; primitiveFloat 200.0; primitiveFloat 100.0; primitiveFloat 400.0; primitiveFloat 150.0; primitiveFloat 250.0 |]

let row2: PrimitiveArray =
  [|Some (!^ "data2"); primitiveFloat 130.0; primitiveFloat 100.0; primitiveFloat 140.0; primitiveFloat 200.0; primitiveFloat 150.0; primitiveFloat 50.0 |]

let row3: PrimitiveArray =
  [|Some (!^ "data3"); primitiveFloat 130.0; primitiveFloat 100.0; primitiveFloat 140.0; primitiveFloat 200.0; primitiveFloat 150.0; primitiveFloat 50.0 |]

let row4: PrimitiveArray =
  [|Some (!^ "data4"); primitiveFloat 130.0; primitiveFloat 100.0; primitiveFloat 140.0; primitiveFloat 200.0; primitiveFloat 150.0; primitiveFloat 50.0 |]


let BarData: Data = {
  columns = Some (ResizeArray [| row1; row2; |])
  ``type`` = Some ChartType.Bar
}

let NewBarData: Data = {
  columns = Some (ResizeArray [| row1; row2; row3; row4 |])
  ``type`` = Some ChartType.Bar
}

let NewBarData2: Data = {
  columns = Some (ResizeArray [| row3; row4 |])
  ``type`` = Some ChartType.Bar
}

// https://stackoverflow.com/questions/21389733/comparing-two-lists-for-unique-items-in-each
