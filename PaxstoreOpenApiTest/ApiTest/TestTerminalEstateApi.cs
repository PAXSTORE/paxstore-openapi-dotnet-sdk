using Newtonsoft.Json;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using Serilog;

namespace Paxstore.Test
{
    [TestFixture()]
    class TestTerminalEstateApi : BaseTest
    {
        public static TerminalEstateApi API = new TerminalEstateApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);

        [Test]
        public void testVerifyTerminalEstate() {
            Result<string> result = API.VerifyTerminalEstate("sn456789");

            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.IsTrue(result.BusinessCode == 0);
        }
    }
}
