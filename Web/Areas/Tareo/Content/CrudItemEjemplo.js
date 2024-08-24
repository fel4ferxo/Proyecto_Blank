var ClaseGlobalVar = {};
(function () {
    /*
     * Variables globales
     */
    this.getRowsTable = function () {
        return 10;
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

    this.filter = {
        tipo: '',
        valor: '',
        tabla: '',
        consulta: '',
        p_inicio: 0,
        p_intervalo: ClaseGlobalVar.getRowsTable(),
    }

    this.FlagUpdate = false;
    this.totalRecords = 0;
}).apply(ClaseGlobalVar);

///////////////////////

var ClasePrincipal = {};
(function () {

    //Entidad
    var entity = {};
    var entityClass = function () {//DTO campos
        this.Id;

        this.nombre;
        this.descripcion;

        this.FECHA_CREACION;
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

    this.setEntity = function (objeto) { //DTO
        entity.Id = objeto.Id;

        entity.nombre = objeto.nombre;
        entity.descripcion = objeto.descripcion;

        entity.FECHA_CREACION = objeto.FECHA_CREACION;
        entity.RowVersion = objeto.RowVersion;
    };

    this.setDataEntity = function (objeto) {//usados en pagina
        entity.nombre = objeto.nombre;
        entity.descripcion = objeto.descripcion;
    };

    this.setNewEntity = function (objeto) { // inicializar nueva entidad
        this.setEntity(objeto);
        entity.Id = ClaseGlobalVar.getIdEmpty();
        entity.RowVersion = ClaseGlobalVar.getRowVersionEmpty();
    };
    this.getEntity = function () {
        return entity;
    };

}).apply(ClasePrincipal);


/*
 * Clases que contienen los campos a validar
 */

var ClaseValidadorItem = {};
ClaseValidadorItem["noempty"] = ["txtNombre", "txtDescripcion"];

/*
 * Clases que contienen los campos a validar al actualizar
 */

var ClaseValidadorItemUpdate = {};
ClaseValidadorItemUpdate["noempty"] = ["txtUpdNombre", "txtUpdDescripcion"];

/*
 * Funcions que interactuan con los formularios
 */

function getFormValuesInsert() {

    var objRow = new Object();

    objRow.nombre = ($('#txtNombre').val()).trim();
    objRow.descripcion = ($('#txtDescripcion').val()).trim();

    return objRow;
}

function getFormValuesUpdate() {

    var objRow = new Object();

    objRow.nombre = ($('#txtUpdNombre').val()).trim();
    objRow.descripcion = ($('#txtUpdDescripcion').val()).trim();

    return objRow;
}

function setFormValuesEdit() {

    $('#txtUpdNombre').val(ClasePrincipal.getEntity().nombre);
    $('#txtUpdDescripcion').val(ClasePrincipal.getEntity().descripcion);

    gl_clearValidationStyle(ClaseValidadorItemUpdate);

    $('#dlgUpdate').modal('show');
}

function clearFormEdit() {

    $('#txtNombre').val("");
    $('#txtDescripcion').val("");
    $('#txtUpdNombre').val("");
    $('#txtUpdDescripcion').val("");
}

var setFilters = function () {
    ClaseGlobalVar.filter.tipo = $("#dropTipoFiltro").puidropdown("getSelectedValue");
    ClaseGlobalVar.filter.valor = $('#filtroValor').val();
    ClaseGlobalVar.filter.p_intervalo = ClaseGlobalVar.getRowsTable();
};

/**
 * Funciones que interactuan con servicios web
 */

/// <summary>
/// FUNCION QUE LLAMA A UNA FUNCION DEL CONTROLADOR POR MEDIO DE AJAX Y
/// RETORNA LA RESPUESTA A LA FUNCION QUE LA INVOCO
/// </summary>

function callAjax(objRow, url, httpOp) {

    var objetoSend = JSON.stringify(objRow);
    return $.ajax({
        url: url,
        data: objetoSend,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: httpOp,
        success: function (rpta) {
            //console.log(rpta);
        },
        error: function (rpta) {
            console.log(rpta.Result);
            /*if (url === urlInsert) {
                if (typeof rpta.responseJSON.Message != 'undefined' && rpta.responseJSON.Message.includes('FluentValidation')) {
                    console.log(rpta.responseJSON.Message);
                }
                fload('hide');
            }*/
        }
    }).always(function () {

    }).fail(function () {

    });
}

$(function () {
    initFiltros();
    initDropdown();
    initValidator();
    initData();
});

/****************************************************
 * **************************************************
 * Inicializar los widgets
 * **************************************************
 ****************************************************/

var initData = function () {
    setFilters();
    initDataTable();
    initPaginator();
};

function initDropdown() {
    $('#dropTipoFiltro').puidropdown({ style: "width: 100%" });
}

var initDataTable = function () {

    $('#tblremoteeager').puidatatable({
        lazy: true,
        caption: "Configuración de ActaCiss",
        paginator: {
            rows: ClaseGlobalVar.getRowsTable(),
            totalRecords: ClaseGlobalVar.totalRecords
        },
        columns: [
            { field: 'nombre', headerText: 'Nombre', headerStyle: "width: 15%", sortable: false, filter: false },
            { field: 'descripcion', headerText: 'Descripcion', headerStyle: "width: 15%", sortable: false, filter: false },

            {
                headerText: 'Acción', headerStyle: "width: 15%", bodyClass: "text-center",
                content: function (row) {

                    var btnEdit = $("<div>", {
                        class: "btn btn-sm btn-outline-warning",
                        attr: { "onclick": "eventClickEditar(this)" }
                    }).append($("<i>", { class: "fa fa-edit" }));
                    btnEdit.data("row-data", row);

                    var btnDelete = $("<div>", {
                        class: "btn btn-sm btn-outline-danger",
                        attr: { "onclick": "eventClickOpenModalDelete(this)" }
                    }).append($("<i>", { class: "fa fa-trash" }));
                    btnDelete.data("row-data", row);


                    return [btnEdit, $('<span>&nbsp;</span>'), btnDelete];
                }
            }
        ],
        datasource: function (callback, ui) {
            var f = fvalue(ClaseGlobalVar.filter);
            var tmp = fvalue(ClaseGlobalVar.filter);
            if (f !== tmp) {
                initPaginator();
            }
            else {
                if (ClaseGlobalVar.totalRecords > 0) {
                    ClaseGlobalVar.filter.p_inicio = ui.first;
                    getPaged(this, ClaseGlobalVar.filter, callback, ui);
                }
                else {
                    callback.call(this, []);
                }
            }
        }
    });

};

var initPaginator = function () {
    fload("show");
    callAjax(ClaseGlobalVar.filter, urlGetTotalActaCis, "POST").done(function (totalRecords) {
        fload("hide");
        ClaseGlobalVar.totalRecords = totalRecords;

        $('#tblremoteeager').puidatatable('getPaginator').puipaginator('option', 'totalRecords', ClaseGlobalVar.totalRecords);
        initDataTable();
    });
};

var getPaged = function (thisTable, pagedItem, callback, ui) {
    fload("show");
    callAjax(pagedItem, urlGetListaActaCis, "POST")
        .done(function (response) {
            fload("hide");
            callback.call(thisTable, response);
        });
};

var initDropdown = function () {
    $('#dropTipoFiltro').puidropdown({
        style: "width: 100%",
        filter:true,
        data: [
            { value: "nombre", label: "Nombre" },
            { value: "descripcion", label: "Descripcion" }
        ]
    });
};

var initValidator = function () {
    gl_setInputEvents(ClaseValidadorItem);
    gl_setInputEvents(ClaseValidadorItemUpdate);
};

function initFiltros() {
    $('#filtroTable').puiaccordion({
        activeIndex: 0
    });
    $('#filtroTable').show();
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
    gl_clearValidationStyle(ClaseValidadorItem);
    clearFormEdit();
    $("#dlgInsert").modal("show");
}

function eventClickCloseModalInsert() {
    $("#dlgInsert").modal("hide");
}

var eventSaveItem = function () {
    if (gl_validateForm(ClaseValidadorItem)) {
        ClasePrincipal.setNewEntity(getFormValuesInsert());
        fload('show');
        callAjax(ClasePrincipal.getEntity(), urlInsert, "POST")
            .done(function (rpta) {
                console.log(rpta);
                //$('#tblremoteeager').puidatatable('reload');
                initPaginator();
                SaveModificacionModulo("INSERCION", rpta.Id, rpta.codigo, rpta.nombre); //Auditoria
                eventClickCloseModalInsert();
                //  clearFormEdit();                       
                fload('hide');
                Noty("success", "Operación Exitosa", "El registro se guardó correctamente.");

            }).fail(function () {
                fload('hide');
                Noty("error", "Operación Fallida", "El registro no se guardó correctamente.");
            });
    } else {
        Noty("warning", "Campos Obligatorios", "Debe llenar los campos en rojo.");
    }
};

//Actualizar 
var eventClickEditar = function ($this) {
    var data = $($this).data("row-data");
    ClasePrincipal.setEntity(data);
    clearFormEdit();
    setFormValuesEdit();
};

var eventClickCloseModalUpdate = function () {
    $('#dlgUpdate').modal('hide');
};

var eventUpdateItem = function () {
    if (gl_validateForm(ClaseValidadorItemUpdate)) {
        ClasePrincipal.setDataEntity(getFormValuesUpdate());
        fload('show');
        callAjax(ClasePrincipal.getEntity(), urlUpdate, "PUT")
            .done(function (rpta) {

                //$('#tblremoteeager').puidatatable('reload');
                initPaginator();
                eventClickCloseModalUpdate();
                fload('hide');
                Noty("success", "Operación Exitosa", "El registro se actualizó correctamente.");

            }).fail(function () {
                fload('hide');
                Noty("error", "Operación Fallida", "El registro no se actualizó correctamente.");
            });
    } else {
        Noty("warning", "Campos Obligatorios", "Debe llenar los campos en rojo.");
    }
};

//Eliminar Registro 
var eventClickOpenModalDelete = function ($this) {
    $('#dlgDelete').modal('show');
    var data = $($this).data("row-data");
    ClasePrincipal.setEntity(data);
};

var eventClickCloseModalDelete = function () {
    $('#dlgDelete').modal('hide');
};

var eventClickEliminar = function () {
    fload('show');
    callAjax(ClasePrincipal.getEntity(), urlDelete, "DELETE")
        .done(function (rpta) {
            console.log(rpta);
            // $('#tblremoteeager').puidatatable('reload');
            initPaginator();
            eventClickCloseModalDelete();
            fload('hide');
            Noty("success", "Operación Exitosa", "El registro se eliminó correctamente.");

        }).fail(function () {
            fload('hide');
            Noty("error", "Operación Fallida", "El registro no se eliminó correctamente.");
        });
}

//Buscar Registro 
var eventClickBuscar = function () {
    setFilters();
    initPaginator();
};

var eventClickRefresh = function () {
    $("#dropTipoFiltro").puidropdown("selectValue", "nombre");
    $("#filtroValor").val("");
    setFilters();
    initPaginator();
};
