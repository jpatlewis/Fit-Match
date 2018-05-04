$(document).ready(function () {
    var inputText = "<input type='text' id='searchString' name='SearchString' class='search-input input-margin' />"
    var inputInt = "<input type='number' min='0' max='99999' id='searchString' name='SearchString' class='search-input input-margin'/>"

    $("button[name='searchButton'").on("click", function (event) {
        var searchString = $("#searchString").val();
        var searchType = $("#SearchTypeID").val();

        $("#searchTable").empty();

        var service = new SearchService();
        service.search(searchString, searchType, refreshSearchTable);
        event.preventDefault();
    })

    $("select")
        .change(function () {
            $("select option:selected").each(function () {
                console.log(this.text)
                $("input[name='SearchString']").remove();
                if (this.text == "Price Per Hour") {
                    $("input[name='SearchString']").remove();
                    console.log(this.text);
                    $(inputInt).insertAfter("select");
                }
                else if (this.text == "Zip Code") {
                    $("input[name='SearchString']").remove();
                    $(inputInt).insertAfter("select");
                }
                else if (this.text == "Name") {
                    $("input[name='SearchString']").remove();
                    $(inputText).insertAfter("select");
                }
            })
        })
        //    var str = "";
        //    $("select option:selected").each(function () {
        //        str += $(this).text() + " ";
        //    });
        //    console.log(str)
        //})
        //.change();


    function refreshSearchTable(searchResults) {
        console.log("Search Results: ", searchResults);

        for (var i = 0; i < searchResults.length; i++) {

            var result = searchResults[i];

            var tr = $("<tr scope='row'>");
            var searchCell = $("<td>")
            var profileURL = "/trainee/trainerprofile/" + result.Trainer_ID

            tr.append(searchCell);

            var profileCell = $("<td class='view-profile'>")
            var firstNameCell = $("<td>").text(result.First_Name);
            var anchor = $("<a>").text("View Profile").attr("href", profileURL);
            var lastNameCell = $("<td>").text(result.Last_Name);
            var emailCell = $("<td>").text(result.Email);
            var priceCell = $("<td>").text(result.Price_Per_Hour);
            var locationCell = $("<td>").text(result.User_Location);

            tr.append(firstNameCell);
            tr.append(lastNameCell);
            tr.append(emailCell);
            tr.append(priceCell);
            tr.append(locationCell);
            tr.append(profileCell);
            profileCell.append(anchor);


            $("#searchTable").append(tr);
        }
    }

    function SearchService() {
        const root = "/Trainee/SearchResult";

        this.search = function (searchString, searchType, successCallback) {
            console.log(searchString);
            console.log(searchType);
            $.ajax({
                url: root,
                method: "GET",
                data: {
                    "searchString": searchString,
                    "searchType": searchType
                }
            }).done(function (data) {
                successCallback(data);
            }).fail(function (xhr, status, error) {
                console.error("Error occured while retrieving search results", error);
            })
        }
    }
})