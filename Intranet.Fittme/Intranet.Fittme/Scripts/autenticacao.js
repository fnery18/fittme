$(function () {
    $(document).on("click", "#btnLogin", function (event) {
        event.preventDefault();
        if (validaCampos()) {
            $("#formLogin").submit();
        };
    });
});

function validaCampos() {
    let login = $(`#txtLogin`);
    let senha = $(`#txtSenha`);

    if(login.val() === ""){
        MensagemErroPersonalizada("Por favor digite um usuario");
        login.focus();
        return false;
    } else if (senha.val() === "") {
        MensagemErroPersonalizada("Por favor digite a senha");
        senha.focus();
        return false;
    };

    return true;
}