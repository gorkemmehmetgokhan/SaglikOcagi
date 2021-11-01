﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaglikOcagi.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> SelectAll();
        T GetById(int bid);
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
        void DeleteById(object id, T obj);
        void Save();
    }
}
