module App.Demo

open Fable.Import.Browser

let counterBtn = document.querySelector("#counter-btn") :?> HTMLButtonElement
let counterResult = document.querySelector("#counter-result")
let mutable count = 0

counterBtn.onclick <-
    fun _ ->
        count <- count + 1
        counterResult.innerHTML <- "You clicked " + string count + " times"

open Fable.Core.JsInterop

let add (x:int) (y:int) : int = import "add" "./Demos/my-lib.js"

add 2 3
|> console.log