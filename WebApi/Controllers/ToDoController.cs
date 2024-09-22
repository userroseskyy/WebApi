using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.Servises;
using static WebApi.Servises.ToDoRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoRepository _toDoRepository;

        public ToDoController(ToDoRepository toDoRepository)
        {
           _toDoRepository = toDoRepository;
        }

        // GET: api/<ToDoController>
        [HttpGet]
        public IActionResult Get()
        {
           var toDoList= _toDoRepository.GetAll().Select(p=> new DtoToDoItemDto
           {
               Id=p.Id ,
                Text=p.Text ,
                InsertTime =p.InsertTime,
                Links=new List<Links>()
                {
                    new Links
                    {
                        Hrf=Url.Action(nameof(Get),"ToDo",new{p.Id },Request.Scheme),
                        Rel="Self",
                        Method="Get"

                    }, new Links
                    {
                        Hrf=Url.Action(nameof(Delete),"ToDo",new{p.Id },Request.Scheme),
                        Rel="Delete",
                        Method="Delete"

                    },
                    new Links
                    {
                        Hrf=Url.Action(nameof(Put),"ToDo",Request.Scheme),
                        Rel="Update",
                        Method="Put"

                    },
                }

                
           });
            return Ok(toDoList);
            
        }

        // GET api/<ToDoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
           var toDo= _toDoRepository.Get(id);
            return Ok(new DtoToDoItemDto
            { Id =toDo.Id ,
             InsertTime=toDo.InsertTime ,
             Text=toDo.Text ,

            });
        }

        // POST api/<ToDoController>
        [HttpPost]
        public IActionResult Post([FromBody] ItemDto item)
        {
          var resultAdd=  _toDoRepository.Add(new AddToDoDto()
            {
                ToDo=new ToDoDto
                {


                    Text=item.Text,
                }
            });
            string url = Url.Action(nameof(Get),"ToDo",new {id=resultAdd.ToDo.Id },Request.Scheme);
            return Created(url, resultAdd);

           
        }

        // PUT api/<ToDoController>/5
        [HttpPut]
        public IActionResult Put(int id, [FromBody] EditToDoDto editToDoDto)
        {
            var editDto = _toDoRepository.Edit(editToDoDto);
            return Ok(editDto);
        }

        // DELETE api/<ToDoController>/5
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _toDoRepository.Delete(id);
            return Ok();
        }
    }
}
