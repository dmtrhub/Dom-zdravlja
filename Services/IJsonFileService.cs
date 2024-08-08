using System;
using System.Collections.Generic;

namespace Dom_zdravlja.Services
{
    public interface IJsonFileService<T>
    {
        List<T> Read();
        void Update(T item, Predicate<T> predicate);
        void Write(List<T> items);
    }
}