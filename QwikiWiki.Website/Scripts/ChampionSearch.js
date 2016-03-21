$(document).ready(function () {

    $('#txtSearch').focus();
    FilterChampions('');

    $('#txtSearch').change(function () {
        FilterChampions($(this).val());
    });

    $('#clearSearch').click(function () {
        $('#txtSearch').val('');
        FilterChampions('');
        $('#txtSearch').focus();
    });

    //setup before functions
    var typingTimer;                //timer identifier
    var doneTypingInterval = 300;  //time in ms, 5 second for example

    //user is "finished typing," do something
    function doneTyping() {
        FilterChampions($('#txtSearch').val());
    }

    //on keyup, start the countdown
    $('#txtSearch').keyup(function () {
        clearTimeout(typingTimer);
        typingTimer = setTimeout(doneTyping, doneTypingInterval);
    });

    //on keydown, clear the countdown 
    $('#txtSearch').keydown(function () {
        clearTimeout(typingTimer);
    });

});

function RoleFilter(role) {
    $('#txtSearch').val(role);
    FilterChampions(role);
}

function FilterChampions(searchText) {
    if (searchText !== '') {
        $("#clearSearch").show();
    }
    else {
        $("#clearSearch").hide();
    }

    $(".Champion").each(function () {
        var championName = $(this).attr("data-championname");
        var championRole = $(this).attr("data-championrole");
        var championRoleAbbr = $(this).attr("data-championroleabbr");

        searchText = SanitizeSearchText(searchText);
        championName = SanitizeSearchText(championName);
        championRole = SanitizeSearchText(championRole);
        championRoleAbbr = SanitizeSearchText(championRoleAbbr);

        //Hide all champions
        $(this).hide(0);

        if (championName.search(searchText) !== -1 || (championRole.search(searchText) !== -1 && searchText.length > 3) || (championRoleAbbr.search(searchText) != -1 && searchText.length > 2)) {
            $(this).show(0);
        }

    });
}

function SanitizeSearchText(searchText) {
    searchText = searchText.toLowerCase();
    searchText = searchText.replace("'", "");
    searchText = searchText.replace(" ", "");
    return searchText;
}


