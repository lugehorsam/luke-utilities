using UnityEngine;

public abstract class ResourcesDataRequest<TDatum> : DataRequest<TDatum> where TDatum : struct {

    private readonly ResourceRequest request;

    protected abstract string PathFromResources
    {
        get;
    }

    public ResourcesDataRequest()
    {
        request = Resources.LoadAsync<TextAsset>(PathFromResources);
    }

    protected override bool RequestIsDone()
    {
        return request.isDone;
    }

    protected override string GetRequestContent()
    {
        Object asset = request.asset;
        TextAsset textAsset = asset as TextAsset;
        return textAsset.text;
    }

}
