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

type Model = int

type Msg =
| Increment
| Decrement

let init() : Model = 0

// UPDATE

let update (msg:Msg) (model:Model) =
    match msg with
    | Increment -> model + 1
    | Decrement -> model - 1

let chart node data = Fable.C3.c3.generate({bindto = node; data = data})

// VIEW (rendered with React)
// https://stackoverflow.com/questions/52669221/in-fable-elmish-how-to-trigger-a-command-after-the-view-has-been-rendered
let view (model:Model) dispatch =
  div [
    Id "Chart1"
    Ref (fun element ->
      printf "%O" element
      chart (Some (U2.Case2 (element :?> Browser.HTMLElement))) BarData |> ignore
    )
  ]
    [ button [ OnClick (fun _ -> dispatch Increment) ] [ str "+" ]
      div [] [ str (string model) ]
      button [ OnClick (fun _ -> dispatch Decrement) ] [ str "-" ] ]

// App
Program.mkSimple init update view
|> Program.withReact "elmish-app"
|> Program.withConsoleTrace
|> Program.run