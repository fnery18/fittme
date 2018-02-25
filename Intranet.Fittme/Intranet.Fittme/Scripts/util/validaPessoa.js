var nome = $("#txtNome");
var email = $("#txtEmail");
var celular = $("#txtCelular");

$(() => {
    $('#txtCelular').mask('(00) Z0000-0000', {
        translation: {
            'Z': {
                pattern: /[9]/, optional: false
            }
        }
    });
});

function validaEmail(input) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(input);
}

function validaForm(nomeRequired, emailRequired, celularRequired) {
    if(nomeRequired){
        if (nome.val() == "") {
            MensagemErroPersonalizada("Por favor digite o nome");
            nome.focus();
            return false;
        }
    }
    if(emailRequired){
        if (email.val() == "") {
            MensagemErroPersonalizada("Por favor digite o e-mail");
            email.focus();
            return false;
        } else if (!validaEmail(email.val())) {
            MensagemErroPersonalizada("Por favor digite um e-mail valido");
            email.focus();
            return false;
        }
    }
    if(celularRequired){
        if (celular.val() == "") {
            MensagemErroPersonalizada("Por favor digite o celular");
            celular.focus();
            return false;
        }
    }
    return true;
}

function limpaCampos() {
    nome.val("");
    email.val("");
    celular.val("");
}