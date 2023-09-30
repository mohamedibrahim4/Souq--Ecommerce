$(document).ready(function () {
    $("#maincatID").change(function () {
        $.get("/Products/GetSubcatList", { categoryId: $("#maincatID").val() }, function (data) {
            $("#subcatId").empty();
            $.each(data,function(index, row) {

                $("#subcatId").append("<option value='" +row.subcatId +"'>"+ row.subcatName +"</option>")
            });
        })
    })
})
