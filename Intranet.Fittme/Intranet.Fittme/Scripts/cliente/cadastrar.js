$(() => {
    $(document).on("click", "#btnCadastrarCliente", function (event) {
        event.preventDefault();
        if (validaForm(true, false, false)) {
            $.post("/Intranet/CadastraCliente/", {
                "Nome": $("#txtNome").val(),
                "Email": $("#txtEmail").val(),
                "Celular": $("#txtCelular").val()
            }, function (retorno) {
                if (retorno.Sucesso) {
                    MensagemSucesso("Cliente cadastrado com sucesso!");
                    limpaCampos();
                } else {
                    MensagemErroPersonalizada(retorno.Mensagem);
                }
            })
        }
    });

});