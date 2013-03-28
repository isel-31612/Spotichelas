using SpotiChelas.DomainModel.Data;
using System;

namespace SpotiChelas.DomainModel.Database
{
    public interface Repository
    {
        Int32 setT<T>(T t) where T : Identity;
        T getT<T>(int id) where T : Identity;
    }
}
