using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class AutorViewModel
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string Nome { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string UltimoNome { get; set; }

        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }

        //[JsonIgnore]
        public List<LivroViewModel> Livros { get; set; }

        public string NomeCompletoId => $"{Nome} {UltimoNome} ({Id})";
    }
}
