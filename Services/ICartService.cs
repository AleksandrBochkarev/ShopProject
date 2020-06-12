
using System.Collections.Generic;
using Domain;

namespace Services
{
    public interface ICartService<T>
    where T : class
    {
        void Add(T t);

        void Del(int id);

        Dictionary<T, int> GetAll();

        void CheckOut(User user);
        void SetDict(Dictionary<Product, int> dict);

    }
}