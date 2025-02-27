﻿using Newtonsoft.Json;
using Paxstore.OpenApi.Base;
using Paxstore.OpenApi.Help;
using Paxstore.OpenApi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace Paxstore.OpenApi
{
    public class AppApi : BaseApi
    {
        private const string SEARCH_APP_URL = "/v1/3rdsys/apps";

        public AppApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo = null, int timeout = 5000, IWebProxy proxy = null) : base(baseUrl, apiKey, apiSecret, timeZoneInfo, timeout, proxy)
        {

        }

        public AppApi(string baseUrl, string apiKey, string apiSecret, TimeZoneInfo timeZoneInfo) : base(baseUrl, apiKey, apiSecret, timeZoneInfo, DEFAULT_TIMEOUT, null)
        {

        }

        public AppApi(string baseUrl, string apiKey, string apiSecret, IWebProxy proxy) : base(baseUrl, apiKey, apiSecret, null, DEFAULT_TIMEOUT, proxy)
        {

        }

        public AppApi(string baseUrl, string apiKey, string apiSecret, int timeout) : base(baseUrl, apiKey, apiSecret, null, timeout, null)
        {

        }
        
        public Result<PagedApp> SearchApp(
            int pageNo, int pageSize, Nullable<AppSearchOrderBy> orderBy,
                string name,
                AppOsType? osType,
                AppChargeType? chargeType,
                AppBaseType? baseType,
                AppStatus? appStatus,
                ApkStatus? apkStatus,
                bool? specificReseller = false,
                bool? specificMerchantCategory = false,
                bool? includeSubscribedApp = false,
                string resellerName = null,
                string modelName = null)
        {

            IList<string> validationErrs = ValidatePageSizeAndPageNo(pageSize, pageNo);
            if (validationErrs.Count > 0)
            {
                return new Result<PagedApp>(validationErrs);
            }
            RestRequest request = new RestRequest(SEARCH_APP_URL, Method.Get);

            request.AddParameter(Constants.PAGINATION_PAGE_NO, pageNo.ToString());
            request.AddParameter(Constants.PAGINATION_PAGE_LIMIT, pageSize.ToString());

            if (orderBy != null)
            {
                request.AddParameter("orderBy", ExtEnumHelper.GetEnumValue(orderBy));
            }

            request.AddParameter("name", name);

            if (apkStatus != null)
            {
                request.AddParameter("apkStatus", ExtEnumHelper.GetEnumValue(apkStatus));
            }

            if (appStatus != null)
            {
                request.AddParameter("appStatus", ExtEnumHelper.GetEnumValue(appStatus));
            }

            if (baseType != null)
            {
                request.AddParameter("baseType", ExtEnumHelper.GetEnumValue(baseType));
            }
            if (chargeType != null)
            {
                request.AddParameter("chargeType", chargeType.ToString());
            }
            if (osType != null)
            {
                request.AddParameter("osType", ExtEnumHelper.GetEnumValue(osType));
            }
            if (specificReseller != null)
            {
                request.AddParameter("specificReseller", specificReseller.ToString());
            }
            if (specificMerchantCategory != null)
            {
                request.AddParameter("specificMerchantCategory", specificMerchantCategory.ToString());
            }
            if(includeSubscribedApp != null) {
                request.AddParameter("includeSubscribedApp", includeSubscribedApp.ToString());
            }

            if (!string.IsNullOrWhiteSpace(resellerName))
            {
                request.AddParameter("resellerName", resellerName.Trim());
            }
            if (!string.IsNullOrWhiteSpace(modelName))
            {
                request.AddParameter("modelName", modelName.Trim());
            }

            var responseContent = Execute(request);
            AppPageResponse appPageDTO = JsonConvert.DeserializeObject<AppPageResponse>(responseContent);
            Result<PagedApp> result = new Result<PagedApp>(appPageDTO);
            return result;
        }
    }
    
    public enum AppStatus{
        [EnumValue("A")]
        Active,
        [EnumValue("S")]
        Suspend
    }

    public enum AppBaseType{
        [EnumValue("N")]
        Normal,
        [EnumValue("P")]
        Parameter
    }

    public enum ApkStatus{
        [EnumValue("P")]
        Pending,

        [EnumValue("O")]
        Online,

        [EnumValue("R")]
        Rejected,

        [EnumValue("U")]
        Offline
    }

    public enum AppChargeType{
        Free=0,
        Charging=1
    }

    public enum AppOsType{
        [EnumValue("A")]
        Android,
        [EnumValue("T")]
        Traditional
    }

    public enum AppSearchOrderBy{
        [EnumValue("CONVERT( app.name USING gbk ) COLLATE gbk_chinese_ci DESC")]
        AppName_desc,

        [EnumValue("CONVERT( app.name USING gbk ) COLLATE gbk_chinese_ci ASC")]
        AppName_asc,

        //[EnumValue("developer.email DESC")]
        //Emial_desc,

        //[EnumValue("developer.email ASC")]
        //Emial_asc,

        [EnumValue("app.updated_date DESC")]
        UpdatedDate_desc,

        [EnumValue("app.updated_date ASC")]
        UpdatedDate_asc
        
    }
}
