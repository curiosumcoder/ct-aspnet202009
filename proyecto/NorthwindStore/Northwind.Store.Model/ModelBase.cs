using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace Northwind.Store.Model
{
    /// <summary>
    /// Clase base para las clases modelo básicas. Define la gestión del estado del objeto.
    /// </summary>
    public abstract class ModelBase : IObjectWithState
    {
        /// <summary>
        /// Propiedad utilizada para la validación de la Concurrenci Optimista.
        /// Ejemplo: ALTER TABLE [Northwind].[dbo].[Categories] ADD [RowVersion] TIMESTAMP
        /// </summary>
        [ScaffoldColumn(false)]
        [ConcurrencyCheck]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Estado actual del objeto modelo.
        /// </summary>     
        [ScaffoldColumn(false)]
        [NotMapped]
        public ModelState State { get; set; }

        /// <summary>
        /// Propiedades cuyos valores han sido modificados.
        /// </summary>
        [ScaffoldColumn(false)]
        [NotMapped]
        public ObservableCollection<string> ModifiedProperties { get; set; }

        public ModelBase()
        {
            ModifiedProperties = new ObservableCollection<string>();

            // En caso que se conozcan cambios a la colección de propiedades modificadas se debe considerar a objeto del modelo como modificado
            ModifiedProperties.CollectionChanged += (sender, e) =>
                {
                    State = ModelState.Modified;
                };
        }
    }
}
