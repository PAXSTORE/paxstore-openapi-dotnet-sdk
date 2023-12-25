using Newtonsoft.Json;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using Serilog;
using System;

namespace Paxstore.Test
{
    [TestFixture()]
    class TestTerminalFirmwareApi : BaseTest
    {
        public static TerminalFirmwareApi API = new TerminalFirmwareApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);

        [Test]
        public void TestPushFirmware2Terminal() {
            Result<PushFirmwareTaskInfo> result = API.PushFirmware2TerminalBySnAndFirmwareName(null, null);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
            Result<PushFirmwareTaskInfo> result3 = API.PushFirmware2TerminalBySnAndFirmwareName("0820534733", "badfe");
            Log.Debug("Result3=\n{0}", JsonConvert.SerializeObject(result3));
            Assert.AreEqual(result3.BusinessCode, 2034);

            API.PushFirmware2TerminalBySnAndFirmwareName("0820534733", "firmware", true, null, DateTime.Now.AddHours(3));

        }

        [Test]
        public void TestSearchPushFirmwareTask() {
            Result<PushFirmwareTaskInfo> result = API.SearchPushFirmwareTasks(1, 10, SearchOrderBy.CreatedDate_desc, "I1TF6LA2", "PayDroid_5.1.1_Aquarius_V02.3.15_20181012", PushStatus.All);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
        }

        [Test]
        public void TestGetPushFirmwareTask() {
            Result<PushFirmwareTaskInfo> result = API.GetPushFirmwareTask(1000012895);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
        }

        [Test]
        public void TestSuspendPushFirmwareTask()
        {
            Result<string> result = API.DisablePushFirmwareTaskBySnAndFirmwareName("0820881219", "PayDroid_5.1.1_Aquarius_V02.3.15_20181012");
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));

            Result<string> result2 = API.DisablePushFirmwareTaskByTidAndFirmwareName("I1TF6LA2", "PayDroid_5.1.1_Aquarius_V02.3.15_20181012");
            Log.Debug("Result2=\n{0}", JsonConvert.SerializeObject(result2));
        }


    }
}
