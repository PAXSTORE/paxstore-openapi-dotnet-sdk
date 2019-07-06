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
    class TestTerminalFirmwareApi
    {
        private static ILog _logger = LogManager.GetLogger(typeof(TestTerminalApi));
        public static TerminalFirmwareApi API = new TerminalFirmwareApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);

        [Test]
        public void TestPushFirmware2Terminal() {
            Result<string> result = API.PushFirmware2TerminalBySnAndFirmwareName(null, null);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);



            API.SetConnectionTimeoutTime(1);
            Result<string> result2 = API.PushFirmware2TerminalBySnAndFirmwareName("0820534733", "badfe");
            _logger.DebugFormat("Result2=\n{0}", JsonConvert.SerializeObject(result2));
            Assert.AreEqual(result2.BusinessCode, 16000);

            _logger.DebugFormat("==================================");
            API.SetConnectionTimeoutTime(5000);
            API.SetReadWriteTimeoutTime(1);
            Result<string> result3 = API.PushFirmware2TerminalBySnAndFirmwareName("0820534733", "badfe");
            _logger.DebugFormat("Result3=\n{0}", JsonConvert.SerializeObject(result3));
            Assert.AreEqual(result3.BusinessCode, 2034);

        }
    }
}
