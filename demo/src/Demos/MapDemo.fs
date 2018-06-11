module MapDemo

open Fable.Import
open Elmish
open Fable.Core.JsInterop
open Fulma
open Fulma.Extensions
open Fulma.FontAwesome

let accessToken = "pk.eyJ1IjoibWFuZ2VsbWF4aW1lIiwiYSI6ImNqM3VoNm5sZzAwNDYzdXFkZGI2amR3aDgifQ.GkUad_DDjha7kWvAOv72dQ"
let mapboxUrl = "https://api.tiles.mapbox.com/v4/mapbox.streets/{z}/{x}/{y}.png?access_token=" + accessToken

importSideEffects "./leaflet_workaround.js"

let getPosition () : JS.Promise<Browser.Position> = importMember "./geolocation.js"

type UserPosition =
    | Loading
    | Ok of Leaflet.LatLngTuple
    | Error of string

type Model =
    { UserPosition : UserPosition
      ShowMarker : bool }

type Msg =
    | GetPositionSuccess of Browser.Position
    | GetPositionFail of exn
    | ToggleMarkerDisplay

let init () =
    { UserPosition = Loading
      ShowMarker = false }, Cmd.ofPromise getPosition () GetPositionSuccess GetPositionFail

let update msg model =
    match msg with
    | GetPositionSuccess position ->
        { model with UserPosition = Ok (position.coords.latitude, position.coords.longitude) }, Cmd.none

    | GetPositionFail error ->
        Browser.console.error error
        { model with UserPosition = Error error.Message }, Cmd.none

    | ToggleMarkerDisplay ->
        { model with ShowMarker = not model.ShowMarker }, Cmd.none

module RL = ReactLeaflet
open Fable.Helpers.React
open Fable.Helpers.React.Props

let private map showMarker (userPosition : Leaflet.LatLngTuple) =
    RL.map [ RL.MapProps.Center !^userPosition
             RL.MapProps.Zoom 14.
             RL.MapProps.Key "map"
             RL.MapProps.Style [ Height "100%"
                                 Width "100%" ] ]
        [ yield RL.tileLayer [ RL.TileLayerProps.Url mapboxUrl ]
            [ ]
          if showMarker then
            yield RL.marker [ RL.MarkerProps.Position !^userPosition ]
                [ RL.popup [ ]
                    [ str "You are here" ] ] ]

let view model dispatch =
    let mapContent =
        match model.UserPosition with
        | Loading ->
            div [ Style [ Margin "10rem 50%" ] ]
                [ Icon.faIcon [ Icon.Size IsLarge ]
                    [ Fa.icon Fa.I.Spinner
                      Fa.fa3x
                      Fa.spin ] ]
        | Ok pos -> map model.ShowMarker pos
        | Error msg ->
            Message.message [ Message.Color IsDanger ]
                [ Message.body [ ]
                    [ str msg ] ]
    Columns.columns [ Columns.Props [ Style [ MinHeight "300px" ] ] ]
        [ Column.column [ ]
            [ Switch.switch [ Switch.Checked model.ShowMarker
                              Switch.OnChange (fun _ -> dispatch ToggleMarkerDisplay) ]
                [ str "Show your position" ] ]
          Column.column [ ]
             [ mapContent ] ]
