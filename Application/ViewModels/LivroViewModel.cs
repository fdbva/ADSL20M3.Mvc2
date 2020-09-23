using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Application.ViewModels
{
    public class LivroViewModel : BaseViewModel
    {
        [Remote(action: "CheckIsbn", controller: "Livro", AdditionalFields = nameof(Id))]
        public string Isbn { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string Titulo { get; set; }

        [Range(50, 2000)]
        public int Paginas { get; set; }

        [DataType(DataType.Date)]
        public DateTime Lancamento { get; set; }

        public int AutorId { get; set; }
        public AutorViewModel Autor { get; set; }
    }
}
