$(document).ready(function () {
    Highcharts.chart('pie-container', {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        title: {
            text: 'Ti le theo vung mien, 2018'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        accessibility: {
            point: {
                valueSuffix: '%'
            }
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                }
            }
        },
        series: [{
            name: 'Brands',
            data: [{
                name: 'Chrome',
                y: 60,
                color: 'red'
            }, {
                name: 'Internet Explorer',
                y: 10
            }, {
                name: 'Firefox',
                y: 10
            }, {
                name: 'Edge',
                y: 10
            },{
                name: 'Other',
                y: 10
            }]
        }]
    });
})
