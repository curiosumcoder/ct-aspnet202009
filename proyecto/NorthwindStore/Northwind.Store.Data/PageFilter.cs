using System;

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
        /// Cantidad de páginas.
        /// </summary>
        public int PageCount => (int)Math.Ceiling((decimal)Count / PageSize);

        /// <summary>
        /// Nombre del campo utilizado para el ordenamiento.
        /// </summary>
        public string Sort { get; set; }

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
                if (string.IsNullOrEmpty(Sort))
                {
                    throw new InvalidOperationException("The PageFilter needs a default sort.");
                }

                return string.IsNullOrEmpty(Sort) ? "" : $"{Sort} {SortDir}";
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
