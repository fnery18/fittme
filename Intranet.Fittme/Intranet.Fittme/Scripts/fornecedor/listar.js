var modalEditar = $(`#modalEditar`);
var modalExcluir = $(`#modalExcluir`);

$(function () {
    $(document).ready(function () {
        BuscaFornecedores();
        $('[data-toggle="tooltip"]').tooltip();
    });

    $(document).on("click", "a[data-opcao='excluir']", function () {
        let nome = ($(this).data("nome"));
        let codigo = $(this).data("codigo");
        $('#txtCodigo').val(codigo);
        $('#txtFornecedor').text(nome);
        modalExcluir.modal();
    });

    $(document).on("click", "a[data-opcao='editar']", function () {
        $("#txtEmail").val($(this).data("email"));
        $("#txtNome").val($(this).data("nome"));
        $("#txtCelular").val($(this).data("cel"));
        $("#txtCodigo").val($(this).data("codigo"));
        modalEditar.modal();
    });

    $(document).on("click", "#btnSalvar", function () {
        if(validaForm()){
            $.post("/Intranet/AlteraFornecedor/", {
                "Nome": $("#txtNome").val(),
                "Email": $("#txtEmail").val(),
                "Celular": $("#txtCelular").val(),
                "Codigo": $("#txtCodigo").val()
            }, function (retorno) {
                if (retorno.Sucesso) {
                    MensagemSucesso("Fornecedor alterado com sucesso!");
                    BuscaFornecedores();
                    limpaCampos();
                } else {
                    MensagemErroPersonalizada(retorno.Mensagem);
                }
                modalEditar.modal("toggle");
            });
        }
        
    });

    $(document).on("click", "#btnExcluir", function () {
        $.post("/Intranet/ExcluiFornecedor/", {
            "Codigo": $("#txtCodigo").val(),
        }, function (retorno) {
            if (retorno.Sucesso) {
                MensagemSucesso("Fornecedor excluido com sucesso!");
                BuscaFornecedores();
            } else {
                MensagemErroPersonalizada(retorno.Mensagem);
            }
            modalExcluir.modal("toggle");
        })
    });
});

function BuscaFornecedores() {
    $.get("/Intranet/TabelaFornecedores/", {}, (html) =>
    {
        $("#partialFornecedores").html(html);
        $('[data-toggle="tooltip"]').tooltip();
    })
}