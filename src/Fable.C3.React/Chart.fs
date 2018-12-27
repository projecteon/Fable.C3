module Fable.C3.React

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Browser
open Fable.Import.React
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.C3

type ChartProps = { data: Fable.C3.Data }

type Chart(initialProps) =
  inherit PureComponent<ChartProps, obj>(initialProps)
  let mutable chart: Fable.C3.ChartAPI option =  None
  let mutable chartElement: Browser.HTMLElement option =  None

  member this.generateChart config =
    chart <- Some (Fable.C3.c3.generate config)

  member this.destroyChart() =
    match chart with
    | Some chart -> chart.destroy() |> ignore
    | None -> ()
    chart <- None
    chartElement <- None

  member this.loadNewData data =
    match chart with
    | Some chart -> data |> chart.load |> ignore
    | None -> ()

  override this.componentDidMount() =
    match chartElement with
    | Some element -> chart <- Some ({bindto = Some (U2.Case2 element); data = this.props.data} |> Fable.C3.c3.generate)
    | None -> ()
    printf "%O" chartElement.Value

  //https://developmentarc.gitbooks.io/react-indepth/content/life_cycle/update/postrender_with_componentdidupdate.html
  override this.componentDidUpdate(prevProps, prevState) =
    if prevProps <> this.props then
      printf "componentDidUpdate %O" this.props.data
      match chart with
      | Some x ->
        x.load {columns = this.props.data.columns; ``type`` = prevProps.data.``type``; rows = None; unload = (Some true)}
      | None -> ()

  override this.componentWillUnmount() =
    this.destroyChart()

  override this.render() =
    div [
      Ref (fun element ->
        chartElement <- Some (element :?> Browser.HTMLElement)
      )
    ] [ ]

let inline chart props = ofType<Chart,_,_> props []