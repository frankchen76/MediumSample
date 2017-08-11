var postRenderCallbackArgs = {
    titleId: titleId,
    currentItem: ctx.CurrentItem
};
AddPostRenderCallback(ctx, Function.createDelegate(postRenderCallbackArgs, function () {
    var parameter = this;
    //you get your ctx.CurrentItem.
    console.log(parameter.currentItem);
}));
