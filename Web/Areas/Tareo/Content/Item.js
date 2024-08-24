var ClaseGlobalVar = {};
(function () {
    /*
     * Variables globales
     */
    this.getRowsTable = function () {
        return 5;
    };
    this.getStartIndex = function () {
        return 0;
    };
    this.getIdEmpty = function () {
        return "00000000-0000-0000-0000-000000000000";
    };
    this.getRowVersionEmpty = function () {
        return "000000000000";
    };

    this.getTipoTarifa = function () {
        var list = [
            { label: "&nbsp;", value: "" },
            { label: "Ida", value: 1 },
            { label: "Retorno", value: 2 },
            { label: "Especial", value: 3 },
            { label: "Local", value: 4 }
        ];

        return list;
    };

    this.PaginaActual = 1;//declaramos en 1 la pag
    this.flagUpdate = false;//al actualizar 

}).apply(ClaseGlobalVar);

var ClasePrincipal = {};
(function () {
    /**
     * Definir la entidad
     */
    var entity = {};
    var entityClass = function () {
        this.Id;
        this.nombre;
        this.descripcion;


        this.RowVersion;
    };
    entityClass.apply(entity);

    var FilterInfo = {};
    var EntityFilterInfo = function () {
        this.Logical;
        this.PropertyName;
        this.value;
        this.Operator;
    };
    EntityFilterInfo.apply(FilterInfo);

    this.getFilterInfo = function (Logical, PropertyName, value, Operator) {
        FilterInfo.Logical = Logical;
        FilterInfo.PropertyName = PropertyName;
        FilterInfo.value = value;
        FilterInfo.Operator = Operator;
        return FilterInfo;
    }

    var OrderInfo = {};
    var EntityOrderInfo = function () {
        this.OrderType;
        this.Property;
        this.Index;
    };
    EntityOrderInfo.apply(OrderInfo);

    this.getOrderInfo = function (OrderType, Property, Index) {
        OrderInfo.OrderType = OrderType;
        OrderInfo.Property = Property;
        OrderInfo.Index = Index;
        return OrderInfo;
    }

    var PagedItem = {};
    var EntityPagedItem = function () {
        this.filtros = [];
        this.orden = [];
        this.startIndex = ClaseGlobalVar.getStartIndex();
        this.length = ClaseGlobalVar.getRowsTable();
    };
    EntityPagedItem.apply(PagedItem);

    this.getPagedItem = function (filtros, orden, startIndex, length) {
        PagedItem.filtros = filtros;
        PagedItem.orden = orden;
        PagedItem.startIndex = startIndex;
        PagedItem.length = length;

        return PagedItem;
    }

    this.getPagedItem = function () {
        return PagedItem;
    }

    this.setEntity = function (objeto) {
        //validar antes de establecer el objeto
        entity.Id = objeto.Id;
        entity.nombre = objeto.nombre;
        entity.descripcion = objeto.descripcion;


        entity.RowVersion = objeto.RowVersion;
    };
    this.setDataEntity = function (objeto) {
        //validar antes de establecer el objeto
        entity.nombre = objeto.nombre;
        entity.descripcion = objeto.descripcion;

    };
    this.setNewEntity = function (objeto) {
        //validar antes de establecer el objeto
        this.setEntity(objeto);
        entity.Id = ClaseGlobalVar.getIdEmpty();
        entity.RowVersion = ClaseGlobalVar.getRowVersionEmpty();
    };
    this.getEntity = function () {
        console.log("Get objeto" + entity);
        return entity;
    };

}).apply(ClasePrincipal);

/**
 * Funciones que interactuan con los formularios
 */
function getFormValuesInsert() {
    var objRow = new Object();
    objRow.nombre = $('#txtNombre').val();
    objRow.descripcion = $('#txtDescripcion').val();

    return objRow;
}

function getFormValuesUpdate() {
    var objRow = new Object();
    objRow.nombre = $('#txtUpdNombre').val();
    objRow.descripcion = $('#txtUpdDescripcion').val();


    return objRow;
}

function setFormValuesEdit() {
    $('#txtUpdNombre').val(ClasePrincipal.getEntity().nombre);//nombreTransportista
    $('#txtUpdDescripcion').val(ClasePrincipal.getEntity().descripcion);//puntoControlIncial

    $('#dlgUpdate').puidialog('show');
}
function clearFormEdit() {
    $('#txtNombre').val("");
    $('#txtDescripcion').val("");
    $('#txtDireccion').val("");
    $('#txtPersonaContacto').val("");
    $('#txtTelefonoContacto').val("");
    $('#txtEmailContacto').val("");

}
/**
 * Funciones que interactuan con servicios web
 */

/// <summary>
/// FUNCION QUE LLAMA A UNA FUNCION DEL CONTROLADOR POR MEDIO DE AJAX Y
/// RETORNA LA RESPUESTA A LA FUNCION QUE LA INVOCO
/// </summary>
function callAjax(objRow, url, httpOp) {
    //displayLoading(document.body);
    var objetoSend = JSON.stringify(objRow);
    return $.ajax({
        url: url,
        data: objetoSend,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: httpOp,
        success: function (rpta) {
            console.log(rpta);
        },
        error: function (rpta) {
            console.log(rpta);
        }
    }).always(function () {
        //hideLoading(document.body);
        //clearFieldsForm();
    }).fail(function () {
        //hideLoading(document.body);
        //clearFieldsForm();
    });
}

$(function () {
    initFiltros();
    initModal();
    initDropdown();
    /**
        Crear la tabla principal
    **/
    initDataTable();


});

/****************************************************
 * **************************************************
 * Inicializar los widgets
 * **************************************************
 ****************************************************/
function initDropdown() {


}

function initDataTable() {

    var rowsLength = ClaseGlobalVar.getRowsTable();
    var filters = [];

    var datasourceIncidencias = function (callback, ui, otro) {
        var pagedItem = ClasePrincipal.getPagedItem();
        pagedItem.startIndex = ui.first;

        console.log("inicioPagina", ui);
        if (ui.rows > 0 && !ClaseGlobalVar.flagUpdate) {
            ClaseGlobalVar.PaginaActual = (ui.first / ui.rows) + 1;
        }
        if (ClaseGlobalVar.flagUpdate) {
            pagedItem.startIndex = ui.rows * (ClaseGlobalVar.PaginaActual - 1);
        }

        pagedItem.length = ClaseGlobalVar.getRowsTable();//rowsLength;
        var thisTablaIncidencias = this;

        if (ui.filters != null && typeof ui.filters != 'undefined' && ui.filters.length > 0)
            ui.filters.forEach(function (entry) {
                if (entry.value != null && typeof entry.value != 'undefined' && entry.value != "") {
                    var filtroTmp = ClasePrincipal.getFilterInfo("AND", entry.field, entry.value, entry.filterMatchMode);
                    pagedItem.filtros.push(filtroTmp);
                }
            });

        fload('show');
        $.when(callAjax(pagedItem, urlGetPaged, "POST"), callAjax(pagedItem.filtros, urlCountAll, "POST"))
            .done(function (resp1, resp2) {
                callback.call(thisTablaIncidencias, resp1[0]);
                if (ui.first == 0)
                    $('#tblremoteeager').puidatatable('setTotalRecords', resp2[0]);
                if (ClaseGlobalVar.flagUpdate) {
                    ClaseGlobalVar.flagUpdate = false;
                    console.log('current', ClaseGlobalVar.PaginaActual);
                    $('#tblremoteeager').puidatatable('getPaginator').puipaginator('setPage', "" + (ClaseGlobalVar.PaginaActual - 1));
                    $($(".ui-paginator-pages")[0]).children(":contains('" + ClaseGlobalVar.PaginaActual + "')").addClass("ui-state-active");
                }
                fload('hide');
            });
    }
    //fload('show');
    $.when(callAjax(filters, urlCountAll, "POST"))
        .done(function (resp1) {

            $('#tblremoteeager').puidatatable({
                lazy: true,
                caption: "Ejemplo Item",
                paginator: {
                    rows: rowsLength,
                    totalRecords: resp1,
                },
                columns: [
                    { field: 'nombre', headerText: 'Razón social', sortable: false, filter: false },
                    { field: 'descripcion', headerText: 'RUC', sortable: false, filter: false },
                    {
                        headerText: 'Acción', headerStyle: "width: 10%", bodyClass: "text-center",
                        content: function (row) {

                            var btnDelete = $("<div>", {
                                class: "btn btn-xs btn-default",
                                attr: { "onclick": "eventClickOpenModalDelete(this)" }
                            }).append($("<i>", { class: "fa fa-trash" }));
                            btnDelete.data("row-data", row);

                            var btnEdit = $("<div>", {
                                class: "btn btn-xs btn-default",
                                attr: { "onclick": "eventClickEditar(this)" }
                            }).append($("<i>", { class: "fa fa-edit" }));
                            btnEdit.data("row-data", row);

                            return [btnEdit, $('<span>&nbsp;</span>'), btnDelete];
                        }
                    }

                ],
                datasource: datasourceIncidencias,
                selectionMode: 'single'
            });
            //fload('hide');
        });
}

function initModal() {
    $('#dlgInsert').puidialog({
        showEffect: 'fade',
        hideEffect: 'fade',
        responsive: true,
        width: 800,
        modal: true,
        buttons: [{
            text: 'Guardar',
            icon: 'fa-save',
            click: eventSaveItem
        },
        {
            text: 'Cancelar',
            icon: 'fa-close',
            click: eventClickCloseModalInsert
        }
        ],
        beforeHide: function (event) {
            clearFormEdit();
        }
    });

    $('#dlgUpdate').puidialog({
        showEffect: 'fade',
        hideEffect: 'fade',
        responsive: true,
        width: 800,
        modal: true,
        buttons: [{
            text: 'Guardar',
            icon: 'fa-save',
            click: eventUpdateItem
        },
        {
            text: 'Cancelar',
            icon: 'fa-close',
            click: eventClickCloseModalUpdate
        }
        ],
        beforeHide: function (event) {
            clearFormEdit();
        }
    });
    $('#dlgDelete').puidialog({
        showEffect: 'fade',
        hideEffect: 'fade',
        responsive: true,
        width: 400,
        modal: true,
        buttons: [{
            text: 'Confirmar',
            icon: 'fa-save',
            click: eventClickEliminar
        },
        {
            text: 'Cancelar',
            icon: 'fa-close',
            click: eventClickCloseModalDelete
        }
        ],
        beforeHide: function (event) {
            clearFormEdit();
        }
    });

    $("#modalContent").show();
}

function initFiltros() {
    $('#default').puiaccordion({
        activeIndex: -1
    });
}

/****************************************************
 * **************************************************
 * Eventos
 * **************************************************
 ****************************************************/
// Cuando se lanza un evento(Se produce una interaccion) puede pasar las siguientes cosas
// - Se modifica la pagina
// - Se hace una peticion ajax
// - Se modifica el estado de los datos (Logica de negocio)

function eventClickOpenModalInsert() {
    $('#dlgInsert').puidialog('show');
}
function eventClickCloseModalInsert() {
    $('#dlgInsert').puidialog('hide');
}

function eventClickOpenModalUpdate() {
    $('#dlgUpdate').puidialog('show');
}
function eventClickCloseModalUpdate() {
    $('#dlgUpdate').puidialog('hide');
}

function eventClickOpenModalDelete($this) {
    $('#dlgDelete').puidialog('show');
    var data = $($this).data("row-data");
    ClasePrincipal.setEntity(data);
}
function eventClickCloseModalDelete() {
    $('#dlgDelete').puidialog('hide');
}

function eventClickEliminar() {
    fload('show');
    callAjax(ClasePrincipal.getEntity(), urlDelete, "DELETE")
        .done(function (rpta) {
            console.log(rpta);
            $('#tblremoteeager').puidatatable('reload');
            fload('hide');
        });
    eventClickCloseModalDelete();
}

function eventClickEditar($this) {

    var data = $($this).data("row-data");

    ClasePrincipal.setEntity(data);
    setFormValuesEdit();
}


function eventSaveItem() {
    ClasePrincipal.setNewEntity(getFormValuesInsert());
    fload('show');
    callAjax(ClasePrincipal.getEntity(), urlInsert, "POST")
        .done(function (rpta) {
            console.log(rpta);
            $('#tblremoteeager').puidatatable('reload');
            fload('hide');
        });
    eventClickCloseModalInsert();
}

function eventUpdateItem() {
    ClasePrincipal.setDataEntity(getFormValuesUpdate());
    fload('show');
    callAjax(ClasePrincipal.getEntity(), urlUpdate, "PUT")
        .done(function (rpta) {
            console.log(rpta);

            ClaseGlobalVar.flagUpdate = true;

            $('#tblremoteeager').puidatatable('reload');
            fload('hide');
        });
    eventClickCloseModalUpdate();
}

function eventFilterItems() {
    var nombre = $("#filtroNombre").val();
    var descripcion = $("#filtroDescripcion").val();

    //Ocultas los filtros
    $('#default').puiaccordion('select', -10);

    var filtroNombre = $.extend(true, {}, ClasePrincipal.getFilterInfo("AND", "nombre", nombre, "Contains"));
    var filtroDescripcion = $.extend(true, {}, ClasePrincipal.getFilterInfo("AND", "descripcion", descripcion, "Contains"));

    var filtroTmp = new Object();
    filtroTmp["filtroNombre"] = filtroNombre;
    filtroTmp["filtroDescripcion"] = filtroDescripcion;

    initDataTable2(filtroTmp);
}

function initDataTable2(filtro) {

    var rowsLength = ClaseGlobalVar.getRowsTable();
    var filters = [];

    var datasourceIncidencias = function (callback, ui, otro) {
        var pagedItem = ClasePrincipal.getPagedItem();
        pagedItem.filtros = [];
        pagedItem.startIndex = ui.first;
        pagedItem.length = ClaseGlobalVar.getRowsTable();//rowsLength;
        var thisTablaIncidencias = this;

        if (ui.filters != null && typeof ui.filters != 'undefined' && ui.filters.length > 0)
            ui.filters.forEach(function (entry) {
                if (entry.value != null && typeof entry.value != 'undefined' && entry.value != "") {
                    var filtroTmp = ClasePrincipal.getFilterInfo("AND", entry.field, entry.value, entry.filterMatchMode);
                    pagedItem.filtros.push(filtroTmp);
                }
            });

        if (filtro != null) {
            filters.push(filtro.filtroNombre);
            filters.push(filtro.filtroDescripcion);
            pagedItem.filtros.push(filtro.filtroNombre);
            pagedItem.filtros.push(filtro.filtroDescripcion);
        }

        fload('show');
        $.when(callAjax(pagedItem, urlGetPaged, "POST"), callAjax(pagedItem.filtros, urlCountAll, "POST"))
            .done(function (resp1, resp2) {
                callback.call(thisTablaIncidencias, resp1[0]);
                if (ui.first == 0)
                    $('#tblremoteeager').puidatatable('setTotalRecords', resp2[0]);
                fload('hide');
            });
    }
    fload('show');
    $.when(callAjax(filters, urlCountAll, "POST"))
        .done(function (resp1) {

            $('#tblremoteeager').puidatatable({
                lazy: true,
                caption: "Directorio",
                paginator: {
                    rows: rowsLength,
                    totalRecords: resp1,
                },
                columns: [
                    { field: 'nombre', headerText: 'Razón social', sortable: false, filter: false },
                    { field: 'descripcion', headerText: 'RUC', sortable: false, filter: false },
                    {
                        headerText: 'Acción', headerStyle: "width: 10%", bodyClass: "text-center",
                        content: function (row) {

                            var btnDelete = $("<div>", {
                                class: "btn btn-xs btn-default",
                                attr: { "onclick": "eventClickOpenModalDelete(this)" }
                            }).append($("<i>", { class: "fa fa-trash" }));
                            btnDelete.data("row-data", row);

                            var btnEdit = $("<div>", {
                                class: "btn btn-xs btn-default",
                                attr: { "onclick": "eventClickEditar(this)" }
                            }).append($("<i>", { class: "fa fa-edit" }));
                            btnEdit.data("row-data", row);

                            return [btnEdit, $('<span>&nbsp;</span>'), btnDelete];
                        }
                    }

                ],
                datasource: datasourceIncidencias,
                selectionMode: 'single'
            });
            fload('hide');
        });
}