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
            CreateTerminalApkRequest createRequest = new CreateTerminalApkRequest();
            string templateName = "a|b|c|d|ee|f|g|hh|gg|ll";
            createRequest.TemplateName = templateName;
            createRequest.PackageName = "baidu.ai";
            createRequest.TID = "12345678";
            Result<string> result = API.CreateTerminalApk(createRequest);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 2028);
        }
    }
}
