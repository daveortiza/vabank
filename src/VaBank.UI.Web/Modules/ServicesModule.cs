﻿using Autofac;
using AutoMapper;
using FluentValidation;
using System;
using System.Linq;
using VaBank.Common.Data;
using VaBank.Common.IoC;
using VaBank.Common.Validation;
using VaBank.Core.Common;
using VaBank.Services.Common;
using VaBank.Services.Contracts;
using Module = Autofac.Module;

namespace VaBank.UI.Web.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Add auto mapper profiles
            var mappingProfiles =
                typeof (BaseService).Assembly.GetTypes().Where(t => typeof (Profile).IsAssignableFrom(t)).ToList();
            mappingProfiles.ForEach(x => Mapper.AddProfile(Activator.CreateInstance(x) as Profile));

            //Register validation system
            builder.RegisterType<AutofacFactory>().AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterType<JsonNetConverter>().AsImplementedInterfaces().InstancePerRequest();
            var validatorTypes = typeof (BaseService).Assembly.GetTypes()
                .Union(typeof(Entity).Assembly.GetTypes())
                .Where(t => typeof (IValidator).IsAssignableFrom(t) || typeof(IObjectValidator).IsAssignableFrom(t))
                .Where(t => !t.IsGenericType)
                .ToList();
            var staticValidators =
                validatorTypes.Where(t => t.IsDefined(typeof (StaticValidatorAttribute), false)).ToList();
            var otherValidators = validatorTypes.Except(staticValidators).ToList();

            staticValidators.ForEach(t => builder.RegisterType(t).AsImplementedInterfaces().AsSelf().SingleInstance());
            otherValidators.ForEach(t => builder.RegisterType(t).AsImplementedInterfaces().AsSelf().InstancePerRequest());

            //Register operation provider
            builder.RegisterType<ServiceOperationProvider>().AsSelf()
                .InstancePerRequest();

            //Register dependency collections
            builder.RegisterAssemblyTypes(typeof (BaseService).Assembly)
                .Where(t => typeof (IDependencyCollection).IsAssignableFrom(t))
                .PropertiesAutowired()
                .AsSelf()
                .InstancePerRequest();

            //Register service bus
            builder.RegisterInstance(VaBankServiceBus.Instance)
                .AsImplementedInterfaces()
                .SingleInstance();

            //Register services
            builder.RegisterAssemblyTypes(typeof (BaseService).Assembly)
                .Where(t => typeof (IService).IsAssignableFrom(t))
                .AsImplementedInterfaces()
                .InstancePerRequest();
        }
    }
}