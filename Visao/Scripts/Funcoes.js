// Início do código de Aumentar/ Diminuir a letra
// Para usar coloque o comando: "javascript:mudaTamanho('tag_ou_id_alvo', -1);" para diminuir
// e o comando "javascript:mudaTamanho('tag_ou_id_alvo', +1);" para aumentar
var tagAlvo = new Array('p'); //pega todas as tags p//
// Especificando os possíveis tamanhos de fontes, poderia ser: x-small, small...
var tamanhos = new Array('9px', '10px', '11px', '12px', '13px', '14px', '15px');
var tamanhoInicial = 2;

function mudaTamanho(idAlvo, acao) {
    if (!document.getElementById) return
    var selecionados = null, tamanho = tamanhoInicial, i, j, tagsAlvo;
    tamanho += acao;
    if (tamanho < 0) tamanho = 0;
    if (tamanho > 6) tamanho = 6;
    tamanhoInicial = tamanho;
    if (!(selecionados = document.getElementById(idAlvo))) selecionados = document.getElementsByTagName(idAlvo)[0];

    selecionados.style.fontSize = tamanhos[tamanho];

    for (i = 0; i < tagAlvo.length; i++) {
        tagsAlvo = selecionados.getElementsByTagName(tagAlvo[i]);
        for (j = 0; j < tagsAlvo.length; j++) tagsAlvo[j].style.fontSize = tamanhos[tamanho];
    }
}


function goTop(acceleration, time) {
    acceleration = acceleration || 0.1;
    time = time || 16;
    var dx = 0;
    var dy = 0;
    var bx = 0;
    var by = 0;
    var wx = 0;
    var wy = 0;
    if (document.documentElement) {
        dx = document.documentElement.scrollLeft || 0;
        dy = document.documentElement.scrollTop || 0;
    }

    if (document.body) {
        bx = document.body.scrollLeft || 0;
        by = document.body.scrollTop || 0;
    }
    var wx = window.scrollX || 0;
    var wy = window.scrollY || 0;
    var x = Math.max(wx, Math.max(bx, dx));
    var y = Math.max(wy, Math.max(by, dy));
    var speed = 1 + acceleration;
    window.scrollTo(Math.floor(x / speed), Math.floor(y / speed));
    if (x > 0 || y > 0) {
        var invokeFunction = "goTop(" + acceleration + ", " + time + ")"
        window.setTimeout(invokeFunction, time);
    }
}

function printDiv(id, pg) {
    var oPrint, oJan;
    oPrint = window.document.getElementById(id).innerHTML;
    oJan = window.open(pg);
    oJan.document.write(oPrint);
    oJan.window.print();
    oJan.document.close();
    oJan.focus();
}

function printDiv2(id, pg) {
    var oPrint, oJan;
    oPrint = window.document.getElementById(id).innerHTML;
    oJan = window.open(pg);
    oJan.document.write(oPrint);
    oJan.window.print();
    oJan.document.close();
    oJan.focus();
}


function CarregarIFrame(id, src) {
    var frame = document.getElementById(id);
    frame.src = src;
}

///////////////////////////////////////////////////// Máscaras //////////////////////////////////////////////////

////////////////////////////////////////////////////// Exemplo de Utilização ///////////////////////////////////
/*
<input type="text" name="cep" onKeyPress="MascaraCep(form1.cep);"
maxlength="10" onBlur="ValidaCep(form1.cep)">
<br><br>DATA:
<input type="text" name="data" onKeyPress="MascaraData(form1.data);"
maxlength="10" onBlur= "ValidaDataform1.data);">
<br><br>Telefone: 
<input type="text" name="tel" onKeyPress="MascaraTelefone(form1.tel);" 
maxlength="14"  onBlur="ValidaTelefone(form1.tel);">
<br><br>CPF:
<input type="text" name="cpf" onBlur="ValidarCPF(form1.cpf);" 
onKeyPress="MascaraCPF(form1.cpf);" maxlength="14">
<br><br>CNPJ:
<input type="text" name="cnpj" onKeyPress="MascaraCNPJ(form1.cnpj);" 
maxlength="18" onBlur="ValidarCNPJ(form1.cnpj);">
*/



function MascaraCNPJ(cnpj) {
    if (mascaraInteiro(cnpj) == false) {
        event.returnValue = false;
    }
    return formataCampo(cnpj, '00.000.000/0000-00', event);
}

//adiciona mascara de cep
function MascaraCep(cep) {
    if (mascaraInteiro(cep) == false) {
        event.returnValue = false;
    }
    return formataCampo(cep, '00.000-000', event);
}

//adiciona mascara de data
function MascaraData(data) {
    if (mascaraInteiro(data) == false) {
        alert("teste");
        event.returnValue = false;
    }
    return formataCampo(data, '00/00/0000', event);
}

function MascaraHora(hora) {
    $(hora).focusout(function () { reformartarHora(this); });
    if (mascaraInteiro(hora) == false) {
        event.returnValue = false;
    }
    formataCampo(hora, '00:00:00', event);
}

function reformartarHora(hora) {
    if (hora.value.toString() != "") {
        var novoValor = "";

        while (hora.value.toString().length < 8)
            hora.value += hora.value.toString().length == 2 || hora.value.toString().length == 5 ? ":" : "0";

        var horaFormatada = hora.value.toString().substr(0, 8);
        for (i = 0; i < hora.value.split(':').length; i++) {
            if (horaFormatada.split(':')[i] != "") {
                var valor = eval(horaFormatada.split(':')[i]) > (i > 0 ? 59 : 23) ? (i > 0 ? "59" : "23") : horaFormatada.split(':')[i];
                novoValor += valor;
                novoValor += ":";
            }
        }
        hora.value = novoValor.substr(0, 8);
    } else
        hora.value = "00:00:00";

    //alert(hora.value.toString().length);

}

//adiciona mascara ao telefone
function MascaraTelefone(tel) {
    if (mascaraInteiro(tel) == false) {
        event.returnValue = false;
    }
    return formataCampo(tel, '(00) 0000-0000', event);
}

//adiciona mascara ao CPF
function MascaraCPF(cpf) {
    if (mascaraInteiro(cpf) == false) {
        event.returnValue = false;
    }
    return formataCampo(cpf, '000.000.000-00', event);
}



//////////////////////////////////////////////// Validações /////////////////////////////////////////////////////////////////
//valida telefone

function ValidaEMail(email) {
    var sEmail = $(email).val();
    var emailFilter = /^.+@.+\..{2,}$/;
    var illegalChars = /[\(\)\<\>\,\;\:\\\/\"\[\]]/

    return emailFilter.test(sEmail) || sEmail.match(illegalChars);
}

function ValidaTelefone(tel) {
    exp = /\(\d{2}\)\ \d{4}\-\d{4}/
    if (!exp.test(tel.value))
        alert('Numero de Telefone Invalido!');
}

//valida CEP
function ValidaCep(cep) {
    exp = /\d{2}\.\d{3}\-\d{3}/
    if (!exp.test(cep.value))
        alert('Numero de Cep Invalido!');
}

//valida data
function ValidaData(data) {
    exp = /\d{2}\/\d{2}\/\d{4}/
    if (!exp.test(data.value))
        alert('Data Invalida!');
}

//valida o CPF digitado
function ValidarCPF(Objcpf) {
    var cpf = Objcpf.value;
    exp = /\.|\-/g
    cpf = cpf.toString().replace(exp, "");
    var digitoDigitado = eval(cpf.charAt(9) + cpf.charAt(10));
    var soma1 = 0, soma2 = 0;
    var vlr = 11;

    for (i = 0; i < 9; i++) {
        soma1 += eval(cpf.charAt(i) * (vlr - 1));
        soma2 += eval(cpf.charAt(i) * vlr);
        vlr--;
    }
    soma1 = (((soma1 * 10) % 11) == 10 ? 0 : ((soma1 * 10) % 11));
    soma2 = (((soma2 + (2 * soma1)) * 10) % 11);

    var digitoGerado = (soma1 * 10) + soma2;
    if (digitoGerado != digitoDigitado)
        alert('CPF Invalido!');
}

//valida numero inteiro com mascara
function mascaraInteiro() {
    if (event.keyCode < 48 || event.keyCode > 57) {
        event.returnValue = false;
        return false;
    }
    return true;
}

//valida o CNPJ digitado
function ValidarCNPJ(ObjCnpj) {
    var cnpj = ObjCnpj.value;
    var valida = new Array(6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2);
    var dig1 = new Number;
    var dig2 = new Number;

    exp = /\.|\-|\//g
    cnpj = cnpj.toString().replace(exp, "");
    var digito = new Number(eval(cnpj.charAt(12) + cnpj.charAt(13)));

    for (i = 0; i < valida.length; i++) {
        dig1 += (i > 0 ? (cnpj.charAt(i - 1) * valida[i]) : 0);
        dig2 += cnpj.charAt(i) * valida[i];
    }
    dig1 = (((dig1 % 11) < 2) ? 0 : (11 - (dig1 % 11)));
    dig2 = (((dig2 % 11) < 2) ? 0 : (11 - (dig2 % 11)));

    if (((dig1 * 10) + dig2) != digito)
        alert('CNPJ Invalido!');

}

//formata de forma generica os campos
function formataCampo(campo, Mascara, evento) {
    var boleanoMascara;

    var Digitato = evento.keyCode;
    exp = /\-|\.|\/|\(|\)|\:| /g
    campoSoNumeros = campo.value.toString().replace(exp, "");

    var posicaoCampo = 0;
    var NovoValorCampo = "";
    var TamanhoMascara = campoSoNumeros.toString().length;

    if (Digitato != 8) { // backspace 
        for (i = 0; i <= TamanhoMascara; i++) {
            boleanoMascara = ((Mascara.charAt(i) == "-") ||
                             (Mascara.charAt(i) == ".") ||
                             (Mascara.charAt(i) == ":") ||
                             (Mascara.charAt(i) == "/"))
            boleanoMascara = boleanoMascara ||
                            ((Mascara.charAt(i) == "(") ||
                            (Mascara.charAt(i) == ")") ||
                            (Mascara.charAt(i) == " "))
            if (boleanoMascara) {
                NovoValorCampo += Mascara.charAt(i);
                TamanhoMascara++;
            } else {
                NovoValorCampo += campoSoNumeros.charAt(posicaoCampo);
                posicaoCampo++;
            }
        }
        campo.value = NovoValorCampo;
    }
    if (campo.value.toString().length >= Mascara.length)
        campo.value = campo.value.toString().substr(0, Mascara.length - 1);
}

function marcarDesmarcarClientes(chk) {
    var checkar = $(chk).attr('checked') == "checked" ? true : false;
    for (var i = 0; i < document.getElementsByClassName('chkSelecionarCliente').length; i++) {
        document.getElementsByClassName('chkSelecionarCliente')[i].children[0].checked = checkar;
    }
}

function marcarDesmarcarGrid(chk, classeDoCheckDasLinhas) {
    var checkar = $(chk).attr('checked') == "checked" ? true : false;
    for (var i = 0; i < document.getElementsByClassName(classeDoCheckDasLinhas).length; i++) {
        document.getElementsByClassName(classeDoCheckDasLinhas)[i].children[0].checked = checkar;
    }
}

function Center(theItem) {
    var winWidth = $(window).width();
    var winHeight = $(window).height();
    var windowCenter = winWidth / 2;
    var itemCenter = $(theItem).width() / 2;
    var theCenter = windowCenter - itemCenter;
    var windowMiddle = winHeight / 2;
    var itemMiddle = $(theItem).height() / 2;
    var theMiddle = windowMiddle - itemMiddle;
    if (winWidth > $(theItem).width()) { //horizontal
        $(theItem).css('left', theCenter);
    } else {
        $(theItem).css('left', '0');
    }
    if (winHeight > $(theItem).height()) { //vertical
        $(theItem).css('top', theMiddle);
    } else {
        $(theItem).css('top', '0');
    }
}