$(function () {

    console.log("LocalitiesDropdown.js has fired!");

    //Find the dropdowns in the DOM
    var $countiesList = $("#IdCounty");
    var $citiesList = $("#IdCity");

    //Configure select2 on Counties List
    $countiesList.select2({
        placeholder: "Select your county",
        allowClear: true
    });

    //Configure select2 on Cities List
    $citiesList.select2({
        placeholder: "Select your city",
        allowClear: true
    });

    //Connect the 2 drop down lists
    $countiesList.on("change", function (e) {
        repopulateCityList($countiesList.select2("val"));
    });

    //Makes an AJAX call that gets all cities belonging to a county
    //It then binds those cities to the cities drop down list
    function repopulateCityList(countyId) {
        var ajaxUrl = "/socializr/Api/GetCitiesByCountyId";
        $.ajax({
            url: ajaxUrl,
            data: { id: countyId },
            dataType: "json",
            cache: false,
            type: "POST",
            success: processAjaxResult,
        });
        function processAjaxResult(data, status) {
            $citiesList.empty();
            $citiesList.select2({
                data: data,
                allowClear: true
            });
        }
    }

});
