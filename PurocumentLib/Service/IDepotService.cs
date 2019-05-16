using System;
using System.Collections.Generic;
using PurocumentLib.Model;
using DevelopBase.Services;

namespace PurocumentLib.Service
{
    public interface IDepotService : IService
    {
        //收货
        void StockCheck(int departmentID, int userID, List<Model.DepotDetailMedel> listDepotDetails);
    }
}
