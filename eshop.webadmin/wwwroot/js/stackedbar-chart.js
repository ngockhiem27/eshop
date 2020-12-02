$(document).ready(function () {

    var colorStack = ['black', 'blue', 'green', 'purple', 'yellow', 'aqua', 'orange', 'red'];

    var dataOrigin = [
        {
            area: 'g1',
            visible: true,
            svData: [{ name: 'sv1', data: [5, 3, 4, 2, 2] }, { name: 'sv2', data: [3, 3, 4, 3, 2] }, { name: 'sv3', data: [3, 2, 4, 3, 2] }]
        },
        {
            area: 'g2',
            visible: true,
            svData: [{ name: 'sv1', data: [2, 3, 2, 3, 2] }, { name: 'sv2', data: [2, 1, 3, 1, 2] }, { name: 'sv3', data: [1, 3, 4, 2, 3] }]
        }];

    var dataOrigin2 = [
        {
            filter: 'center',
            visible: true,
            svData: [{ name: 'c1', data: [5, 3, 4, 2, 2] }, { name: 'c2', data: [3, 3, 4, 3, 2] }, { name: 'c3', data: [3, 2, 4, 3, 2] }, { name: 'c4', data: [3, 2, 4, 3, 2] }, { name: 'c5', data: [3, 2, 4, 3, 2] }]
        },
        {
            filter: 'service',
            visible: true,
            svData: [{ name: 'sv1', data: [1, 2, 2, 1, 2] }, { name: 'sv2', data: [1, 2, 1, 1, 2] }, { name: 'sv3', data: [1, 2, 1, 2, 3] }, { name: 'sv4', data: [2, 3, 2, 1, 3] }]
        },
        {
            filter: 'serviceGroup',
            visible: true,
            svData: [{ name: 'sg1', data: [2, 3, 2, 3, 2] }, { name: 'sg2', data: [2, 1, 3, 1, 2] }, { name: 'sg3', data: [1, 3, 4, 2, 3] }]
        }];

    var stackedbarChart = Highcharts.chart('stackedbar-container', {
        chart: {
            type: 'bar'
        },
        title: {
            text: 'Tang truong thue bao'
        },
        xAxis: [{
            categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            crosshair: true,
            labels: {
                style: {
                    width: '50px',
                    'min-width': '50px'
                },
                useHTML: true
            }
        }],
        yAxis: [{
            min: 0,
            title: {
                text: 'Total consumption'
            },
            stackLabels: {
                style: {
                    color: 'black'
                },
                enabled: true,
                overflow: 'allow',
                crop: false,
                align: 'left',
                x: -30,
                formatter: function () {
                    return this.stack;
                }
            }
        }],
        legend: {
            reversed: true
        },
        plotOptions: {
            series: {
                stacking: 'normal',
                colorByPoint: false
            }

        },
        series: filterSeries()
    });

    function buildCheckBoxes() {
        $.each(dataOrigin, function (i, area) {
            var box = '<label><input type="checkbox" id="' + area.area + '" name="' + area.area + '" val="' + area.area + '" checked />' + area.area + '</label>';
            $('#stackedbar-checkboxes fieldset').append(box);
        });
    }

    function filterSeries() {
        var series = [];
        var beginStackUnicode = 65;

        var dataVisible = dataOrigin.filter(e => e.visible);
        

        if (dataVisible.length > 0) {
            for (var i = 0; i < dataVisible[0].svData.length; i++) {
                var leadSvName = dataVisible[0].svData[i].name;
                var leadSerie = {
                    name: dataVisible[0].svData[i].name,
                    data: dataVisible[0].svData[i].data,
                    stack: dataVisible[0].area,
                    color: colorStack[i]
                };
                series.push(leadSerie);
                var stackUnicode = beginStackUnicode + 1;
                for (var j = 1; j < dataVisible.length; j++) {
                    if (dataVisible[j].visible) {
                        var sv = dataVisible[j].svData.find(sv => sv.name === leadSvName);
                        var serie = {
                            name: sv.name,
                            data: sv.data,
                            linkedTo: ':previous',
                            stack: String.fromCharCode(stackUnicode),
                            color: colorStack[i],
                            stack: dataVisible[j].area
                        };
                        series.push(serie);
                        stackUnicode++;
                    }
                }
            }
        }
        return series;
    }


    $(document).on('click', '#stackedbar-checkboxes input', function (e) {
        $.each($('#stackedbar-checkboxes input'), function (i, box) {
            var checkbox = this;
            var area = dataOrigin.find((area) => area.area === checkbox.id);
            area.visible = checkbox.checked;
        });
        //send the chart object and the chartData object
        rebuildData();
    });

    //-----------------------------------------------
    //rebuild the chart data
    function rebuildData() {
        for (var i = stackedbarChart.series.length - 1; i >= 0; i--) {
            stackedbarChart.series[i].remove(false);
        }

        var newSeries = filterSeries();
        newSeries.forEach(s => stackedbarChart.addSeries(s));
    }

    buildCheckBoxes();

    var centerFilter = $("#centerFilter").data("kendoDropDownTree");
    var centerFilterData = dataOrigin.map((d, i) => Object.create( { CDNUMBER: i, DESCRIPTION: d.area }));
    centerFilter.dataSource.data(centerFilterData);
    centerFilter.value(centerFilterData.map(d => d.DESCRIPTION));

    var serviceFilter = $("#centerService").data("kendoDropDownTree");
    var serviceFilterData = dataOrigin.length > 0 ? dataOrigin[0].svData.map((d, i) => Object.create({ CDNUMBER: i, DESCRIPTION: d.name })) : [];
    serviceFilter.dataSource.data(serviceFilterData);
    serviceFilter.value(serviceFilterData.map(d => d.DESCRIPTION));

    serviceFilter.setOptions({
        close: function (e) {
            var filterOpt = this.value();
            stackedbarChart.series.forEach(s => {
                if (filterOpt.indexOf(s.name) === -1)
                    s.setVisible(false);
                else
                    s.setVisible(true);
            });
        },
    });

    centerFilter.setOptions({
        close: function (e) {
            var filterOpt = this.value();
            dataOrigin.forEach(area => {
                if (filterOpt.indexOf(area.area) === -1)
                    area.visible = false;
                else
                    area.visible = true;
            });
            $.each($('#stackedbar-checkboxes input'), function (i, box) {
                var checkbox = this;
                var area = dataOrigin.find((area) => area.area === checkbox.id);
                checkbox.checked = area.visible;
            });
            rebuildData();
        }
    });

});
