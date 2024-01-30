namespace Bai1.Repository
{
    using Bai1.Models;
    using System.Collections.Generic;

    public interface IProductRepository
    {

        IEnumerable<Product> GetAll();

        Product GetById(int id);

        void Add(Product product);
        void Update(Product product);

        void Delete(int id);

    }
}