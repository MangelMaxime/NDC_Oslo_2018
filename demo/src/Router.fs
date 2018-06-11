[<RequireQualifiedAccess>]
module Router

open Fable.Import
open Fable.Helpers.React.Props
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser

type Page =
    | Counter
    | CounterList
    | StyledCounterList
    | FulmaCounterList
    | MapDemo

let private toHash page =
    match page with
    | Counter -> "#counter"
    | CounterList -> "#counter-list"
    | StyledCounterList -> "#styled-counter-list"
    | FulmaCounterList -> "#fulma-counter-list"
    | MapDemo -> "#demo-map"

let pageParser: Parser<Page->Page,Page> =
    oneOf [
        map Counter ( s "counter")
        map CounterList ( s "counter-list")
        map StyledCounterList ( s "styled-counter-list")
        map FulmaCounterList ( s "fulma-counter-list")
        map MapDemo ( s "demo-map")
        map Counter top ]

let href route =
    Href (toHash route)

let modifyUrl route =
    route |> toHash |> Navigation.modifyUrl

let newUrl route =
    route |> toHash |> Navigation.newUrl

let modifyLocation route =
    Browser.window.location.href <- toHash route
