var viccek;

function letöltés() {
    fetch('/jokes.json')
        .then(response => response.json())
        .then(data => letöltésBefejeződött(data)
        );
}

function letöltésBefejeződött(d) {
    let jokes = document.getElementById("jokes");
    console.log("Sikeres letöltés");
    console.log(d);
    viccek = d;

    for (i = 0; i < viccek.length; i++) {
        let joke = document.createElement("div");
        joke.innerHTML = "<h1>" + viccek[i].question + "</h1>";
        jokes.appendChild(joke);
    }
}