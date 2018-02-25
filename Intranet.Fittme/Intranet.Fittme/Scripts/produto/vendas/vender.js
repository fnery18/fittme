$(() => {
    var codigo = $('#txtCodigo');
    var quantidade = $('#txtQuantidade');

    //FUNCOES
    var FinalizaVenda = function () {
        let codigos = $('[data-xcodigo]');
        let produtos = [];

        for (var i = 0; i < codigos.length; i++) {
            let produto = {
                "CodigoProduto": codigos[i].getAttribute("data-xcodigo"),
                "QuantidadeEscolhida": codigos[i].getAttribute("data-xquantidade")
            }
            produtos.push(produto);
        }

        if (produtos.length > 0) {
            $.ajax({
                beforeSend: () => {
                    habilitaLoading()
                },
                type: "POST",
                url: "/Produto/FinalizaVenda",
                data: { "model": produtos },
                success: (data) => {
                    if (data.Sucesso) {
                        MensagemSucesso();
                    } else {
                        MensagemErroPersonalizada(data.Mensagem);
                    }
                },
                complete: () => {
                    desabilitaLoading();
                }
            });
        } else {
            MensagemErroPersonalizada("Por favor adicione produtos para a venda.");
            codigo.focus();
        }
    }

    var ativarTooltip = function () {
        $('[data-toggle="tooltip"]').tooltip();
    }

    var contaQuantidade = function (codigoProduto) {
        let produtos = $(`[data-xcodigo='${codigoProduto}']`);
        let qtd = 0;
        for (var i = 0; i < produtos.length; i++) {
            qtd = qtd + eval(produtos[i].getAttribute("data-xquantidade"));
        }
        return qtd;
    }
    var criarDivProduto = function (produto){
        $.post("/Produto/GeraDivProduto/", { "produto": produto }, (partial) => {
            var conteudo = $('#partialProdutos').html();
            $('#partialProdutos').html(conteudo + partial);
            ativarTooltip();
        });
    }

    var ehNumero = function (valor) {
        let regex = /^[-+]?(\d+|\d+\.\d*|\d*\.\d+)$/;
        return regex.test(valor);
    }

    var validaCamposAdiciona = function () {
        if (codigo.val() == "") {
            MensagemErroPersonalizada("Por favor digite o código FITTME");
            codigo.focus();
            return false;
        } else if (quantidade.val() == "") {
            MensagemErroPersonalizada("Por favor digite a quantidade");
            quantidade.focus();
            return false;
        } else if (!ehNumero(quantidade.val())) {
            MensagemErroPersonalizada("Por favor digite apenas números");
            quantidade.val("");
            quantidade.focus();
            return false;
        }
        return true;
    }
    //FIM FUNCOES

    ativarTooltip();
    $(document).on("click", "[data-opcao]", function () {
        let elementoClicado = $(this);
        let idElemento = $(this).data("id");
        let elementoQueAbrira = $(`#${idElemento}`)
        let classeExtra = 'clicavel';
        abreFechaFiltro(elementoClicado, elementoQueAbrira, classeExtra);
    });

    $(document).on("click", "#removeProduto", function () {
        $(this).parent().addClass("slide-out-left");
        var el = $(this);
        setTimeout(function () {
            el.parent().next().remove(); // conteudo-produto
            el.parent().remove(); // titulo-produto
        }, 600);
    });

    $(document).on("click", "#btnAdicionar", function () {
        if (validaCamposAdiciona()) {
            let quantidadesJaAdicionadas = contaQuantidade(codigo.val().toUpperCase());
            let quantidadeTotal = quantidadesJaAdicionadas + eval(quantidade.val());
            $.ajax({
                beforeSend: () => {
                    habilitaLoading();
                },
                type: "GET",
                url: "/Produto/BuscaProduto/",
                data: {
                    "codigoProduto": codigo.val().toUpperCase(),
                    "quantidadeTotal": quantidadeTotal,
                    "quantidadeEscolhida": quantidade.val()
                },
                success: (data) => {
                    if (data.Sucesso)
                        criarDivProduto(data.Retorno);
                    else
                        MensagemErroPersonalizada(data.Mensagem);
                },
                complete: () => {
                    desabilitaLoading();
                }
            });
        }
    });

    $(document).on("click", "#btnVender", function (event) {
        event.preventDefault();
        FinalizaVenda();
    });
});