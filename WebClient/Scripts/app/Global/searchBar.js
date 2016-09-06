$(function () {

    var literals = {
        searchBarId: "search-bar"
    };
    var $search = $("#" + literals.searchBarId);


    console.log("searchBar.js has fired!");
    $search.select2({
        minimumInputLength: 3,
        placeholder: "Search for your friends!",
        ajax: {
            url: "/socializr/Profile/SearchUsers",
            data: function (term, page) {
                return {
                    query: term.term
                };
            },
            dataType: "json",
            type: 'GET',
            delay: 250,
            processResults: function (data) {
                console.log(data);
                return {
                    results: data
                };
            }
        }
    });

    $search.on("change", function (e) {
        console.log($search.val());
        window.location.replace('http://localhost/Socializr/Profile/ViewProfile/' +$search.val())
    });

});