using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using Domain.Interfaces;
using Microsoft.Practices.Unity;
using Repositories.Repositories;
using Services.Interfaces;
using Services.Services;

namespace PublishSubscribe
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));


            //Dependency Injection
            var container = new UnityContainer();

            #region Types Definitions

            container.RegisterType<IMessageRepository, MessageRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMessageService, MessageService>(new HierarchicalLifetimeManager());

            container.RegisterType<IPublishersRepository, PublishersRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPublisherService, PublisherService>(new HierarchicalLifetimeManager());
            
            container.RegisterType<ISubscribersRepository, SubscribersRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISubscriberService, SubscriberService>(new HierarchicalLifetimeManager());
            
            #endregion

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
