jQuery(function ($) {

    function getPopularityRanking() {
        $.getJSON('SemanticScoreData.ashx').done(function (data) {
            setData(data);
        });
    }

    function setData(data) {
        for (var i = 0; i < data.data.length; i++) {
            var ranking = data.data[i];

            $('#projectrankingtable').dataTable().fnAddData(
                [ranking.name, ranking.score.toFixed(1), ranking.totaltweets,ranking.xfactor.toFixed(1)]
                );
        }
    }

    getPopularityRanking();

});