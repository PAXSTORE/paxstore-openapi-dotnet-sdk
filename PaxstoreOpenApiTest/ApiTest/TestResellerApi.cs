
using log4net;
using Newtonsoft.Json;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using System;
using System.Globalization;
using System.Threading;

namespace Paxstore.Test
{
    [TestFixture()]
    public class TestResellerApi
    {
        private static ILog _logger = LogManager.GetLogger(typeof(TestResellerApi));
        public static ResellerApi API = new ResellerApi (TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);



        [Test()]
        public void TestSearchResellerInvalidPageNo()
        {
            Result<PagedReseller> result = API.SearchReseller(-1, 3000, ResellerSearchOrderBy.Name, null, ResellerStatus.All);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.IsTrue(result.BusinessCode == -1);
        }

        [Test()]
        public void TestSearchReseller(){
            Result<PagedReseller> result = API.SearchReseller(1, 10, ResellerSearchOrderBy.Name, null, ResellerStatus.All);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.IsTrue(result.BusinessCode == 0);
            Assert.IsTrue(result.PageInfo.TotalCount > 0);
        }

        [Test()]
        public void TestGetResellerInvalidId(){
            Result<Reseller> result = API.GetReseller(-1);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(-1, result.BusinessCode);
        }

        [Test()]
        public void TestGetResellerCorrect() {
            Result<Reseller> result = API.GetReseller(1000001975);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(0, result.BusinessCode);
        }

        [Test()]
        public void TestGetResellerNotExist() {
            Result<Reseller> result = API.GetReseller(1000);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(1759, result.BusinessCode);
        }




        [Test()]
        public void TestCreateNullReseller() {
            Result<Reseller> result = API.CreateReseller(null);
            Console.WriteLine(JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
        }

        [Test]
        public void TestCreateInvalidReseller() {

            ResellerCreateRequest request = new ResellerCreateRequest();
            request.Name = "TestReseller";
            request.Address = "suzhou";
            request.Email = "suzhou";
            request.ParentResellerName = "reseller";
            Result<Reseller> result = API.CreateReseller(request);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
        }

        [Test]
        public void TestCreateResellerSuccess()
        {
            ResellerCreateRequest request = new ResellerCreateRequest();
            request.Name = "TestResellerToDelete";
            request.Address = "suzhou";
            request.Email = "zhangsan@163.com";
            request.Country = "CN";
            request.Contact = "ZhangSan";
            request.Phone = "22323";
            request.ParentResellerName = "reseller";
            request.setActivateWhenCreate(true);
            Result<Reseller> result = API.CreateReseller(request);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestActivateResellerNotExist() {
            Result<string> result = API.ActivateReseller(1000);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 1759);
        }

        [Test]
        public void TestActivateResellerAlreadyActive()
        {
            Result<string> result = API.ActivateReseller(1000005215);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 1891);
        }

        [Test]
        public void TestActivateResellerSuccess()
        {
            Result<string> result = API.ActivateReseller(1000005215);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestDisableReseller()
        {
            Result<string> result = API.DisableReseller(1000005215);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestDeleteReseller() {
            Result<string> result = API.DeleteReseller(1000005213);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestReplaceResellerEmail() {
            Result<string> result = API.ReplaceResellerEmail(1000005215, "zhangsan@pax.com");
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }


        [Test]
        public void TestCreateUpdateActivateDisableDelete() {
            ResellerCreateRequest request = new ResellerCreateRequest();
            request.Name = "Reseller For Test5";
            request.Address = "suzhou";
            request.Email = "zhangsan@163.com";
            request.Country = "CN";
            request.Contact = "ZhangSan";
            request.Phone = "88889999";
            Result<Reseller> createResult = API.CreateReseller(request);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(createResult));
            Assert.AreEqual(createResult.BusinessCode, 0);

            Result<PagedReseller> searchResultAfterCreate = API.SearchReseller(1, 10, ResellerSearchOrderBy.Name, "Reseller For Test", ResellerStatus.All);
            _logger.DebugFormat("Search Result After Create=\n{0}", JsonConvert.SerializeObject(searchResultAfterCreate));
            Assert.IsTrue(searchResultAfterCreate.BusinessCode == 0);
            Assert.IsTrue(searchResultAfterCreate.PageInfo.TotalCount > 0);

            long resellerId = createResult.Data.ID;


            ResellerUpdateRequest updateRequest = new ResellerUpdateRequest();
            updateRequest.Name = "Reseller For Test5";
            updateRequest.Address = "suzhou2";
            updateRequest.Email = "zhangsan@163.com";
            updateRequest.Country = "CN";
            updateRequest.Contact = "ZhangSan2";
            updateRequest.Phone = "44445555";
            updateRequest.ParentResellerName = "GLOBAL";
            Result<Reseller> updateResult = API.UpdateReseller(resellerId, updateRequest);
            _logger.DebugFormat("Update Result=\n{0}", JsonConvert.SerializeObject(updateResult));
            Assert.AreEqual(updateResult.BusinessCode, 0);
            Assert.AreEqual("suzhou2", updateResult.Data.Address);
            Assert.AreEqual("ZhangSan2", updateResult.Data.Contact);
            Assert.AreEqual("44445555", updateResult.Data.Phone);

            Result<string> activateResult = API.ActivateReseller(resellerId);
            _logger.DebugFormat("Activate Result=\n{0}", JsonConvert.SerializeObject(activateResult));
            Assert.AreEqual(activateResult.BusinessCode, 0);

            Result<string> replaceEmailResult = API.ReplaceResellerEmail(resellerId, "tan@pax.com");
            _logger.DebugFormat("Replace Email Result=\n{0}", JsonConvert.SerializeObject(replaceEmailResult));
            Assert.AreEqual(replaceEmailResult.BusinessCode, 0);

            Result<string> disableResult = API.DisableReseller(resellerId);
            _logger.DebugFormat("Disable Result=\n{0}", JsonConvert.SerializeObject(disableResult));
            Assert.AreEqual(disableResult.BusinessCode, 0);

            Result<string> deleteResult = API.DeleteReseller(resellerId);
            _logger.DebugFormat("Delete Result=\n{0}", JsonConvert.SerializeObject(deleteResult));
            Assert.AreEqual(deleteResult.BusinessCode, 0);


            Result<PagedReseller> searchResult = API.SearchReseller(1, 10, ResellerSearchOrderBy.Name, "Reseller For Test5", ResellerStatus.All);
            _logger.DebugFormat("Search Result After Delete=\n{0}", JsonConvert.SerializeObject(searchResult));
            Assert.IsTrue(searchResult.BusinessCode == 0);
            Assert.IsTrue(searchResult.PageInfo.TotalCount == 0);


        }

        [Test]
        public void TestUrl() {
            string baseUrl = "http://localhost:8080/p-market-api/";
            if (baseUrl != null && baseUrl.EndsWith("/"))
            {
                baseUrl = baseUrl.Remove(baseUrl.Length-1);
                Console.WriteLine(baseUrl);
            }

            Assert.IsTrue(baseUrl.Equals("http://localhost:8080/p-market-api"));
        }

    }
}