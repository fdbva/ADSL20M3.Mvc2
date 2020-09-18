using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Model.Models.Constants;

namespace Domain.Model.Models
{
    public class AutorModel : BaseModel
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = ValidationConstants.StringLengthErrorMessage)]
        public string Nome { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = ValidationConstants.StringLengthErrorMessage)]
        public string UltimoNome { get; set; }

        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }

        //[JsonIgnore]
        public List<LivroModel> Livros { get; set; }

        public string NomeCompletoId => $"{Nome} {UltimoNome} ({Id})";
    }
}
