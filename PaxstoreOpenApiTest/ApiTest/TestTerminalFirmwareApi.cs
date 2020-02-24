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
            Result<PushFirmwareTaskInfo> result = API.PushFirmware2TerminalBySnAndFirmwareName(null, null);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);


            _logger.DebugFormat("==================================");
            Result<PushFirmwareTaskInfo> result3 = API.PushFirmware2TerminalBySnAndFirmwareName("0820534733", "badfe");
            _logger.DebugFormat("Result3=\n{0}", JsonConvert.SerializeObject(result3));
            Assert.AreEqual(result3.BusinessCode, 2034);

        }

        [Test]
        public void TestSearchPushFirmwareTask() {
            Result<PushFirmwareTaskInfo> result = API.SearchPushFirmwareTasks(1, 10, SearchOrderBy.CreatedDate_desc, "I1TF6LA2", "PayDroid_5.1.1_Aquarius_V02.3.15_20181012", PushStatus.All);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
        }

        [Test]
        public void TestGetPushFirmwareTask() {
            Result<PushFirmwareTaskInfo> result = API.GetPushFirmwareTask(1000012895);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
        }

        [Test]
        public void TestSuspendPushFirmwareTask()
        {
            Result<string> result = API.DisablePushFirmwareTaskBySnAndFirmwareName("0820881219", "PayDroid_5.1.1_Aquarius_V02.3.15_20181012");
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));

            Result<string> result2 = API.DisablePushFirmwareTaskByTidAndFirmwareName("I1TF6LA2", "PayDroid_5.1.1_Aquarius_V02.3.15_20181012");
            _logger.DebugFormat("Result2=\n{0}", JsonConvert.SerializeObject(result2));
        }


    }
}
