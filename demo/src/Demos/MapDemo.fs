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

