$(document).ready(function () {
    getMailList();
    searchBarText();
    activeEmail();
});

var str = "";
function getMailList() {
    $.ajax({
        url: '/email/getmail',
        type: 'POST',
        datatype: 'json'
    }).done(function (data) {
        $.each(data, function (index, item) {
            str += "<li id='" + index + "' class='list-group-item'> " +
                "<h4 class='list-group-item-heading'>Message :"
                + index + "</h4>" +
            "<p class='list-group-item-text'> Subject : "
                + item.Subject + "</p>"
            + "<p class='list-group-item-text'> From : "
                + item.From + "</p>"
            + "</li>";
        });
        $('#emailListDisplay').append(str);
    });
}

function searchBarText() {
    $("#search_button").click(function () {
        $.ajax({
            url: '',
            type: 'POST'
        })
        alert($("#search_Bar_Text").val());
    });
}


function activeEmail() {
    $(document).on('click', '.list-group li', function (e) {
        $(".list-group li").removeClass('active');
        $(this).addClass('active');
        e.preventDefault();
    });
}