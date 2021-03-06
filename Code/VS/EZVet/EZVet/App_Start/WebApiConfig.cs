﻿using EZVet.Common;
using EZVet.Filters;
using System;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace EZVet
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Adds authorization / authentication for all requests to controllers
            GlobalConfiguration.Configuration.Filters.Add(new GlobalAuthorizationFilter());

	        // Adds a filter that validate that the data sent from the client is valid.
	        GlobalConfiguration.Configuration.Filters.Add(new ValidateModelAttribute());

            // formats errors for client
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // serializing responses and request body dates
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(
             new EZVetDateConverter());

            // serializing dates sent on url
            config.ParameterBindingRules
              .Add(typeof(DateTime?), des => new DateTimeParameterBinding(des));
        }
    }
}
