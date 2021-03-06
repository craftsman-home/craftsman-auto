﻿using JumpForward.Common.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JumpForward.Common.ServiceCall
{
    public class ComplianceServiceClient
    {
        private RestClient _client;

        #region Init data
        public ComplianceServiceClient()
        {
            if (_client != null) { return; }

            //TODO : get url for config file.
            var client = new RestClient("https://college-china.jumpforward.com");
            client.CookieContainer = new CookieContainer();

            var requestIndex = new RestRequest("index.aspx", Method.GET);
            var requestSignIn = new RestRequest("index.aspx", Method.POST);

            var responseIndex = client.Get(requestIndex);

            var _viewState = GetInputDomValue("__VIEWSTATE", responseIndex.Content);
            var _viewStateGenerator = GetInputDomValue("__VIEWSTATEGENERATOR", responseIndex.Content);
            var _eventValidation = GetInputDomValue("__EVENTVALIDATION", responseIndex.Content);
            requestSignIn.AddParameter("__VIEWSTATE", _viewState);
            requestSignIn.AddParameter("__VIEWSTATEGENERATOR", _viewStateGenerator);
            requestSignIn.AddParameter("__EVENTVALIDATION", _eventValidation);

            requestSignIn.AddParameter("ctl00$ContentPlaceHolder1$txtUsername", "demicompliance@activenetwork.com");
            requestSignIn.AddParameter("ctl00$ContentPlaceHolder1$txtPassword", "active");
            requestSignIn.AddParameter("ctl00$ContentPlaceHolder1$bttnLogin", "Sign In");

            client.Post(requestSignIn);
            this._client = client;
        }
        private string GetInputDomValue(string id, string content)
        {
            var strRegex = string.Format("<input type=\"hidden\" name=\"{0}\" id=\"{0}\" value=\"(?<value>[\\s\\S]*?)\"", id);
            var match = Regex.Match(content, strRegex, RegexOptions.IgnoreCase);

            var value = (match.Groups["value"] != null) ? match.Groups["value"].Value : string.Empty;
            return value;
        }
        #endregion

        #region Action Prospect
        
        #endregion
    }
}
