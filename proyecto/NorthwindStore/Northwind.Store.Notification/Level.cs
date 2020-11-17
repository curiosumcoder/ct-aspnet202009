namespace Northwind.Store.Notification
{
    public enum  Level : short
    {
        /// <summary>
        /// Notificación informativa
        /// </summary>
        Information,
        /// <summary>
        /// Notificación de precaución
        /// </summary>
        Warning,
        /// <summary>
        /// Notificación de excepción
        /// </summary>
        Exception,
        /// <summary>
        /// Notificación de mensaje de validación
        /// </summary>
        Validation,
        /// <summary>
        /// Notificación de pregunta
        /// </summary>
        Question,
        /// <summary>
        /// Notificación para el alto de ejecución
        /// </summary>
        Stop
    }
}
