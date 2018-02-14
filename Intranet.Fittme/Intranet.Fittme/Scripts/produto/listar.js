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
                data: { "codigoProduto" : codigo },
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
            if($(this).data("opcao") == "abrir"){
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
});

function habilitaDetalhes(html) {
    $('#produtos').fadeOut(200);
    $('#detalhes').html(html);
    $('#detalhes').fadeIn(200);
}
