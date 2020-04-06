using System;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary.Infrastructure.Interfaces
{
    public interface IRepository<T> 

    {
        T GetById(string id);

        void Create(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}
