using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        Category GetCategory(int id);
        Category GetCategoryByProducts(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
        int Save();
    }
}
