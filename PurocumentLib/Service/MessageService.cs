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
                userids = touser,
                title = title,
                time  = time,
                content  = content,
                url = url,
            });
            var client = ServiceBase.IHttpClientFactory.CreateClient();
            await client.PostAsync(ServiceBase.MessageUri, contentObj);
        }

    }
}
