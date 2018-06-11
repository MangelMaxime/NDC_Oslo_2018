module App.Demo

open Fable.Import.Browser

let counterBtn = document.querySelector("#counter-btn") :?> HTMLButtonElement
let counterResult = document.querySelector("#counter-result") :?> HTMLSpanElement
let mutable count = 0

counterBtn.onclick <-
    fun _ ->
        count <- count + 1
        counterResult.innerHTML <- "You clicked " + string count + " times"
        null
