var id = 1;

function showQ(q) {
    document.getElementById("kérdés").innerText = q.question1
    document.getElementById("válasz1").innerText = q.answer1
    document.getElementById("válasz2").innerText = q.answer2
    document.getElementById("válasz3").innerText = q.answer3
    document.getElementById("kép").src = "https://szoft1.comeback.hu/hajo/" + q.image;
}

function kérdésBetöltés(id) {
    fetch(`/questions/${id}`)
        .then(response => {
            if (!response.ok) {
                console.error(`Hibás válasz: ${response.status}`)
            }
            else {
                return response.json()
            }
        })
        .then(data => showQ(data));
}    

function next() {
    kérdésBetöltés(id + 1);
}

function back() {
    kérdésBetöltés(id - 1);
}

