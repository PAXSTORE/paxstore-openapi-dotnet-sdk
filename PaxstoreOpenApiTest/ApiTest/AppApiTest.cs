

using Newtonsoft.Json;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using Serilog;



namespace Paxstore.Test
{
    [TestFixture()]
    public class AppApiTest:BaseTest
    {
        public static AppApi API = new AppApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET, 1000);

        [Test]
        public void TestSearchApp() {
            Result<PagedApp> result = API.SearchApp(1, 10, null, null, null, null,
            null,
            null,
            null,
            false,
            false);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
        }


    }
}
