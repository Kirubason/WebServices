function GetAllHeroes() {
    $.ajax({
        url: "Service1.svc/GetAllHeroes",
        type: "GET",
        dataType: "json",
        success: function (result) {
            heroes = result
            drawHeroTable(result)
            console.log(result)
        }
    });
}

function addHero() {
    var NewHero = {
        "FirstName": $("#addFirstname").val(),
        "LastName": $("#addLastname").val(),
        "HeroName": $("#addHeroname").val(),
        "PlaceOfBirth": $("#addPlaceOfBirth").val(),
        "Combat": $("#addCombatPoints").val(),
    }

    $.ajax({
        url: "Service1.svc/AddHero",
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(NewHero),
        success: function () {
            showOverview();
        }
    });
}


function putHero() {
    
        updateHero.FirstName = $("#updateFirstname").val(),
        updateHero.LastName = $("#updateLastname").val(),
        updateHero.HeroName = $("#updateHeroname").val(),
        updateHero.PlaceOfBirth = $("#updatePlaceOfBirth").val(),
        updateHero.Combat = $("#updateCombatPoints").val(),
    

    $.ajax({
        url: "Service1.svc/UpdateHero/" + updateHero.Id,
        type: "PUT",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(updateHero),
        success: function () {
            showOverview();
        }
    });
}

function deleteHero(id) {
        $.ajax({
            url: "Service1.svc/DeleteHero/"+ id,
        type: "DELETE",
        dataType: "json",
        contentType: "application/json",
        success: function (result) {
            heroes = result
            drawHeroTable(result)
        }
    });
}


function searchHero() {
    var word = $("#searchText").val();
    $.ajax({
        url: "Service1.svc/SearchWord/" + word,
        type: "GET",
        dataType: "json",
        success: function (result) {
            heroes = result
            drawHeroTable(result)
            console.log(result)
        }
    });
}