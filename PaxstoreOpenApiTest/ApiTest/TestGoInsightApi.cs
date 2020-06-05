using log4net;
using Newtonsoft.Json;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.Test
{
    [TestFixture()]
    public class TestGoInsightApi
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(TestGoInsightApi));
        public static GoInsightApi API = new GoInsightApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET, TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));

        [Test]
        public void testQueryData_query_code_invalid() {
            Result<DataQueryResult> result1 = API.FindDataFromInsight("abc");
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result1));
            Assert.AreNotEqual(result1.BusinessCode, 0);
        }

        [Test]
        public void testQueryData_success() {
            Result<DataQueryResult> result1 = API.FindDataFromInsight("abcdaaaa");
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result1));
            Assert.AreEqual(result1.BusinessCode, 0);
        }
    }
}
