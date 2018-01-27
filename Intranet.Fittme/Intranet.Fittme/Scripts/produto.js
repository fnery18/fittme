var form;
$(() => {
    $('#imgProduto').hide();

    $(document).on("click", "#btnCadastrar", (event) => {
        event.preventDefault();
        if (ValidaCampos()) {
            form.append('Codigo_Produto', $('#txtCodigo').val());
            form.append('Nome', $('#txtNome').val());
            form.append('Quantidade', $('#txtQuantidade').val());
            form.append('Codigo_Tipo', $('#selectTipo option:selected').val());
            form.append('Codigo_Fornecedor', $('#selectFornecedor option:selected').val());
            form.append('Preco', $('#txtPreco').val());

            $.ajax({
                url: '/Intranet/CadastraProduto/', // Url do lado server que vai receber o arquivo
                data: form,
                processData: false,
                contentType: false,
                type: 'POST',
                success: (data) => {
                    if (data.Sucesso)
                        MensagemSucesso("Produto cadastrado com sucesso!");
                    else 
                        MensagemErroPersonalizada(data.Mensagem);
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
    var nome = $('#txtNome');
    var tipo = $('#selectTipo option:selected');
    var fornecedor = $('#selectFornecedor option:selected');
    var imagem = $('#fileProduto');
    var preco = $('#txtPreco');
    var quantidade = $('#txtQuantidade');

    if (codigo.val().length <= 0) {
        MensagemErroPersonalizada("Por favor digite o código do produto");
        codigo.focus();
        return false;
    } else if (nome.val().length <= 0) {
        MensagemErroPersonalizada("Por favor digite o nome do produto");
        nome.focus();
        return false;
    } else if (tipo.index() <= 0) {
        MensagemErroPersonalizada("Por favor selecione um tipo");
        $('#selectTipo').focus();
        return false;
    } else if (fornecedor.index() <= 0) {
        MensagemErroPersonalizada("Por favor selecione um fornecedor");
        $('#selectFornecedor').focus();
        return false;
    } else if (imagem[0].files[0].length <= 0) {
        MensagemErroPersonalizada("Por favor selecione uma imagem válida");
        imagem.focus();
        return false;
    } else if (preco.val().length <= 0) {
        MensagemErroPersonalizada("Por favor digite o preço do produto");
        preco.focus();
        return false;
    } else if (quantidade.val().length <= 0) {
        MensagemErroPersonalizada("Por favor digite a quantidade do produto");
        quantidade.focus();
        return false;
    }
    return true;
}