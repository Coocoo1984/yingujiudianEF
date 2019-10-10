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

namespace PurocumentLib.Service
{
    public class MessageService: ServiceBase
    {
        public MessageService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public static async void Post(string touser, string title, string time, string content,string url)
        {
            HttpContent contentObj = new TextMesage( new  {
                touser = touser,
                msgtype = "textcard",
                agentid = "1000008",
                textcard = new {
                    title = title,
                    description = "'<div class=\"gray\">'" +time + "'</div> <div class=\"normal\">'" + content + "</div>",
                    url = url,
                }
            });

            System.Console.WriteLine($"HttpContent:{contentObj.ToString()}");

            var client = ServiceBase.IHttpClientFactory.CreateClient();

            ////var response = client.GetAsync(ServiceBase.GetTockenUri).ConfigureAwait(false);
            ////string result = await response.Content.ReadAsStringAsync();
          
            await client.PostAsync(ServiceBase.MessageUri, contentObj);
        }


    }
}
