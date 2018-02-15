$(() => {
    $(document).ready(function () {

        // Lift card and show stats on Mouseover
        $('.product-card').hover(function () {
            $(this).addClass('animate');
            $('div.carouselNext, div.carouselPrev').addClass('visible');
        }, function () {
            $(this).removeClass('animate');
            $('div.carouselNext, div.carouselPrev').removeClass('visible');
        });

        $('.view_details').click(function () {
            let codigo = $(this).data("codigo");
            $.ajax({
                beforeSend: () => {
                    habilitaLoading();
                },
                type: "GET",
                data: { "codigoProduto": codigo },
                url: "/Intranet/BuscaDetalhesProduto/",
                success: (html) => {
                    habilitaDetalhes(html);
                },
                complete: () => {
                    desabilitaLoading();
                }
            });
        });

        $('#btnLimpar').click(function (event) {
            event.preventDefault();
            $('#btnReset').trigger('click');
        });

        $('.tituloFiltro').click(function () {
            if ($(this).data("opcao") == "abrir") {
                $('.filtro').fadeIn(500);
                $(this).data("opcao", "fechar");
                $(this).find("i").removeClass().addClass("glyphicon glyphicon-chevron-up");
            } else {
                $('.filtro').fadeOut(500);
                $(this).data("opcao", "abrir");
                $(this).find("i").removeClass().addClass("glyphicon glyphicon-chevron-down");
            }
        });
    });

    $(document).on("click", '#btnVoltar', () => {
        desabilitaDetalhes();
    });
    $(document).on("click", '#btnAlterar', function () {
        alert($(this).data("codigo"));
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
            url: "/Intranet/ExcluiProduto/",
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
        url: "/Intranet/RetornaListaProdutos/",
        success: (html) => {
            $('#partialProdutos').html("");
            $('#partialProdutos').html(html);
        },
        complete: () => {
            desabilitaLoading();
        }
    });
}
