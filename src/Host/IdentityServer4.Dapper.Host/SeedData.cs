﻿using IdentityServer4.Dapper.Host.Configuration;
using IdentityServer4.Dapper.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace IdentityServer4.Dapper.Host
{
    public class SeedData
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                Console.WriteLine("Seeding database...");
                var clientprovider = scope.ServiceProvider.GetService<IClientProvider>();
                EnsureSeedClientData(clientprovider);
                var identityprovider = scope.ServiceProvider.GetService<IIdentityResourceProvider>();
                EnsureSeedIdentityResourcesData(identityprovider);
                var apiprovider = scope.ServiceProvider.GetService<IApiResourceProvider>();
                EnsureSeedApiResourcesData(apiprovider);
                Console.WriteLine("Done seeding database.");
                Console.WriteLine();
            }
        }

        private static void EnsureSeedClientData(IClientProvider clientProvider)
        {
            if (clientProvider != null)
            {
                Console.WriteLine("Clients being populated");
                foreach (var client in Clients.Get().ToList())
                {
                    if (clientProvider.FindClientById(client.ClientId) == null)
                    {
                        clientProvider.Add(client);
                    }
                }
            }
            else
            {
                Console.WriteLine("Clients already populated");
            }
        }
        private static void EnsureSeedIdentityResourcesData(IIdentityResourceProvider identityResourceProvider)
        {
            if (identityResourceProvider != null)
            {
                Console.WriteLine("IdentityResources being populated");
                foreach (var resource in Resources.GetIdentityResources().ToList())
                {
                    if (identityResourceProvider.FindIdentityResourcesByName(resource.Name) == null)
                    {
                        identityResourceProvider.Add(resource);
                    }
                }
            }
            else
            {
                Console.WriteLine("IdentityResources already populated");
            }


        }

        private static void EnsureSeedApiResourcesData(IApiResourceProvider apiResourceProvider)
        {
            if (apiResourceProvider != null)
            {
                Console.WriteLine("ApiResources being populated");
                foreach (var resource in Resources.GetApiResources().ToList())
                {
                    if (apiResourceProvider.FindApiResource(resource.Name) == null)
                    {
                        apiResourceProvider.Add(resource);
                    }
                }
            }
            else
            {
                Console.WriteLine("ApiResources already populated");
            }
        }
    }
}
