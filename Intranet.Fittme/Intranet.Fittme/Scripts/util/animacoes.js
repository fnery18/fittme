var carregando = $('#loading');

function habilitaLoading() {
    carregando.fadeIn(100);
}
function desabilitaLoading() {
    carregando.fadeOut(100);
}

function abreFechaFiltro(elementoTitulo, elementoDiv, classeExtra) {
    if(classeExtra == null){
        classeExtra = "";
    }
    if (elementoTitulo.data("opcao") == "abrir") {
        elementoDiv.fadeIn(500);
        elementoTitulo.data("opcao", "fechar");
        elementoTitulo.find("i").removeClass().addClass(`glyphicon ${classeExtra} glyphicon-chevron-up`);
    } else {
        elementoDiv.fadeOut(500);
        elementoTitulo.data("opcao", "abrir");
        elementoTitulo.find("i").removeClass().addClass(`glyphicon ${classeExtra} glyphicon-chevron-down`);
    }
}