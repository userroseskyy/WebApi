using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Servises;
using static WebApi.Servises.CategoryRepository;

namespace WebApi.Controllers
{

    [ApiVersion("2")]
    [Route("api/v{version:apiversion}[controller]")]
    [ApiController]
    [Authorize]
   
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepository;
        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// لیست دسته بندی ها را دریافت کن
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_categoryRepository.GetAll());
        }
        /// <summary>
        /// اطلاعات دسته بندی رو دریافت کن
        /// </summary>
        /// <param name="id">شناسه دسته بندی را دریافت کن</param>
        /// <returns></returns>
          [HttpGet("{id}")]
        public IActionResult GetAction(int id) 
        {
            return Ok(_categoryRepository.Find(id));

         }
        
        [HttpPut]
        public IActionResult Put(CategoryDto categoryDto) 
        {
            return Ok(_categoryRepository.Edit(categoryDto));
        } 

        [HttpPost]
        public IActionResult Post(string Name)
        {
            var result = _categoryRepository.AddCategory(Name);
            return Created(Url.Action(nameof(Get), "Categories", new { id = result }, Request.Scheme),true);
        }
        [HttpDelete]
        public IActionResult Delete(int id) 
        { 
            return Ok(_categoryRepository.Delete(id));
        }

    }
}
