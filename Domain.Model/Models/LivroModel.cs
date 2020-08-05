using System;
using System.ComponentModel.DataAnnotations;
using Domain.Model.Models.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Model.Models
{
    public class LivroModel
    {
        public int Id { get; set; }

        [Remote(action: "CheckIsbn", controller: "Livro", AdditionalFields = nameof(Id))]
        public string Isbn { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = ValidationConstants.StringLengthErrorMessage)]
        public string Titulo { get; set; }

        [Range(50, 2000)]
        public int Paginas { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Lancamento { get; set; }

        public int AutorId { get; set; }
        public AutorModel Autor { get; set; }
    }
}
