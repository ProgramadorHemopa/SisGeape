/// <reference path="../grid/datatables.min.js" />
var btnAcao = $("input[type='button']");
var formulario = $("#formCrud");

$('input').keypress(function (e) {
    var code = null;
    code = (e.keyCode ? e.keyCode : e.which);
    return (code == 13) ? false : true;
});

$('input.btn').on('click', submeterForm);

function submeterForm(event) {

    event.preventDefault()

    if (validarFormulario()) {
        var data;
        var contentType = "application/x-www-form-urlencoded";
        var processData = false;
        if (formulario.attr('enctype') == 'multipart/form-data') {
            data = new FormData(formulario.get(0));//seleciona classe form-horizontal adicionada na tag form do html
            contentType = false;
            processData = false;
        } else {
            data = formulario.serialize();
        }
        $.ajax({
            data: data,
            type: formulario.attr('method'),
            url: formulario.attr('action'),
            success: tratarRetorno
        });
    }
};

function validarFormulario() {

    btnAcao.prop("disabled", true);

    var validado = false;

    // verifica se formulario tem o método valido
    if (formulario.valid == undefined) {

        validado = true;
        btnAcao.prop("disabled", false);

    }
    else {

        validado = formulario.valid();

        if (!validado) {

            btnAcao.prop("disabled", false);
            notificaoAlerta("Não foi possível enviar o formulario, verifique se ele está preenchido corretamente");
        }
    }


    return validado;

};


function tratarRetorno(resultadoServidor) {

    if (resultadoServidor.resultado == true) {

        notificaoSucesso(resultadoServidor.msg);

        $('#minhaModal').modal('hide');




        dataTable.ajax.reload();


        btnAcao.prop("disabled", false);


    }
    else {


        notificaoAlerta(resultadoServidor.msg);

        btnAcao.prop("disabled", false);
        //  $('#minhaModal').modal('hide');

    }
}
