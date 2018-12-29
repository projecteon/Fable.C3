module Fable.C3.React

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Browser
open Fable.Import.React
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.C3

type ChartProps = {
  data: Data
}

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

  member this.mountedElement (element:Element) =
    chartElement <- Some (element :?> Browser.HTMLElement)

  member this.getColumnNames (columnData: ResizeArray<PrimitiveArray> option) =
    match columnData with
    | None -> [||]
    | Some arr ->
      (arr.ToArray())
      |> Array.map (fun x -> x.[0])
      |> Array.choose id

  member this.getColumnDiff prevProps props =
    let L = set (this.getColumnNames prevProps.data.columns)
    let R = set (this.getColumnNames props.data.columns)
    L - R |> Set.toArray

  override this.componentDidMount() =
    match chartElement with
    | Some element -> chart <- Some ({bindto = Some (U2.Case2 element); data = this.props.data} |> Fable.C3.c3.generate)
    | None -> ()

  //https://developmentarc.gitbooks.io/react-indepth/content/life_cycle/update/postrender_with_componentdidupdate.html
  override this.componentDidUpdate(prevProps, prevState) =
    if prevProps <> this.props then
      match chart with
      | Some x ->
        x.load {
          columns = this.props.data.columns
          ``type`` = this.props.data.``type``
          unload = (this.getColumnDiff prevProps this.props)
        }
      | None -> ()

  override this.componentWillUnmount() =
    this.destroyChart()

  override this.render() =
    div [ Ref this.mountedElement ] [ ]

let inline chart props = ofType<Chart,_,_> props []