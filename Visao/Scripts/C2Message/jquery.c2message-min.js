(function (c) {
    var b = { inEffect: { opacity: "show" }, name: "", inEffectDuration: 600, stayTime: 3500, text: "", sticky: false, type: "notice", position: "top-right", closeText: "", close: null,
        buttonOK: false, textoBotaoOK: "", scriptBotaoOK: "",
        buttonCancel: false, textoBotaoCancel: "", scriptBotaoCancel: ""
    };
    var a = { init: function (d) {
        if (d) {
            c.extend(b, d)
        }
    }, showToast: function (f) {
        var g = {};
        c.extend(g, b, f);
        var j, e, d, i, h;
        j = (!c(".toast-container").length) ? c("<div></div>").addClass("toast-container").addClass("toast-position-" + g.position).appendTo("body") : c(".toast-container");
        e = c("<div></div>").addClass("toast-item-wrapper").attr('id', g.name);
        d = c("<div></div>").hide().addClass("toast-item toast-type-" + g.type).appendTo(j).html(c("<p>").append(g.text)).animate(g.inEffect, g.inEffectDuration).wrap(e);
        if (g.buttonOK)
            c("<input id='botaoOK_C2MessageBox' type='button' value='" + g.textoBotaoOK + "' onclick=" + g.scriptBotaoOK + " />").addClass("botao-OK").appendTo(d);
        if (g.buttonCancel)
            c("<input id='botaoOK_C2MessageBox' type='button' value='" + g.textoBotaoCancel + "' onclick=" + g.scriptBotaoCancel + " />").addClass("botao-Cancel").appendTo(d);
        i = c("<div></div>").addClass("toast-item-close").prependTo(d).html(g.closeText).click(function () {
            c().toastmessage("removeToast", d, g)
        });
        h = c("<div></div>").addClass("toast-item-image").addClass("toast-item-image-" + g.type).prependTo(d);
        if (navigator.userAgent.match(/MSIE 6/i)) {
            j.css({ top: document.documentElement.scrollTop })
        } if (!g.sticky) {
            setTimeout(function () {
                c().toastmessage("removeToast", d, g)
            }, g.stayTime)
        } return d
    }, showNoticeToast: function (e) {
        var d = { text: e, type: "notice" };
        return c().toastmessage("showToast", d)
    }, showSuccessToast: function (e) {
        var d = { text: e, type: "success" };
        return c().toastmessage("showToast", d)
    }, showErrorToast: function (e) {
        var d = { text: e, type: "error" };
        return c().toastmessage("showToast", d)
    }, showWarningToast: function (e) {
        var d = { text: e, type: "warning" };
        return c().toastmessage("showToast", d)
    }, removeToast: function (e, d) {
        e.animate({ opacity: "0" }, 600, function () {
            e.parent().animate({ height: "0px" }, 300, function () {
                e.parent().remove();
                $(".toast-container").remove();
            })
        });
        if (d && d.close !== null) {
            d.close()
        }
    }
    };
    c.fn.toastmessage = function (d) {
        if (a[d]) {
            return a[d].apply(this, Array.prototype.slice.call(arguments, 1))
        } else {
            if (typeof d === "object" || !d) {
                return a.init.apply(this, arguments)
            } else {
                c.error("Metodo " + d + " não existe em jQuery.C2message")
            }
        }
    }
})(jQuery);