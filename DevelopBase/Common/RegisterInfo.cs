using System;

namespace DevelopBase.Common
{
    public class RegisterInfo
    {
        public Type From{get;set;}
        public Type To{get;set;}
        public LifeScope LifeScope{get;set;}
        public object[] ConstructorParams{get;set;}
    }
}
