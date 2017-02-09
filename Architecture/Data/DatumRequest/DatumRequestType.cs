namespace Datum
{
    public enum DatumRequestType {
        Local,
        Web
    }

    public static class DatumRequestTypeExt {

        public static DatumRequest<TDatum> ToRequest<TDatum>(this DatumRequestType thisType, string path)
        {
            switch (thisType)
            {
                case DatumRequestType.Local:
                    return new ResourcesRequest<TDatum>(path);
            }

            return null;
        }
    }
}
