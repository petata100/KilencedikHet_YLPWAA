fetch("hajo/kerdesek/4")
    .then(response => response.json())
    .then(data => showQ(data))

function showQ(q) {
    document.getElementById("kérdés").innerText = q.question1
    document.getElementById("válasz1").innerText = q.answer1
    document.getElementById("válasz2").innerText = q.answer2
    document.getElementById("válasz3").innerText = q.answer3
    document.getElementById("kép").src = "https://szoft1.comeback.hu/hajo/" + q.image;
}