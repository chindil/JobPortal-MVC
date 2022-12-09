using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

public class HttpRequestHandler : DelegatingHandler
{
    public HttpRequestHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private IHttpContextAccessor _httpContextAccessor;
    public string Resource { get; }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!request.Headers.Contains("Authorization"))
        {
            // Fetch your token here
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
        //var response = await base.SendAsync(request, cancellationToken);
        //if (!response.IsSuccessStatusCode2)
        //{
        //    _writer.WriteLine("{0}\t{1}\t{2}", request.RequestUri,
        //        (int)response.StatusCode, response.Headers.Date);
        //}
        //return response;
        return await base.SendAsync(request, cancellationToken);
    }
}


public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestResponseLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        //First, get the incoming request
        //var request = await FormatRequest(context.Request);
                    
        //Format the response from the server
        //var response = await FormatResponse(context.Response);
        if(context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
        {

        }

        ////Copy a pointer to the original response body stream
        //var originalBodyStream = context.Response.Body;

        ////Create a new memory stream...
        //using (var responseBody = new MemoryStream())
        //{
        //    //...and use that for the temporary response body
        //    context.Response.Body = responseBody;

        //    //Continue down the Middleware pipeline, eventually returning to this class


        //    //TODO: Save log to chosen datastore

        //    //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
        //    await responseBody.CopyToAsync(originalBodyStream);
        //}
        await _next(context);
    }

}