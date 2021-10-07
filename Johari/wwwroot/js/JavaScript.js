var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/client/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "firstName", "autoWidth": true },
            { data: "lastName" },
            { data: "email" },
            { data: "phoneNumber" },
            {
                data: "id", width: "40%",
                "render": function (data) {
                    return `<div class="text-center">
                            <a href="/Admin/Clients/Upsert?id=${data}"
                            class ="btn btn-success text-white style="cursor:pointer; width=100px;"> <i class="far fa-edit"></i>Edit</a>
                            <a href="/Admin/ClientWindow/JohariWindow?id=${data}"
                            class ="btn btn-primary text-white style="cursor:pointer; width=100px;"> <i class="far fa-solid fa-hourglass"></i>&nbsp;Run Johari Window</a>
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
    //Sweet alert
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