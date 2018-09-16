using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Com;
using Com.Pax.OpenApi.Sdk;
using Com.Pax.OpenApi.Sdk.Base.Dto;
using Com.Pax.OpenApi.Sdk.Dto.Reseller;
using FluentValidation.Results;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Com.Pax.OpenApi.Sdk.Api;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Reflection;

using Test.Reseller;

namespace api_net_testcc {
    class Program {
        static void Main (string[] args) {
            // TestResellerApi.TestSearchReseller();

            TestResellerApi.TestCreateReseller();
        }


        

        

    }
}