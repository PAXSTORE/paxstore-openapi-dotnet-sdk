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
    class TestTerminalApkApi
    {
        private static ILog _logger = LogManager.GetLogger(typeof(TestTerminalApkApi));
        public static TerminalApkApi API = new TerminalApkApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);

        [Test]
        public void TestCreateTerminalApkNull() {
            Result<string> result = API.CreateTerminalApk(null);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
        }

        [Test]
        public void TestCreateTerminalApkInvalid() {
            CreateTerminalApkRequest createRequest = new CreateTerminalApkRequest();
            string templateName = "a|b|c|d|ee|f|g|hh|gg|ll|yy|zz|xx|pp|fe|";
            createRequest.TemplateName = templateName;
            Result<string> result = API.CreateTerminalApk(createRequest);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
        }

        [Test]
        public void TestCreateTerminalApkTerminalNotFound()
        {
            CreateTerminalApkRequest createTerminalApkRequest = new CreateTerminalApkRequest();
            createTerminalApkRequest.TID = "ABC09098989";
            createTerminalApkRequest.PackageName = "com.baidu.map";
            createTerminalApkRequest.TemplateName = "template_map";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("PID.locationCode", "cn_js_sz");
            parameters.Add("PID.showtraffic", "true");
            createTerminalApkRequest.Parameters = parameters;
            Result<string> result = API.CreateTerminalApk(createTerminalApkRequest);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 2028);
        }

        [Test]
        public void TestSearchApkPushHistory() {
            Result<PushApkHistory> result = API.SearchPushApkHistory(1, 10, SearchOrderBy.CreatedDate_desc, "7L03HWP9", "com.pax.android.demoapp", PushStatus.All);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestSearchApkPushHistoryTIDNull()
        {
            Result<PushApkHistory> result = API.SearchPushApkHistory(1, 10, SearchOrderBy.CreatedDate_desc, null, "com.pax.android.demoapp", PushStatus.All);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
        }

        [Test]
        public void TestGetApkPushHistoryById() {
            Result<PushApkHistory> result = API.GetPushApkHistory(5050);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }
    }
}
