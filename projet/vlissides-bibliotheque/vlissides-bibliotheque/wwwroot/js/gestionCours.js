function assignerCours(CoursId) {

    var Cocher = document.querySelector('#' + "Cours" + "-" + CoursId).checked

    var DonnerRecus =
    {
        CoursId: CoursId,
        cocher: Cocher,

    };


    var data = JSON.stringify(DonnerRecus);


    fetch("/GestionProfil/AssignerCours", {
        method: 'Post',
        body: data,
        contentType: "application/json; charset=utf-8",
        headers: {
            "Content-Type": "application/json",
        },

    }).then(function (response) {

        return null;

    });
}