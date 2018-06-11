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

### Modern mobile app development?

- Fable
- Elmish
- Tooling
- Elmish ecosystem
- Html to Elmish
- What's coming next ?

***

### Fable

- Compiler F# to JavaScript
- Readable output
- Best of .Net & JavaScript

---

### Fable

## Demo

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

### Show me the code

***

### TakeAways

* Learn all the FP you can!
* Simple modular design

***

### Thank you!

* https://github.com/fable-compiler/fable-elmish
* https://ionide.io
* https://facebook.github.io/react-native/
