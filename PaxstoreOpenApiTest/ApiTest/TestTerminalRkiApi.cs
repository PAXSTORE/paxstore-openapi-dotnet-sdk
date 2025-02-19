using Newtonsoft.Json;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using Serilog;
using System;


namespace Paxstore.Test
{

    [TestFixture()]
    class TestTerminalRkiApi : BaseTest
    {
        public static TerminalRkiApi API = new TerminalRkiApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);
        public static TerminalApi TerminalAPI = new TerminalApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);

        [Test]
        public void TestPushRkiKey() {
            Result<Terminal> terminals = TerminalAPI.SearchTerminal(1, 1, TerminalSearchOrderBy.SerialNo, TerminalStatus.Active, null);
            Assert.AreEqual(terminals.BusinessCode, 0);
            Assert.IsTrue(terminals.PageInfo.TotalCount > 1);
            string sn = terminals.PageInfo.DataSet[0].SerialNo;
            PushRki2TerminalRequest request = new PushRki2TerminalRequest();
            request.SerialNo = sn;
            request.RkiKey = "acd";
            request.EffectiveTime = DateTime.Now.AddDays(1);
            Result<TerminalRkiTaskInfo> result = API.PushRkiKey2Terminal(request);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestSearchTask() {
            Result<TerminalRkiTaskInfo>  result = API.SearchPushRkiTasks(1, 10, SearchOrderBy.CreatedDate_asc,
                                                                   "ddd", null, PushStatus.All);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
        }

        [Test]
        public void TestGetPushRkiTask() {
            Result<TerminalRkiTaskInfo>  result = API.GetPushRkiTask(100);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
        }

        [Test]
        public void TestDisableRkiTask() {
            DisablePushRkiTaskRequest request = new DisablePushRkiTaskRequest();
            request.RkiKey = "acd";
            request.SerialNo = "232320";
            Result<string> result = API.DisablePushRkiTask(request);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
        }
    }
}
