var postRenderCallbackArgs = {
    titleId: titleId,
    currentItem: ctx.CurrentItem
};
AddPostRenderCallback(ctx, Function.createDelegate(postRenderCallbackArgs, function () {
    var parameter = this;
    var rendering = function (para) {
        //your logics go there.
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
