
function getError() {
    var label = $("<label>").append("Campo obligatorio");
    var icon = $("<i>", { class: "fa fa-exclamation-circle" });
    var span = $("<span>", { class: "detail" }).append(icon, label);
    var content = $("<div>", { class: "error" }).append(span);
    return content;
}


//Validadores de input
function setInputFilter(textbox, inputFilter) {
    ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (event) {
        textbox.addEventListener(event, function () {
            if (inputFilter(this.value)) {
                this.oldValue = this.value;
                this.oldSelectionStart = this.selectionStart;
                this.oldSelectionEnd = this.selectionEnd;
            } else if (this.hasOwnProperty("oldValue")) {
                this.value = this.oldValue;
                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            }
        });
    });
}

// Install input filters.
function setearInputInteger(id) {
    setInputFilter(document.getElementById(id), function (value) {
        return /^-?\d*$/.test(value);
    });
}
function setearInputFloat(id) {
    setInputFilter(document.getElementById(id), function (value) {
        return /^-?\d*[.,]?\d*$/.test(value);
    });
}
function limpiarEstiloValidacion(id) {
    $("#" + id).removeClass("valido");
    $("#" + id).removeClass("novalido");
    if ($("#" + id).next().is('div.error'))
        //$("#" + id).next().hide();
        $("#" + id).next().remove();
}

function resaltarValido(id) {
    $("#" + id).removeClass("novalido");
    $("#" + id).addClass("valido");
    //$("#" + id).next().hide();
    $("#" + id).next().remove();
}

function resaltarNoValido(id) {
    $("#" + id).removeClass("valido");
    $("#" + id).addClass("novalido");
    //$("#" + id).next().show();
    if (!$("#" + id).next().is('div.error'))
        $("#" + id).after(getError());
}

function limpiarEstiloValidacionFecha(id) {
    $("#" + id).children("input").removeClass("valido");
    $("#" + id).children("input").removeClass("novalido");
    //$("#" + id).siblings("span").hide();
    if ($("#" + id).next().is('div.error'))
        $("#" + id).next().remove();
}

function resaltarValidoFecha(id) {
    $("#" + id).children("input").removeClass("novalido");
    $("#" + id).children("input").addClass("valido");
    $("#" + id).siblings("span").hide();

}

function resaltarNoValidoFecha(id) {
    $("#" + id).children("input").removeClass("valido");
    $("#" + id).children("input").addClass("novalido");
    //$("#" + id).siblings("span").show();
    if (!$("#" + id).next().is('div.error'))
        $("#" + id).after(getError());
}

function limpiarEstiloValidacionDropdown(id) {
    $("#" + id).parent().siblings("label").removeClass("valido");
    $("#" + id).parent().siblings("label").removeClass("novalido");
    //  $("#" + id).parent().parent().siblings("span").hide();
    if ($("#" + id).parent().parent().next().is('div.error'))
        $("#" + id).parent().parent().next().remove();
}

function resaltarValidoDropdown(id) {
    $("#" + id).parent().siblings("label").removeClass("novalido");
    $("#" + id).parent().siblings("label").addClass("valido");
    $("#" + id).parent().parent().siblings("span").hide();
}

function resaltarNoValidoDropdown(id) {
    $("#" + id).parent().siblings("label").removeClass("valido");
    $("#" + id).parent().siblings("label").addClass("novalido");
    // $("#" + id).parent().parent().siblings("span").show();
    if (!$("#" + id).parent().parent().next().is('div.error'))
        $("#" + id).parent().parent().after(getError());
}

//Validador para Busqueda - modal
function limpiarEstiloValidacionSearch(id) {
    $("#" + id).removeClass("valido");
    $("#" + id).removeClass("novalido");
    if ($("#" + id).parent().next().is('div.error'))
        $("#" + id).parent().next().remove();
}

function resaltarValidoSearch(id) {
    $("#" + id).removeClass("novalido");
    $("#" + id).addClass("valido");
    $("#" + id).parent().next().remove();
}

function resaltarNoValidoSearch(id) {
    $("#" + id).removeClass("valido");
    $("#" + id).addClass("novalido");
    if (!$("#" + id).parent().next().is('div.error'))
        $("#" + id).parent().after(getError());
}


function isInt(n) {
    //return Number(n) === n && n % 1 === 0;

    var regExpInt = new RegExp('[+]?[0-9]+');

    //return Number(n) == n && n % 1 === 0 && regExpInt.test(n);

    if (regExpInt.exec(n) == null) return false;

    return regExpInt.exec(n)[0] === n;
}

function isFloat(n) {
    //return Number(n) === n && n % 1 !== 0;

    //var regExpFloat = new RegExp('[+]?[0-9]*\.?[0-9]*');
    var regExpFloat = new RegExp('[+]?([0-9]*[.])?[0-9]+');

    //return Number(n) == n && regExpFloat.test(n);

    if (regExpFloat.exec(n) == null) return false;

    return regExpFloat.exec(n)[0] === n;
}

function isEmail(n) {

    //var regExpFloat = new RegExp('^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$');
    var regExpEmail = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    if (regExpEmail.exec(n) == null) return false;

    return regExpEmail.exec(n)[0] === n;
}

function setEventsValidadorNoEmpty(id) {
    $("#" + id).keyup(function () {
        var valor = this.value;
        if (valor.trim() != "") {
            limpiarEstiloValidacion(id);
        }
        else {
            resaltarNoValido(id);
        }
    });
    $("#" + id).focusout(function () {
        var valor = this.value;
        if (valor.trim() != "") {
            limpiarEstiloValidacion(id);
        }
        else {
            resaltarNoValido(id);
        }
    });
}

function setEventsValidadorDateNoEmpty(id) {
    $("#" + id).keyup(function () {
        var valor = $(this).children('input').val();
        if (valor.trim() != "") {
            limpiarEstiloValidacionFecha(id);
        }
        else {
            resaltarNoValidoFecha(id);
        }
    });
    $("#" + id).change(function () {
        var valor = $(this).children('input').val();
        if (valor.trim() != "") {
            limpiarEstiloValidacionFecha(id);
        }
        else {
            resaltarNoValidoFecha(id);
        }
    });
    $("#" + id).focusout(function () {
        var valor = $(this).children('input').val();
        if (valor.trim() != "") {
            limpiarEstiloValidacionFecha(id);
        }
        else {
            resaltarNoValidoFecha(id);
        }
    });
}

function setEventsValidadorInt(id) {
    $("#" + id).keyup(function () {
        var valor = this.value;

        if (valor != "" && isInt(valor)) {
            limpiarEstiloValidacion(id);
        }
        else {
            resaltarNoValido(id);
        }
    });
    $("#" + id).focusout(function () {
        var valor = this.value;
        if (valor != "" && isInt(valor)) {
            limpiarEstiloValidacion(id);
        }
        else {
            resaltarNoValido(id);
        }
    });
}

function setEventsValidadorFloat(id) {
    $("#" + id).keyup(function () {
        var valor = this.value;
        if (isFloat(valor)) {
            limpiarEstiloValidacion(id);
        }
        else {
            resaltarNoValido(id);
        }
    });
    $("#" + id).focusout(function () {
        var valor = this.value;
        if (isFloat(valor)) {
            limpiarEstiloValidacion(id);
        }
        else {
            resaltarNoValido(id);
        }
    });
}

function setEventsValidadorEmail(id) {
    $("#" + id).keyup(function () {
        var valor = this.value;
        if (valor.trim() != "" && isEmail(valor)) {
            limpiarEstiloValidacion(id);
        }
        else {
            resaltarNoValido(id);
        }
    });
    $("#" + id).focusout(function () {
        var valor = this.value;
        if (valor.trim() != "" && isEmail(valor)) {
            limpiarEstiloValidacion(id);
        }
        else {
            resaltarNoValido(id);
        }
    });
}

function setEventsValidadorUploadFile(id) {
    $("#" + id).on("change", function () {
        var valor = this.value;
        if (valor.trim() != "") {
            limpiarEstiloValidacion(id);
        }
        else {
            resaltarNoValido(id);
        }
    });
    $("#" + id).focusout(function () {
        var valor = this.value;
        if (valor.trim() != "") {
            limpiarEstiloValidacion(id);
        }
        else {
            resaltarNoValido(id);
        }
    });
}

function setEventsValidadorDropdown(id, defaultValue) {
    //agregar funcion cuando el usuario seleccione un valor
    $("#" + id).parent().parent().focusout(function () {
        var valor = $("#" + id).puidropdown('getSelectedValue');
        if (valor.trim() != defaultValue.trim()) {
            limpiarEstiloValidacionDropdown(id);
        }
        else {
            resaltarNoValidoDropdown(id);
        }
    });
}

function setEventsValidadorAutocomplete(idInput, idHidden) {
    //agregar funcion cuando el usuario seleccione un valor
    $("#" + idInput).focusout(function () {
        var valor = $("#" + idHidden).val();
        if (valor.trim() !== "" && valor.trim() !== "00000000-0000-0000-0000-000000000000") {
            limpiarEstiloValidacion(idInput);
        }
        else {
            resaltarNoValido(idInput);
        }
    });
}

function setEventsValidadorSearch(id) {
    $("#" + id).keyup(function () {
        var valor = this.value;
        if (valor.trim() != "") {
            limpiarEstiloValidacionSearch(id);
        }
        else {
            resaltarNoValidoSearch(id);
        }
    });
    $("#" + id).change(function () {
        var valor = this.value;
        if (valor.trim() != "") {
            limpiarEstiloValidacionSearch(id);
        }
        else {
            resaltarNoValidoSearch(id);
        }
    });
    $("#" + id).focusout(function () {
        var valor = this.value;
        if (valor.trim() != "") {
            limpiarEstiloValidacionSearch(id);
        }
        else {
            resaltarNoValidoSearch(id);
        }
    });
}

var gl_setInputEvents = function (clase) {

    var listaDropdown = clase["dropdown"];
    if (listaDropdown !== undefined && listaDropdown.length > 0) {
        for (var i1 = 0; i1 < listaDropdown.length; ++i1) {
            setEventsValidadorDropdown(listaDropdown[i1], "");
        }
    }
    var listaNoEmpty = clase["noempty"];
    if (listaNoEmpty !== undefined && listaNoEmpty.length > 0) {
        for (var i2 = 0; i2 < listaNoEmpty.length; ++i2) {
            setEventsValidadorNoEmpty(listaNoEmpty[i2]);
        }
    }
    var listaFecha = clase["fecha"];
    if (listaFecha !== undefined && listaFecha.length > 0) {
        for (var i3 = 0; i3 < listaFecha.length; ++i3) {
            setEventsValidadorDateNoEmpty(listaFecha[i3]);
        }
    }
    var listaInt = clase["int"];
    if (listaInt !== undefined && listaInt.length > 0) {
        for (var i4 = 0; i4 < listaInt.length; ++i4) {
            setEventsValidadorInt(listaInt[i4]);
        }
    }
    var listaFloat = clase["float"];
    if (listaFloat !== undefined && listaFloat.length > 0) {
        for (var i5 = 0; i5 < listaFloat.length; ++i5) {
            setEventsValidadorFloat(listaFloat[i5]);
        }
    }
    var listaEmail = clase["email"];
    if (listaEmail !== undefined && listaEmail.length > 0) {
        for (var i6 = 0; i6 < listaEmail.length; ++i6) {
            setEventsValidadorEmail(listaEmail[i6]);
        }
    }
    var listaUpload = clase["upload"];
    if (listaUpload !== undefined && listaUpload.length > 0) {
        for (var i7 = 0; i7 < listaUpload.length; ++i7) {
            setEventsValidadorUploadFile(listaUpload[i7]);
        }
    }
    var listaDropdownId = clase["dropdownid"];
    if (listaDropdownId !== undefined && listaDropdownId.length > 0) {
        for (var i8 = 0; i8 < listaDropdownId.length; ++i8) {
            setEventsValidadorDropdown(listaDropdownId[i8], "00000000-0000-0000-0000-000000000000");
        }
    }
    var listaAutocomplete = clase["autocomplete"];
    if (listaAutocomplete !== undefined && listaAutocomplete.length > 0) {
        for (var i9 = 0; i9 < listaAutocomplete.length; ++i9) {
            var idautocomplete = listaAutocomplete[i9].split("**");
            var idInput = idautocomplete[0];
            var idHidden = idautocomplete[1];
            setEventsValidadorAutocomplete(idInput, idHidden);
        }
    }
    var listaSearch = clase["search"];
    if (listaSearch !== undefined && listaSearch.length > 0) {
        for (var i10 = 0; i10 < listaSearch.length; ++i10) {
            setEventsValidadorSearch(listaSearch[i10]);
        }
    }
};

var gl_validateForm = function (clase) {

    var camposPorValidar = 0;
    var camposValidados = 0;

    var listaDropdown = clase["dropdown"];
    if (listaDropdown !== undefined && listaDropdown.length > 0) {
        camposPorValidar += listaDropdown.length;
        for (var i1 = 0; i1 < listaDropdown.length; ++i1) {
            var valor1 = $("#" + listaDropdown[i1]).puidropdown('getSelectedValue');
            if (valor1.trim() !== "") {
                limpiarEstiloValidacionDropdown(listaDropdown[i1]);
                camposValidados++;
            }
            else {
                resaltarNoValidoDropdown(listaDropdown[i1]);
            }
        }
    }
    var listaNoEmpty = clase["noempty"];
    if (listaNoEmpty !== undefined && listaNoEmpty.length > 0) {
        camposPorValidar += listaNoEmpty.length;
        for (var i2 = 0; i2 < listaNoEmpty.length; ++i2) {
            var valor2 = $("#" + listaNoEmpty[i2]).val();
            if (valor2.trim() !== "") {
                limpiarEstiloValidacion(listaNoEmpty[i2]);
                camposValidados++;
            }
            else {
                resaltarNoValido(listaNoEmpty[i2]);
            }
        }
    }
    var listaFecha = clase["fecha"];
    if (listaFecha !== undefined && listaFecha.length > 0) {
        camposPorValidar += listaFecha.length;
        for (var i3 = 0; i3 < listaFecha.length; ++i3) {
            var valor3 = $("#" + listaFecha[i3]).children('input').val();
            if (valor3.trim() !== "") {
                limpiarEstiloValidacionFecha(listaFecha[i3]);
                camposValidados++;
            }
            else {
                resaltarNoValidoFecha(listaFecha[i3]);
            }
        }
    }
    var listaInt = clase["int"];
    if (listaInt !== undefined && listaInt.length > 0) {
        camposPorValidar += listaInt.length;
        for (var i4 = 0; i4 < listaInt.length; ++i4) {
            var valor4 = $("#" + listaInt[i4]).val();
            if (valor4.trim() !== "" && isInt(valor4)) {
                limpiarEstiloValidacion(listaInt[i4]);
                camposValidados++;
            }
            else {
                resaltarNoValido(listaInt[i4]);
            }
        }
    }
    var listaFloat = clase["float"];
    if (listaFloat !== undefined && listaFloat.length > 0) {
        camposPorValidar += listaFloat.length;
        for (var i5 = 0; i5 < listaFloat.length; ++i5) {
            var valor5 = $("#" + listaFloat[i5]).val();
            if (isFloat(valor5)) {
                limpiarEstiloValidacion(listaFloat[i5]);
                camposValidados++;
            }
            else {
                resaltarNoValido(listaFloat[i5]);
            }
        }
    }
    var listaEmail = clase["email"];
    if (listaEmail !== undefined && listaEmail.length > 0) {
        camposPorValidar += listaEmail.length;
        for (var i6 = 0; i6 < listaEmail.length; ++i6) {
            var valor6 = $("#" + listaEmail[i6]).val();
            if (valor6.trim() !== "" && isEmail(valor6)) {
                limpiarEstiloValidacion(listaEmail[i6]);
                camposValidados++;
            }
            else {
                resaltarNoValido(listaEmail[i6]);
            }
        }
    }
    var listaUpload = clase["upload"];
    if (listaUpload !== undefined && listaUpload.length > 0) {
        camposPorValidar += listaUpload.length;
        for (var i7 = 0; i7 < listaUpload.length; ++i7) {
            var valor7 = $("#" + listaUpload[i7]).val();
            if (valor7.trim() !== "") {
                limpiarEstiloValidacion(listaUpload[i7]);
                camposValidados++;
            }
            else {
                resaltarNoValido(listaUpload[i7]);
            }
        }
    }
    var listaDropdownId = clase["dropdownid"];
    if (listaDropdownId !== undefined && listaDropdownId.length > 0) {
        camposPorValidar += listaDropdownId.length;
        for (var i8 = 0; i8 < listaDropdownId.length; ++i8) {
            var valor8 = $("#" + listaDropdownId[i8]).puidropdown('getSelectedValue');
            if (valor8.trim() !== "00000000-0000-0000-0000-000000000000") {
                limpiarEstiloValidacionDropdown(listaDropdownId[i8]);
                camposValidados++;
            }
            else {
                resaltarNoValidoDropdown(listaDropdownId[i8]);
            }
        }
    }
    var listaAutocomplete = clase["autocomplete"];
    if (listaAutocomplete !== undefined && listaAutocomplete.length > 0) {
        camposPorValidar += listaAutocomplete.length;
        for (var i9 = 0; i9 < listaAutocomplete.length; ++i9) {
            var idautocomplete = listaAutocomplete[i9].split("**");
            var idInput = idautocomplete[0];
            var idHidden = idautocomplete[1];
            var valor9 = $("#" + idHidden).val();
            if (valor9.trim() !== "" && valor9.trim() !== "00000000-0000-0000-0000-000000000000") {
                limpiarEstiloValidacion(idInput);
                camposValidados++;
            }
            else {
                resaltarNoValido(idInput);
            }
        }
    }

    var listaSearch = clase["search"];
    if (listaSearch !== undefined && listaSearch.length > 0) {
        camposPorValidar += listaSearch.length;
        for (var i10 = 0; i10 < listaSearch.length; ++i10) {
            var valor10 = $("#" + listaSearch[i10]).val();
            if (valor10.trim() !== "") {
                limpiarEstiloValidacionSearch(listaSearch[i10]);
                camposValidados++;
            }
            else {
                resaltarNoValidoSearch(listaSearch[i10]);
            }
        }
    }

    return camposPorValidar === camposValidados;
};

var gl_clearValidationStyle = function (clase) {

    var listaDropdown = clase["dropdown"];
    if (listaDropdown !== undefined && listaDropdown.length > 0) {
        for (var i1 = 0; i1 < listaDropdown.length; ++i1) {
            limpiarEstiloValidacionDropdown(listaDropdown[i1]);
        }
    }
    var listaNoEmpty = clase["noempty"];
    if (listaNoEmpty !== undefined && listaNoEmpty.length > 0) {
        for (var i2 = 0; i2 < listaNoEmpty.length; ++i2) {
            limpiarEstiloValidacion(listaNoEmpty[i2]);
        }
    }
    var listaFecha = clase["fecha"];
    if (listaFecha !== undefined && listaFecha.length > 0) {
        for (var i3 = 0; i3 < listaFecha.length; ++i3) {
            limpiarEstiloValidacionFecha(listaFecha[i3]);
        }
    }
    var listaInt = clase["int"];
    if (listaInt !== undefined && listaInt.length > 0) {
        for (var i4 = 0; i4 < listaInt.length; ++i4) {
            limpiarEstiloValidacion(listaInt[i4]);
        }
    }
    var listaFloat = clase["float"];
    if (listaFloat !== undefined && listaFloat.length > 0) {
        for (var i5 = 0; i5 < listaFloat.length; ++i5) {
            limpiarEstiloValidacion(listaFloat[i5]);
        }
    }
    var listaEmail = clase["email"];
    if (listaEmail !== undefined && listaEmail.length > 0) {
        for (var i6 = 0; i6 < listaEmail.length; ++i6) {
            limpiarEstiloValidacion(listaEmail[i6]);
        }
    }
    var listaUpload = clase["upload"];
    if (listaUpload !== undefined && listaUpload.length > 0) {
        for (var i7 = 0; i7 < listaUpload.length; ++i7) {
            limpiarEstiloValidacion(listaUpload[i7]);
        }
    }
    var listaDropdownId = clase["dropdownid"];
    if (listaDropdownId !== undefined && listaDropdownId.length > 0) {
        for (var i8 = 0; i8 < listaDropdownId.length; ++i8) {
            limpiarEstiloValidacionDropdown(listaDropdownId[i8]);
        }
    }
    var listaAutocomplete = clase["autocomplete"];
    if (listaAutocomplete !== undefined && listaAutocomplete.length > 0) {
        for (var i9 = 0; i9 < listaAutocomplete.length; ++i9) {
            var idautocomplete = listaAutocomplete[i9].split("**");
            var idInput = idautocomplete[0];
            //var idHidden = idautocomplete[1];
            limpiarEstiloValidacion(idInput);
        }
    }
    var listaSearch = clase["search"];
    if (listaSearch !== undefined && listaSearch.length > 0) {
        for (var i10 = 0; i10 < listaSearch.length; ++i10) {
            limpiarEstiloValidacionSearch(listaSearch[i10]);
        }
    }

};