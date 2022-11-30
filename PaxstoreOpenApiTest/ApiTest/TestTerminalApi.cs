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
    class TestTerminalApi
    {
        private static ILog _logger = LogManager.GetLogger(typeof(TestTerminalApi));
        public static TerminalApi API = new TerminalApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);


        [Test]
        public void TestSearchTerminal() {
            Result<Terminal> result = API.SearchTerminal(1, 10, TerminalSearchOrderBy.SerialNo, TerminalStatus.All, null);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestGetTerminalInvalidId() {
            Result<Terminal> result = API.GetTerminal(0);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
        }

        [Test]
        public void TestGetTerminalNotExist() {
            Result<Terminal> result = API.GetTerminal(100);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 1801);
        }

        [Test]
        public void TestCreateTerminalNull() {
            Result<Terminal> result = API.CreateTerminal(null);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
        }

        [Test]
        public void TestCreateTerminalInvalid()
        {
            TerminalCreateRequest createRequest = new TerminalCreateRequest();
            createRequest.Name = "dddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd";
            createRequest.TID = "4578";
            Result<Terminal> result = API.CreateTerminal(createRequest);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
        }

        [Test]
        public void TestCreateTerminalInvalidStatus() {
            TerminalCreateRequest createRequest = new TerminalCreateRequest();
            createRequest.Status = "a";
            Result<Terminal> result = API.CreateTerminal(createRequest);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
        }

        [Test]
        public void TestUpdateTerminalInvalidId() {
            TerminalUpdateRequest updateRequest = new TerminalUpdateRequest();
            Result<Terminal> result = API.UpdateTerminal(0, updateRequest);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
        }

        [Test]
        public void TestUpdateTerminalNotExit() {
            TerminalUpdateRequest updateRequest = new TerminalUpdateRequest();
            updateRequest.Name = "test";
            updateRequest.Location = "beijing";
            updateRequest.MerchantName = "testmerchant";
            updateRequest.ResellerName = "testreseller";
            updateRequest.SerialNo = "sn0001";
            updateRequest.ModelName = "A920";
            Result<Terminal> result = API.UpdateTerminal(10, updateRequest);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreNotEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestCreateUpdateActivateDisableDelete() {
            //start create terminal
            TerminalCreateRequest createRequest = new TerminalCreateRequest();
            createRequest.Name = "Terminal 1";
            createRequest.ResellerName = "reseller test";
            createRequest.MerchantName = "merchant test";
            createRequest.SerialNo = "ZT0001";
            createRequest.ModelName = "A920";
            Result<Terminal> result = API.CreateTerminal(createRequest);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);

            long terminalId = result.Data.ID;


            //start to update terminal
            TerminalUpdateRequest updateRequest = new TerminalUpdateRequest();
            updateRequest.Name = "Terminal 1";
            updateRequest.Location = "Suzhou";
            updateRequest.MerchantName = "merchant test";
            updateRequest.ResellerName = "reseller test";
            updateRequest.SerialNo = "ZT0002";
            updateRequest.ModelName = "A920";
            Result<Terminal> updateResult = API.UpdateTerminal(terminalId, updateRequest);
            _logger.DebugFormat("Update Result=\n{0}", JsonConvert.SerializeObject(updateResult));
            Assert.AreEqual(updateResult.BusinessCode, 0);


            Result<string> activateResult = API.ActivateTerminal(terminalId);
            _logger.DebugFormat("Activate Result=\n{0}", JsonConvert.SerializeObject(activateResult));
            Assert.AreEqual(updateResult.BusinessCode, 0);

            Result<string> disableResult = API.DisableTerminal(terminalId);
            _logger.DebugFormat("Disable Result=\n{0}", JsonConvert.SerializeObject(disableResult));
            Assert.AreEqual(disableResult.BusinessCode, 0);

            Result<string> deleteResult = API.DeleteTerminal(terminalId);
            _logger.DebugFormat("Delete Result=\n{0}", JsonConvert.SerializeObject(deleteResult));
            Assert.AreEqual(deleteResult.BusinessCode, 0);

            Result<Terminal> terminal = API.GetTerminal(terminalId);
            _logger.DebugFormat("Get Terminal Result After Delete=\n{0}", JsonConvert.SerializeObject(terminal));
            Assert.AreEqual(terminal.BusinessCode, 1801);
        }

        [Test]
        public void TestActivateDisable()
        {
            

            Result<string> disableResult = API.DisableTerminal(1000000600);
            _logger.DebugFormat("Disable Result=\n{0}", JsonConvert.SerializeObject(disableResult));
            Assert.AreEqual(disableResult.BusinessCode, 0);

            Result<string> activateResult = API.ActivateTerminal(1000000600);
            _logger.DebugFormat("Activate Result=\n{0}", JsonConvert.SerializeObject(activateResult));
            Assert.AreEqual(activateResult.BusinessCode, 0);
        }

        [Test]
        public void TestSearchTerminalWithAddrParameters()
        {
            //API.SetReadWriteTimeoutTime(30000);
            API.SetConnectionTimeoutTime(30000);
            Result<Terminal> result = API.SearchTerminal(1, 10, TerminalSearchOrderBy.SerialNo, TerminalStatus.All, null, true, true, true);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestBatchAddTerminalToGroup() {
            TerminalGroupRequest request = new TerminalGroupRequest();
            HashSet<long> groupIds = new HashSet<long>();
            groupIds.Add(1);
            groupIds.Add(2);

            HashSet<long> terminalIds = new HashSet<long>();
            terminalIds.Add(1);
            terminalIds.Add(2);

            request.GroupIds = groupIds;
            request.TerminalIds = terminalIds;
            Result<string> result = API.BatchAddTerminalToGroup(request);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestMoveTerminal() {
            Result<string> result = API.MoveTerminal(1019920356, "Neal", "w");
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestGetTerminalConfig() {
            Result<TerminalConfig> result = API.GetTerminalConfig(1019920370);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestUpdateTerminalConfig() {
            TerminalConfigUpdateRequest terminalConfigUpdateRequest = new TerminalConfigUpdateRequest();
            terminalConfigUpdateRequest.AllowReplacement = true;
            Result<string> result = API.UpdateTerminalConfig(1019920370, terminalConfigUpdateRequest);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestGetTerminalPED() {
            Result<TerminalPED> result = API.GetTerminalPED(1019920370);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestGetTerminalWithDetail() {
            Result<Terminal> result = API.GetTerminal(1017767065, true);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
            Assert.NotNull(result.Data.TerminalDetail.PN);
        }

        [Test]
        public void TestPushCmdToTerminal() {
            Result<string> result = API.PushCmdToTerminal(1023805258, TerminalPushCmd.Unlock);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestCopy() {
            TerminalCopyRequest copyRequest = new TerminalCopyRequest();
            copyRequest.TerminalId = 1472214090121357;
            copyRequest.Name = "tanjiedezhongduan";
            copyRequest.SerialNo = "s00000001";
            copyRequest.Tid = "t010101000";
            copyRequest.Status = "A";
            Result<Terminal> result = API.CopyTerminal(copyRequest);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.IsTrue(result.BusinessCode == 0);
        }
        [Test]
        public void TestCopy_local_validation_fail() {
            Result<Terminal> result = API.CopyTerminal(null);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.IsTrue(result.BusinessCode == -1);

            TerminalCopyRequest copyRequest = new TerminalCopyRequest();
            copyRequest.Name = "fefeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee";
            copyRequest.Tid = "2";
            copyRequest.SerialNo = "fefffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff";
            result = API.CopyTerminal(copyRequest);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.IsTrue(result.BusinessCode == -1);

        }

    }

    
}
