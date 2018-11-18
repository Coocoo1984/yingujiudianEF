using System;
using System.Threading.Tasks;
namespace DevelopBase.Message
{
    public abstract class HandlerBase<T> where T:RequestBase
    {
        private IServiceProvider _serviceProvider=null;
        protected IServiceProvider ServiceProvider{get=>_serviceProvider;}
        public HandlerBase(IServiceProvider serviceProvider)
        {
            if(serviceProvider==null)
            {
                throw new ArgumentNullException();   
            }
            _serviceProvider=serviceProvider;
        }
        public abstract ResponseBase Handler(T request);
        public async Task<ResponseBase> HandlerAsync(T request)
        {
            return await Task.Run(()=>Handler(request));
        }
    }
}
