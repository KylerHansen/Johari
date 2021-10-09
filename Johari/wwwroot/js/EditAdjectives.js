var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/adjective/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [           
            {
                data: "type", width: "10%",
                "render": function (data) {
                    if (data == 0) {
                        return `Negative`;
                    }

                    return `Positive`;
                }
            },
            { data: "name", width: "20%" },
            { data: "definition", width: "60%" },
            {
                data: "id", width: "10%",
                "render": function (data) {
                    return `<div class="text-center">
                          <a onClick=Delete('/api/adjective/'+${data})
                            class ="text-danger" style="cursor:pointer; width=100px;"> <span class="material-icons md-48 ">delete_outline</span></a>
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

function Delete(url) { 
    swal({
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore this data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else
                        toastr.error(data.message);
                }
            })
        }
    })
}