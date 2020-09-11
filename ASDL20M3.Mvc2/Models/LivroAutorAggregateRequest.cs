using System;

namespace ASDL20M3.Mvc2.Models
{
    public class LivroAutorAggregateRequest
    {
        public LivroViewModel Livro { get; set; }
        public AutorViewModel Autor { get; set; }

        public LivroAutorAggregateRequest(
            LivroAutorCreateViewModel livroAutorCreateViewModel)
        {
            Livro = new LivroViewModel
            {
                Isbn = livroAutorCreateViewModel.Isbn,
                Titulo = livroAutorCreateViewModel.Titulo,
                Paginas = livroAutorCreateViewModel.Paginas,
                Lancamento = livroAutorCreateViewModel.Lancamento,
                AutorId = livroAutorCreateViewModel.AutorId ?? 0
            };

            if (Livro.AutorId > 0)
            {
                return;
            }

            Autor = new AutorViewModel
            {
                Nome = livroAutorCreateViewModel.Nome,
                UltimoNome = livroAutorCreateViewModel.UltimoNome,
                Nascimento = livroAutorCreateViewModel.Nascimento ?? default
            };
        }
    }
}
