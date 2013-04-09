using Entities;
using System;

namespace DataAccess
{
    public interface Repository
    {
        Int32 setT<T>(T t) where T : Identity;
        T getT<T>(int id) where T : Identity;
        T[] getAll<T>() where T : Identity;
        T[] getAllLike<T>(T t) where T : Identity;
        int update<T>(int id, T t) where T : Identity;
        T remove<T>(int id) where T : Identity;
    }
}
