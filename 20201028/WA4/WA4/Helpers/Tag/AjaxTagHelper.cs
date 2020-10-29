using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WA4.Helpers.Tag
{
    /// <summary>
    /// Gilberto Bermúdez Garro. gbermude@outlook.com
    /// Se requiere el plugin de jQuery jquery.unobtrusive-ajax.
    /// </summary>
    [HtmlTargetElement("a", Attributes = AjaxAnchor)]
    [HtmlTargetElement("form", Attributes = AjaxAnchor)]
    public class AjaxTagHelper : TagHelper
    {
        private const string AjaxAnchor = "ajax";

        /// <summary>
        /// Si esta propiedad se incluye se asume que se requiere el soporte de AJAX
        /// y se aplica el Tag Helper a la etiqueta a (anchor)
        /// </summary>
        [HtmlAttributeName(AjaxAnchor)]
        public bool Ajax { get; set; }

        /// <summary>
        /// Mensaje que se desea mostrar al usuario antes de enviar la solicitud.
        /// Se utiliza un "prompt" estandar del navegador.
        /// Ej.: ajax-confirm="¿Está seguro?"
        /// </summary>
        [HtmlAttributeName("ajax-confirm")]
        public string Confirm { get; set; }

        /// <summary>
        /// Especifica el método de HTTP que se desea enviar en la solicitud
        /// por defecto para los <a> es GET, pero pueden ser el resto de los valores
        /// estándar de HTTP: POST, PUT, ...
        /// </summary>
        [HtmlAttributeName("ajax-method")]
        public string HttpMethod { get; set; } = "GET";

        /// <summary>
        /// A partir definir la propiedad ajax-target que es donde se desea colocar el contenido
        /// resultante de la ejecución de la solicitud se define como se incluye el resultado, así:
        /// BEFORE: El resultado se incluye como hijo del target, antes del último en entrar
        /// AFTER: El resultado se incluye como hijo del target, después del último en entrar
        /// REPLACE: El resultado reemplazará al elemento target
        /// Sino se incluye ningún valor, el resultado se incluye como hijo del target y reemplaza al valor anterior
        /// </summary>
        [HtmlAttributeName("ajax-mode")]
        public string InsertionMode { get; set; }

        /// <summary>
        /// Cantidad de milisegundos
        /// </summary>
        [HtmlAttributeName("ajax-loading-duration")]
        public int LoadingElementDuration { get; set; }

        /// <summary>
        /// Id del elemento de HTML que se desea mostrar si la solicitud
        /// tarda más de la cantidad de milisegundos indicados en la propiedad
        /// ajax-loading-duration. El elemento debe tener el estilo display: none
        /// Ej.: <div id="processing" style="display: none;">Procesando ...</div>
        /// </summary>
        [HtmlAttributeName("ajax-loading")]
        public string LoadingElementId { get; set; }

        /// <summary>
        /// Nombre de la función de JavaScript a llamar cuando se inicia 
        /// la ejecución de la solicitud.
        /// </summary>

        [HtmlAttributeName("ajax-begin")]
        public string OnBegin { get; set; }

        /// <summary>
        /// Nombre de la función de JavaScript a llamar cuando se completa 
        /// la ejecución de la solicitud.
        /// </summary>
        [HtmlAttributeName("ajax-complete")]
        public string OnComplete { get; set; }

        /// <summary>
        /// Nombre de la función de JavaScript a llamar cuando se tiene un error en 
        /// la ejecución de la solicitud.
        /// </summary>

        [HtmlAttributeName("ajax-failure")]
        public string OnFailure { get; set; }

        /// <summary>
        /// Nombre de la función de JavaScript a llamar cuando se completa 
        /// la ejecución de la solicitud y los datos obtenidos se aplican en el DOM.
        /// </summary>
        [HtmlAttributeName("ajax-success")]
        public string OnSuccess { get; set; }

        /// <summary>
        /// Identificador del elemento del DOM donde se coloca el resultado
        /// obtenido de la ejecución de la solicitud. Ej.: ajax-target="#ajax-demo"
        /// Sino se asigna un target no se hace nada con el resultado obtenido.
        /// </summary>
        [HtmlAttributeName("ajax-target")]
        public string UpdateTargetId { get; set; }

        /// <summary>
        /// Dirección URL en donde se encuentra el recurso al cual se desea dirigir la
        /// solicitud. Si no se asigna un valor se asume el /{controller}/{action}
        /// de la vista en donde se incluyó el Tag Helper
        /// </summary>

        [HtmlAttributeName("ajax-url")]
        public string Url { get; set; }

        /// <summary>
        /// Cuando es "true" la solicitud se elimite de forma que el navegador la podría mantener en el cache.
        /// Cuando es diferente de "true" cada solicitud lleva un parámetro adicional de forma que no sea
        /// candida para mantener en el cache del navegador.
        /// </summary>
        [HtmlAttributeName("ajax-cache")]
        public string Cache { get; set; }

        /// <summary>
        /// Gracias a Property Injection se tiene acceso al contexto de la
        /// vista que inclue el Tag Helper
        /// </summary>
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (output != null)
            {
                // Se define la ruta a partir del contexto donde se ejecuta
                var pageController = ViewContext.RouteData.Values["controller"];
                var pageAction = ViewContext.RouteData.Values["action"];

                Url = string.IsNullOrEmpty(Url) ? $"/{pageController}/{pageAction}" : Url;

                output.Attributes.SetAttribute("data-ajax", "true");

                output.Attributes.Add("data-ajax-method", HttpMethod);

                if (!string.IsNullOrEmpty(Confirm))
                {
                    output.Attributes.Add("data-ajax-confirm", Confirm);
                }

                if (LoadingElementDuration > 0)
                {
                    output.Attributes.Add("data-ajax-loading-duration", LoadingElementDuration);
                }
                if (!string.IsNullOrEmpty(LoadingElementId) && LoadingElementDuration > 0)
                {
                    output.Attributes.Add("data-ajax-loading", LoadingElementId);
                }
                if (!string.IsNullOrEmpty(OnBegin))
                {
                    output.Attributes.Add("data-ajax-begin", OnBegin);
                }
                if (!string.IsNullOrEmpty(OnComplete))
                {
                    output.Attributes.Add("data-ajax-complete", OnComplete);
                }
                if (!string.IsNullOrEmpty(OnFailure))
                {
                    output.Attributes.Add("data-ajax-failure", OnFailure);
                }
                if (!string.IsNullOrEmpty(OnSuccess))
                {
                    output.Attributes.Add("data-ajax-success", OnSuccess);
                }

                if (!string.IsNullOrEmpty(UpdateTargetId))
                {
                    output.Attributes.Add("data-ajax-update", UpdateTargetId);
                }

                if (!string.IsNullOrEmpty(InsertionMode) && !string.IsNullOrEmpty(UpdateTargetId))
                {
                    output.Attributes.Add("data-ajax-mode", InsertionMode);
                }

                output.Attributes.Add("data-ajax-url", Url);

                if (!string.IsNullOrEmpty(Cache))
                {
                    output.Attributes.Add("data-ajax-cache", Cache);
                }
            }
        }
    }
}
