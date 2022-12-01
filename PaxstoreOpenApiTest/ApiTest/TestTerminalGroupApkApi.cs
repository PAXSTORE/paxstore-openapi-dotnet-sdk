using log4net;
using Newtonsoft.Json;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using Paxstore.OpenApi.Model.TerminalApkParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.Test
{
    [TestFixture()]
    class TestTerminalGroupApkApi
    {
        private static ILog _logger = LogManager.GetLogger(typeof(TestResellerApi));
        public static TerminalGroupApkApi API = new TerminalGroupApkApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);

        [Test]
        public void TestGetTerminalGroupApk() {
            List<string> pidlist = new List<string>();
            pidlist.Add("c");
            pidlist.Add("d");
            Result<TerminalGroupApkInfo> result = API.GetTerminalGroupApk(1, pidlist);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void SearchTerminalGroupApk()
        {
            Result<TerminalGroupApkInfo> result = API.SearchTerminalGroupApk(1, 10, TerminalGroupApkSearchOrderBy.CreatedDate_asc, 1, false, false, null);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestCreate() {
            CreateTerminalGroupApkRequest request = new CreateTerminalGroupApkRequest();
            request.GroupId = 1001549872;
            request.PackageName = "com.pax.vas.stacklytics.sale";
          
            request.EffectiveTime = DateTime.Now.AddHours(1);
            request.ExpiredTime = DateTime.Now.AddDays(1);
           
            Result<TerminalGroupApkInfo> result = API.CreateAndActiveGroupApk(request);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void CreateAndActiveGroupApk()
        {
            CreateTerminalGroupApkRequest request = new CreateTerminalGroupApkRequest();
            request.GroupId = 1;
            request.PackageName = "packageName";
            request.PushTemplateName = "pushTemplateName";
            request.TemplateName = "templateName";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("a", "aa");
            parameters.Add("b", "bb");
            request.Parameters = parameters;
            List<FileParameter> fileParameters = new List<FileParameter>();
            FileParameter fileParameter1 = new FileParameter();
            fileParameter1.PID = "pid";
            fileParameter1.FileName = "fileName";
            fileParameter1.FileData = "ffffffffffffffffffffff";
            fileParameters.Add(fileParameter1);
            request.Base64FileParameters = fileParameters;
            Result<TerminalGroupApkInfo> result = API.CreateAndActiveGroupApk(request);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void SuspendTerminalGroupApk()
        {
            Result<TerminalGroupApkInfo> result = API.SuspendTerminalGroupApk(1);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void DeleteTerminalGroupApk()
        {
            Result<string> result = API.DeleteTerminalGroupApk(1);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }
    }
}
