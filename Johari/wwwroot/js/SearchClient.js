var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/client",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { data: "First_Name", width: "20%" },
            { data: "Last_Name", width: "20%" },
            { data: "ResponseLimit", width: "10%" },
            { data: "ResponseSubmissionCount", width: "10%" },           
            {
                data: "Id", width: "10%",
                "render": function (data) {
                    return `<div class="text-center">
                            <a href="/Admin/JohariWindow?id=${data}"
                            class ="btn btn-secondary style="cursor:pointer; width=100px;"> Create Window</a>
                           
                    </div>`;
                }
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}