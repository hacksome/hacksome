jQuery(function ($) {
   
    /*************************** Trend chart code ***********************/

    var trendChartData = [];
    var ticks = [];
    function gd(date) {
        return new Date(date).getTime();
    }

    function parseTrendData(data) {
        var projectTweetTrend = [], index,
            tdata = data.data, trend;
        for (index = 0; index < tdata.length; index++) {
            var info = tdata[index];
            trend = {};
            ticks.push(index, info.label);
            trend.label = info.label;
            trend.data = [];
            trend.color = info.color;
            trend.data.push([info.data.pos, info.data.score]);
            projectTweetTrend.push(trend);
        }

        return projectTweetTrend;

    }
    var trendPlaceholder = $('#trendchart-placeholder').css({ 'min-height': '150px' });

    function getTrendChartData() {
        var validJson;
        $.ajax({
//            dataType: "json",
            url: 'SemanticScoreData.ashx?b=1',
            success: function (data) {
                try {
                    validJson = JSON.parse(data);
                    //pieChartData = parseData.data.length == 0 ? dummyPieData : parseData.data;
                } catch (e) {
                    validJson = dummyBarData;
                    // return false;
                }

                var d = validJson.data.length ? validJson : dummyBarData;
                drawTrendChart(trendPlaceholder, parseTrendData(d));
            },
            error: function() {
                drawTrendChart(trendPlaceholder, dummyBarData);
            }
    });
       
    }


    function drawTrendChart(tplaceholder, data) {

        $.plot(tplaceholder, data, {
            hoverable: true,
            shadowSize: 0,
            series: {
                bars: {
                    show: true
                    , barWidth: .5
                    , align: 'center'
                }
            },
            xaxis: {
                ticks: [],
                min: 0,
                max: 10
            },
            yaxis: {
                ticks: 5
            },
            grid: {
                borderWidth: 0
            },
            legend: {
                show: true
            }
            
        });
    }


    getTrendChartData();
    /**
   we saved the drawing function and the data to redraw with different position later when switching to RTL mode dynamically
   so that's not needed actually.
   */
    trendPlaceholder.data('chart', trendChartData);
    trendPlaceholder.data('draw', drawTrendChart);
    

    /************ Pie chart code *******************/
    var pieChartData = [];
    var placeholder = $('#piechart-placeholder').css({ 'width': '70%', 'min-height': '150px' });

    function getPieChartData() {

        $.ajax({
            //dataType: "json",
            url: 'pietweetdata.ashx',
            success: function (data) {
                try {
                   var parseData= JSON.parse(data);
                   pieChartData = parseData.data.length == 0 ? dummyPieData : parseData;
               } catch (e) {
                    pieChartData = dummyPieData;
                    // return false;
                }
               // pieChartData =data.data.length == 0? dummyPieData:data.data;
                
               drawPieChart(placeholder, pieChartData.data);
            },
            error: function() {
                drawPieChart(placeholder, dummyPieData.data);
            }
        });

//        $.getJSON('pietweetdata.ashx').done(function (data) {
        
        //  });
    }
    function drawPieChart(placeholder, data, position) {
        $.plot(placeholder, data, {
            series: {
                pie: {
                    show: true,
                    tilt: 0.8,
                    highlight: {
                        opacity: 0.25
                    },
                    stroke: {
                        color: '#fff',
                        width: 2
                    },
                    startAngle: 2
                }
            },
            legend: {
                show: true,
                position: position || "ne",
                labelBoxBorderColor: null,
                margin: [-30, 15]
            },
            grid: {
                hoverable: true,
                clickable: true
            }
        });
    }

    getPieChartData();

    /**
    we saved the drawing function and the data to redraw with different position later when switching to RTL mode dynamically
    so that's not needed actually.
    */
    placeholder.data('chart', pieChartData);
    placeholder.data('draw', drawPieChart);

    /***
    end of pie chart
    **/
    var $tooltip = $("<div class='tooltip top in'><div class='tooltip-inner'></div></div>").hide().appendTo('body');
    var previousPoint = null;

    placeholder.on('plothover', function (event, pos, item) {
        if (item) {
            if (previousPoint != item.seriesIndex) {
                previousPoint = item.seriesIndex;
                var tip = item.series['label'] + " : " + item.series['percent'] + '%';
                $tooltip.show().children(0).text(tip);
            }
            $tooltip.css({ top: pos.pageY + 10, left: pos.pageX + 10 });
        } else {
            $tooltip.hide();
            previousPoint = null;
        }

    });


    


});