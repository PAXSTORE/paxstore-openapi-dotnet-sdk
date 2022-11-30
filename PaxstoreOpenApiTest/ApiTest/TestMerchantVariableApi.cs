using log4net;
using Newtonsoft.Json;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Help;
using Paxstore.OpenApi.Model;
using Paxstore.OpenApi.Model.MerchantVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.Test
{
    [TestFixture()]
    class TestMerchantVariableApi
    {
        private static ILog _logger = LogManager.GetLogger(typeof(TestResellerApi));
        public static MerchantVariableApi API = new MerchantVariableApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);
        private static long TEST_MERCHANT_ID = 1017953540;

        [Test]
        public void TestSearchMerchantVariable()
        {
            string packageName = null;
            string key = null;
            Result<MerchantVariable> result = API.SearchMerchantVariable(1, 50, MerchantVariableSearchOrderBy.CREATE_DATE_DESC, TEST_MERCHANT_ID, packageName, key, null);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.IsTrue(result.BusinessCode == 0);

            result = API.SearchMerchantVariable(1, 50, null, TEST_MERCHANT_ID, packageName, key, MerchantVariableSource.MERCHANT);
            Assert.IsTrue(result.PageInfo.TotalCount == 0);

            result = API.SearchMerchantVariable(1, 50, null, TEST_MERCHANT_ID, packageName, key, MerchantVariableSource.MARKET);
            Assert.IsTrue(result.PageInfo.TotalCount == 9);

            result = API.SearchMerchantVariable(1, 50, null, TEST_MERCHANT_ID, "abc", key, MerchantVariableSource.MARKET);
            Assert.IsTrue(result.PageInfo.TotalCount == 0);

            result = API.SearchMerchantVariable(1, 50, null, TEST_MERCHANT_ID, packageName, "notexist", MerchantVariableSource.MARKET);
            Assert.IsTrue(result.PageInfo.TotalCount == 0);

            result = API.SearchMerchantVariable(1, 50, null, TEST_MERCHANT_ID, packageName, "pid1", MerchantVariableSource.MARKET);
            Assert.IsTrue(result.PageInfo.TotalCount == 1);
        }

        [Test]
        public void TestCreate_local_validation_fail() {
            MerchantVariableCreateRequest createRequest = new MerchantVariableCreateRequest();
            createRequest.MerchantId = TEST_MERCHANT_ID;
            Result<string> result = API.CreateMerchantVariable(createRequest);
            Assert.IsTrue(result.BusinessCode == -1);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
        }

        [Test]
        public void TestCrud_remote_validation_fail() {
            MerchantVariableCreateRequest createRequest = new MerchantVariableCreateRequest();
            List<ParameterVariable> parameterVariables = new List<ParameterVariable>();
            ParameterVariable p1 = new ParameterVariable();
            p1.Type = "T";
            p1.PackageName = "com.test";
            p1.Remarks = "remark";
            p1.Key = "testparameter";
            p1.Value = "value";
            parameterVariables.Add(p1);
            createRequest.VariableList = parameterVariables;

            ParameterVariable p2 = new ParameterVariable();
            p1.Type = "P";
            p1.PackageName = "com.test";
            p1.Remarks = "remark";
            p1.Key = "testparameter";
            p1.Value = "value";
            parameterVariables.Add(p1);
            createRequest.VariableList = parameterVariables;


            createRequest.MerchantId = 1;
            Result<string> result = API.CreateMerchantVariable(createRequest);
            Assert.IsTrue(result.BusinessCode == 1720);
   

            createRequest.MerchantId = TEST_MERCHANT_ID;
            result = API.CreateMerchantVariable(createRequest);
            Assert.IsTrue(result.BusinessCode == 1000);

            p1.PackageName = "com.pax.android.pax";
            result = API.CreateMerchantVariable(createRequest);
            Assert.IsTrue(result.BusinessCode == 0);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
        }

        [Test]
        public void TestCrud_success() {
            string packageName = "com.pax.android.pax";
            MerchantVariableCreateRequest createRequest = new MerchantVariableCreateRequest();
            createRequest.MerchantId = TEST_MERCHANT_ID;
            List<ParameterVariable> parameterVariables = new List<ParameterVariable>();
            ParameterVariable p0 = new ParameterVariable();

            p0.Type = "T";
            p0.PackageName = packageName;
            p0.Remarks = "remark";
            p0.Key = "testparameter_text_0";
            p0.Value = "value";
            parameterVariables.Add(p0);

            ParameterVariable p1 = new ParameterVariable();
            p1.Type = "T";
            p1.PackageName = packageName;
            p1.Remarks = "remark";
            p1.Key = "testparameter_text_1";
            p1.Value = "value";
            parameterVariables.Add(p1);

            ParameterVariable p2 = new ParameterVariable();
            p2.Type = "P";
            p2.PackageName = packageName;
            p2.Remarks = "remark";
            p2.Key = "testparameter_pwd";
            p2.Value = "pwd";
            parameterVariables.Add(p2);
            createRequest.VariableList = parameterVariables;

            //创建
            Result<string> result = API.CreateMerchantVariable(createRequest);
            Assert.IsTrue(result.BusinessCode == 0);

            //指定key搜索
            Result<MerchantVariable> searchResult = API.SearchMerchantVariable(1, 50, MerchantVariableSearchOrderBy.CREATE_DATE_DESC, TEST_MERCHANT_ID, packageName, "testparameter_text_0", null);
            Assert.IsTrue(searchResult.PageInfo.TotalCount == 1);
            Assert.IsTrue(searchResult.PageInfo.DataSet[0].Value.Equals("value"));

            //更新
            _logger.Debug("Start update");
            MerchantVariableUpdateRequest updateRequest = new MerchantVariableUpdateRequest();
            updateRequest.Type = "T";
            updateRequest.PackageName = packageName;
            updateRequest.Remarks = "remark";
            updateRequest.Key = "testparameter_text_0";
            updateRequest.Value = "value_2";
            result = API.UpdateMerchantVariable(searchResult.PageInfo.DataSet[0].ID, updateRequest);
            Assert.IsTrue(result.BusinessCode == 0);

            //检查更新是否成功
            _logger.Debug("Start verify update");
            searchResult = API.SearchMerchantVariable(1, 50, MerchantVariableSearchOrderBy.CREATE_DATE_DESC, TEST_MERCHANT_ID, packageName, "testparameter_text_0", null);
            Assert.IsTrue(searchResult.PageInfo.TotalCount == 1);
            Assert.IsTrue(searchResult.PageInfo.DataSet[0].Value.Equals("value_2"));

            _logger.Debug("Start search by key testparameter_text");
            //按key模糊搜索
            searchResult = API.SearchMerchantVariable(1, 50, MerchantVariableSearchOrderBy.CREATE_DATE_DESC, TEST_MERCHANT_ID, packageName, "testparameter_text", null);
            Assert.IsTrue(searchResult.PageInfo.TotalCount == 2);

            _logger.Debug("Start batch delete");
            MerchantVariableDeleteRequest batchDeleteRequest = new MerchantVariableDeleteRequest();
            List<long> variableIds = new List<long>();
            for (int i = 0; i < 2; i++) {
                variableIds.Add(searchResult.PageInfo.DataSet[i].ID);
            }
            batchDeleteRequest.VariableIds = variableIds;
            result = API.BatchDeleteMerchantVariable(batchDeleteRequest);
            Assert.IsTrue(result.BusinessCode == 0);

            _logger.Debug("Start verify batch delete");
            searchResult = API.SearchMerchantVariable(1, 50, MerchantVariableSearchOrderBy.CREATE_DATE_DESC, TEST_MERCHANT_ID, packageName, "testparameter_text", null);
            Assert.IsTrue(searchResult.PageInfo.TotalCount == 0);

            _logger.Debug("Start search another parameter");
            searchResult = API.SearchMerchantVariable(1, 50, MerchantVariableSearchOrderBy.CREATE_DATE_DESC, TEST_MERCHANT_ID, packageName, "testparameter_pwd", null);
            Assert.IsTrue(searchResult.PageInfo.TotalCount == 1);

            _logger.Debug("Start delete by id");
            result = API.DeleteMerchantVariable(searchResult.PageInfo.DataSet[0].ID);
            Assert.IsTrue(result.BusinessCode == 0);

            _logger.Debug("Start verify delete by id");
            searchResult = API.SearchMerchantVariable(1, 50, MerchantVariableSearchOrderBy.CREATE_DATE_DESC, TEST_MERCHANT_ID, packageName, "testparameter_pwd", null);
            Assert.IsTrue(searchResult.PageInfo.TotalCount == 0);

        }

        [Test]
        public void test() {
            string packageName = "com.pax.android.pax";

            Result<MerchantVariable>  searchResult = API.SearchMerchantVariable(1, 50, MerchantVariableSearchOrderBy.CREATE_DATE_DESC, TEST_MERCHANT_ID, packageName, "testparameter_text_0", null);
            Assert.IsTrue(searchResult.PageInfo.TotalCount == 1);
            Assert.IsTrue(searchResult.PageInfo.DataSet[0].Value.Equals("value"));

            MerchantVariableUpdateRequest updateRequest = new MerchantVariableUpdateRequest();
            updateRequest.Type = "T";
            updateRequest.PackageName = packageName;
            updateRequest.Remarks = "remark";
            updateRequest.Key = "testparameter_text_0";
            updateRequest.Value = "value_2";
            Result<string> result = API.UpdateMerchantVariable(searchResult.PageInfo.DataSet[0].ID, updateRequest);
            Assert.IsTrue(result.BusinessCode == 0);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
        }
    }
}
