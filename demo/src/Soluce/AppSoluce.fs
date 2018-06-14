module App


(**
# Important

This file isn't beginner friendlt it's just gluing all the demos together into a single application

If you are new to Elmish you should take a look at the Demos folder
*)

open Fable.Helpers.React
open Fable.Helpers.React.Props
open Elmish
open Fable.Import
open Fulma

type DisplayMode =
    | NoStyle
    | ManualStyle
    | FulmaStyle

type Application =
    | Counter of Counter.Model
    | CounterList of DisplayMode * CounterList.Model
    | MapDemo of MapDemo.Model

type Model =
    { CurrentPage : Router.Page
      CurrentApplication : Application }

type Msg =
    | CounterMsg of Counter.Msg
    | CounterListMsg of CounterList.Msg
    | MapDemoMsg of MapDemo.Msg

let urlUpdate (result: Option<Router.Page>) model =
    match result with
    | None ->
        Browser.console.error("Error parsing url: " + Browser.window.location.href)
        model, Router.modifyUrl model.CurrentPage

    | Some page ->
        let model = { model with CurrentPage = page }
        match page with
        | Router.Counter ->
            let counterModel = Counter.init ()
            { model with CurrentApplication = Counter counterModel }, Cmd.none

        | Router.CounterList ->
            let counterListModel = CounterList.init ()
            { model with CurrentApplication = CounterList (NoStyle, counterListModel) }, Cmd.none

        | Router.StyledCounterList ->
            let counterListModel = CounterList.init ()
            { model with CurrentApplication = CounterList (ManualStyle, counterListModel) }, Cmd.none

        | Router.FulmaCounterList ->
            let counterListModel = CounterList.init ()
            { model with CurrentApplication = CounterList (FulmaStyle, counterListModel) }, Cmd.none

        | Router.MapDemo ->
            let (mapDemoModel, mapDemoCmd) = MapDemo.init ()
            { model with CurrentApplication = MapDemo mapDemoModel }, Cmd.map MapDemoMsg mapDemoCmd

let init result =
    urlUpdate result { CurrentPage = Router.Counter
                       CurrentApplication = Counter (Counter.init()) }

let update msg model =
    match (msg, model) with
    | (CounterMsg counterMsg, { CurrentApplication = Counter counterModel }) ->
        let newCounterModel = Counter.update counterMsg counterModel
        { model with CurrentApplication = Counter newCounterModel }, Cmd.none

    | (CounterListMsg counterListMsg, { CurrentApplication = CounterList (style, counterListModel )} ) ->
        let newModel = CounterList.update counterListMsg counterListModel
        { model with CurrentApplication = CounterList (style, newModel ) }, Cmd.none

    | (MapDemoMsg mapDemoMsg, { CurrentApplication = MapDemo mapDemoModel }) ->
        let (newMapDemoModel, mapDemoCmd) = MapDemo.update mapDemoMsg mapDemoModel
        { model with CurrentApplication = MapDemo newMapDemoModel }, Cmd.map MapDemoMsg mapDemoCmd

    | (msg, model) ->
        printfn "Message discarded: %s" (string msg)
        model, Cmd.none

let private menuItem label currentPage targetPage =
        Menu.item [ Menu.Item.IsActive (currentPage = targetPage)
                    Menu.Item.Props [ Router.href targetPage ] ]
           [ str label ]

let private menu (currentPage : Router.Page) =
    Menu.menu [ ]
        [ Menu.label [ ]
            [ str "Simple counter" ]
          Menu.list [ ]
            [ menuItem "Demo" currentPage Router.Counter ]
          Menu.label [ ]
            [ str "Counter list" ]
          Menu.list [ ]
            [ menuItem "Raw html" currentPage Router.CounterList
              menuItem "Manual style" currentPage Router.StyledCounterList
              menuItem "Fulma style" currentPage Router.FulmaCounterList ]
          Menu.label [ ]
            [ str "Leaflet demo" ]
          Menu.list [ ]
            [ menuItem "Demo" currentPage Router.MapDemo ] ]

let private navbar =
    Navbar.navbar [ Navbar.Color IsInfo ]
        [ Container.container [ ]
            [ Navbar.Start.div [ ]
                [ Navbar.Item.div [ ]
                    [ str "NDC Oslo 2018 - Demo" ] ] ] ]

let view model dispatch =
    let content =
        match model.CurrentApplication with
        | Counter counterModel ->
            Counter.viewRawHtml counterModel (CounterMsg >> dispatch)

        | CounterList (style, counterListModel) ->
            match style with
            | NoStyle -> CounterList.viewRawHtml counterListModel (CounterListMsg >> dispatch)
            | ManualStyle -> CounterList.viewManualStyle counterListModel (CounterListMsg >> dispatch)
            | FulmaStyle -> CounterList.viewFulma counterListModel (CounterListMsg >> dispatch)

        | MapDemo mapDemoModel ->
            MapDemo.view mapDemoModel (MapDemoMsg >> dispatch)

    div [ ]
        [ navbar
          Container.container [ Container.Props [ Style [ MarginTop "1rem" ] ] ]
            [ Columns.columns [ ]
                [ Column.column [ Column.Width (Screen.All, Column.Is2) ]
                    [ menu model.CurrentPage ]
                  Column.column [ ]
                    [ content ] ] ] ]

open Elmish.React
open Elmish.Debug
open Elmish.HMR
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser

Program.mkProgram init update view
|> Program.toNavigable (parseHash Router.pageParser) urlUpdate
// |> Program.withHMR
|> Program.withReactUnoptimized "elmish-app"
// |> Program.withDebugger
|> Program.run
