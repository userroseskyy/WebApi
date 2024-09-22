using WebApi.Context;
using WebApi.Entities;
using static WebApi.Servises.ToDoRepository;

namespace WebApi.Servises
{
    public class ToDoRepository
    {
        private readonly DataBaseContext _context;

        public ToDoRepository(DataBaseContext context)
        {
            _context = context;
        }
        public List<ToDoDto> GetAll()
        {
            return _context.ToDos.Select(p => new ToDoDto
            {
                Id=p.Id,
                Text=p.Text,

                InsertTime=p.InsertTime,
                IsRemoved=p.IsRemoved,
            }).ToList();

        }
        public ToDoDto Get(int Id)
        {
            var todo = _context.ToDos.SingleOrDefault(p => p.Id == Id);
            return new ToDoDto()
            {
                Id=todo.Id,
                Text=todo.Text,

                InsertTime=todo.InsertTime,
                IsRemoved=todo.IsRemoved,
            };
        }

        public AddToDoDto Add(AddToDoDto toDo)
        {
            ToDo newToDo = new ToDo()
            {
                Id=toDo.ToDo.Id,
                InsertTime=DateTime.Now,
                IsRemoved=false,
                Text=toDo.ToDo.Text,

            };
            foreach (var item in toDo.Categoreis)
            {
                var category = _context.Categories.SingleOrDefault(p => p.Id==item);
                newToDo.Categories.Add(category);
            }
            _context.ToDos.Add(newToDo);
            _context.SaveChanges();
            return new AddToDoDto
            {
                ToDo = new ToDoDto
                {
                    Id=newToDo.Id,
                    Text=newToDo.Text,
                    InsertTime=DateTime.Now,
                    IsRemoved=newToDo.IsRemoved,
                },
              Categoreis=toDo.Categoreis,
            };
            }
        public void Delete(int id)
        {
            //_context.ToDos.Remove(new ToDo { Id=id });
            var del=_context.ToDos.Find(id);
            del.IsRemoved=true;
            _context.SaveChanges();
        }
        public bool Edit(EditToDoDto edit)
        {
            var todo=_context.ToDos.SingleOrDefault(p=>p.Id==edit.Id);
            todo.Text= edit.Text;
            _context.SaveChanges();
            return true;
        }



        public class EditToDoDto
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public List<int> categories { get; set; }
        }

        public class AddToDoDto
        {
            public ToDoDto ToDo { get; set; }
            public List<int> Categoreis { get; set; }= new List<int>();
        }


        public class ToDoDto
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public DateTime InsertTime { get; set; }
            public bool IsRemoved { get; set; }

        }


    }
}
