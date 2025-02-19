using Newtonsoft.Json;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using Serilog;


namespace Paxstore.Test
{
    [TestFixture()]
    class TestMerchantApi : BaseTest
    {
        public static MerchantApi API = new MerchantApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);

        [Test]
        public void TestSearchMerchantAll() {
            Result<PagedMerchant> result = API.SearchMerchant(1, 10, MerchantSearchOrderBy.Name, "haoxy_test", MerchantStatus.All);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestGetMerchantInvalidId() {
            Result<Merchant> result = API.GetMerchant(0);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
        }

        [Test]
        public void TestGetMerchantNotExist() {
            Result<Merchant> result = API.GetMerchant(1);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 1720);
        }

        [Test]
        public void TestCreateMerchantNull() {
            Result<Merchant> result = API.CreateMerchant(null);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
        }

        [Test]
        public void TestCreateMerchantInvalid() {
            MerchantCreateRequest merchantCreateRequest = new MerchantCreateRequest();
            Result<Merchant> result = API.CreateMerchant(merchantCreateRequest);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
        }

     




        [Test]
        public void TestCreateUpdateActiveDisableDelete() {
            MerchantCreateRequest merchantCreateRequest = new MerchantCreateRequest();
            merchantCreateRequest.Name = "好人民间2";
            merchantCreateRequest.Email = "haoren@163.com";
            merchantCreateRequest.ResellerName = "pax";
            merchantCreateRequest.Contact = "haoren";
            merchantCreateRequest.Country = "CN";
            merchantCreateRequest.Description = "商户好人民间";
            merchantCreateRequest.Phone = "0512-59564515";
            Result<Merchant> result = API.CreateMerchant(merchantCreateRequest);
            Log.Debug("Create Merchant Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
            long merchantId = result.Data.ID;

            MerchantUpdateRequest merchantUpdateRequest = new MerchantUpdateRequest();
            merchantUpdateRequest.Name = "好人民间2";
            //merchantUpdateRequest.Email = "haoren2@163.com";
            //merchantUpdateRequest.ResellerName = "Jesse";
            merchantUpdateRequest.Contact = "haoren2";
            merchantUpdateRequest.Country = "CN";
            merchantUpdateRequest.Description = "商户好人民间";
            merchantUpdateRequest.Phone = "0512-88889999";
            Result<Merchant> updateResult = API.UpdateMerchant(merchantId, merchantUpdateRequest);
            Log.Debug("Update Merchant Result=\n{0}", JsonConvert.SerializeObject(updateResult));
            Assert.AreEqual(updateResult.BusinessCode, 0);

            //Assert.AreEqual(updateResult.Data.Contact, "haoren2");
            //Assert.AreEqual(updateResult.Data.Phone, "0512-88889999");
      

            //Result<string> activateResult = API.ActivateMerchant(merchantId);
            //_logger.DebugFormat("Activate Merchant Result=\n{0}", JsonConvert.SerializeObject(activateResult));
            //Assert.AreEqual(activateResult.BusinessCode, 0);

            //Result<string> replaceEmailResult = API.ReplaceMerchantEmail(merchantId, "tan@pax.com", true);
            //_logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(replaceEmailResult));
            //Assert.AreEqual(replaceEmailResult.BusinessCode, 0);

            //Result<string> disableResult = API.DisableMerchant(merchantId);
            //_logger.DebugFormat("DisableResult Merchant Result=\n{0}", JsonConvert.SerializeObject(disableResult));
            //Assert.AreEqual(disableResult.BusinessCode, 0);

            //Result<string> deleteResult = API.DeleteMerchant(merchantId);
            //_logger.DebugFormat("Delete Merchant Result=\n{0}", JsonConvert.SerializeObject(deleteResult));
            //Assert.AreEqual(deleteResult.BusinessCode, 0);
        }

        [Test]
        public void TestUpdateMerchantInvalid() {
            MerchantUpdateRequest merchantUpdateRequest = new MerchantUpdateRequest();
            merchantUpdateRequest.Name = "好人民间";
            merchantUpdateRequest.Email = "haoren2@163.com";
            merchantUpdateRequest.ResellerName = "Pine Labs";
            merchantUpdateRequest.Contact = "haoren2";
            //merchantUpdateRequest.Country = "CN";
            merchantUpdateRequest.Description = "商户好人民间";
            merchantUpdateRequest.Phone = "0512-88889999";
            Result<Merchant> updateResult = API.UpdateMerchant(1000000, merchantUpdateRequest);
            Log.Debug("Update Merchant Result=\n{0}", JsonConvert.SerializeObject(updateResult));
            Assert.AreEqual(updateResult.BusinessCode, -1);
        }

    }
}
