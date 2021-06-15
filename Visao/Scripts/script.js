/// <reference path="jQuery/js/jquery-1.7.2.min.js" />
/// <reference path="jQuery/js/jquery-ui-1.8.20.custom.min.js" />

(function ($) {
    $.fn.wizard = function () {
        var s = new Object();
        // s recebe as divs do id passado
        //s = this.html();
        s = this.find(".wizard_container");
        //Declarando var que vai receber div de botões
        var botoes = new String();
        //Pegando div dos com botões
        botoes = this.find(".wizard_botoes");

        // edita-se o html com a linha abaixo
        this.html("<div id='wizard_header2213' class='wizard_header'></div>");

        //Cria div que vai receber o conteúdo
        this.append("<div id='wizard_container2213'></div>");
        //Cria div que recebe os botões
        this.append("<div id='wizard_botoes2213'></div>");
        $("#wizard_botoes2213").html(botoes);

        //adiciona o conteudo antigo que esta armazenado em s
        $("#wizard_container2213").append(s);

        $('#wizard_container2213').children().each(function (index) {
            $(this).attr("id", "step" + index);
        });

        //Conta quantos objetos tem para se criar os passos.
        var quantidadePassos = $("#wizard_container2213").children().size();

        //Declarando String para manipulação
        var stringHeader = new String();
        var stringHeaderLeft = new String();
        var stringHeaderRight = new String();
        var stringHeaderCenter = new String();

        //String que recebe o passo esquerdo.
        stringHeaderLeft = "  <div class='wizard_header_itens fl w" + quantidadePassos + "'> " +
        "   <div class='wizard_header_itens_titulo wiz_act'></div>" +
        "   <div class='wizard_header_itens_linha linha_left'><div class='wizard_header_itens_linha_marcador marcador_atual'></div></div>" +
        "   <div class='wizard_header_itens_informacao wiz_act'></div>" +
        "   </div>";

        //String que recebe o passo direito.
        stringHeaderRight = "  <div class='wizard_header_itens fl w" + quantidadePassos + "'> " +
        "   <div class='wizard_header_itens_titulo'></div>" +
        "   <div class='wizard_header_itens_linha linha_right'><div class='wizard_header_itens_linha_marcador'></div></div>" +
        "   <div class='wizard_header_itens_informacao'></div>" +
        "   </div>";

        //String que recebe o passo do centro.
        stringHeaderCenter = "  <div class='wizard_header_itens fl w" + quantidadePassos + "'> " +
        "   <div class='wizard_header_itens_titulo'></div>" +
        "   <div class='wizard_header_itens_linha'><div class='wizard_header_itens_linha_marcador'></div></div>" +
        "   <div class='wizard_header_itens_informacao'></div>" +
        "   </div>";

        //De acordo com a quantidade de passo entra nas condições
        if (quantidadePassos == 2) {
            stringHeader = stringHeaderLeft + stringHeaderRight;
        } else {
            stringHeader = stringHeaderLeft;
            for (var i = 0; i < quantidadePassos - 2; i++) {
                stringHeader += stringHeaderCenter;
            }
            stringHeader += stringHeaderRight;
        }

        //Seta na div declarada os valores da stringHeader
        $("#wizard_header2213").html(stringHeader);



        //Declarando array de titulos.
        var listaTitulo = new Array(quantidadePassos);
        //Percorre a lista do conteúdo pegando os nomes;
        $("#wizard_container2213").children().each(function (index) {
            listaTitulo[index] = $(this).find(".wizard_container_titulo").text();
            $(this).find(".wizard_container_titulo").text("Passo " + (index + 1) + ": " + listaTitulo[index]);
            var dadosTitulo = new String();
        });


        //Percorre a lista de passos preenchendo as informações.
        $('#wizard_header2213').children().each(function (index) {
            $(this).find(".wizard_header_itens_titulo").html("Passo " + (index + 1));
            var dadosTitulo = new String();
            dadosTitulo = listaTitulo[index];
            $.trim(dadosTitulo);
            $(this).find(".wizard_header_itens_informacao").html(dadosTitulo);
        });


        $('#wizard_container2213').children().each(function (index) {
            if (index == 0) {// correto lembrar de tirar
            //if (index == 1) {
                $(this).css("display", "block");
            } else {
                $(this).css("display", "none");
            }
        });


        $('#wizard_voltar').attr("disabled", true);
        $('#wizard_voltar').removeClass("vermelho");
        $('#wizard_voltar').addClass("desabilitado");

        $('#wizard_avancar').click(function () {
            var valor = 0;
            //Verifica qual div esta sendo mostrada
            $('#wizard_container2213').children().each(function (index) {
                if ($(this).css("display") == "block") {
                    valor = index;
                }
            });

            //Mostra a próxima div
            var contador = $('#wizard_container2213').children().size();

            $('#step' + valor).css("display", "none");
            $('#step' + (valor + 1)).fadeToggle("slow", "linear");
            //$('#step' + valor).slideToggle("normal");
            //$('#step' + (valor + 1)).slideToggle("normal");
            //$('#step' + valor).css("display", "none");
            //$('#step' + (valor + 1)).css("display", "block");

            $('#wizard_header2213').children().each(function (index) {
                if (index == valor) {
                    $(this).find(".wizard_header_itens_linha").children().addClass("marcador_feito");
                }
                if (index == (valor + 1)) {
                    $(this).find(".wizard_header_itens_linha").children().removeClass("marcador_feito");
                    $(this).find(".wizard_header_itens_linha").children().addClass("marcador_atual");
                    $(this).find(".wizard_header_itens_titulo").addClass("wiz_act");
                    $(this).find(".wizard_header_itens_informacao").addClass("wiz_act");
                }
            });




            //Verifica se ao passar a div pode desabilitar o botão
            if ((valor + 2) == contador) {

                $(this).attr("disabled", true);
                $(this).removeClass("vermelho");
                $(this).addClass("desabilitado");

            }

            //ao avançar o botão de voltar é habilitado
            if ($("#wizard_voltar").attr("disabled") == "disabled") {
                $('#wizard_voltar').attr("disabled", false);
                $('#wizard_voltar').removeClass("desabilitado");
                $('#wizard_voltar').addClass("vermelho");
            }
        });

        $('#wizard_voltar').click(function () {
            var valor = 0;
            //Verifica qual div esta sendo mostrada
            $('#wizard_container2213').children().each(function (index) {
                if ($(this).css("display") == "block") {
                    valor = index;
                }
            });
            //Mostra a div anterior
            var contador2 = $('#wizard_container2213').children().size();

            $('#step' + valor).css("display", "none");
            $('#step' + (valor - 1)).fadeToggle("slow", "linear");
            //$('#step' + valor).css("display", "none");
            //$('#step' + (valor - 1)).css("display", "block");

            $('#wizard_header2213').children().each(function (index) {
                if (index == valor) {
                    $(this).find(".wizard_header_itens_linha").children().addClass("marcador_feito");

                }
                if (index == (valor - 1)) {
                    $(this).find(".wizard_header_itens_linha").children().removeClass("marcador_feito");
                    $(this).find(".wizard_header_itens_linha").children().addClass("marcador_atual");
                    $(this).find(".wizard_header_itens_titulo").addClass("wiz_act");
                    $(this).find(".wizard_header_itens_informacao").addClass("wiz_act");
                }
            });

            //Verifica se ao passar a div pode desabilitar o botão
            if ((valor + 2) == contador2) {

                $(this).attr("disabled", true);
                $(this).removeClass("vermelho");
                $(this).addClass("desabilitado");
            }

            //ao voltar o botão de avançar é habilitado
            if ($("#wizard_avancar").attr("disabled") == "disabled") {
                $('#wizard_avancar').attr("disabled", false);
                $('#wizard_avancar').removeClass("desabilitado");
                $('#wizard_avancar').addClass("vermelho");
            }
        });

        function wizardPasso(passo) {
            $('#wizard_container2213').children().each(function (index) {
                $(this).css("display", "none");
                if (index == (passo - 1)) {
                    $(this).css("display", "block");
                }
            });
        }

        
    }
































































})(jQuery);