
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
            request.ParentResellerName = "reseller test";
            Result<Reseller> result = API.CreateReseller(request);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
        }

        [Test]
        public void TestCreateResellerSuccess()
        {
            ResellerCreateRequest request = new ResellerCreateRequest();
            request.Name = "TestResellerToDelete3";
            request.Address = "suzhou";
            request.Email = "zhangsan@163.com";
            request.Country = "CN";
            request.Contact = "ZhangSan";
            request.Phone = "22323";
            request.ParentResellerName = "reseller test";
            request.setActivateWhenCreate(false);
            Result<Reseller> result = API.CreateReseller(request);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);


            //Test get reseller by id
            long resellerId = result.Data.ID;


            ResellerUpdateRequest updateRequest = new ResellerUpdateRequest();
            updateRequest.Name = "TestResellerToDelete4";
            updateRequest.Address = "suzhou2";
            updateRequest.Email = "zhangsan@163.com";
            updateRequest.Country = "CN";
            updateRequest.Contact = "ZhangSan2";
            updateRequest.Phone = "44445555";
            updateRequest.ParentResellerName = "reseller test";
            Result<Reseller> updateResult = API.UpdateReseller(resellerId, updateRequest);
            _logger.DebugFormat("Update Result=\n{0}", JsonConvert.SerializeObject(updateResult));
            Assert.AreEqual(updateResult.BusinessCode, 0);
            Assert.AreEqual("suzhou2", updateResult.Data.Address);
            Assert.AreEqual("ZhangSan2", updateResult.Data.Contact);
            Assert.AreEqual("44445555", updateResult.Data.Phone);



            Result<Reseller> getResellerResult = API.GetReseller(resellerId);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(getResellerResult));
            Assert.AreEqual(0, getResellerResult.BusinessCode);

            //Test activate reseller
            Result<string> activateResellerResult = API.ActivateReseller(resellerId);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(activateResellerResult));
            Assert.AreEqual(activateResellerResult.BusinessCode, 0);


            Result<string> replaceEmailResult = API.ReplaceResellerEmail(resellerId, "zhangsan@pax.com");
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(replaceEmailResult));
            Assert.AreEqual(replaceEmailResult.BusinessCode, 0);

            //Test activate reseller already active
            Result<string> activateResellerResult2 = API.ActivateReseller(resellerId);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(activateResellerResult2));
            Assert.AreEqual(activateResellerResult2.BusinessCode, 1891);


            //Test disable reseller
            Result<string> disableResellerResult = API.DisableReseller(resellerId);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(disableResellerResult));
            Assert.AreEqual(disableResellerResult.BusinessCode, 0);

            //Test delete
            Result<string> deleteResellerResult = API.DeleteReseller(resellerId);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(deleteResellerResult));
            Assert.AreEqual(deleteResellerResult.BusinessCode, 0);

        }

        [Test]
        public void TestActivateResellerNotExist() {
            Result<string> result = API.ActivateReseller(1000);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 1759);
        }

        [Test]
        public void TestSearchResellerRkiKeyList() {
            Result<ResellerRkiKeyInfo> result = API.SearchResellerRkiKeyList(1, 1, 20, null);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
        }

    }
}