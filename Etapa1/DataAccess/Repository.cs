using Entities;
using System;

namespace DataAccess
{
    public interface Repository
    {
        Int32 setT<T>(T t) where T : Identity;
        T getT<T>(int id, out T t) where T : Identity;
        T[] getAllLike<T>(T t) where T : Identity;
    }
}
