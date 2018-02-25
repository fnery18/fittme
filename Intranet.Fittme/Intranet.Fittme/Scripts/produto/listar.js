$(() => {
    $(document).ready(function () {

        $(document).on('mouseenter', '.product-card', function () {
            $(this).addClass('animate');
            $('div.carouselNext, div.carouselPrev').addClass('visible');
        });
        $(document).on('mouseleave', '.product-card', function () {
            $(this).removeClass('animate');
            $('div.carouselNext, div.carouselPrev').removeClass('visible');
        });

        $(document).on('click','.view_details', function () {
            let codigo = $(this).data("codigo");
            $.ajax({
                beforeSend: () => {
                    habilitaLoading();
                },
                type: "GET",
                data: { "codigoProduto": codigo },
                url: "/Produto/BuscaDetalhesProduto/",
                success: (html) => {
                    habilitaDetalhes(html);
                },
                complete: () => {
                    desabilitaLoading();
                }
            });
        });

        $(document).on('click', '#btnLimpar', function (event) {
            event.preventDefault();
            $('#btnReset').trigger('click');
        });

        $(document).on('click', '.tituloFiltro', function () {
            abreFechaFiltro($(this), $('.filtro'));
        });
    });

    $(document).on("click", '#btnVoltar', () => {
        desabilitaDetalhes();
    });
    $(document).on("click", '#btnAlterar', function () {
        var produto = {
            CodigoProduto: $(this).data("codigo"),
            Nome: $('#txtNome').val(),
            PrecoVenda: $('#txtVenda').val(),
            PrecoCusto: $('#txtCusto').val(),
            PrecoNota: $('#txtNota').val()
        }
        
        $.ajax({
            beforeSend: () => {
                habilitaLoading();
            },
            type: "POST",
            url: "/Produto/AlteraProduto/",
            data: { "model" : produto },
            success: (data) => {
                if(data.Sucesso){
                    MensagemSucesso("Produto atualizado com sucesso!");
                    atualizaProdutos();
                } else {
                    MensagemErroPersonalizada(data.Mensagem);
                }
            },
            complete: () => {
                desabilitaLoading();
            }
        });
    });
    $(document).on("click", '#btnExcluir', function () {
        let codigo = $(this).data("codigo");
        let nome = $(this).data("nome");
        $('#modalExcluirProduto').modal();
        $('#txtProduto').text(nome);
        $('#txtCodigoProdutoDelete').val(codigo);
    });
    $(document).on("click", '#btnExcluirProduto', function () {
        let codigo = $('#txtCodigoProdutoDelete').val();

        $.ajax({
            beforeSend: () => {
                habilitaLoading();
            },
            data: { "codigoProduto": codigo },
            url: "/Produto/ExcluiProduto/",
            type: "POST",
            success: (data) => {
                if (data.Sucesso) {
                    MensagemSucesso("Produto excluído com sucesso!");
                    atualizaProdutos();
                    desabilitaDetalhes();
                } else {
                    MensagemErroPersonalizada(data.Mensagem);
                }
            },
            complete: () => {
                desabilitaLoading();
                $('#modalExcluirProduto').modal('toggle');
            }
        });
    });
});

function habilitaDetalhes(html) {
    $('#produtos').fadeOut(200);
    $('#detalhes').html(html);
    $('#detalhes').fadeIn(200);
}
function desabilitaDetalhes() {
    $('#detalhes').fadeOut(200);
    $('#produtos').fadeIn();
    $('#detalhes').html("");
}

function atualizaProdutos() {
    $.ajax({
        beforeSend: () => {
            habilitaLoading();
        },
        type: "GET",
        url: "/Produto/RetornaListaProdutos/",
        success: (html) => {
            $('#partialProdutos').html("");
            $('#partialProdutos').html(html);
        },
        complete: () => {
            desabilitaLoading();
        }
    });
}
