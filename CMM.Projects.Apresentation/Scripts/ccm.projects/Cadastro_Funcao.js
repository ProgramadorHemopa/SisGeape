$('#formFuncao').submit(function (event) {
    var data = $("#formFuncao").serialize();
    var contentType = "application/x-www-form-urlencoded";
    var processData = true;
    event.preventDefault()

    $.ajax({
        data: data,
        type: $("#formFuncao").attr('method'),
        url: $("#formFuncao").attr('action'),
        contentType: contentType,
        processData: processData,
        success: function (data) {
            if (data.resultado) {

                notificaoSucesso(data.msg);
                atualizarGrid();
              
            } else {
                notificaoAlerta("Ocorreu um problema com a sua solicitação. Verifique as seguintes informações: <br/>" + data.msg);
            }


        },
        error: function (data) {
            notificaoAlerta("Ocorreu um problema com a sua solicitação. Verifique as seguintes informações: <br/>" + data.msg);

        },
    });
});