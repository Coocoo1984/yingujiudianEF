using System;
using PurocumentLib.Model;
using System.Linq;
using System.Collections.Generic;
using Entity = PurocumentLib.Entity;
using PurocumentLib.Dbcontext;
using DevelopBase.Common;
using Microsoft.EntityFrameworkCore;
using PurocumentLib.Entity;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using DevelopBase.Services;
using Newtonsoft.Json;
using System.IO;

namespace PurocumentLib.Service
{
    public class MessageService: ServiceBase
    {
        public MessageService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public static async void Post(string touser, string title, string time, string content)
        {
            var httpClient = ServiceBase.IHttpClientFactory.CreateClient();
            //取token(目前达不到24小时2000次的限制 不判断token失效操作)

            var response = httpClient.GetAsync(GetTockenUri).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(result);
            token jsonObj = (token)serializer.Deserialize(new JsonTextReader(sr), typeof(token));
            if (jsonObj.errcode == 0)
            {
                string strAccessToken = jsonObj.access_token;

                HttpContent contentObj = new TextMesage(new
                {
                    touser = touser,
                    msgtype = "textcard",
                    agentid = "1000008",
                    textcard = new
                    {
                        title = title,
                        description = "<div class=\"gray\">" + time + "</div> <div class=\"normal\">" + content + "</div>",
                        url = "http://wxadmin.changan-hotel.cn",
                        btntxt = "",
                    }
                });
                await httpClient.PostAsync(MessageUri + strAccessToken, contentObj);
            }
        }
    }
}
