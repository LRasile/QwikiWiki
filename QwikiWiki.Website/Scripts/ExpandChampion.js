
$(document).ready(function() {
    $('.Champion').click(function() {
        if ($(this).children().closest('.expandChampion').is(':visible')) {
            ExpandChampion($(this).attr("data-championname"));
        }
    });
});


function ExpandChampion(champName) {
    $(".Champion").each(function () {
        var championName = $(this).attr("data-championname");

        if (champName === championName) {
            $(this).attr("style", "height:auto;");
            $(this).children().closest('.expandChampion').toggleClass('expandChampionRotation');
            $(this).children().closest(".spellWrapper").slideToggle(1500);
        }
    });
}
