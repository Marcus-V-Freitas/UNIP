using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnipDLL.BLL.Libraries.Middleware
{
    /*
     * Classe para validação de requisição do método post
     */
    public class ValidateAntiForgeryTokenMiddleware
    {
        private RequestDelegate _next;
        private IAntiforgery _antiforgery;

        public ValidateAntiForgeryTokenMiddleware(RequestDelegate next, IAntiforgery antiforgery)
        {
            _next = next;
            _antiforgery = antiforgery;
        }

        public async Task Invoke(HttpContext context)
        {
            var cabecalho = context.Request.Headers["x-requested-with"];
            var AJAX = (cabecalho == "XMLHttpRequest") ? true : false;


            if (HttpMethods.IsPost(context.Request.Method) && !(context.Request.Form.Files.Count == 1 && AJAX))
            {

                await _antiforgery.ValidateRequestAsync(context);
            }

            await _next(context);
        }
    }
}
