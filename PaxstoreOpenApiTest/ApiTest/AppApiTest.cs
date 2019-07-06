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
    public class AppApiTest
    {
        private static ILog _logger = LogManager.GetLogger(typeof(TestTerminalApi));
        public static AppApi API = new AppApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);

        [Test]
        public void TestSearchApp() {
            Result<PagedApp> result = API.SearchApp(1, 10, null, null, null, null,
            null,
            null,
            null,
            false,
            false);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
        }


    }
}
