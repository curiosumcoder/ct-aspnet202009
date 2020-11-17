using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Store.Data
{
    /// <summary>
    /// Permite representar los valores para la paginación y ordenamiento de datos básicos.
    /// </summary>
    public class PageFilter
    {
        /// <summary>
        /// Número de página.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Cantidad de elementos de la página.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Nombre del campo utilizado para el ordenamiento.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// Nombre del campo utilizado para el ordenamiento en caso de que no se asigne.
        /// </summary>
        public string DefaultSort { get; set; }

        /// <summary>
        /// Dirección del ordenamiento (ASC = ascendente, DESC = descendente)
        /// </summary>
        public string SortDir { get; set; }

        /// <summary>
        /// Expresión completa de ordenamiento.
        /// </summary>
        public string Sorting
        {
            get
            {
                if (string.IsNullOrEmpty(DefaultSort))
                {
                    throw new InvalidOperationException("The PageFilter needs a default sort.");
                }

                return string.IsNullOrEmpty(Sort) ? $"{DefaultSort} {SortDir}" :
                    $"{Sort} {SortDir}";
            }
        }

        /// <summary>
        /// Cantidad de elementos totales que retorna la consulta. Debe ser actualizado por el que usa el objeto.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Constructor con los valores por defecto.
        /// </summary>
        public PageFilter()
        {
            Page = 1;
            PageSize = 10;
            Sort = "";
            SortDir = "ASC";
        }
    }
}
