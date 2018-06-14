- title : F# and Fable
- description : Re-think web development
- author : Maxime Mangel
- theme : solarized
- transition : default

***

## F# and Fable

<br />

### Re-think web development

<br />
Maxime Mangel - [@MangelMaxime](http://www.twitter.com/MangelMaxime)

***

- Fable
- Elmish
- Tooling
- Elmish ecosystem
- Live coding
- What's coming next ?

***

### Fable

- Compiler F# to JavaScript
- Readable output
- Best of .Net & JavaScript

---

### Fable

## Demo

---

### Fable

- Intellisense
- Error at design time
- Great interop

***

## Elmish

---

## Model - View - Update

<img src="images/elmish-flow.png" style="background: white;"/>

---

---

## Model - View - Update

### Model

```fs
type Model =
    { Value : int }

type Msg =
    | Increment
    | Decrement

let init () =
    { Value = 0 }
```

---

## Model - View - Update

### Update

```fs
let update msg model =
    match msg with
    | Increment ->
        { model with Value = model.Value + 1 }

    | Decrement ->
        { model with Value = model.Value - 1 }
```

---

## Model - View - Update

### View

```fs
let view model dispatch =
    div [ ]
        [ button [ OnClick (fun _ -> dispatch Decrement) ]
            [ str "-" ]
          div [ ]
            [ str (string model.Value) ]
          button [ OnClick (fun _ -> dispatch Increment) ]
            [ str "+" ] ]
```

---

## Model - View - Update

### Linking everything

```fs
Program.mkSimple init update view
|> Program.withReact "elmish-app"
|> Program.run
```

---

## Model - View - Update

### Demo

***

### Nested components

### Model

```fs
type Model =
    { Counters : Counter.Model list }

type Msg =
    | Add
    | Remove
    | Modify of int * Counter.Msg

let init () = { Counters = [] }
```

---

## Nested components

### Update (1/2)

```fs
let update msg model =
    match msg with
    | Add ->
        { model with Counters = Counter.init () :: model.Counters }

    | Remove ->
        { model with
            Counters =
                match model.Counters with
                | [] -> []
                | _ :: tail -> tail }
```

---

## Nested components

### Update (2/2)

```fs
let update msg model =
    match msg with
    | Modify (counterIndex, counterMsg) ->
        { model with
            Counters =
                model.Counters
                |> List.mapi (fun localIndex counterModel ->
                    if localIndex = counterIndex then
                        Counter.update counterMsg counterModel
                    else
                        counterModel
                ) }
```

---

## Nested components

### View

```fs
let view model dispatch =
    let counterDispatch i msg = dispatch (Modify (i, msg))

    let counters =
        model.Counters
        |> List.mapi (fun i c -> Counter.view c (counterDispatch i))


    div [ ]
        [ yield button [ OnClick (fun _ -> dispatch Remove) ]
            [  str "Remove" ]
          yield button [ OnClick (fun _ -> dispatch Add) ]
            [ str "Add" ]
          yield! counters ]
```

---

## Model - View - Update

### Demo

***

### Tooling

---

## Elmish.Debugger

<img src="images/debugger_code.png" style="background: white;"/>

---

## Elmish.HMR

<img src="images/hmr_code.png" style="background: white;"/>

***

## The CSS problem

---

## Manual css with Bulma

```fs
let view model dispatch =
    div [ Class "columns is-vcentered"
          Style [ Width "300px" ] ]
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
```

---

## Strongly typed css wtih Fulma

```fs
let view model dispatch =
    Columns.columns [ Columns.Props [ Style [ Width "300px" ] ]
                      Column.IsVcentered ]
        [ Column.column [ ]
            [ Button.button [ Button.OnClick (fun _ -> dispatch Decrement) ]
                [ str "-" ] ]
            Column.column [ ]
                [ str (string model.Value) ]
            Column.column [ ]
                [ Button.button [ Button.OnClick (fun _ -> dispatch Increment) ]
                    [ str "+" ] ] ]
```

***

## React

<img src="images/react_leaflet.png">

***

## Time to code

***

## Thoth.Json

- No need to follow the json structure
- Decode only what's needed
- Strongly typed
- Easily extensible
- Nice error message

---

### Thot.Json.Decode

<img src="images/thot-decode.png">

---

### Thot.Json.Decode

<img src="images/thoth_code_1.png">

---

### Thot.Json.Decode

<img src="images/thoth_code_2.png">

---

#### Thot.Json generator (WIP)

<img src="images/thot-generator-demo.gif">

---

### Thot.Json-next

- Auto decoder/encoder using Reflection
- Better errors
<img src="images/thoth_error_next_gen.png">

***

## Look at these projects too

- [SAFE stack](https://github.com/SAFE-Stack)
- [Fulma demo](https://mangelmaxime.github.io/fulma-demo/)
- [Fable.Remoting](https://zaid-ajaj.github.io/Fable.Remoting/)
- [Elmish.Toastr](https://zaid-ajaj.github.io/Elmish.Toastr/)

***

## What's coming ?

- Html to Elmish
    - Fulma
- React bindings generator
- Thot.Json generator tool
- Fable 2
    - Reduce application size

***

## F# and Fable

### Summary

* No more HTML templates
* Views are just code
* Strongly typed CSS
* Strongly typed components
* Quick feedback
* Handle every case
* The compiler have your back

***

### Thank you!

http://fable.io/fableconf