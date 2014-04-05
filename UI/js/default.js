jQuery(function ($) {
   
    /*************************** Trend chart code ***********************/

    var trendChartData = [];

    function gd(date) {
        return new Date(date).getTime();
    }

    function parseTrendData(data) {
        var projectTweetTrend = [], index,
            tdata = data.data, trend;
        for (index = 0; index < tdata.length; index++) {
            var info = tdata[index];
            trend = {};
            trend.label = info.label;
            trend.data = [];
            for (var j = 0; j < info.data.length; j++) {
                var tweetinfo = info.data[j];
                trend.data.push([gd(tweetinfo.date), tweetinfo.tweets]);
            }
            projectTweetTrend.push(trend);
        }

        return projectTweetTrend;

    }
    var trendPlaceholder = $('#trendchart-placeholder').css({ 'min-height': '150px' });

    function getTrendChartData() {
        $.getJSON('test/testdata/trenddata.txt').done(function (data) {
            drawTrendChart(trendPlaceholder, parseTrendData(data));
        });
    }


    function drawTrendChart(placeholder, data) {

        $.plot(placeholder, data, {
            hoverable: true,
            shadowSize: 0,
            series: {
                lines: { show: true },
                points: { show: true }
            },
            xaxis: {

            },
            yaxis: {
                ticks: 10
            },
            grid: {
                borderWidth: 0
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
        $.getJSON('pietweetdata.ashx').done(function (data) {
            pieChartData = data.data;
            drawPieChart(placeholder, data.data);
        });
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


    

    //$('#recent-box [data-rel="tooltip"]').tooltip({ placement: tooltip_placement });

    //function tooltip_placement(context, source) {
    //    var $source = $(source);
    //    var $parent = $source.closest('.tab-content');
    //    var off1 = $parent.offset();
    //    var w1 = $parent.width();

    //    var off2 = $source.offset();
    //    var w2 = $source.width();

    //    if (parseInt(off2.left) < parseInt(off1.left) + parseInt(w1 / 2)) return 'right';
    //    return 'left';
    //}



});