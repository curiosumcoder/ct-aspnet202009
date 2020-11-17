namespace Northwind.Store.Notification
{
    public static class Messages
    {
        public static class General
        {
            // TODO Evaluar el uso de recursos
            public static Message READY = new Message()
            {
                Id = 1,
                Level = Level.Information,
                Title = "La acción se ejecutó satisfactoriamente."
            };

            public static Message EXCEPTION = new Message()
            {
                Id = 2,
                Level = Level.Exception,
                Title = "Se ha presentado una excepción."
            };

            public static ConcurrencyMessage CONCURRENCY_DELETE = new ConcurrencyMessage()
            {
                Id = 3,
                Level = Level.Exception,
                Title = "No fué posible aplicar la eliminación. Los datos fueron eliminados por otro usuario.",
                Description = "La acción fué cancelada debido a que los datos que se intenta eliminar ya no existen en la base de datos y fueron eliminados por otro usuario."
            };

            public static ConcurrencyMessage CONCURRENCY_UPDATE = new ConcurrencyMessage()
            {
                Id = 4,
                Level = Level.Exception,
                Title = "No fué posible aplicar la modificación. Los datos fueron modificados por otro usuario.",
                Description = "La acción fué cancelada debido a que los datos que se intenta modificar, fueron previamente por otro usuario."
            };

            public static Message NO_EXISTS = new Message()
            {
                Id = 5,
                Level = Level.Warning,
                Title = "El dato solicitado no existe.",
                Description = "La acción intentó afectar datos que no existen."
            };
        }
    }
}
