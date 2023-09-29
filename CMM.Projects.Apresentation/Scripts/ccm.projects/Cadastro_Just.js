$('#formJust').submit(function (event) {
    event.preventDefault()
    var data = $("#formJust").serialize();
    var contentType = "application/x-www-form-urlencoded";
    var processData = true;

    $.ajax({
        data: data,
        type: $("#formJust").attr('method'),
        url: $("#formJust").attr('action'),
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
            notificaoAlerta(data.msg);

        },
    });
});