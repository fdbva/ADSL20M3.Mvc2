using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ASDL20M3.Mvc2.Models
{
    public class LivroAutorCreateViewModel
    {
        [Remote(action: "CheckIsbn", controller: "Livro")]
        public string Isbn { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string Titulo { get; set; }

        [Range(50, 2000)]
        public int Paginas { get; set; }

        [DataType(DataType.Date)]
        public DateTime Lancamento { get; set; }

        public int? AutorId { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string? Nome { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string? UltimoNome { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Nascimento { get; set; }
    }
}
