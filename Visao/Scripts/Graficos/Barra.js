
function CriarGraficoBarraSimples(idClassContainer, titulo, departamentos, valoresEstimados, PrefixoResultado, ano) {

    var arrayDepartamentos = departamentos.split(";");
    var arrayValsEstimados = valoresEstimados.split(";");
    var sampleData = [];

    for (var i = 0; i < arrayDepartamentos.length; i++) {
        arrayDepartamentos[i] = arrayDepartamentos[i].replace('"', '');
        arrayValsEstimados[i] = parseFloat(arrayValsEstimados[i].replace('"', ''));

        var valoresDepartamento = [];
        valoresDepartamento[0] = parseFloat(arrayValsEstimados[i]);

        sampleData[i] = { name: arrayDepartamentos[i], data: valoresDepartamento }
    }


    $(idClassContainer).highcharts({
        chart: {
            type: 'column'
        },
        title: {
            text: titulo.replace('"', '').replace('"', '')
        },
        subtitle: {
            text: ano.replace('"', '').replace('"', '')
        },
        credits: {
            enabled: false
        },
        xAxis: {
            min: 0,
            categories: ['']
        },
        yAxis: {
            min: 0,
            max: 100,
            title: {
                text: '%'
            }
        },
        legend: {
            enabled: true,
            layout: 'vertical',
            align: 'right',
            x: -30,
            verticalAlign: 'middle',
            y: 45,
            borderColor: 'white',
            borderWidth: 1
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f}%</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                borderWidth: 0,
                dataLabels: {
                    enabled: true,
                    format: '{point.y:.1f}%',
                    style: {
                        fontWeight: 'bold',
                        fontSize: '10px'
                    }
                }

            },
            series: {
                pointPadding: 0,
                groupPadding: 0.2,
            }
        },
        series: sampleData
    });
}

function CriarGraficoBarraSimplesLegendaEmbaixo(idClassContainer, titulo, departamentos, valoresEstimados, PrefixoResultado, ano) {

    var arrayDepartamentos = departamentos.split(";");
    var arrayValsEstimados = valoresEstimados.split(";");
    var sampleData = [];

    for (var i = 0; i < arrayDepartamentos.length; i++) {
        if (arrayValsEstimados[i] != '') {
            arrayDepartamentos[i] = arrayDepartamentos[i].replace('"', '');
            arrayValsEstimados[i] = parseFloat(arrayValsEstimados[i].replace('"', ''));

            var valoresDepartamento = [];
            valoresDepartamento[0] = parseFloat(arrayValsEstimados[i]);

            sampleData[i] = { name: arrayDepartamentos[i], data: valoresDepartamento }
        }
    }


    $(idClassContainer).highcharts({
        exporting: {
            sourceWidth: 960,
            sourceHeight: 600,
            filename: 'grafico_ambientalis'
        },
        chart: {
            type: 'column'
        },
        title: {
            text: titulo.replace('"', '').replace('"', '')
        },
        subtitle: {
            text: ano.replace('"', '').replace('"', '')
        },
        credits: {
            enabled: false
        },
        xAxis: {
            categories: ['']
        },
        yAxis: {
            min: 0,
            max: 100,
            title: {
                text: ''
            }
        },
        legend: {
            enabled: true,
            layout: 'vertical',
            borderColor: 'white',
            borderWidth: 1
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f}%</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                borderWidth: 0,
                pointPadding: 0.2,
                borderWidth: 1,
                dataLabels: {
                    enabled: true,
                    format: '{point.y:.1f}%',
                    style: {
                        fontWeight: 'bold',
                        fontSize: '10px'
                    }
                }
            },
            series: {
                pointPadding: 0,
                groupPadding: 0,
            }
        },
        series: sampleData
    });
}

function CriarGraficoBarraDuplasProdutividadePorResponsavel(idClassContainer, titulo, funcionarios, titulosSeries,
    percentuaisEncerradasNoPrazo, percentuaisEncerradasNoPrazoComPedidoAdiamento, percentuaisVencidas,
    valorMaxY, ano, prefixoDataLabel) {

    var arrayTitulosSeries = titulosSeries.split(";");

    var tituloEncerradasNoPrazoSemPedidoAdiamento = arrayTitulosSeries[0].replace(',', '.');
    var tituloEncerradasNoPrazoComPedidoAdiamento = arrayTitulosSeries[1].replace(',', '.');
    var tituloVencidas = arrayTitulosSeries[2].replace(',', '.');

    var arrayFuncionarios = funcionarios.split(";");
    var arrayPercentuaisEncerradasNoPrazo = percentuaisEncerradasNoPrazo.split(";");
    var arrayPercentuaisEncerradasNoPrazoComPedidoAdiamento = percentuaisEncerradasNoPrazoComPedidoAdiamento.split(";");
    var arrayPercentuaisVencidas = percentuaisVencidas.split(";");

    for (var i = 0; i < arrayFuncionarios.length; i++) {
        arrayFuncionarios[i] = arrayFuncionarios[i].replace('"', '');
        arrayPercentuaisEncerradasNoPrazo[i] = parseFloat(arrayPercentuaisEncerradasNoPrazo[i].replace('"', ''));
        arrayPercentuaisEncerradasNoPrazoComPedidoAdiamento[i] = parseFloat(arrayPercentuaisEncerradasNoPrazoComPedidoAdiamento[i].replace('"', ''));
        arrayPercentuaisVencidas[i] = parseFloat(arrayPercentuaisVencidas[i].replace('"', ''));
    }

    $(idClassContainer).highcharts({
        chart: {
            type: 'column'
        },

        title: {
            text: titulo.replace('"', '').replace('"', '')
        },
        subtitle: {
            text: ano.replace('"', '').replace('"', '')
        },
        xAxis: {
            categories: arrayFuncionarios
        },

        yAxis: {
            allowDecimals: false,
            min: 0,
            max: eval(valorMaxY.replace('"', '').replace('"', '')),
            title: {
                text: ''
            }
        },

        tooltip: {
            formatter: function () {
                return '<b>' + this.x + '</b><br/>' +
                    this.series.name + ': ' + this.y + '<br/>' +
                    'Total: ' + this.point.stackTotal;
            }
        },

        plotOptions: {
            column: {
                stacking: 'normal',
                dataLabels: {
                    enabled: true,
                    color: '#000000',
                    connectorColor: '#000000',
                    formatter: function () {
                        return this.y + prefixoDataLabel.replace('"', '').replace('"', '');
                    }
                }
            },
            series: {
                pointPadding: 0,
                groupPadding: 0,
            }
        },

        series: [{
            name: tituloEncerradasNoPrazoSemPedidoAdiamento,
            data: arrayPercentuaisEncerradasNoPrazo,
            stack: 'male',
            color: '#7adb58'
        }, {
            name: tituloEncerradasNoPrazoComPedidoAdiamento,
            data: arrayPercentuaisEncerradasNoPrazoComPedidoAdiamento,
            stack: 'male',
            color: '#eff64e'
        },{
            name: tituloVencidas,
            data: arrayPercentuaisVencidas,
            stack: 'male',
            color: '#bb3a3a'
        }]
    });
};