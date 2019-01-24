module Fable.C3.Sample.App

open Elmish
open Elmish.React
open Fable.Helpers.React
open Fable.Helpers.React.Props

open Fable.C3.Sample.Data
open Fable.C3
// MODEL

type Model = {
  counter: int
  data: Data
}

type Msg =
| Increment
| Decrement
| LoadData of Data

let init arg =
  let defaultModel = {
    counter = 1
    data = BarData
  }
  defaultModel, Cmd.none

let morphType counter (data: Data) : Data =
  printf "morphType %i" (counter%3)
  match counter%3 with
  | 0 -> { data with ``type`` = Some ChartType.Pie }
  | _ -> { data with ``type`` = Some ChartType.Bar }


let changeData counter =
  printf "changeData %i" (counter%2)
  match counter%2 with
  | 0 -> Cmd.ofMsg (LoadData (morphType counter NewBarData))
  | _ -> Cmd.ofMsg (LoadData (morphType counter BarData))

// UPDATE
let update (msg:Msg) (model:Model) =
  match msg with
  | Increment -> { model with counter = model.counter + 1 }, changeData (model.counter + 1)
  | Decrement -> { model with counter = model.counter - 1 }, changeData (model.counter - 1)
  | LoadData data -> { model with data = data }, Cmd.none

// VIEW (rendered with React)
// https://stackoverflow.com/questions/52669221/in-fable-elmish-how-to-trigger-a-command-after-the-view-has-been-rendered
let view (model:Model) dispatch =
  div [ Style [ Height 400 ] ] [
    div [][
      button [ OnClick (fun _ -> dispatch Increment) ] [ str "+" ]
      div [] [ str (string model.counter) ]
      button [ OnClick (fun _ -> dispatch Decrement) ] [ str "-" ]
      br []
      br []
    ]
    div [][
      React.chart { data = model.data; axis = None; height = 600 }
    ]
  ]

// App
Program.mkProgram init update view
|> Program.withReact "elmish-app"
|> Program.withConsoleTrace
|> Program.run