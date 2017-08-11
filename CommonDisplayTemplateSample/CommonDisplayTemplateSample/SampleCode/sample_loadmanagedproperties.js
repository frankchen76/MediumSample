var postRenderCallbackArgs = {
    titleId: titleId,
    currentItem: ctx.CurrentItem
};
AddPostRenderCallback(ctx, Function.createDelegate(postRenderCallbackArgs, function () {
    var parameter = this;
    var rendering = function (para) {
        var context = Srch.ScriptApplicationManager.get_clientRuntimeContext();
        var keywordQuery = new Microsoft.SharePoint.Client.Search.Query.KeywordQuery(context);
        //using WorkId to retrieve the item's managed properties.
        keywordQuery.set_queryText('WorkId:"' + para.currentItem.WorkId + '"');
        var properties = keywordQuery.get_selectProperties();
        //Add "IsExternalContent" managed property which identified if the result is on-premises or SPO.
        properties.add('IsExternalContent');
        var searchExecutor = new Microsoft.SharePoint.Client.Search.Query.SearchExecutor(context);
        var results = searchExecutor.executeQuery(keywordQuery);
        var titleId = "#" + $htmlEncode(para.currentItem.csr_id + Srch.U.Ids.titleLink);
        var searchCallbackArgs = {
            ctrlLink: $(para.titleId),
            ctrlImage: $("<img src='" + Srch.U.replaceUrlTokens("~site/PublishingImages/giphy.gif") + "' style='hight:16px;width:16px;'>"),
            results: results,
            currentItem: para.currentItem
        };

        context.executeQueryAsync(Function.createDelegate(searchCallbackArgs, 
            function () {
                var searchArgs = this;
                var resultTables = this.results.m_value.ResultTables;
                //console.log(searchArgs);
                if (resultTables != null &&
                    resultTables.length > 0 &&
                    resultTables[0].ResultRows != null &&
                    resultTables[0].ResultRows.length > 0) {
                    var resultRow = resultTables[0].ResultRows[0];
                    if (resultRow["IsExternalContent"] != undefined && resultRow["IsExternalContent"]) {
                        //insert your logics for On-Premise search result.
                    } else {
                        //insert your logics for SPO search result.
                    }
                }
            }),
            function (sender, args) {
            console.log("error");
        });
    };
    //load jQuery
    if (typeof jQuery === "undefined") {
        RegisterSod('jquery-1.11.2.min.js', 'https://code.jquery.com/jquery-1.11.2.min.js');
        EnsureScriptFunc('jquery-1.11.2.min.js', 'jQuery', function () {
            rendering(parameter);
        });
    } else {
        console.log("jQuery is loaded");
        rendering(parameter);
    }
}));
