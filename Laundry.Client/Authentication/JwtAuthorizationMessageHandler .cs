﻿// JwtAuthorizationMessageHandler.cs
using System.Net.Http.Headers;
using Microsoft.JSInterop;

namespace Laundry.Client.Authentication
{
   
    public class JwtAuthorizationMessageHandler : DelegatingHandler
    {
        private readonly IJSRuntime _jsRuntime;

        public JwtAuthorizationMessageHandler(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }

}
