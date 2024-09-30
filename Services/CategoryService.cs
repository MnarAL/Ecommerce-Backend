using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public interface ICategoryService{
    Task<List<Category>> GetAllCategoryService();
    Task<CategoryDto?> GetCategoryByIdService(Guid categoryId);
    Task<bool> DeleteCategoryByIdService(Guid id);
    Task<Category> CreateCategoryService(CreateCategoryDto newcategory);
    Task<Category> UpdateCategoryService(Guid id, CategoryDto UpdateCategoryDto);
    }
public class CategoryService
  {

    private readonly AppDBContext _appDbContext;
public CategoryService(AppDBContext appDbContext){
  _appDbContext=appDbContext;
}
      
// public class PaginatedResult<T>
// {
//     // = new List<Item>();تاكدي بعدين هل يوضع لها ؟ او 
//     public List<T>? Items { get; set; }
//     public int TotalCount { get; set; }
//     public int PageNumber { get; set; }
//     public int PageSize { get; set; }
//         public string? SearchBy { get; set; }

//         public PaginatedResult<CategoryDto> GetCategoryService(int pageNumber, int pageSize, string? searchBy = null)
//     {
//         // products => Id, Name, Price, Description, CreatedAt 
//         // ProductDto => Id, Name, Price


//         var filterCategory = _category.Where(p => 
//         string.IsNullOrEmpty(searchBy) || p.Name.Contains(searchBy, StringComparison.OrdinalIgnoreCase));


//         var totalFilteredCategory = filterCategory.Count();

   
//     var Category = filterCategory.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(Category => new CategoryDto
//     {
//         CategoryId = Category.CategoryId,
//         Name = Category.Name,
//         Description = Category.Description
//     }).ToList();

   
//     var paginatedResult = new PaginatedResult<CategoryDto>
//     {
//         PageSize = pageSize,
//         PageNumber = pageNumber,
//         SearchBy = searchBy,
//         TotalCount = totalFilteredCategory,  
//         Items = Category
//     };

//     return paginatedResult;}}
    public async Task<List<Category>> GetAllCategoryService()
    {
      try
      {
        var category= await _appDbContext.Category.ToListAsync();
      return category;
      }
      catch (System.Exception)
      {
        
        throw new ApplicationException("erorr ocurred when get the data from the category table");
      }
    }
    public async Task<CategoryDto?> GetCategoryByIdService(Guid categoryId)
{
    try
    {
        var category = await _appDbContext.Category
            .FirstOrDefaultAsync(u => u.CategoryId == categoryId);

        if (category == null)
        {
            return null; 
        }
        
        var CategoryDto = new CategoryDto
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            Description = category.Description,
            // Map other properties as needed
        };

        return CategoryDto;
    }
    catch (Exception)
    {
        throw new ApplicationException("Error occurred while retrieving the user.");
    }
}
     public async Task<bool> DeleteCategoryByIdService(Guid id)
{
    try
    {
        var categoryToRemove = await _appDbContext.Category.FirstOrDefaultAsync(u => u.CategoryId == id);

        if (categoryToRemove != null)
        {
            _appDbContext.Category.Remove(categoryToRemove);
            await _appDbContext.SaveChangesAsync();
            return true; 
        }
        return false; 
    }
    catch (Exception)
    {
        throw new ApplicationException("Error occurred while deleting the category.");
    }
}
public async Task<Category> CreateCategoryService(CreateCategoryDto newcategory)
    {
      try
      {
        var category = new Category {
          CategoryId=newcategory.CategoryId,
          CategoryName = newcategory.CategoryName,};
         
         await _appDbContext.Category.AddAsync(category);
         await _appDbContext.SaveChangesAsync();
         return category;
      }
      catch (System.Exception)
      {
        
        throw new ApplicationException("erorr ocurred when creat the  category ");
      }
    }
    public async Task<Category> UpdateCategoryService(Guid id, CategoryDto UpdateCategoryDto)
{
    try
    {
        var existingcategory = await _appDbContext.Category.FindAsync(id);

        if (existingcategory == null)
        {
            throw new ApplicationException("category not found.");
        }

        existingcategory.CategoryName = UpdateCategoryDto.CategoryName ?? existingcategory.CategoryName;
        existingcategory.Description = UpdateCategoryDto.Description ?? existingcategory.Description;

        _appDbContext.Category.Update(existingcategory);
        await _appDbContext.SaveChangesAsync();

        return existingcategory;
    }
    catch (System.Exception)
    {
        throw new ApplicationException("Error occurred when updating the user.");
    }
}
}