using Com.Pax.OpenApi.Sdk.Api;
using Com.Pax.OpenApi.Sdk.Base.Dto;
using Com.Pax.OpenApi.Sdk.Dto.Reseller;
using Newtonsoft.Json;
using System;

namespace Test.Reseller
{
    public class TestResellerApi
    {
        public static ResellerApi API = new ResellerApi ("https://api.whatspos.cn/p-market-api/", "ZJFXJAG7SJXPPESKVAPO", "AXN5ES2BFYYY8FRMSAPXKQ2ZMT22WYTQGCOGGFM9");
        public static void TestSearchReseller(){
            Result<ResellerPageDTO> result = API.SearchReseller(1, 10, ResellerSearchOrderBy.Name, null, ResellerStatus.All);
            Console.WriteLine (JsonConvert.SerializeObject(result));
        }

        public static void TestGetReseller(){
            Result<ResellerDTO> result = API.GetReseller(-1);
            Console.WriteLine (JsonConvert.SerializeObject(result));
        }

        public static void TestCreateReseller(){
            ResellerCreateRequest request = new ResellerCreateRequest();
            Result<ResellerDTO> result = API.CreateReseller(null);
            Console.WriteLine (JsonConvert.SerializeObject(result));
        }
    }
}