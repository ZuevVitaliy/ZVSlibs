using System;

namespace ZVS.Global.Interfaces
{
    public interface ICloneable<T> : ICloneable
    {
        new T Clone();
    }
}