using WebApi.Context;
using WebApi.Entities;

namespace WebApi.Servises
{
    public class CategoryRepository
    {
        private readonly DataBaseContext _context;
        public CategoryRepository(DataBaseContext context)
        {
            _context=context;
        }
        public List<CategoryDto> GetAll()
        {
            var data = _context.Categories.ToList()
                    .Select(p => new CategoryDto
                    {
                        Id=p.Id,
                        Name=p.Name
                    }).ToList();
            return data;

        }
        public int AddCategory(string Name)
        {
            Category category = new Category()
            {
                Name=Name
            };
            _context.Add(category);
             _context.SaveChanges();
            return category.Id;
        }

        public int Delete(int Id)
        {
            _context.Categories.Remove(new Category { Id=Id });
            return _context.SaveChanges();
        }

        public int Edit(CategoryDto categoryDto)
        {
            var category = _context.Categories.SingleOrDefault(p => p.Id==categoryDto.Id);
            return _context.SaveChanges();
        }
        public CategoryDto Find(int Id)
        {
            var category = _context.Categories.Find(Id);
            return new CategoryDto
            {
                Id=category.Id,
                Name = category.Name
            };

        }



        public class CategoryDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
