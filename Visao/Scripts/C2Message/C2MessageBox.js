/// <reference path="jQuery/js/jquery-1.7.2.min.js" />
/// <reference path="jQuery/js/jquery-ui-1.8.20.custom.min.js" />
//1- Mensagem, 2-tipo da mensagem, 3-posição da mensagem ex. x-y, 4-buttonOK=bool, 5-textoBotaoOK=string, 6-scriptBotaoOK=script, 7-buttonCancel=bool,

function C2MessageBox(mensagem, tipo, posicao) {
    var tipoMensagem;

    if (posicao == "" || posicao == null) {
        posicao = "centro-centro";
    }
    switch (tipo) {
        case "sucesso": tipoMensagem = 'success'; break;
        case "alerta": tipoMensagem = 'warning'; break;
        case "erro": tipoMensagem = 'error'; break;
        case "noticia": tipoMensagem = 'notice'; break;
        case "pergunta": tipoMensagem = 'question'; break;
        default: tipoMensagem = 'success';
    }
    $().toastmessage('showToast', {
        text: mensagem,
        sticky: false,
        position: posicao,
        type: tipoMensagem
    });
}

function alert(mensagem, tipo, posicao) {
    var tipoMensagem;

    if (posicao == "" || posicao == null) {
        posicao = "centro-centro";
    }
    switch (tipo) {
        case "sucesso": tipoMensagem = 'success'; break;
        case "alerta": tipoMensagem = 'warning'; break;
        case "erro": tipoMensagem = 'error'; break;
        case "noticia": tipoMensagem = 'notice'; break;
        case "pergunta": tipoMensagem = 'question'; break;
        default: tipoMensagem = 'success';
    }
    $().toastmessage('showToast', {
        text: mensagem,
        sticky: false,
        position: posicao,
        type: tipoMensagem
    });
}

function removerToast() {
    $(".toast-container").remove();
}



//function confirm(msg, scriptOK, tipo, botaoOk, textoBotaoOk, botaoCancelar, botaoCancelarTexto, scriptCancelar) {
//    var retornoFuncao = false;
//    if (textoBotaoOk == "" || textoBotaoOk == null) {
//        textoBotaoOk = "Confirmar";
//    }
//    if (botaoCancelarTexto == "" || botaoCancelarTexto == null) {
//        botaoCancelarTexto = "Cancelar";
//    }
//    if (botaoOk == null) {
//        botaoOk = true;
//    }
//    if (botaoCancelar == null) {
//        botaoCancelar = true;
//    }
//    if (scriptOK == "" || scriptOK == null) {
//        scriptOK = "";
//    }
//    if (scriptCancelar == "" || scriptCancelar == null) {
//        scriptCancelar = "return false";
//    }

//    var tipoMensagem;
//    switch (tipo) {
//        case "sucesso": tipoMensagem = 'success'; break;
//        case "alerta": tipoMensagem = 'warning'; break;
//        case "erro": tipoMensagem = 'error'; break;
//        case "noticia": tipoMensagem = 'notice'; break;
//        case "pergunta": tipoMensagem = 'question'; break;
//        default: tipoMensagem = 'success';
//    }

//    $().toastmessage('showToast', {
//        text: msg,
//        name: 'C2MessageBox1',
//        sticky: true,
//        position: 'centro-centro',
//        buttonOK: botaoOk,
//        textoBotaoOK: textoBotaoOk,
//        scriptBotaoOK: 'removerToast();' + scriptOK,
//        buttonCancel: botaoCancelar,
//        textoBotaoCancel: botaoCancelarTexto,
//        scriptBotaoCancel: 'removerToast();',
//        type: tipoMensagem
//    });

//}

//    $('#confirm')
//    .jqmShow()
//    .find('p.jqmConfirmMsg')
//    .html("<span>" + msg + "</span>")
//    .end()
//    .find(':submit:visible')
//    .click(function () {
//        if (this.value == 'Sim')
//            (typeof callback == 'string') ?
//            window.location.href = callback :
//            callback;
//        $('#confirm').jqmHide();
//    });


//$().toastmessage('showToast', {
//    text: 'teste',
//    name: 'C2MessageBox1',
//    sticky: true,
//    position: 'centro-centro',
//    buttonOK: true,
//    textoBotaoOK: 'Confirmar',
//    scriptBotaoOK: '',
//    buttonCancel: true,
//    textoBotaoCancel: 'Cancelar',
//    scriptBotaoCancel: '',
//    type: 'warning'
//});




