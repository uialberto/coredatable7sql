"use strict";

$(() => {

    if ($('#tbl-peoples').length !== 0) {

        var table = $('#tbl-peoples').DataTable({
            language: {
                "lengthMenu": "Mostrar _MENU_ registros",
                "zeroRecords": "No se encontraron resultados",
                "emptyTable": "Ningún dato disponible en esta tabla",
                "info": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                "infoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                "infoFiltered": "(filtrado de un total de _MAX_ registros)",
                "processing": "<div class=\"loadingTable\" ><img src='" + '/' + "img/loading.gif'></div>",
                "loadingRecords": "Cargando...",
                "paginate":
                {
                    "first": "Primero",
                    "previous": "Anterior",
                    "next": "Siguiente",
                    "last": "Último"
                }
            },
            pagingType: "full_numbers",
            searching: true,
            processing: true,
            serverSide: true,
            orderCellsTop: true,
            autoWidth: true,
            deferRender: true,
            lengthMenu: [[10, 15, 20, 100], [10, 15, 20, 100]],            
            //dom: '<"row"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6 text-right"B>><"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
            dom: '<"row"<"col-sm-12 col-md-6"B><"col-sm-12 col-md-6 text-right"l>><"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
            buttons: [
                {
                    text: 'Create',
                    className: 'btn btn-sm btn-success',
                    action: function (e, dt, node, config) {
                        $('#createModal').modal('show');
                    },
                    init: function (api, node, config) {
                        $(node).removeClass('dt-button');
                    }
                },
                {
                    text: 'Excel',
                    className: 'btn btn-sm btn-outline-dark',
                    action: function (e, dt, node, config) {
                        window.location.href = "/users/getexcel";
                    },
                    init: function (api, node, config) {
                        $(node).removeClass('dt-button');
                    }
                },
                {
                    text: 'CSV',
                    className: 'btn btn-sm btn-outline-dark',
                    action: function (e, dt, node, config) {
                        window.location.href = "/users/getcsv";
                    },
                    init: function (api, node, config) {
                        $(node).removeClass('dt-button');
                    }
                },
                {
                    text: 'Imprimir',
                    extend: 'print',
                    title: "Listado de Usuarios",
                    message: "Documento Público Generado: " + moment().format('MMMM Do YYYY, h:mm:ss a'),
                    className: 'btn btn-sm btn-outline-dark',
                    autoPrint: false,
                    init: function (api, node, config) {
                        $(node).removeClass('dt-button');
                    },
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7]
                    },
                    customize: function (win) {
                        $(win.document.body)
                            .css('font-size', '8pt');

                        //.prepend(
                        //    '<img src="http://datatables.net/media/images/logo-fade.png" style="position:absolute; top:0; left:0;" />'
                        //);

                        $(win.document.body)
                            .find('table')
                            .addClass('compact')
                            .css('font-size', 'inherit');

                    },
                }
                //{
                //    text: 'CSV',
                //    className: 'btn btn-sm btn-outline-dark',
                //    action: function (e, dt, node, config) {
                //        window.location.href = "/Home/GetExcel";
                //    },
                //    init: function (api, node, config) {
                //        $(node).removeClass('dt-button');
                //    }
                //}
            ],
            ajax: {
                type: "POST",
                url: '/users/search/',                
                contentType: "application/json; charset=utf-8",
                async: true,
                headers: {
                    "XSRF-TOKEN": document.querySelector('[name="__RequestVerificationToken"]').value
                },
                data: function (data) {
                    let additionalValues = [];
                    additionalValues[0] = "Additional Parameters 1";
                    additionalValues[1] = "Additional Parameters 2";
                    data.AdditionalValues = additionalValues;
                    return JSON.stringify(data);
                }
            },
            columns: [
                {
                    data: "codigo",
                    name: "eq",
                    visible: false,
                    searchable: false,
                    orderable: false
                },
                {
                    data: "usuario",
                    name: "eq",
                },
                {
                    data: "nombres",
                    name: "eq",
                },
                {
                    data: "apellidos",
                    name: "eq",
                },
                {
                    data: "email",
                    name: "eq",
                },
                {
                    data: "fechaCreacion",
                    render: function (data, type, row) {
                        if (data)
                            return window.moment(data).format("DD/MM/YYYY");
                        else
                            return null;
                    },
                    name: "gt"
                },
                {
                    orderable: false,
                    width: 100,
                    data: "opciones",
                    render: function (data, type, row) {
                        return `<div>
                                    <button type="button" class="btn btn-sm btn-outline-dark mr-2 btnEdit" data-key="${row.codigo}"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
  <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
  <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
</svg></button>
                                    <button type="button" class="btn btn-sm btn-outline-dark btnDelete" data-key="${row.codigo}"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash" viewBox="0 0 16 16">
  <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5Zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5Zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6Z"/>
  <path d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1ZM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118ZM2.5 3h11V2h-11v1Z"/>
</svg></button>
                                </div>`;
                    }
                }
            ]
        });

        table.columns().every(function (index) {
            console.log(index);
            $('#tbl-peoples thead tr:last th:eq(' + index + ') input')
                .on('keyup',
                    function (e) {
                        console.log(e);
                        if (e.keyCode === 13) {
                            //table.column($(this).parent().index() + ':visible').search(this.value).draw();
                            table.search(this.value).draw();
                        }
                    });
        });

        $(document)
            .off('click', '#btnCreate')
            .on('click', '#btnCreate', function () {
                fetch('/users/create/',
                    {
                        method: 'POST',
                        cache: 'no-cache',
                        body: new URLSearchParams(new FormData(document.querySelector('#frmCreate')))
                    })
                    .then((response) => {
                        table.ajax.reload();
                        $('#createModal').modal('hide');
                        document.querySelector('#frmCreate').reset();
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            });

        $(document)
            .off('click', '.btnEdit')
            .on('click', '.btnEdit', function () {
                const id = $(this).attr('data-key');

                fetch(`/users/edit/${id}`,
                    {
                        method: 'GET',
                        cache: 'no-cache'
                    })
                    .then((response) => {
                        return response.text();
                    })
                    .then((result) => {
                        $('#editPartial').html(result);
                        $('#editModal').modal('show');
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            });

        $(document)
            .off('click', '#btnUpdate')
            .on('click', '#btnUpdate', function () {
                fetch('/users/edit/',
                    {
                        method: 'PUT',
                        cache: 'no-cache',
                        body: new URLSearchParams(new FormData(document.querySelector('#frmEdit')))
                    })
                    .then((response) => {
                        table.ajax.reload();
                        $('#editModal').modal('hide');
                        $('#editPartial').html('');
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            });

        $(document)
            .off('click', '.btnDelete')
            .on('click', '.btnDelete', function () {
                const id = $(this).attr('data-key');

                if (confirm('Esta seguro de eliminar el registro?')) {
                    fetch(`/users/delete/${id}`,
                        {
                            method: 'DELETE',
                            cache: 'no-cache'
                        })
                        .then((response) => {
                            table.ajax.reload();
                        })
                        .catch((error) => {
                            console.log(error);
                        });
                }
            });

        $('#btnExternalSearch').click(function () {
            var data = $('#txtExternalSearch').val();
            console.log(data);
            //table.column('0:visible').search(data).draw(); // Busqueda Independiente por Columna
            table.search(data).draw();
        });

        $('#txtExternalSearch').keyup(function (e) {
            if (e.keyCode === 13) {
                //table.column($(this).parent().index() + ':visible').search(this.value).draw();
                table.search(this.value).draw();
            }
        });

    }
});
