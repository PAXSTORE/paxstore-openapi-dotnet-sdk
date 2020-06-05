using Newtonsoft.Json;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Help;
using Paxstore.OpenApi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paxstore.OpenApi
{
    public class GoInsightApi: BaseApi
    {

        private const int QUERY_CODE_LENGTH = 8;

        private const string SEARCH_GO_INSIGHT_DATA_URL = "/v1/3rdsys/goInsight/data/querying/{queryCode}";

        public GoInsightApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo) : base(baseUrl, apiKey, apiSecret, timeZoneInfo)
        {

        }

        public GoInsightApi(string baseUrl, string apiKey, string apiSecret) : base(baseUrl, apiKey, apiSecret)
        {

        }

        public Result<DataQueryResult> FindDataFromInsight(string queryCode)
        {
            return FindDataFromInsight(queryCode, null, null, null);
        }

        public Result<DataQueryResult> FindDataFromInsight(string queryCode, TimeRange rangeType)
        {
            return FindDataFromInsight(queryCode, rangeType, null, null);
        }

        public Result<DataQueryResult> FindDataFromInsight(string queryCode, Nullable<TimeRange> timeRange, Nullable<int> pageNo, Nullable<int> pageSize)
        {
            List<string> validationErrs = new List<string>();
            if (string.IsNullOrWhiteSpace(queryCode)) {
                validationErrs.Add(GetMsgByKey("parameterQueryCodeIsNull"));
            }else if(queryCode.Length != QUERY_CODE_LENGTH){
                validationErrs.Add(GetMsgByKey("parameterQueryCodeLengthInvalid"));
            }
            if (pageSize != null && (pageSize <= 0 || pageSize > 1000))
            {
                validationErrs.Add(GetMsgByKey("parametePageSizeLengthInvalid"));
            }
            if (validationErrs.Count > 0)
            {
                return new Result<DataQueryResult>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_GO_INSIGHT_DATA_URL, Method.GET);
            request.AddUrlSegment("queryCode", queryCode);
            if (pageNo != null && pageNo > 0 && pageSize != null && pageSize > 0)
            {
                request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo);
                request.AddParameter("pageSize", pageSize);
            }
            if (timeRange != null)
            {
                request.AddParameter("timeRangeType", ExtEnumHelper.GetEnumValue(timeRange)); 
            }
            var responseContent = Execute(request);
            DataQueryResponse dataQueryResponse = JsonConvert.DeserializeObject<DataQueryResponse>(responseContent);
            Result<DataQueryResult> result = new Result<DataQueryResult>(dataQueryResponse);
            return result;
        }
    }

    public enum TimeRange{
        [EnumValue("p1d")]
        YESTERDAY,

        [EnumValue("p1w")]
        LAST_WEEK,

        [EnumValue("p1m")]
        LAST_MONTH,

        [EnumValue("p1y")]
        LAST_YEAR,

        [EnumValue("r1d")]
        RECENT_DAY,

        [EnumValue("r1w")]
        RECENT_WEEK,
    
        [EnumValue("r1m")]
        RECENT_MONTH,
    
        [EnumValue("r1y")]
        RECENT_YEAR,
    
        [EnumValue("t1d")]
        TODAY,

        [EnumValue("t1w")]
        THIS_WEEK,
    
        [EnumValue("t1m")]
        THIS_MONTH,
    
        [EnumValue("t1y")]
        THIS_YEAR
    }
}
