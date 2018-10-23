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
            createRequest.ResellerName = "GLOBAL";
            createRequest.MerchantName = "JD";
            createRequest.SerialNo = "sn021215";
            createRequest.ModelName = "A920";
            Result<Terminal> result = API.CreateTerminal(createRequest);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);

            long terminalId = result.Data.ID;


            //start to update terminal
            TerminalUpdateRequest updateRequest = new TerminalUpdateRequest();
            updateRequest.Name = "Terminal 1";
            updateRequest.Location = "Suzhou";
            updateRequest.MerchantName = "JD";
            updateRequest.ResellerName = "GLOBAL";
            updateRequest.SerialNo = "sn021215";
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


    }
}
