using Newtonsoft.Json;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using Serilog;
using System.Collections.Generic;

namespace Paxstore.Test
{
    [TestFixture()]
    class TestTerminalGroupApi : BaseTest
    {
        public static TerminalGroupApi API = new TerminalGroupApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);

        [Test]
        public void TestSearchGroup() {
            //Result<TerminalGroup> result = API.SearchTerminalGroup(1, 10, null, "cc", null, null, null, true, true);
            Result<TerminalGroup> result = API.SearchTerminalGroup(1, 10, TerminalGroupSearchOrderBy.CreatedDate_asc,
                                                        TerminalGroupStatus.ACTIVE, null, null, null, true);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestGetGroup() {
            Result<TerminalGroup> result = API.GetTerminalGroup(1);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestCreateGroup() {
            CreateTerminalGroupRequest request = new CreateTerminalGroupRequest();
            request.Description = "test group";
            request.Name = "testgroup6";
            request.ModelName = "A920";
            request.ResellerName = "reseller a";
            Result<TerminalGroup>  result = API.CreateTerminalGroup(request);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestSearchTerminal()
        {
            Result<Terminal> result = API.SearchTerminal(1, 10, TerminalSearchOrderBy.TID, null,
                                             null, null, null, null, null);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);

        }

        [Test]
        public void TestUpdateTerminalGroup() {
            UpdateTerminalGroupRequest request = new UpdateTerminalGroupRequest();
            request.Name = "test group a";
            request.ResellerName = "reseller a";
            request.ModelName = "A920";

            Result<TerminalGroup> result = API.UpdateTerminalGroup(1, request);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestActivateGroup() {
            Result<string>  result = API.ActiveGroup(1);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestDisableGroup() {
            Result<string> result = API.DisableGroup(1);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestDeleteGroup() {
            Result<string> result = API.DeleteGroup(4);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestSearchTerminalsInGroup() {
            Result<Terminal> result = API.SearchTerminalsInGroup(1, 10, TerminalSearchOrderBy.Name,
                                                           1, null, null);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestAddTerminalToGroup() {
            HashSet<long> terminalIds = new HashSet<long>();
            terminalIds.Add(1000002164);
            Result<string>  result = API.AddTerminalToGroup(1, terminalIds);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestRemoveTerminalOutGroup() {
            HashSet<long> terminalIds = new HashSet<long>();
            terminalIds.Add(1000002164);
            Result<string>  result = API.RemoveTerminalOutGroup(1, terminalIds);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }


    }
}
