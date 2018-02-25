var modalEditar = $(`#modalEditar`);
var modalExcluir = $(`#modalExcluir`);

$(function () {
    $(document).ready(function () {
        BuscaClientes();
        $('[data-toggle="tooltip"]').tooltip();
    });

    $(document).on("click", "a[data-opcao='excluir']", function () {
        let nome = ($(this).data("nome"));
        let codigo = $(this).data("codigo");
        $('#txtCodigo').val(codigo);
        $('#txtCliente').text(nome);
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
        if (validaForm(true, false, false)) {
            $.post("/Intranet/AlteraCliente/", {
                "Nome": $("#txtNome").val(),
                "Email": $("#txtEmail").val(),
                "Celular": $("#txtCelular").val(),
                "Codigo": $("#txtCodigo").val()
            }, function (retorno) {
                if (retorno.Sucesso) {
                    MensagemSucesso("Cliente alterado com sucesso!");
                    BuscaClientes();
                    limpaCampos();
                } else {
                    MensagemErroPersonalizada(retorno.Mensagem);
                }
                modalEditar.modal("toggle");
            });
        }

    });

    $(document).on("click", "#btnExcluir", function () {
        $.post("/Intranet/ExcluiCliente/", {
            "Codigo": $("#txtCodigo").val(),
        }, function (retorno) {
            if (retorno.Sucesso) {
                MensagemSucesso("Cliente excluido com sucesso!");
                BuscaCliente();
            } else {
                MensagemErroPersonalizada(retorno.Mensagem);
            }
            modalExcluir.modal("toggle");
        })
    });
});

function BuscaClientes() {
    $.get("/Intranet/TabelaClientes/", {}, (html) => {
        $("#partialClientes").html(html);
        $('[data-toggle="tooltip"]').tooltip();
    })
}