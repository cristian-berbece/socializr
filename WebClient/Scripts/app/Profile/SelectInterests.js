$(function () {
    console.log("SelectInterests.js has fired!");

    var $interestsList = $("#InterestIds");
    $interestsList.select2({
        allowClear: false,
        minimumInputLength: 2,
        ajax: {
            url: "/socializr/Api/SearchInterests",
            data: function (term, page) {
                return {
                    searchTerm: term.term
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
    })

    $("select").on("select2:select", function (evt) {
        var element = evt.params.data.element;
        var $element = $(element);
        $element.detach();
        $(this).append($element);
        $(this).trigger("change");
    });

    $("#InterestIds > option").prop("selected", "selected");
    $("#InterestIds").trigger("change");
});