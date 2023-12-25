using Newtonsoft.Json;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using Serilog;
using System;

namespace Paxstore.Test
{
    [TestFixture()]
    public class TestGoInsightApit : BaseTest
    {
        public static GoInsightApi API = new GoInsightApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET, TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));

        [Test]
        public void testQueryData_query_code_invalid() {
            Result<DataQueryResult> result1 = API.FindDataFromInsight("abc");
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result1));
            Assert.AreNotEqual(result1.BusinessCode, 0);
        }

        [Test]
        public void testQueryData_success() {
            Result<DataQueryResult> result1 = API.FindDataFromInsight("s4x5iz40");
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result1));
            Assert.AreEqual(result1.BusinessCode, 0);
        }
    }
}
