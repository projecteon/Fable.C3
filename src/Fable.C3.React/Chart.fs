module Fable.C3.React

open Fable.Core
open Fable.Import
open Fable.Import.Browser
open Fable.Import.React
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.C3

type ChartProps = {
  data: Data
  axis: Axis option
  height: int
}

type Chart(initialProps) =
  inherit PureComponent<ChartProps, obj>(initialProps)
  let mutable chart: ChartAPI option =  None
  let mutable chartElement: Browser.HTMLElement option =  None

  member this.generateChart config =
    chart <- Some (c3.generate config)

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
    | None -> Array.empty
    | Some arr ->
      (arr.ToArray())
      |> Array.map (fun x -> x.[0])
      |> Array.choose id

  member this.getColumnDiff prevProps props =
    if prevProps = props then Array.empty
    else
      let L = set (this.getColumnNames prevProps.data.columns)
      let R = set (this.getColumnNames props.data.columns)
      L - R |> Set.toArray

  override this.componentDidMount() =
    match chartElement with
    | Some element -> chart <- Some ({bindto = Some (U2.Case2 element); data = this.props.data; axis = this.props.axis} |> c3.generate)
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
    div [ Ref this.mountedElement; Style [ Height this.props.height; MaxHeight this.props.height ] ] [ ]

let inline chart props = ofType<Chart,_,_> props []