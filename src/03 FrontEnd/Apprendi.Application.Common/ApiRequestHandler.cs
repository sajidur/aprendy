using Apprendi.Application.Common.Base;
using Apprendi.Application.Factories;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Apprendi.Application.Common
{
    public class ApiRequestHandler<TRequest, TResponse> : RequestHandler<TRequest, TResponse>
        where TRequest : Request<TResponse>
        where TResponse : Response, new()
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<ApiRequestHandlerOptions> _options;

        public ApiRequestHandler(HttpClient httpClient, IOptions<ApiRequestHandlerOptions> options, IResponseFactory<TResponse> responseFactory) : base(responseFactory)
        {
            _httpClient = httpClient;
            _options = options;
        }

        private static string IncludeTrailingSlash(string url)
        {
            url = url.Trim();
            return url.EndsWith('/') ? url : url + "/";
        }

        public override async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken) 
        {
            TResponse response;
            HttpResponseMessage httpResponse = null;
            try
            {
                var endpoint = $"{IncludeTrailingSlash(_options.Value.Endpoint)}{request.GetType().Name}";
                httpResponse = await _httpClient.PostAsJsonAsync(endpoint, request, cancellationToken);

                httpResponse.EnsureSuccessStatusCode();

                response = await httpResponse.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
            }
            catch (HttpRequestException e) when (IsUnavailable(e))
            {
                response = Activator.CreateInstance<TResponse>();
                response.StatusCode = System.Net.HttpStatusCode.ServiceUnavailable;
                response.ErrorMessage = "Sevice unavailable. Please try again after a few minutes. If the problem persists contact support";
            }
            catch (HttpRequestException e)
            {
                try
                {   
                    response = await httpResponse.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
                }
                catch (Exception)
                {
                    response = Activator.CreateInstance<TResponse>();
                }
                response.IsSuccess = false;
                if (string.IsNullOrWhiteSpace(response.ErrorMessage))
                {
                    response.ErrorMessage = e.Message;
                }
            }            
            return response;
        }

        private static bool IsUnavailable(HttpRequestException e)
        {
            if (e.Message.StartsWith("TypeError: Failed to fetch", StringComparison.OrdinalIgnoreCase)) return true;
            if (e.Message.StartsWith("No connection could be made because the target machine actively refused it", StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }
    }

    public class ApiRequestHandlerOptions
    {
        public string Endpoint { get; set; }
    }
}
