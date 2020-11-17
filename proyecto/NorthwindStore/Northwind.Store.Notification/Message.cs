using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Northwind.Store.Notification
{
    public class Message
    {
        public Message()
        {
            Params = new List<string>();
        }
        public Message(int id, Level level, string description) : this()
        {
            this.Id = id;
            this.Level = level;
            this.Description = description;
        }

        public Message(int id, Level level, string message, string friendly) : this(id, level, message)
        {
            this.Friendly = friendly;
        }

        /// <summary>
        /// Identificación del mensaje 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nivel de impacto sobre la aplicación de la situación que dió origen al mensaje.
        /// </summary>
        public Level Level { get; set; }

        /// <summary>
        /// Título del mensaje.
        /// </summary>
        public string Title { get; set; }

        string _description = "";
        /// <summary>
        /// Descripción del mensaje. Si existe una colección de parámetros se espera que el valor de la variable _description
        /// contenga un Format String ({0} {1} {2}).
        /// </summary>        
        public string Description
        {
            get
            {
                if (Params.Any())
                {
                    return string.Format(_description, Params.ToArray());
                }

                return _description;
            }
            set
            {
                _description = value;
            }
        }

        /// <summary>
        /// Descripción detallada y modifible por el desarrollador sobre la situación encontrada.
        /// </summary>
        public string Friendly { get; set; }

        /// <summary>
        /// Parametros opcionales que pueden ser requeridos por Description
        /// solamente si Descripciton incluye elemento de Format String {0}
        /// </summary>
        [JsonIgnore]
        public List<string> Params { get; set; }

        /// <summary>
        /// En caso de que existe algún código para ejecutar una vez procesado el mensaje
        /// </summary>
        [JsonIgnore]
        public Action Accion { get; set; }
    }
}
