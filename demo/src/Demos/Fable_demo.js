import { createAtom } from "fable-core/Util";
export const counterBtn = document.querySelector("#counter-btn");
export const counterResult = document.querySelector("#counter-result");
export let count = createAtom(0);

counterBtn.onclick = function (ev) {
  count(count() + 1);
  counterResult.innerHTML = "You clicked " + count().toString() + " times";
};
