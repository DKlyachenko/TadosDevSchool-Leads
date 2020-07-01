﻿namespace Leads.WebApi.Test.Tests.Common
{
    using System;
    using System.Net.Http;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Xunit;

    public abstract class ApiTestBase : IClassFixture<WebApiApplicationFactory>
    {
        private readonly WebApiApplicationFactory _webApiApplicationFactory;


        protected ApiTestBase(WebApiApplicationFactory webApiApplicationFactory)
        {
            _webApiApplicationFactory = webApiApplicationFactory ??
                                        throw new ArgumentNullException(nameof(webApiApplicationFactory));
        }


        protected HttpClient CreateClient()
        {
            return _webApiApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions()
            {
                HandleCookies = true
            });
        }

        protected Api.Api CreateApi()
        {
            return new Api.Api(CreateClient());
        }
    }
}