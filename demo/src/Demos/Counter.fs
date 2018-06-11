module Counter

open Fable.Helpers.React
open Fable.Helpers.React.Props

type Model =
    { Value : int }

type Msg =
    | Increment
    | Decrement

let init () =
    { Value = 0 }

let update msg model =
    match msg with
    | Increment ->
        { model with Value = model.Value + 1 }

    | Decrement ->
        { model with Value = model.Value - 1 }

let viewFulma model dispatch =
    div [ Class "columns is-vcentered" ]
        [ div [ Class "column" ]
            [ button [ Class "button"
                       OnClick (fun _ -> dispatch Decrement) ]
                [ str "-" ] ]
          div [ Class "column" ]
            [ str (string model.Value) ]
          div [ Class "column" ]
              [ button [ Class "button"
                         OnClick (fun _ -> dispatch Increment) ]
                    [ str "+" ] ] ]

let viewRawHtml model dispatch =
    div [ ]
        [ button [ OnClick (fun _ -> dispatch Decrement) ]
            [ str "-" ]
          div [ ]
            [ str (string model.Value) ]
          button [ OnClick (fun _ -> dispatch Increment) ]
            [ str "+" ] ]
