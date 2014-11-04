﻿using Autofac;
using System;
using VaBank.Common.Events;
using VaBank.Jobs.Common;
using VaBank.Jobs.Configuration;

namespace VaBank.Jobs.Modules
{
    public class BackgroundServicesModule : Module
    {
        private readonly IServiceBus _serviceBus;

        public BackgroundServicesModule(IServiceBus serviceBus)
        {
            if (serviceBus == null)
            {
                throw new ArgumentNullException("serviceBus");
            }
            _serviceBus = serviceBus;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<CoreModule>();
            builder.RegisterModule<DataAccessModule>();
            builder.RegisterModule<ServicesModule>();
            builder.RegisterInstance(_serviceBus)
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(typeof (IJobContext).IsAssignableFrom)
                .AsSelf()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(DefaultJobContext<>))
                .As(typeof(IJobContext<>))
                .AsSelf()
                .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(typeof (IJob).IsAssignableFrom)
                .Where(x => !x.IsAbstract)
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterType<JobStartup>().As<IStartable>();

            //Configuration
            builder.RegisterType<SettingsJobConfigProvider>().As<IJobConfigProvider>().SingleInstance();
        }
    }
}