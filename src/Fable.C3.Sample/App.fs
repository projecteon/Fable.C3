module Fable.C3.Sample.App

(**
 The famous Increment/Decrement ported from Elm.
 You can find more info about Emish architecture and samples at https://elmish.github.io/
*)

open Elmish
open Elmish.React
open Fable.Core
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.Import

open Fable.C3.Sample.Data

// MODEL

type Model = {
  counter: int
  chart: Fable.C3.ChartAPI option
  data: Fable.C3.Data option
}

type Msg =
| Increment
| Decrement
| LoadData of Fable.C3.Data
| LoadChart of U2<string, Browser.HTMLElement> option

let init arg =
  let defaultModel = {
    counter = 1
    chart = None
    data = Some BarData
  }
  defaultModel, Cmd.none


let loadChart node data =
  Fable.C3.c3.generate({bindto = node; data = data})

let updateChart (chart: Fable.C3.ChartAPI) data =
  data
  |> chart.load
  |> ignore

let changeData counter =
  printf "%i" (counter%2)
  match counter%2 with
  | 0 -> Cmd.ofMsg (LoadData NewBarData)
  | _ -> Cmd.ofMsg (LoadData BarData)

// UPDATE
let update (msg:Msg) (model:Model) =
  match msg with
  | Increment -> { model with counter = model.counter + 1 }, changeData (model.counter + 1)
  | Decrement -> { model with counter = model.counter - 1 }, changeData (model.counter - 1)
  | LoadChart node ->
    match model.data with
    | Some data ->
      { model with chart = Some (loadChart node data)}, Cmd.none
    | None -> model, Cmd.none
  | LoadData data ->
    match model.chart with
    | Some chart ->
      let api: Fable.C3.ChartAPILoadArgs = {columns = data.columns; ``type`` = Some "bar"; rows = None}
      updateChart chart api |> ignore
      { model with data = Some data }, Cmd.none
    | None -> { model with data = Some data }, Cmd.none

// let chart node data = Fable.C3.c3.generate({bindto = node; data = data})

// VIEW (rendered with React)
// https://stackoverflow.com/questions/52669221/in-fable-elmish-how-to-trigger-a-command-after-the-view-has-been-rendered
let view (model:Model) dispatch =
  div [ Style [Height 400] ] [
    div [][
      button [ OnClick (fun _ -> dispatch Increment) ] [ str "+" ]
      div [] [ str (string model.counter) ]
      button [ OnClick (fun _ -> dispatch Decrement) ] [ str "-" ]
      br []
      br []
    ]
    div [
      Id "chart"
      Ref (fun element ->
        // printf "%O" element
        // dispatch (Msg.LoadChart (Some (U2.Case2 (element :?> Browser.HTMLElement))))
        match model.chart with
        | None -> dispatch (Msg.LoadChart (Some (U2.Case2 (element :?> Browser.HTMLElement)))) |> ignore
        | Some x -> printf "chart elements are equal: %b" ((element :?> Browser.HTMLElement) = x.element)
        // chart (Some (U2.Case2 (element :?> Browser.HTMLElement))) BarData |> ignore
      )
    ][]
  ]

// App
Program.mkProgram init update view
|> Program.withReact "elmish-app"
|> Program.withConsoleTrace
|> Program.run