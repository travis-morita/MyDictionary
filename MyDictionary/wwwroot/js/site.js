// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



$(document).ready(function () {
    $("#word").autocomplete({
        source: function (request, response) {
            $.ajax({
                async: true,
                crossDomain: true,
                url: "https://wordsapiv1.p.rapidapi.com/words/?letterPattern=^" + request.term + ".*&lettersmin=3&limit=50",
                method: 'get',
                headers: {
                    "x-rapidapi-host": "wordsapiv1.p.rapidapi.com",
                    "x-rapidapi-key": "433ee81754mshd7eaa03edec82d5p1609e6jsn5b1a086d7b86"
                },
                success: function (obj) {
                    response(obj.results.data);
                    //var obj = JSON.parse(data);
                    //response(obj.results.data);
                }
            });
        },
        minLength: 3,
        error: function (message) {
            alert(message);
        },
        autoFocus: true
    });

    //$("#word").autocomplete({
    //    source: function (request, response) {
    //        $.ajax({
    //            url: "/Home/StringSearch/?searchString=" + request.term,
    //            method: 'get',
    //            success: function (data) {
    //                var obj = JSON.parse(data);
    //                response(obj.results.data);
    //            }
    //        });
    //    },
    //    minLength: 3,
    //    error: function (message) {
    //        alert(message);
    //    },
    //    autoFocus: true
    });
});

