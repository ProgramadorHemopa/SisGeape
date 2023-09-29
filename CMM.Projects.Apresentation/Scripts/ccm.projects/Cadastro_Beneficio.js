$('#formBeneficio').submit(function (event) {
    event.preventDefault()
    var data = $("#formBeneficio").serialize();
    var contentType = "application/x-www-form-urlencoded";
    var processData = true;

    $.ajax({
        data: data,
        type: $("#formBeneficio").attr('method'),
        url: $("#formBeneficio").attr('action'),
        contentType: contentType,
        processData: processData,
        success: function (data) {
            if (data.resultado) {
                notificaoSucesso(data.msg);
                atualizarGrid();
              
            } else {
                notificaoAlerta(data.msg);
            }


        },
        error: function (data) {
            notificaoAlerta(data.msg);

        },
    });
});