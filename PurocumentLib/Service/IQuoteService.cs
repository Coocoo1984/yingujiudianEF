using System;
using System.Linq;
using DevelopBase.Services;
using PurocumentLib.Model;
namespace PurocumentLib.Service
{
    public interface IQuoteService:IService
    {
        void Add(QuoteModel model);
        void Update(QuoteModel model);
        QuoteModel Load(int id);
    }
}
