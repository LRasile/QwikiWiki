using System.Diagnostics;

namespace QwikiWiki.DataLayer
{
    [DebuggerStepThrough]
    public class DataLayerProvider
    {
        public static IDataAccess GetDataAccess()
        {
            return new DataAccess();
        }
    }
}
