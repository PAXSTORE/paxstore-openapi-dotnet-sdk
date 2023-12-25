using Newtonsoft.Json;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using Serilog;
using System;
using System.Collections.Generic;

namespace Paxstore.Test
{
    [TestFixture()]
    class TestTerminalVariableApi : BaseTest
    {
        public static TerminalVariableApi API = new TerminalVariableApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);

        [Test]
        public void testGetTerminalVariable() {
            //Result<TerminalParameterVariable> result = API.GetTerminalVariable("JDEW5LCP", null, "com.pax.android.demoapp", "sys.param.acqInsCode", null);
            Result<TerminalParameterVariable> result = API.GetTerminalVariable(1, 10, VariableSearchOrderBy.Variable_asc, "JDEW5LCP", null, "com.pax.android.demoapp", "testCreateVariable1Api4", null);
            //Result<TerminalParameterVariable> result = API.GetTerminalVariable("JDEW5LCP", null, "cn.ycmedia.youpin", "V1", null);
            //Result<TerminalParameterVariable> result = API.GetTerminalVariable("JDEW5LCP", null, "air.tv.douyu.android", "app1", null);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void testCreateTerminalVariable() {
            TerminalParameterVariableCreateRequest createRequest = new TerminalParameterVariableCreateRequest();
            ParameterVariable parameterVariable1 = new ParameterVariable();
            parameterVariable1.Key="testCreateVariable1Api3";
            parameterVariable1.Value="testApiCreate3";
            parameterVariable1.PackageName= "com.pax.android.demoapp";
            parameterVariable1.Remarks="今日头条app testCreateApi3";

            ParameterVariable parameterVariable2 = new ParameterVariable();
            parameterVariable2.Key="testCreateVariable1Api4";
            parameterVariable2.Value="testApiCreate4";
            parameterVariable2.PackageName= "com.pax.android.demoapp";
            parameterVariable2.Remarks="今日头条app testCreateApi4";

            List<ParameterVariable> variableList = new List<ParameterVariable>();
            variableList.Add(parameterVariable1);
            variableList.Add(parameterVariable2);
            createRequest.TID= "JDEW5LCP";
            createRequest.VariableList = variableList;
            Result<String> createResult = API.CreateTerminalVariable(createRequest);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(createResult));
            Assert.AreEqual(createResult.BusinessCode, 0);
        }

        [Test]
        public void testUpdateTerminalVariable_success()
        {
            long terminalVariableId = 1001464170;

            TerminalVariableUpdateRequest updateRequest = new TerminalVariableUpdateRequest();
            updateRequest.Key = "testCreateVariable1Api4";
            updateRequest.Value= "testApiCreate4_updated";
            updateRequest.Remarks="updateRemarks1";
            updateRequest.PackageName= "com.pax.android.demoapp";

            Result<string> updateResult = API.UpdateTerminalVariable(terminalVariableId, updateRequest);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(updateResult));
            Assert.AreEqual(updateResult.BusinessCode, 0);
        }

        [Test]
        public void testDeleteTerminalVariable(){
            long terminalVariableId = 1001464170;
            Result<string> deleteResult = API.DeleteTerminalVariable(terminalVariableId);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(deleteResult));
            Assert.AreEqual(deleteResult.BusinessCode, 0);
        }

        [Test]
        public void testBatchDeletionTerminalVariable(){
            TerminalParameterVariableDeleteRequest batchDeletionRequest = new TerminalParameterVariableDeleteRequest();
            List<long> variableIds = new List<long>();
            variableIds.Add(1001464163);
            variableIds.Add(1001464142);
            batchDeletionRequest.VariableIds = variableIds;
            Result<string> batchDeletionResult = API.BatchDeletionTerminalVariable(batchDeletionRequest);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(batchDeletionResult));
            Assert.AreEqual(batchDeletionResult.BusinessCode, 0);
        }

    }
}
