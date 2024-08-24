
$(function () {
    console.log("Principal ..");
    initPluginGeneral();
});

// =================================================================
// PLUGIN ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// =================================================================

var initPluginGeneral = function(){
    $('.innn-datepicker').datepicker(
        {
            format: 'dd-mm-yyyy',
            enableOnReadonly: true,
            todayHighlight: true,
        }
    ).on('changeDate', function(e) {
        $(this).datepicker('hide');
    });
}

// =================================================================
// AJAX ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// =================================================================

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
            //console.log(rpta.Result);
        },
        error: function (rpta) {
            //console.log(rpta.Result);
        }
    }).always(function () {
        //console.log();
    }).fail(function () {
        //console.log();
    });
}

// =================================================================
// URL PARAMETRO |||||||||||||||||||||||||||||||||||||||||||||||||||
// =================================================================

var GetUrlParameter = function (sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
};

// =================================================================
// ALERTA ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// =================================================================


var Noty = function(tipo, title, message){
	var icon = "check";
	if(tipo === 'danger') icon = "close";

	$.jasmineNoty({
        type: tipo,
        icon: 'fa fa-'+icon,
        title: title,
        message: message,
        container: 'floating',
        timer: 3000
    });
}

// =================================================================
// LOADING |||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// =================================================================

var fload = function(op) { 
	if (op == 'hide') { 
		$('.loader-box').hide();
	} else { 
		$('.loader-box').show();
	} 
}

// =================================================================
// SET NAME DROP |||||||||||||||||||||||||||||||||||||||||||||||||||
// =================================================================

var setNameDrop = function(idDrop, idCampo){
    var name = "";
    var listaOptions = $("select#"+idDrop+" option").map(function () { return { value: $(this).val(), label: $(this).text() }; }).get();
    listaOptions.forEach(function (o, i) {
        if (o.value == idCampo) name = o.label;
        console.log("1");
    });
    console.log("2");
    return name;
};

// =================================================================
// GET NUMBER INDEX ||||||||||||||||||||||||||||||||||||||||||||||||
// =================================================================

var getNro = function (index){
    return ClaseRegistro.pagedItem.startIndex + (index+1);
}

// =================================================================
// SET DATA DROPDOWN |||||||||||||||||||||||||||||||||||||||||||||||
// =================================================================

var setDataDropdown = function (dropCampo, lista) {
    if (lista.length > 0) $("#"+dropCampo).empty();
    $("#"+dropCampo).append($("<option>",{ attr: { value: ""}}).append("Seleccione una opción"));
    lista.forEach(function(item) {
        $("#"+dropCampo).append($("<option>",{ attr: { value: item.Id }}).append(item.nombre));
    });
};

var fvalue = function (f) {
    var v = "";
    for (var key in f) {
        v = v + ";" + f[key];
    }
    return v;
};