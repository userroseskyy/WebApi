namespace WebApi.Model
{
    public class DtoToDoItemDto
    {
        public int Id { get; set; }
   
       public string Text {  get; set; }
        public DateTime InsertTime { get; set; }
        public List<Links> Links { get; set; }
    
    }
}
