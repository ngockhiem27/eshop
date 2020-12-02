$(document).ready(function () {
    var colorStack = ['black', 'blue', 'green', 'purple', 'yellow', 'aqua', 'orange', 'red'];
    var datasetObs = new kendo.data.ObservableObject({
        data: []
    });

    var stackedbarChart = Highcharts.chart('stackedbar-main-container', {
        chart: {
            type: 'bar'
        },
        title: {
            text: 'Tang truong thue bao'
        },
        xAxis: [{
            categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            crosshair: true
        }],
        yAxis: [{
            min: 0,
            title: {
                text: 'Total value'
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
        series: []
    });

    $("#btnApply-main").click(function (e) {
        e.preventDefault();
        var timeVal = ['Q1/2020', 'Q2/2020', 'Q3/2020', 'Q4/2020'];
        var centerFilterData = $("#centerFilter-main").data("kendoDropDownTree").value();
        var serviceFilterData = $("#centerService-main").data("kendoDropDownTree").value();
        var groupSerivceFilterData = $("#groupService-main").data("kendoDropDownTree").value();
        console.log(centerFilterData, serviceFilterData, groupSerivceFilterData);
        var resultDataSet = [];
        if (centerFilterData.length > 0) {
            var data = [];
            centerFilterData.forEach(c => {
                var points = timeVal.map(t => Math.floor(Math.random() * 10) + 1);
                data.push({ name: c, data: points });
            });
            var centerResult = {
                view: 'center',
                data: data,
                time: timeVal
            };
            resultDataSet.push(centerResult);
        }
        if (serviceFilterData.length > 0) {
            var data = [];
            serviceFilterData.forEach(c => {
                var points = timeVal.map(t => Math.floor(Math.random() * 10) + 1);
                data.push({ name: c, data: points });
            });
            var serviceResult = {
                view: 'service',
                data: data,
                time: timeVal
            };
            resultDataSet.push(serviceResult);
        }
        if (groupSerivceFilterData.length > 0) {
            var data = [];
            groupSerivceFilterData.forEach(c => {
                var points = timeVal.map(t => Math.floor(Math.random() * 10) + 1);
                data.push({ name: c, data: points });
            });
            var groupResult = {
                view: 'groupService',
                data: data,
                time: timeVal
            };
            resultDataSet.push(groupResult);
        }
        datasetObs.set("data", resultDataSet);
    });

    $("#dropdown-view-filter").kendoDropDownList({
        dataSource: {
            data: []
        },
        select: function (e) {
            console.log(e);
            var viewValue = e.item.text();
            var dataSet = datasetObs.get("data");
            var viewData = dataSet.find(ele => ele.view === viewValue);
            console.log('select', viewData);
            setSeries(viewData);
        }
    });

    $("#stackedbar-dimension-switch").click(function () {
        stackedbarChart.update({
            chart: {
                type: stackedbarChart.types[0] === "bar" ? "column" : "bar"
            },
        });
    });

    datasetObs.bind("change", function (e) {
        var dropdownView = $("#dropdown-view-filter").data("kendoDropDownList");
        dropdownView.dataSource.data(datasetObs.get("data").map(d => d.view));
        dropdownView.value(datasetObs.get("data")[0].view);
        dropdownView.trigger("select", { item: { text: () => datasetObs.get("data")[0].view } });

        console.log("d0", datasetObs.get("data")[0].time);
        stackedbarChart.xAxis[0].setCategories(datasetObs.get("data")[0].time);
    });

    function createSeries(src) {
        var series = [];
        var color = Array.from(colorStack);
        src.data.forEach(e => {
            series.push({
                name: e.name,
                data: e.data,
                color: color.pop()
            });
        });
        return series;
    }

    function setSeries(src) {
        for (var i = stackedbarChart.series.length - 1; i >= 0; i--) {
            stackedbarChart.series[i].remove(false);
        }

        var newSeries = createSeries(src);
        newSeries.forEach(s => stackedbarChart.addSeries(s));
    }

    $("#centerFilter-main").kendoDropDownTree({
        placeholder: "--Lựa chọn--",
        checkboxes: true,
        //checkAll: true,
        clearButton: true,
        tagMode: "single",
        //checkAllTemplate: "Tất cả",
        filter: "contains",
        autoClose: false,
        messages: {
            clear: "Loại bỏ",
            deleteTag: "Xóa tất cả",
            singleTag: "được chọn",
            noData: "Không có dữ liệu!"
        },
        dataTextField: "DESCRIPTION",
        dataValueField: "DESCRIPTION",
        dataSource: { data: [{ DESCRIPTION: "c1" }, { DESCRIPTION: "c2" }, { DESCRIPTION: "c3" }, { DESCRIPTION: "c4" }]},
        height: 220,
    });

    $("#centerService-main").kendoDropDownTree({
        placeholder: "--Lựa chọn--",
        checkboxes: true,
        //checkAll: true,
        clearButton: true,
        tagMode: "single",
        //checkAllTemplate: "Tất cả",
        filter: "contains",
        autoClose: false,
        messages: {
            clear: "Loại bỏ",
            deleteTag: "Xóa tất cả",
            singleTag: "được chọn",
            noData: "Không có dữ liệu!"
        },
        dataTextField: "DESCRIPTION",
        dataValueField: "DESCRIPTION",
        dataSource: { data: [{ DESCRIPTION: "s1" }, { DESCRIPTION: "s2" }, { DESCRIPTION: "s3" }, { DESCRIPTION: "s4" }] },
        height: 220,
    });

    $("#groupService-main").kendoDropDownTree({
        placeholder: "--Lựa chọn--",
        checkboxes: true,
        //checkAll: true,
        clearButton: true,
        tagMode: "single",
        //checkAllTemplate: "Tất cả",
        filter: "contains",
        autoClose: false,
        messages: {
            clear: "Loại bỏ",
            deleteTag: "Xóa tất cả",
            singleTag: "được chọn",
            noData: "Không có dữ liệu!"
        },
        dataTextField: "DESCRIPTION",
        dataValueField: "DESCRIPTION",
        dataSource: { data: [{ DESCRIPTION: "g1" }, { DESCRIPTION: "g2" }, { DESCRIPTION: "g3" }, { DESCRIPTION: "g4" }] },
        height: 220,
    });

});
