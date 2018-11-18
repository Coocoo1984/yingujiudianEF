using System;

namespace DevelopBase.Message
{
    public class ResponseGeneric<T>:ResponseBase
    {
        public T Data{get;set;}
    }
}
