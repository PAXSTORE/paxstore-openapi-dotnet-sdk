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
            Result<ParameterPushHistoryInfo> result = API.SearchParameterPushHistory(1, 10, "com.pax.android.demoapp",
            "0820534734", PushHistoryStatus.Success, null);

            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestSearchOptimizedPushHistory_success() {
            Result<OptimizedParamPushHistory> result = API.SearchOptimizedParameterPushHistory(1, 10, "com.pax.android.demoapp",
            "0820534734", PushHistoryStatus.Success, null);

            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestLatestPushHistory_success()
        {
            Result<ParameterPushHistoryInfo> result = API.SearchLatestParameterPushHistory(1, 10, "com.pax.android.demoapp",
            "0820534734", PushHistoryStatus.Success, null);

            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestSearchLatestOptimizedPushHistory_success()
        {
            Result<OptimizedParamPushHistory> result = API.SearchLatestOptimizedParameterPushHistory(1, 10, "com.pax.android.demoapp",
            "0820534734", PushHistoryStatus.Success, null);

            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }
    }
}
