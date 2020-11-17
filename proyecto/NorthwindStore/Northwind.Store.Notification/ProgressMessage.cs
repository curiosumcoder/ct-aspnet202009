namespace Northwind.Store.Notification
{

    /// <summary>
    /// Notificación del progreso de una acción.
    /// </summary>
    public class ProgressMessage : Message
    {
        /// <summary>
        /// Valor absoluto del desarrollo del proceso.
        /// </summary>
        public double AbsoluteValue { get; set; }

        /// <summary>
        /// Valor total absoluto del desarrollo de proceso.
        /// </summary>
        public double AbsoluteTotalValue { get; set; } 

        /// <summary>
        /// Valor relativo del desarrollo de proceso.
        /// </summary>
        public double RelativeValue { get; set; }       
    }
}
