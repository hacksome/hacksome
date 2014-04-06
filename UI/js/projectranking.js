jQuery(function ($) {

    function getPopularityRanking() {
        $.ajax({
            dataType: "json",
            url: 'SemanticScoreData.ashx',
            success: function (data) {
                if (data.data.length == 0) {
                    data = dummyPopularityData;
                }
                setData(data);
                
            },
            error: function() {
                setData(dummyPopularityData);
            }
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