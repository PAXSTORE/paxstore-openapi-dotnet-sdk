using Newtonsoft.Json;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using Serilog;
using System.Collections.Generic;

namespace Paxstore.Test
{
    [TestFixture()]
    class TestTerminalApkParameterApi : BaseTest
    {
        public static TerminalApkParameterApi API = new TerminalApkParameterApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);

        [Test]
        public void testGetTerminalApkParameter() {
            Result<ApkParameter> result = API.GetTerminalApkParameter(1, 10, TerminalApkParamSearchOrderBy.ApkParameter_asc, null, "zhiyoucanshu", "1.2");
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
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
            Result<string> result = API.CreateApkParameter(createApkParameterRequest);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void testUpdateTerminalApkParameter_success() {
            UpdateApkParameterRequest updateApkParameterRequest = new UpdateApkParameterRequest();
            updateApkParameterRequest.ParamTemplateName = "paxstore_app_param.xml";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("sys_F2_sys_param_acqInsCode", "00000000033");
            updateApkParameterRequest.Parameters = parameters;
            Result<string> result = API.UpdateApkParameter(1000101970, updateApkParameterRequest);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void testDeleteTerminalApkParameter_success() {
            Result<string> result = API.DeleteApkParameter(1000102047);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

    }
}
