using System;

namespace DevelopBase.Services
{
    public abstract class ServiceBase
    {
        private IServiceProvider _serviceProvider;
        protected IServiceProvider ServiceProvider{get=>_serviceProvider;}
        public ServiceBase(IServiceProvider serviceProvider)
        {
            if(serviceProvider==null)
            {
                throw new ArgumentNullException();
            }
            _serviceProvider=serviceProvider;
        }
    }
}