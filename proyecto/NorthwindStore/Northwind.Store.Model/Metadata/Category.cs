using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category : ModelBase
    {
        /// <summary>
        /// Fotografía en formato Base64. Para utilizar en presentación.
        /// </summary>
        [NotMapped]
        public string PictureBase64 { get; set; }

        public class CategoryMetadata
        {
            [Required(ErrorMessage = "El nombre es requerido.")]
            [Display(Name = "Nombre", Prompt = "Digite el nombre")]
            public string CategoryName { get; set; }

            [Required(ErrorMessage = "La descripción es requerida.")]
            [Display(Name = "Descripción", Prompt = "Digite la descripción")]
            [StringLength(256, ErrorMessage = "Máximo de {1} caracteres")]
            public string Description { get; set; }
        }
    }
}
