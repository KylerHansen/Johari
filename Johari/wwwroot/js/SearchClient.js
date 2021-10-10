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
            { data: "first_Name", width: "20%" },
            { data: "last_Name", width: "20%" },
            { data: "hasResponded", width: "10%" },
            {
                data: "responseLimit", width: "10%",
                "render": function (data) {
                    return `${data}`;
                }
            },
            { data: "responseSubmissionCount", width: "10%" },           
            {
                data: "id", width: "30%",
                "render": function (data) {
                    return `<div class="text-center">
                            <div class="pb-1" title="Create Johari Window">
                                <a href="/Admin/JohariWindow?id=${data}"
                                class ="btn btn-secondary w-100" style="cursor:pointer;"; "> Create</a>
                            </div>
                            <div title="Resets all responses of client and friends">
                            <a href="/Admin/ResetResponses?id=${data}"
                            class ="btn btn-danger w-100 " style="cursor:pointer; "> Reset</a>
                            </div>
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


