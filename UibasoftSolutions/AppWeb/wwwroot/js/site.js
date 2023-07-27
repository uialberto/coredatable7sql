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
                "processing": "Procesando...",
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
            lengthMenu: [[5, 10, 15, 20, 100], [5, 10, 15, 20, 100]],            
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
                    visible: true,
                    searchable: true,
                    orderable: true,
                },
                {
                    data: "usuario",
                },
                {
                    data: "nombres",
                },
                {
                    data: "apellidos",
                },
                {
                    data: "email",
                },
                {
                    data: "FechaInicio",
                    render: function (data, type, row) {
                        if (data)
                            return window.moment(data).format("DD/MM/YYYY");
                        else
                            return null;
                    },
                    name: "gt"
                },
                {
                    data: "fechaCreacion",
                    render: function (data, type, row) {
                        if (data)
                            return window.moment(data).format("DD/MM/YYYY");
                        else
                            return null;
                    }
                },
                {
                    orderable: false,
                    width: 250,
                    data: "opciones",
                    render: function (data, type, row) {
                        return `<div>
                                    <button type="button" class="btn btn-sm btn-info mr-2 btnEdit" data-key="${row.codigo}">Edit</button>
                                    <button type="button" class="btn btn-sm btn-danger btnDelete" data-key="${row.codigo}">Delete</button>
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
