using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NingaApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //InsertNinga();
            // InsertMultibleNinga();
            // InsertMultibleDifferentObjects();
            SimpleNingaQuery();
            CreateHostBuilder(args).Build().Run();
        }

        private static void SimpleNingaQuery()
        {
            using (var context = new NingaContext())
            {
                 var ningas = context.Ningas.ToList();
                // var query = context.Ningas;
                foreach (var item in ningas)
                {
                    Console.WriteLine(item);
                }
            }
        }

        private async static void InsertMultibleDifferentObjects()
        {
            var NewNinga = new NingaApp.Domain.Ninga { Name = "Salah Aldin" };
            var NewBattle = new NingaApp.Domain.Battle
            {
                Name = "Hatten",
                StartDate = new DateTime(1595, 03, 12),
                EndDate = new DateTime(1533, 03, 12)
            };
            using var context = new NingaContext();
            await context.AddRangeAsync(NewNinga, NewBattle);
            await context.SaveChangesAsync();
        }

        private async static void InsertMultibleNinga()
        {
            var NingaFaragallah = new NingaApp.Domain.Ninga { Name = "Faragallah" };
            var NingaRami = new NingaApp.Domain.Ninga { Name = "Rami" };
            using var context = new NingaContext();
            await context.Ningas.AddRangeAsync(NingaFaragallah, NingaRami);
            await context.SaveChangesAsync();
        }

        private async static void InsertNinga()
        {
            var Ninga = new NingaApp.Domain.Ninga { Name = "Faragallah" };
            using (var context = new NingaContext())
            {
                await context.Ningas.AddAsync(Ninga);
                await context.SaveChangesAsync();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
