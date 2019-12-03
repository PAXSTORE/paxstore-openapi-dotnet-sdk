using log4net;
using Newtonsoft.Json;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using Paxstore.OpenApi.Model.PushHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Paxstore.OpenApi.PushHistoryApi;

namespace Paxstore.Test
{
    [TestFixture()]
    class TestPushHistoryApi
    {
        private static ILog _logger = LogManager.GetLogger(typeof(TestPushHistoryApi));
        public static PushHistoryApi API = new PushHistoryApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);

        [Test]
        public void TestSearchPushHistory_success()
        {
            Result<AppPushHistoryInfo> result = API.SearchAppPushStatus(1, 10, PushHistorySearchOrderBy.AppPushTime, "com.pax.posviewer",
            null, PushHistoryStatus.Failed, null);

            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }
    }
}
