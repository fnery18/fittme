var modais = {
    "Cor": {
        "Excluir": {
            "Form": $("#modalExcluirCor"),
            "Codigo": $("#txtCodigoCorDelete")
        },
        "Editar": {
            "Form": $("#modalEditarCor"),
            "Codigo": $("#txtCodigoCor")
        },
        "TituloEditar": $("#tituloModalCor"),
        "TituloExcluir": $("#txtCorNome")
    },
    "Tipo": {
        "Excluir": {
            "Form": $("#modalExcluirTipo"),
            "Codigo": $("#txtCodigoTipoDelete")
        },
        "Editar": {
            "Form": $("#modalEditarTipo"),
            "Codigo": $("#txtCodigoTipo")
        },
        "TituloEditar": $("#tituloModalTipo"),
        "TituloExcluir": $("#txtTipo")
    }
};
$(function () {
    $(document).ready(function () {
        BuscaPropriedades();
        $('[data-toggle="tooltip"]').tooltip();
    });

    //COR
    $(document).on("click", "#btnExcluirCor", function () {
        $.post("/Intranet/ExcluiCor/", { "Codigo": modais.Cor.Excluir.Codigo.val() }, function (data) {
            if (data.Sucesso) {
                MensagemSucesso("Tipo excluido com sucesso!");
                BuscaCores();
            } else {
                MensagemErroPersonalizada(data.Mensagem);
            }
            modais.Cor.Excluir.Form.modal("toggle");
        });
    });
    $(document).on("click", "#btnSalvarCor", function () {
        let caminho = "";
        let parametros = {
            "Nome": $("#txtNomeCor").val(),
            "Codigo": 0,
            "Codigo_Cor": $("#txtCodigo_Cor").val(),
            "Cor" : $("#txtCor").val()
        }

       

        if (modais.Cor.Editar.Codigo.val() == "") {
            caminho = "/Intranet/CadastraCor/";
        } else {
            caminho = "/Intranet/AlteraCor/"
            parametros.Codigo = modais.Cor.Editar.Codigo.val();
        }

        if (validaCores()) {
            $.post(caminho, parametros, function (data) {
                if (data.Sucesso) {
                    MensagemSucesso("Cor cadastrada com sucesso!");
                    BuscaCores();
                } else {
                    MensagemErroPersonalizada(data.Mensagem);
                }
                modais.Cor.Editar.Form.modal("toggle");
            })
        }
    });
    $(document).on("click", "a[data-opcao='excluirCor']", function () {
        modais.Cor.Excluir.Codigo.val($(this).data("codigo"));
        modais.Cor.TituloExcluir.text($(this).data("nome"));
        modais.Cor.Excluir.Form.modal();
    });

    $(document).on("click", "a[data-opcao='editarCor']", function () {
        modais.Cor.TituloEditar.text("Editar Tipo");
        $("#txtNomeCor").val($(this).data("nome"));
        $("#txtCodigo_Cor").val($(this).data("codigocor"));
        $("#txtCor").val($(this).data("cor"));
        modais.Cor.Editar.Codigo.val($(this).data("codigo"));
        modais.Cor.Editar.Form.modal();
    });


    $(document).on("click", "a[data-opcao='cadastrarCor']", function () {
        modais.Cor.TituloEditar.text("Cadastrar Cor");
        $("#txtNomeCor").val("");
        $("#txtCodigo_Cor").val("");
        $("#txtCor").val("");
        modais.Cor.Editar.Codigo.val("");
        modais.Cor.Editar.Form.modal();
    });

    //TIPO
    
    $(document).on("click", "#btnExcluirTipo", function () {
        $.post("/Intranet/ExcluiTipo/", { "Codigo": modais.Tipo.Excluir.Codigo.val() }, function (data) {
            if (data.Sucesso) {
                MensagemSucesso("Tipo excluido com sucesso!");
                BuscaTipos();
            } else {
                MensagemErroPersonalizada(data.Mensagem);
            }
            modais.Tipo.Excluir.Form.modal("toggle");
        });
    });
    $(document).on("click", "#btnSalvarTipo", function () {
        let caminho = "";
        let parametros = {
            "Nome": $("#txtNomeTipo").val(),
            "Codigo": 0
        }
        if (modais.Tipo.Editar.Codigo.val() == "") {
            caminho = "/Intranet/CadastraTipo/";
        } else {
            caminho = "/Intranet/AlteraTipo/"
            parametros.Codigo = modais.Tipo.Editar.Codigo.val();
        }

        if (validaTipos()) {
            $.post(caminho, parametros, function (data) {
                if (data.Sucesso) {
                    MensagemSucesso("Tipo cadastrado com sucesso!");
                    BuscaTipos();
                } else {
                    MensagemErroPersonalizada(data.Mensagem);
                }
                modais.Tipo.Editar.Form.modal("toggle");
            })
        }

    });
    $(document).on("click", "a[data-opcao='excluirTipo']", function () {
        modais.Tipo.Excluir.Codigo.val($(this).data("codigo"));
        modais.Tipo.TituloExcluir.text($(this).data("nome"));
        modais.Tipo.Excluir.Form.modal();
    });

    $(document).on("click", "a[data-opcao='editarTipo']", function () {
        modais.Tipo.TituloEditar.text("Editar Tipo");
        $("#txtNomeTipo").val($(this).data("nome"));
        modais.Tipo.Editar.Codigo.val($(this).data("codigo"));
        modais.Tipo.Editar.Form.modal();
    });

    $(document).on("click", "a[data-opcao='cadastrarTipo']", function () {
        modais.Tipo.TituloEditar.text("Cadastrar Tipo");
        $("#txtNomeTipo").val("");
        modais.Tipo.Editar.Codigo.val("");
        modais.Tipo.Editar.Form.modal();
    });
});

function BuscaPropriedades() {
    BuscaCores();
    BuscaTipos();
    $('[data-toggle="tooltip"]').tooltip();
};
function BuscaCores() {
    $.get("/Intranet/TabelaCores/", {}, (html) => {
        $("#partialCores").html(html);
        $('[data-toggle="tooltip"]').tooltip();
    });
};
function BuscaTipos() {
    $.get("/Intranet/TabelaTipos/", {}, (html) => {
        $("#partialTipos").html(html);
        $('[data-toggle="tooltip"]').tooltip();
    });
};


function validaTipos() {
    let nome = $("#txtNomeTipo");
    if (nome.val() == "") {
        MensagemErroPersonalizada("Por favor digite o tamanho");
        nome.focus();
        return false;
    }
    return true;
};

function validaCores() {
    let nome = $("#txtNomeCor");
    let codigoCor = $("#txtCodigo_Cor");
    let cor = $("#txtCor");
    if (nome.val() == "") {
        MensagemErroPersonalizada("Por favor digite o nome");
        nome.focus();
        return false;
    } else if (codigoCor.val() == "") {
        MensagemErroPersonalizada("Por favor digite o codigo da cor");
        codigoCor.focus();
        return false;
    } else if (cor.val() == "") {
        MensagemErroPersonalizada("Por favor digite o hexadecimal da cor");
        cor.focus();
        return false;
    }

    return true;
};