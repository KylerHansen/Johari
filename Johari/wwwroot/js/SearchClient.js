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
            {
                data: "responseLimit", width: "10%",
                "render": function (data) {
                    return `${data}`;
                }
            },
            { data: "responseSubmissionCount", width: "10%" },           
            {
                data: "id", width: "40%",
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


