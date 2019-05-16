using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DevelopBase.Common;
using DevelopBase.Services;
using PurocumentLib.Dbcontext;
using PurocumentLib.Entity;
using PurocumentLib.Model;

namespace PurocumentLib.Service
{
    public class DepotService : ServiceBase, IDepotService
    {
        public DepotService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public void StockCheck(int departmentID, int userID, List<DepotDetailMedel> listDepotDetails)
        {
            return;
        }
    }
}
