using System.Collections.ObjectModel;

namespace Northwind.Store.Model
{
    /// <summary>
    /// Interface que define el estado de un objeto y de las propiedades cuyos valores han sido modificados.
    /// </summary>
    public interface IObjectWithState
    {
        /// <summary>
        /// Estado del objeto modelo.
        /// </summary>
        ModelState State { get; set; }

        /// <summary>
        /// Propiedades cuyos valores han sido modificados.
        /// </summary>
        ObservableCollection<string> ModifiedProperties { get; set; }
    }
}
