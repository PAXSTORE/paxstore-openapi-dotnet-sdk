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
    class TestTerminalRkiApi
    {
        private static ILog _logger = LogManager.GetLogger(typeof(TestResellerApi));
        public static TerminalRkiApi API = new TerminalRkiApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);

        [Test]
        public void TestPushRkiKey() {
            PushRki2TerminalRequest request = new PushRki2TerminalRequest();
            request.SerialNo = "TEST999";
            request.RkiKey = "acd";
            Result<TerminalRkiTaskInfo> result = API.PushRkiKey2Terminal(request);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
        }

        [Test]
        public void TestSearchTask() {
            Result<TerminalRkiTaskInfo>  result = API.SearchPushRkiTasks(1, 10, SearchOrderBy.CreatedDate_asc,
                                                                   "ddd", null, PushStatus.All);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
        }

        [Test]
        public void TestGetPushRkiTask() {
            Result<TerminalRkiTaskInfo>  result = API.GetPushRkiTask(100);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
        }

        [Test]
        public void TestDisableRkiTask() {
            DisablePushRkiTaskRequest request = new DisablePushRkiTaskRequest();
            request.RkiKey = "acd";
            request.SerialNo = "232320";
            Result<string> result = API.DisablePushRkiTask(request);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
        }
    }
}
