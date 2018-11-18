using System;
using DevelopBase.Services;
namespace DevelopBase.Common
{
    public static partial class ServiceProviderExtend
    {
        public static T GetService<T>(this IServiceProvider serviceProvider) where T:IService
        {
            var serviceType=typeof(T);
            return (T)serviceProvider.GetService(serviceType);
        }
    }
}
