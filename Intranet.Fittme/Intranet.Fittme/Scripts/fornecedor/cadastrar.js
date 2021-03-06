﻿$(function () {
    $(document).on("click", "#btnCadastrar", function (event) {
        event.preventDefault();
        if (validaForm(true, true, true)) {
            $.post("/Intranet/CadastraFornecedor/", {
                "Nome": $("#txtNome").val(),
                "Email": $("#txtEmail").val(),
                "Celular": $("#txtCelular").val()
            }, function (retorno) {
                if(retorno.Sucesso){
                    MensagemSucesso("Fornecedor cadastrado com sucesso!");
                    limpaCampos();
                } else {
                    MensagemErroPersonalizada(retorno.Mensagem);
                }
            })
        }
    });
});