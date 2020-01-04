using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Conferences.Domain.Interfaces;
using Conferences.Domain.Handlers;
using MediatR;
using Conferences.Domain.Conferences.Commands;
using Conferences.Domain.Organizers.Commands;
using Conferences.Domain.Core.Notifications;
using Conferences.Domain.Conferences.Events;
using Conferences.Domain.Organizers.Events;
using Conferences.Application.AppServies;
using Conferences.Application.Interfaces;
using Conferences.Domain.Conferences.Repository;
using Conferences.Domain.Organizers.Repository;
using Conferences.Infra.Data.UoW;
using Conferences.Infra.Data.Repository;
using Conferences.Infra.Data.Context;
using Conferences.Infra.Data.Repository.EventSourcing;
using Conferences.Infra.Data.EventSourcing;
using Conference.Infra.CrossCutting.Identity.Models;
using Conferences.Application.AutoMapper;

namespace Conferences.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASPNET
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            var mappingConfig = AutoMapperConfiguration.RegisterMapping();
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            //services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Application - Services
            services.AddScoped<IConferenceService, ConferenceService>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterConferenceCommand>, ConferenceCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateConferenceCommand>, ConferenceCommandHandler>();
            services.AddScoped<IRequestHandler<ExcludeConferenceCommand>, ConferenceCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateAddressConferenceCommand>, ConferenceCommandHandler>();
            services.AddScoped<IRequestHandler<AddAddressConferenceCommand>, ConferenceCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterOrganizerCommand>, OrganizerCommandHandler>();

            // Domain - Eventos
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<ConferenceRegisteredEvent>, ConferenceEventHandler>();
            services.AddScoped<INotificationHandler<ConferenceUpdatedEvent>, ConferenceEventHandler>();
            services.AddScoped<INotificationHandler<ConferenceExcludedEvent>, ConferenceEventHandler>();
            services.AddScoped<INotificationHandler<AddressConferenceUpdatedEvent>, ConferenceEventHandler>();
            services.AddScoped<INotificationHandler<AddressConferenceAddedEvent>, ConferenceEventHandler>();
            services.AddScoped<INotificationHandler<OrganizerRegisteredEvent>, OrganizerEventHandler>();

            // Infra - Data
            services.AddScoped<IConferenceRepository, ConferenceRepository>();
            services.AddScoped<IOrganizerRepository, OrganizerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ConferenceContext>();

            // Infra - Identity
            //services.AddTransient<IEmailSender, AuthMessageSender>();
            //services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddScoped<IUser, AspNetUser>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSqlContext>();
        }
    }
}
