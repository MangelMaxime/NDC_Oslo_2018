module CounterList

open Fable.Helpers.React
open Fable.Helpers.React.Props
open Elmish

type Model =
    { Counters : Counter.Model list }

type Msg =
    | Add
    | Remove
    | Modify of int * Counter.Msg

let init () = { Counters = [] }

let update msg model =
    match msg with
    | Add ->
        { model with Counters =  Counter.init() :: model.Counters }

    | Remove ->
        let counters =
            match model.Counters with
            | _::tail -> tail
            | [] -> []
        { model with Counters = counters }

    | Modify (counterIndex, counterMsg) ->
        let counters =
            model.Counters
            |> List.mapi (fun localIndex counterModel ->
                if localIndex = counterIndex then
                    Counter.update counterMsg counterModel
                else
                    counterModel
            )
        { model with Counters = counters }

let viewRawHtml model dispatch =
    let counterDispatch i msg = dispatch (Modify (i, msg))

    let counters =
        model.Counters
        |> List.mapi (fun i c -> Counter.viewRawHtml c (counterDispatch i))

    div [ ]
        [ div [ ]
            [ button [ OnClick (fun _ -> dispatch Remove) ]
                        [  str "Remove" ]
              button [ OnClick (fun _ -> dispatch Add) ]
                        [ str "Add" ] ]
          div [ ]
            counters ]

let viewManualStyle model dispatch =
    let counterDispatch i msg = dispatch (Modify (i, msg))

    let counters =
        model.Counters
        |> List.mapi (fun i c -> Counter.viewFulma c (counterDispatch i))

    div [ Style [ Width "300px" ]
          Class "container" ]
        [ div [ Class "columns" ]
            [ div [ Class "column" ]
                    [ button [ Class "button"
                               OnClick (fun _ -> dispatch Remove) ]
                        [  str "Remove" ] ]
              div [ Class "column" ]
                    [ button [ Class "button"
                               OnClick (fun _ -> dispatch Add) ]
                        [ str "Add" ] ] ]
          div [ ]
            counters ]

let viewFulma model dispatch =
    let counterDispatch i msg = dispatch (Modify (i, msg))

    let counters =
        model.Counters
        |> List.mapi (fun i c -> Counter.viewFulma c (counterDispatch i))

    div [ Style [ Width "300px" ]
          Class "container" ]
        [ div [ Class "columns" ]
            [ div [ Class "column" ]
                    [ button [ Class "button"
                               OnClick (fun _ -> dispatch Remove) ]
                        [  str "Remove" ] ]
              div [ Class "column" ]
                    [ button [ Class "button"
                               OnClick (fun _ -> dispatch Add) ]
                        [ str "Add" ] ] ]
          div [ ]
            counters ]
