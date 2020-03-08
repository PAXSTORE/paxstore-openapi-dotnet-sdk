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
using static Paxstore.OpenApi.EntityAttributeApi;

namespace Paxstore.Test
{
    [TestFixture()]
    class TestEntityAttributeApi
    {
        private static ILog _logger = LogManager.GetLogger(typeof(TestTerminalApi));
        public static EntityAttributeApi API = new EntityAttributeApi(TestConst.API_BASE_URL, TestConst.API_KEY, TestConst.API_SECRET);

        [Test]
        public void TestGetEntityAttributeById() {
            Result<EntityAttribute> result = API.GetEntityAttribute(1);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestSearchEntityAttribute() {
            Result<EntityAttribute> result = API.SearchEntityAttributes(1, 10, EntityAttributeSearchOrderBy.EntityType_asc, null, null);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestCreateEntityAttribute() {
            EntityAttributeCreateRequest request = new EntityAttributeCreateRequest();
            request.EntityType = EntityAttributeType.Reseller;
            request.Key = "test";
            request.MaxLength = 12;
            request.MinLength = 1;
            request.Required = false;
            request.InputType = EntityAttributeInputType.Text;
            request.DefaultLabel = "test";

            Result<EntityAttribute> result = API.CreateEntityAttribute(request);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestUpdateEntityAttribute() {
            EntityAttributeUpdateRequest request = new EntityAttributeUpdateRequest();
            request.InputType = EntityAttributeInputType.Text;
            request.MaxLength = 20;
            request.MinLength = 10;
            request.DefaultLabel = "ccdd";
            request.Regex = "fefe";
            Result<EntityAttribute> result = API.UpdateEntityAttribute(4, request);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }

        [Test]
        public void TestUpdateEntityAttributeLabel() {
            EntityAttributeLabelUpdateRequest request = new EntityAttributeLabelUpdateRequest();
            EntityAttributeLabelInfo label = new EntityAttributeLabelInfo();
            label.Label = "cn";
            label.Locale = "zh_CN";
            List<EntityAttributeLabelInfo> labels = new List<EntityAttributeLabelInfo>();
            labels.Add(label);

            EntityAttributeLabelInfo label2 = new EntityAttributeLabelInfo();
            label2.Label = "en";
            label2.Locale = "en";
            labels.Add(label2);
            request.EntityAttributeLabelList = labels;
            Result<string>  result = API.UpdateEntityAttributeLabel(4, request);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);

        }

        [Test]
        public void TestDeleteEntityAttribute() {
            Result<string>  result = API.DeleteEntityAttribute(4);
            _logger.DebugFormat("Result=\n{0}", JsonConvert.SerializeObject(result));
            Assert.AreEqual(result.BusinessCode, 0);
        }


    }
}
