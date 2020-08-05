using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Model.Models.Constants;

namespace Domain.Model.Models
{
    public class AutorModel
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = ValidationConstants.StringLengthErrorMessage)]
        public string Nome { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = ValidationConstants.StringLengthErrorMessage)]
        public string UltimoNome { get; set; }

        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }

        public List<LivroModel> Livros { get; set; }
    }
}
