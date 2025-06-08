using Helper_CL.custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text;
using static Helper_CL.custom._common;

namespace Helper_CL.middleware
{
    public class accessAttribute : Attribute
    {
        public roles[] __roles { get; set; }
        public accessAttribute(params roles[] _roles)
        {
            __roles = _roles;
        }
    }

    public class authorizationMiddleware
    {
        private readonly RequestDelegate _next;
        public authorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (!(bool)skip_authorization(context))
                {
                    var token = fetch_auth_header(context);

                    if (token == null)
                    {
                        challenge_auth_request(context);
                        return;
                    }

                    if (!on_authorize_user(token))
                    {
                        challenge_auth_request(context, token.GetError());
                        return;
                    }
                    context.Items.Add(_site_config.GetConfigValue("login_key").ToString(), token);
                    role_authorization(context);

                }
                log_request(context);
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        protected bool on_authorize_user(_token t)
        {
            return (t.IsValid());
        }

        protected virtual _token fetch_auth_header(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey(_site_config.GetConfigValue("auth_header")))
                return null;
            var token = context.Request.Headers[_site_config.GetConfigValue("auth_header")];
            if (token.First() == null || token.Count() == 0)
            {
                return null;
            }
            string strDec = token.First();
            // string strDec = "/k0ayDVaes+Zgndxp5cFNs5wtHt6oG9RF0CII9JAT290yckd8COofPZFGP5j01IX2q5UWb7mgy74eNA1yfDY5A==";
            if (string.IsNullOrEmpty(strDec)) return null;
            if (strDec.Trim().Length == 0) return null;
            _token t = new _token(strDec);

            return t;
        }

        public bool? skip_authorization(HttpContext context)
        {
            if (!context.Request.Path.ToString().Contains("api/")) return true;
            if (context.GetEndpoint()?.Metadata == null) return false;
            return context.Request.Path.ToString().Contains("api/") ? context.GetEndpoint()?.Metadata.Any(x => x.GetType() == typeof(AllowAnonymousAttribute)) : true;
        }

        /// <summary>
        /// Send the Authentication Challenge request
        /// </summary>
        /// <param name="filterContext"></param>
        private static void challenge_auth_request(HttpContext context, string Message = "Access Token Not Found")
        {
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.Content = new StringContent(Message);
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.WriteAsync(response.ToString());
        }

        private void role_authorization(HttpContext context)
        {
            accessAttribute? _accessAttribute = context.GetEndpoint()?.Metadata.ToList().Find(x => x.GetType() == typeof(accessAttribute)) as accessAttribute;
            if (_accessAttribute != null && context.Session.Keys.Contains("role"))
            {
                if (!_accessAttribute.__roles.ToList().Any(x => x.ToString() == context.Session.GetString("role")?.ToString()))
                {
                    throw new UnauthorizedAccessException();
                }
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = context.Response.StatusCode == 200 ? (int)HttpStatusCode.InternalServerError : context.Response.StatusCode;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                status_code = context.Response.StatusCode,
                message = exception.Message,
                stack_trace = exception.StackTrace
            }.ToString());
        }

        private void log_request(HttpContext context)
        {
            try
            {
                string user_code = "-1";
                var token = context.Request.Headers[_site_config.GetConfigValue("auth_header")];
                if (token.Count() != 0 && token.First() != null)
                {
                    string strDec = token.First();
                    _token t = new _token(strDec);
                    user_code = t.user_code.ToString();
                }
            }
            catch { }
        }

        public static string get_raw_body(HttpRequest request, System.Text.Encoding encoding = null)
        {
            try
            {
                if (!request.Body.CanSeek)
                {
                    request.EnableBuffering(int.MaxValue, int.MaxValue);
                }
                string request_body = "";
                StreamReader reader = new StreamReader(request.Body);
                request_body = reader.ReadToEndAsync().Result;

                return request_body;
            }
            catch
            {
                return "";
            }
            finally
            {
                request.Body.Position = 0;
            }
        }

    }
}
