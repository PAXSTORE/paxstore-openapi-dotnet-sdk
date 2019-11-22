using log4net;
using Newtonsoft.Json;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.Test
{
    
    class TestProxy
    {
        private static ILog _logger = LogManager.GetLogger(typeof(TestResellerApi));
        public static ResellerApi API = new ResellerApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);

        [Test]
        public void testProxy() {
            IWebProxy proxy = new WebProxy("localhost", 1080);
            API.SetConnectionTimeoutTime(5000);
            API.SetProxy(proxy);

            Result<PagedReseller> result = API.SearchReseller(1, 10, ResellerSearchOrderBy.Name, null, ResellerStatus.All);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.IsTrue(result.BusinessCode == 0);
            Assert.IsTrue(result.PageInfo.TotalCount > 0);
        }
    }
}
