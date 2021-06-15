function CriarGraficoPizza(idClassContainer, titulosSeries, quantidades, percentuais, titulo, unidadeMedida) {
    //configurações
    Highcharts.getOptions().colors = Highcharts.map(Highcharts.getOptions().colors, function (color) {
        return {
            radialGradient: { cx: 0.5, cy: 0.3, r: 0.7 },
            stops: [
                [0, color],
                [1, Highcharts.Color(color).brighten(-0.3).get('rgb')] // darken
            ]
        };
    });

    var arrayTitulosSeries = titulosSeries.split(";");
    var arrayQuantidades = quantidades.replace(',', '.').split(";");
    var arrayPercentuais = percentuais.replace(',', '.').split(";");

    var tituloEncerradasNoPrazoSemPedidoAdiamento = arrayTitulosSeries[0].replace(',', '.');
    var quantidadeEncerradasNoPrazoSemPedidoAdiamento = arrayQuantidades[0].replace(',', '.');
    var percentualEncerradasNoPrazoSemPedidoAdiamento = parseFloat(arrayPercentuais[0].replace(',', '.'));

    var tituloEncerradasNoPrazoComPedidoAdiamento = arrayTitulosSeries[1].replace(',', '.');
    var quantidadeEncerradasNoPrazoComPedidoAdiamento = arrayQuantidades[1].replace(',', '.');
    var percentualEncerradasNoPrazoComPedidoAdiamento = parseFloat(arrayPercentuais[1].replace(',', '.'));

    var tituloVencidas = arrayTitulosSeries[2].replace(',', '.');
    var quantidadeVencidas = arrayQuantidades[2].replace(',', '.');
    var percentualVencidas = parseFloat(arrayPercentuais[2].replace(',', '.'));

    $(idClassContainer).highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false
        },
        title: {
            text: titulo
        },
        legend: {
            enabled: true,
            layout: 'vertical',
            align: 'bottom',
            x: -30,
            verticalAlign: 'middle',
            y: 45,
            borderColor: 'white',
            borderWidth: 1
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        credits: {
            enabled: false
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                showInLegend: true,
                dataLabels: {
                    enabled: true,
                    color: '#000000',
                    connectorColor: '#000000',
                    formatter: function () {
                        return '<b>' + this.point.name + ' OS\'s</b> = ' + this.y + '% ' + unidadeMedida;
                    }
                }
            }
        },
        series: [{
            type: 'pie',
            name: unidadeMedida,
            data: [
                {
                    name: tituloEncerradasNoPrazoSemPedidoAdiamento + ' - ' + quantidadeEncerradasNoPrazoSemPedidoAdiamento,
                    y: percentualEncerradasNoPrazoSemPedidoAdiamento,
                    color: '#7adb58'
                },
                {
                    name: tituloEncerradasNoPrazoComPedidoAdiamento + ' - ' + quantidadeEncerradasNoPrazoComPedidoAdiamento,
                    y: percentualEncerradasNoPrazoComPedidoAdiamento,
                    color: '#eff64e'
                },
                {
                    name: tituloVencidas + ' - ' + quantidadeVencidas,
                    y: percentualVencidas,
                    color: '#bb3a3a'
                }
            ]
        }]
    });

    // Build the chart

}

// Radialize the colors
