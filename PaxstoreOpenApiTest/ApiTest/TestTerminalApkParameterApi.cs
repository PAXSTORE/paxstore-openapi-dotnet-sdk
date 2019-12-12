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
    class TestTerminalApkParameterApi
    {
        private static ILog _logger = LogManager.GetLogger(typeof(TestTerminalApkParameterApi));
        public static TerminalApkParameterApi API = new TerminalApkParameterApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);

        [Test]
        public void testGetTerminalApkParameter() {
            Result<ApkParameter> result = API.GetTerminalApkParameter(1, 10, TerminalApkParamSearchOrderBy.ApkParameter_asc, null, "zhiyoucanshu", "1.2");
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void testCreateTerminalApkParameter_success() {
            CreateApkParameterRequest createApkParameterRequest = new CreateApkParameterRequest();
            createApkParameterRequest.ParamTemplateName= "paxstore_app_param.xml";
            createApkParameterRequest.Name= "test114";
            createApkParameterRequest.PackageName= "zhiyoucanshu";
            createApkParameterRequest.Version= "1.2";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("sys_F2_sys_param_acqInsCode", "00000000022");
            createApkParameterRequest.Parameters = parameters;
            Result<ApkParameter> result = API.CreateApkParameter(createApkParameterRequest);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void testUpdateTerminalApkParameter_success() {
            UpdateApkParameterRequest updateApkParameterRequest = new UpdateApkParameterRequest();
            updateApkParameterRequest.ParamTemplateName = "paxstore_app_param.xml";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("sys_F2_sys_param_acqInsCode", "00000000033");
            updateApkParameterRequest.Parameters = parameters;
            Result<ApkParameter> result = API.UpdateApkParameter(1000101970, updateApkParameterRequest);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void testDeleteTerminalApkParameter_success() {
            Result<string> result = API.DeleteApkParameter(1000102047);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

    }
}
