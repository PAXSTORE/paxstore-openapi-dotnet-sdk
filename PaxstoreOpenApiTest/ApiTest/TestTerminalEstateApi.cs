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
    class TestTerminalEstateApi
    {
        private static ILog _logger = LogManager.GetLogger(typeof(TestTerminalEstateApi));
        public static TerminalEstateApi API = new TerminalEstateApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);

        [Test]
        public void testVerifyTerminalEstate() {
            Result<string> result = API.VerifyTerminalEstate("sn456789");

            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.IsTrue(result.BusinessCode == 0);
        }
    }
}
