using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        void Remove(int id);
        Category GetCategoryById(int id);
        public void AddNew(Category category);
        public void Update(Category category);
        public Category GetById(int id);
    }
}