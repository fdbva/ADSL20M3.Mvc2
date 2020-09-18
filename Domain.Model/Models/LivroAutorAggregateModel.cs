namespace Domain.Model.Models
{
    public class LivroAutorAggregateModel : BaseModel
    {
        public LivroModel Livro { get; set; }
        public AutorModel Autor { get; set; }
    }
}
