/// <reference path="jQuery/js/jquery-1.7.2.min.js" />
///<reference path="jQuery/js/jquery-ui-1.8.20.custom.min.js" />

(function ($) {
    $.fn.tabC2 = function (tabs, ancora, validadorAtivo, Valor, mensagem) {

        $(tabs).children("li").hide();
        $(tabs).children("li").each(function (index) {
            if (index == 0) {
                $(this).show();
            }
        });
        $(ancora).children("li").each(function (index) {
            if (index == 0) {
                $(this).addClass("tab-escolhida");
            }
        });

        $(ancora).children("li").click(function () {
            if (validadorAtivo == "ativo") {
                validadorValor = $("#" + Valor + "").val();
                if (eval(validadorValor) > 0) {
                    $(ancora).children("li").each(function (index) {
                        $(this).removeClass("tab-escolhida");
                    });
                    $(this).addClass("tab-escolhida");
                    var numero = $(this).attr("id");
                    numero = "tabs-" + numero;
                    $(tabs).children("li").each(function (index) {
                        var comparacao = $(this).attr("id");
                        if (numero == comparacao) {
                            $(this).show();
                        } else {
                            $(this).hide();
                        }
                    });

                } else {
                    alert(mensagem, 'erro', 'direita-pe');
                }
            } else {
                $(ancora).children("li").each(function (index) {
                    $(this).removeClass("tab-escolhida");
                });
                $(this).addClass("tab-escolhida");
                var numero = $(this).attr("id");
                numero = "tabs-" + numero;
                $(tabs).children("li").each(function (index) {
                    var comparacao = $(this).attr("id");
                    if (numero == comparacao) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    }
                });
            }
        });
    }
})(jQuery);

(function ($) {
    $.fn.tabC2Position = function (tabs, ancora, validadorAtivo, Valor, mensagem, posicaoMensagem) {

        $(tabs).children("li").hide();
        $(tabs).children("li").each(function (index) {
            if (index == 0) {
                $(this).show();
            }
        });
        $(ancora).children("li").each(function (index) {
            if (index == 0) {
                $(this).addClass("tab-escolhida");
            }
        });

        $(ancora).children("li").click(function () {
            if (validadorAtivo == "ativo") {
                validadorValor = $("#" + Valor + "").val();
                if (eval(validadorValor) > 0) {
                    $(ancora).children("li").each(function (index) {
                        $(this).removeClass("tab-escolhida");
                    });
                    $(this).addClass("tab-escolhida");
                    var numero = $(this).attr("id");
                    numero = "tabs-" + numero;
                    $(tabs).children("li").each(function (index) {
                        var comparacao = $(this).attr("id");
                        if (numero == comparacao) {
                            $(this).show();
                        } else {
                            $(this).hide();
                        }
                    });

                } else {
                    alert(mensagem, 'erro', posicaoMensagem);
                }
            } else {
                $(ancora).children("li").each(function (index) {
                    $(this).removeClass("tab-escolhida");
                });
                $(this).addClass("tab-escolhida");
                var numero = $(this).attr("id");
                numero = "tabs-" + numero;
                $(tabs).children("li").each(function (index) {
                    var comparacao = $(this).attr("id");
                    if (numero == comparacao) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    }
                });
            }
        });
    }
})(jQuery);

