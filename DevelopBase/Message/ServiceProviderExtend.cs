using System;
using DevelopBase.Message;
namespace DevelopBase.Common
{
    public static partial class ServiceProviderExtend
    {
        public static T GetHandler<T>(this IServiceProvider serviceProvider) where T:RequestBase
        {
            Type handlerType=typeof(HandlerBase<>).MakeGenericType(typeof(T));
            return (T)serviceProvider.GetService(handlerType);
        }
    }
}
