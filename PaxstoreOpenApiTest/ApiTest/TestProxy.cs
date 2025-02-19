using Newtonsoft.Json;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using Serilog;
using System.Net;
namespace Paxstore.Test
{
    
    class TestProxy : BaseTest
    {
        public static ResellerApi API = new ResellerApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET, new WebProxy("localhost", 33210));

        [Test]
        public void testProxy() {
            Result<PagedReseller> result = API.SearchReseller(1, 10, ResellerSearchOrderBy.Name, null, ResellerStatus.All);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.IsTrue(result.BusinessCode == 0);
            Assert.IsTrue(result.PageInfo.TotalCount > 0);
        }
    }
}
