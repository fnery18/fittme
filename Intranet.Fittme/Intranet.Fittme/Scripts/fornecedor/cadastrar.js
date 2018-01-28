$(function () {
    $('#txtCelular').mask('(00) Z0000-0000', {
        translation: {
            'Z': {
                pattern: /[9]/, optional: false
            }
        }
    });

    $(document).on("click", "#btnCadastrar", function (event) {
        event.preventDefault();
        if (validaForm()) {
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

function validaEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function validaForm() {
    let nome = $("#txtNome");
    let email = $("#txtEmail");
    let celular = $("#txtCelular");
    if (nome.val() == "") {
        MensagemErroPersonalizada("Por favor digite o nome do fornecedor");
        nome.focus();
        return false;
    } else if (email.val() == "") {
        MensagemErroPersonalizada("Por favor digite o e-mail do fornecedor");
        email.focus();
        return false;
    } else if (!validaEmail(email.val())) {
        MensagemErroPersonalizada("Por favor digite um e-mail valido");
        email.focus();
        return false;
    } else if (celular.val() == "") {
        MensagemErroPersonalizada("Por favor digite o celular do fornecedor");
        celular.focus();
        return false;
    }
    return true;
}

function limpaCampos() {
    let nome = $("#txtNome");
    let email = $("#txtEmail");
    let celular = $("#txtCelular");

    nome.val("");
    email.val("");
    celular.val("");
}


