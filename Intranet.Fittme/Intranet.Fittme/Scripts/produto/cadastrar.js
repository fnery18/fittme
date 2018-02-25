var form;
$(() => {
    $('#imgProduto').hide();

    $(document).on("click", "#btnCadastrar", (event) => {
        event.preventDefault();
        if (ValidaCampos()) {
            form.append('CodigoProduto', $('#txtCodigo').val());
            form.append('CodigoProdutoFornecedor', $('#txtCodigoFornecedor').val());
            form.append('Quantidade', $('#txtQuantidade').val());
            form.append('CodigoTipo', $('#selectTipo option:selected').val());
            form.append('CodigoFornecedor', $('#selectFornecedor option:selected').val());
            form.append('CodigoCor', $('#selectCor option:selected').val());
            form.append('Nome', $('#txtNome').val());
            form.append('PrecoCusto', $('#txtPrecoCusto').val());
            form.append('PrecoNota', $('#txtPrecoNota').val());
            form.append('PrecoVenda', $('#txtPrecoVenda').val());

            $.ajax({
                url: '/Produto/CadastraProduto/', // Url do lado server que vai receber o arquivo
                data: form,
                processData: false,
                contentType: false,
                type: 'POST',
                beforeSend: () => {
                    habilitaLoading();
                },
                success: (data) => {
                    if (data.Sucesso) {
                        MensagemSucesso("Produto cadastrado com sucesso!");
                        limpaCampos();
                    }
                    else
                        MensagemErroPersonalizada(data.Mensagem);
                },
                complete: () => {
                    desabilitaLoading();
                }
            });
        };
    });

    $('#fileProduto').change(function (event) {

        form = new FormData();
        form.append('Imagem', event.target.files[0]);
        if (event.target.files[0].type.includes("image")) {
            form = new FormData();
            form.append('Imagem', event.target.files[0]);
        } else {
            MensagemErroPersonalizada("Por favor selecione uma imagem");
            $('#fileProduto').val("");
            $('#fileProduto').focus();
        };
    });
});

function CarregaImagem() {
    var oFReader = new FileReader();
    oFReader.readAsDataURL(document.getElementById("fileProduto").files[0]);
    $('#imgProduto').hide();
    oFReader.onload = function (oFREvent) {
        $('#imgProduto').fadeIn(1000);
        $('p').hide();
        document.getElementById("imgProduto").src = oFREvent.target.result;
    };
};

function ValidaCampos() {
    var codigo = $('#txtCodigo');
    var codigoFornecedor = $('#txtCodigoFornecedor');
    var nome = $('#txtNome');
    var codigoCor = $('#selectCor option:selected');
    var tipo = $('#selectTipo option:selected');
    var fornecedor = $('#selectFornecedor option:selected');
    var imagem = $('#fileProduto');
    var precoVenda = $('#txtPrecoVenda');
    var precoCusto = $('#txtPrecoCusto');
    var precoNota = $('#txtPrecoNota');
    var quantidade = $('#txtQuantidade');

    if (codigo.val().length <= 0) {
        MensagemErroPersonalizada("Por favor digite o código do produto");
        codigo.focus();
        return false;
    } else if (codigoFornecedor.val().length <= 0) {
        MensagemErroPersonalizada("Por favor digite o codigo do produto do fornecedor");
        codigoFornecedor.focus();
        return false;
    } else if (quantidade.val().length <= 0) {
        MensagemErroPersonalizada("Por favor digite a quantidade do produto");
        quantidade.focus();
        return false;
    } else if (tipo.index() <= 0) {
        MensagemErroPersonalizada("Por favor selecione um tipo");
        $('#selectTipo').focus();
        return false;
    } else if (fornecedor.index() <= 0) {
        MensagemErroPersonalizada("Por favor selecione um fornecedor");
        $('#selectFornecedor').focus();
        return false;
    } else if (codigoCor.index() <= 0) {
        MensagemErroPersonalizada("Por favor selecione uma cor");
        $('#selectCor').focus();
        return false;
    } else if (nome.val().length <= 0) {
        MensagemErroPersonalizada("Por favor digite o nome do produto");
        nome.focus();
        return false;
    } else if (imagem[0].files[0] === undefined) {
        MensagemErroPersonalizada("Por favor selecione uma imagem");
        imagem.focus();
        return false;
    } else if (imagem[0].files[0].length <= 0) {
        MensagemErroPersonalizada("Por favor selecione uma imagem válida");
        imagem.focus();
        return false;
    } else if (precoCusto.val().length <= 0) {
        MensagemErroPersonalizada("Por favor digite o preço de custo");
        precoCusto.focus();
        return false;
    } else if (precoNota.val().length <= 0) {
        MensagemErroPersonalizada("Por favor digite o preço de nota");
        precoNota.focus();
        return false;
    } else if (precoVenda.val().length <= 0) {
        MensagemErroPersonalizada("Por favor digite o preço de venda");
        precoVenda.focus();
        return false;
    }
    return true;
}


function limpaCampos() {
    $(`#btnLimparCampos`).trigger("click");
    $('#fileProduto').val("");
    $('#imgProduto').hide();
}
