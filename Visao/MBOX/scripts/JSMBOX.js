function HideMBOX() {
    $(".fundo_MBOX_Visivel").css("display", "none");
    $(".pop_MBOX_Visivel").css("display", "none");
    
}

function ShowMBOX() {
    $(".fundo_MBOX_Visivel").css("display", "block");
    $(".pop_MBOX_Visivel").css("display", "block");
}

function Show(caption,texto,img,botoes) {
    ShowMBOX();
    $(".popCaption").html(caption);
    $(".popTexto").html(texto);
    $(".popIMG").attr('src', '../MBOX/icons/' + img.toLowerCase() + '.png');
    var substr = botoes.split(';');

    for (var x = 0; x < substr.length; x++) {
        $("." + substr[x].toLowerCase()).css("display", "block");
        
    }

}

function Show(caption, texto) {
    ShowMBOX();
    $(".popCaption").html(caption);
    $(".popTexto").html(texto);
    $(".popIMG").attr('src', '../MBOX/icons/sucesso.png');

}

function Show(caption, texto, img) {
    ShowMBOX();
    $(".popCaption").html(caption);
    $(".popTexto").html(texto);
    $(".popIMG").attr('src', '../MBOX/icons/' + img.toLowerCase() + '.png');
}