function configGrid() {


    $.ajaxSetup({ cache: false });


   var dataTable = $('#grdMain').DataTable({
        "processing": "true",
        "serverSide": "true",
        "ajax": {
            "url": caminhoConsulta,
            "type": "GET",
            "datatype": "json"
        },
        "columns": colunas,

        "order": [0, "asc"],

        "dom": "Bfrtip"


    });
   

};
function abrirModal(url) {
    $("#conteudoModal").load(url,
        function () {

            $('#minhaModal').modal('show');
        });
};
