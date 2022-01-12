using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;

namespace Paxstore.Test
{
    [TestFixture()]
    public class TestTerminalGroupRkiApi
    {
        private static ILog _logger = LogManager.GetLogger(typeof(TestTerminalGroupRkiApi));
        public static TerminalGroupRkiApi API = new TerminalGroupRkiApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);

        [Test]
        public void testSearchValidateFail() {
            Result<TerminalGroupRkiTask> result = API.SearchGroupPushRkiTask(0, 0, SearchOrderBy.CreatedDate_asc,
            1001218169, null, null, "TEMPLATE_BDK_11");
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
        }

        [Test]
        public void testPushRki2GroupValidateFail()
        {
            CreateTerminalGroupRkiTaskRequest request = new CreateTerminalGroupRkiTaskRequest();
            Result<TerminalGroupRkiTask> result = API.PushRkiKey2Group(request);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
        }

        [Test]
        public void testPushRki2Group() {
            CreateTerminalGroupRkiTaskRequest request = new CreateTerminalGroupRkiTaskRequest();
            request.GroupId = 1001218169;
            request.RkiKey = "TEMPLATE_BDK_11";
            Result<TerminalGroupRkiTask> result = API.PushRkiKey2Group(request);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void testSearch()
        {
            Result<TerminalGroupRkiTask> result = API.SearchGroupPushRkiTask(1, 10, SearchOrderBy.CreatedDate_asc,
            1001218169, null, null, "");
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void getPushRki2GroupTask() {
            Result<TerminalGroupRkiTask> result = API.GetGroupPushRkiTask(79513);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void testDisableTask() {
            Result<TerminalGroupRkiTask> result = API.DisableGroupRkiPushTask(79513);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

 
    }
}
