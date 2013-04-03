using SpotiChelas.DomainModel.Data;
using System;

namespace SpotiChelas.DomainModel.Database
{
    public interface Repository
    {
        Int32 setT<T>(T t) where T : Identity;
        T getT<T>(int id, out T t) where T : Identity;
        T[] getAllLike<T>(T t) where T : Identity;
    }
}
