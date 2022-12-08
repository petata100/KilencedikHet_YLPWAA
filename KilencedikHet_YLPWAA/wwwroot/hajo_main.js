var jóVálasz
var questionId = 4

window.onload = function (e) {
    console.log("Oldal betöltve...");
    document.getElementById("előre_gomb").onclick = előre;
    document.getElementById("vissza_gomb").onclick = vissza;
    document.getElementById("válasz1").onclick = check1;
    document.getElementById("válasz2").onclick = check2;
    document.getElementById("válasz3").onclick = check3;
    kérdésBetöltés(questionId)
}

function kérdésMegjelenítés(kerdes) {
    if (!kerdes) return; //Ha undefined a kérdés objektum, nincs mit tenni
    console.log(kerdes);
    document.getElementById("kérdés_szöveg").innerText = kerdes.question1;
    document.getElementById("válasz1").innerText = kerdes.answer1;
    document.getElementById("válasz2").innerText = kerdes.answer2;
    document.getElementById("válasz3").innerText = kerdes.answer3;
    jóVálasz = kerdes.correctAnswer;
    if (kerdes.image) {
        document.getElementById("kép1").src = "https://szoft1.comeback.hu/hajo/" + kerdes.image;
        document.getElementById("kép1").classList.remove("rejtett")
    }
    else {
        document.getElementById("kép1").classList.add("rejtett")
    }
    //Jó és rossz kérdések jelölésének levétele
    document.getElementById("válasz1").classList.remove("jó", "rossz");
    document.getElementById("válasz2").classList.remove("jó", "rossz");
    document.getElementById("válasz3").classList.remove("jó", "rossz");
}

function kérdésBetöltés(id) {
    fetch(`/hajo/kerdesek/${id}`)
        .then(response => {
            if (!response.ok) {
                console.error(`Hibás válasz: ${response.status}`)
            }
            else {
                return response.json()
            }
        })
        .then(data => kérdésMegjelenítés(data));

}

function előre() {
    questionId++;
    kérdésBetöltés(questionId)
}

function vissza() {
    questionId--;
    kérdésBetöltés(questionId)
}

function check1() {
    választás(1);
}

function check2() {
    választás(2);
}
function check3() {
    választás(3);
}

function választás(n) {
    if (n != jóVálasz) {
        document.getElementById(`válasz${n}`).classList.add("rossz");
        document.getElementById(`válasz${jóVálasz}`).classList.add("jó");
    }
    else {
        document.getElementById(`válasz${jóVálasz}`).classList.add("jó");
    }
}
