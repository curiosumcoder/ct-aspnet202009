using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category
    {
        public class CategoryMetadata
        {
            [Required(ErrorMessage = "El nombre es requerido.")]
            [Display(Name = "Nombre", Prompt = "Digite el nombre")]
            public string CategoryName { get; set; }

            [Required(ErrorMessage = "La descripción es requerida.")]
            [Display(Name = "Descripción", Prompt = "Digite la descripción")]
            [StringLength(16, ErrorMessage = "Máximo de {1} caracteres")]
            public string Description { get; set; }
        }
    }
}
