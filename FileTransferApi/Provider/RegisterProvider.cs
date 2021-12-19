using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FileTransferApi.Provider
{
    public static class ProviderExtensions
    {
        public static void RegisterProvider(this ContainerBuilder builder, Assembly assembly)
        {

            builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces();

            var providerConfigurationTypes = assembly.GetTypes().Where(t => t.BaseType == typeof(ProviderConfiguration));

            if (providerConfigurationTypes != null)
            {
                foreach (var type in providerConfigurationTypes)
                {
                    ProviderConfiguration providerType = (ProviderConfiguration)Activator.CreateInstance(type);

                    //register service types
                    var providerNamespaceTypes = assembly.GetTypes().Where(t => t.Namespace == type.Namespace);
                    var transferServicetype = providerNamespaceTypes.SingleOrDefault(t=>t.GetInterfaces().Contains(typeof(ITransferService))); 
                    if (transferServicetype != null)
                    {
                        builder.RegisterType(transferServicetype).Keyed<ITransferService>(providerType.Name);
                    }

                    //register settings types
                    var platformSettings = providerNamespaceTypes.SingleOrDefault(t => t.GetInterfaces().Contains(typeof(IProviderSettings)));
                    if (platformSettings != null)
                    {
                        builder.RegisterType(platformSettings).Keyed<IProviderSettings>(providerType.Name);
                    }

                    //register settings validator
                    var platformSettingsValidator = providerNamespaceTypes.SingleOrDefault(t => t.GetInterfaces().Contains(typeof(IProviderSettingsValidator)));
                    if (platformSettingsValidator != null)
                    {
                        builder.RegisterType(platformSettingsValidator).Keyed<IProviderSettingsValidator>(providerType.Name);
                    }
                }
            }

            builder.RegisterType<TransferServiceFactory>().As<ITransferServiceFactory>();
        }

        //public static IEnumerable<ProviderConfiguration> GetAllProviders(string path)
        //{
        //    List<ProviderConfiguration> providerTypes = new List<ProviderConfiguration>();
        //    foreach(var assembly in GetAssemblies(path))
        //    {
        //        var type = assembly.GetTypes().Where(t => t.BaseType == typeof(ProviderConfiguration)).SingleOrDefault();
        //        if (type != null)
        //        {
        //            ProviderConfiguration providerType = (ProviderConfiguration)Activator.CreateInstance(type);
        //            providerTypes.Add(providerType);
        //        }
        //    }

        //    return providerTypes;

        //}
    }
}
