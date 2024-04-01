using Lab_7_2_Net_4.Models;

namespace Lab_7_2_Net_4.Services
{
    //Lớp này là một service được sử dụng để truy xuất dữ liệu liên quan đến các danh mục (categories).
    public class CategoryRepository: ICategoryRepository
    {
        private readonly ShopBanCamContext _dbContext;

        public CategoryRepository(ShopBanCamContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _dbContext.Categories;
        }
    }
}
