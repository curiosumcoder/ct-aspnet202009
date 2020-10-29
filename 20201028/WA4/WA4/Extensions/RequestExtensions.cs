using Microsoft.AspNetCore.Http;
using System;

namespace WA4.Extensions
{
    /// <summary>
    /// http://stackoverflow.com/questions/35202804/submitting-a-razor-form-using-jquery-ajax-in-mvc6-using-the-built-in-functionali
    /// </summary>
    public static class RequestExtensions
    {
        /// <summary>
        /// Determines whether the specified HTTP request is an AJAX request.
        /// </summary>
        /// <returns>
        /// true if the specified HTTP request is an AJAX request; otherwise, false.
        /// </returns>
        /// <param name="request">The HTTP request.</param>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }
    }
}
