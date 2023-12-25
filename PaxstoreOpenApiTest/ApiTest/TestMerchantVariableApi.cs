using Newtonsoft.Json;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using Serilog;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Paxstore.Test
{
    [TestFixture()]
    public class TestMerchantVariableApi: BaseTest
    {
        public static MerchantVariableApi API = new MerchantVariableApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET, 1000);

        [Test]
        public void TestSearchMerchantVariable() {

            Result<MerchantVariable> result = API.SearchMerchantVariable(1, 10, MerchantVariableSearchOrderBy.CREATED_DESC, 1018624270, "com.pax.pdm", null, MerchantVariableSource.MERCHANT);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(0, result.BusinessCode);
        }

        [Test]
        public void TestCreateMerchantVariable() { 
            MerchantVariableCreateRequest request = new MerchantVariableCreateRequest();
            request.MerchantId = 1018624270;
            IList<ParameterVariable> merchantVariable = new List<ParameterVariable>();
           /* ParameterVariable parameter = new ParameterVariable();
            parameter.Key = "a1";
            parameter.PackageName = "com.pax.pdm";
            merchantVariable.Add(parameter);
            ParameterVariable parameter2 = new ParameterVariable();
            parameter2.Key = "a2";
            parameter2.PackageName = "com.pax.pdm";
            parameter2.Value = "test key2 value";
            merchantVariable.Add(parameter2);*/
            ParameterVariable parameter3 = new ParameterVariable();
            parameter3.Key = "a13";
            parameter3.PackageName = "com.pax.pdm";
            parameter3.Type = "P";
            parameter3.Value = "pwdtest";
            merchantVariable.Add(parameter3);
            request.VariableList = merchantVariable;
            Result<string> result = API.CreateMerchantVariable(request);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(0, result.BusinessCode);
        }

        [Test]
        public void TestUpdateMerchantVariable()
        {
            MerchantVariableUpdateRequest request = new MerchantVariableUpdateRequest();
            request.Key = "key001";
            request.Remarks = "new remark";
            Result<string> result = API.UpdateMerchantVariable(1000534439, request);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(0, result.BusinessCode);
        }

        [Test]
        public void TestDeleteMerchantVariable()
        {
            Result<string> result = API.DeleteMerchantVariable(1000534439);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(0, result.BusinessCode);
        }

        [Test]
        public void TestBatchDeleteMerchantVariable() {
            MerchantVariableDeleteRequest request = new MerchantVariableDeleteRequest();
            IList<long> deleteIds = new List<long>();
            deleteIds.Add(1000534440);
            deleteIds.Add(1000534441);
            request.VariableIds = deleteIds;
            Result<string> result = API.BatchDeletionMerchantVariable(request);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(0, result.BusinessCode);
        }
    }
}
