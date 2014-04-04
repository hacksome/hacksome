jQuery(function ($) {
    var timeline = [];

    function getPopularityRanking(pos) {
        $.getJSON('test/testdata/popularityrankingdata.txt').done(function (data) {
           
            setData(data);
           
        });
    }

    function setData(data) {
        for (var i = 0; i < data.data.length; i++) {
            var ranking = data.data[i];

            $('#projectrankingtable').dataTable().fnAddData(
                [ranking.name, ranking.score, ranking.totaltweets, 0]
                );
        }
    }

    getPopularityRanking();

});