using Newtonsoft.Json;
using NUnit.Framework;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using Serilog;
using System.Collections.Generic;

namespace Paxstore.Test
{
    [TestFixture()]
    class TestMerchantCategoryApi : BaseTest
    {
        public static MerchantCategoryApi API = new MerchantCategoryApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);

        [Test]
        public void TestGetCategory() {
            Result<List<MerchantCategory>> result = API.GetMerchantCategories("Fast food");
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestCreateNullCategory() {
            Result<MerchantCategory> result = API.CreateMerchantCategory(null);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
        }

        [Test]
        public void TestCreateInvalidCategory() {
            MerchantCategoryCreateRequest request = new MerchantCategoryCreateRequest();
            Result<MerchantCategory> result = API.CreateMerchantCategory(request);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
        }

        [Test]
        public void TestUpdateCategoryInvalidId() {
            MerchantCategoryUpdateRequest request = new MerchantCategoryUpdateRequest();
            Result<MerchantCategory> result = API.UpdateMerchantCategory(0, request);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, -1);
        }

        [Test]
        public void TestDeleteCategoryNotExist() {
            Result<string> result = API.DeleteMerchantCategory(10);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 16000);
        }

        [Test]
        public void TestCreateUpdateDelete() {
            //start to create merchant category
            MerchantCategoryCreateRequest createRequest = new MerchantCategoryCreateRequest();
            createRequest.Name = "test";
            createRequest.Remarks = "testdesc";
            Result<MerchantCategory> createResult = API.CreateMerchantCategory(createRequest);
            Log.Debug("Create Result=\n{0}", JsonConvert.SerializeObject(createResult));
            Assert.AreEqual(createResult.BusinessCode, 0);
            long categoryId = createResult.Data.ID;

            //start to update
            MerchantCategoryUpdateRequest updateRequest = new MerchantCategoryUpdateRequest();
            updateRequest.Name = "test2";
            updateRequest.Remarks = "test2desc";

            Result<MerchantCategory> updateResult = API.UpdateMerchantCategory(categoryId, updateRequest);
            Log.Debug("Update Result=\n{0}", JsonConvert.SerializeObject(updateResult));
            Assert.AreEqual(updateResult.BusinessCode, 0);
            Assert.AreEqual("test2", updateResult.Data.Name);
            Assert.AreEqual("test2desc", updateResult.Data.Remarks);

            Result<string> deleteResult = API.DeleteMerchantCategory(categoryId);
            Log.Debug("Delete Result=\n{0}", JsonConvert.SerializeObject(deleteResult));
            Assert.AreEqual(deleteResult.BusinessCode, 0);

        }

        [Test]
        public void TestBatchCreate() {
            List<MerchantCategoryCreateRequest> createList = new List<MerchantCategoryCreateRequest>();
            MerchantCategoryCreateRequest createRequest1 = new MerchantCategoryCreateRequest();
            createRequest1.Name = "test_1";
            createRequest1.Remarks = "testdesc_1";

            MerchantCategoryCreateRequest createRequest2 = new MerchantCategoryCreateRequest();
            createRequest2.Name = "test_2";
            createRequest2.Remarks = "testdesc_2";

            MerchantCategoryCreateRequest createRequest3 = new MerchantCategoryCreateRequest();
            createRequest3.Name = "test_3";
            createRequest3.Remarks = "testdesc_3";
            createList.Add(createRequest1);
            createList.Add(createRequest2);
            createList.Add(createRequest3);
            Result<List<MerchantCategory>> result = API.BatchCreateMerchantCategory(createList, true);
            Log.Debug("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }


    }
}
