using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
        public static NingaContext _context = new NingaContext();
        public static void Main(string[] args)
        {
            //InsertNinga();
            // InsertMultibleNinga();
            // InsertMultibleDifferentObjects();
            // SimpleNingaQuery();
            // MoreQueries();
            // RetriveAndUpdateNinga();
            // RetriveAndUpdateMultipleNinga();
            // MultipleDatabaseOperations();
            // InsertBattle();
            // QueryAndUpdateBattle_Disconnected();
            // AddSomeNingas();
            //DeleteWhileTracked();
            // DeleteWhileNotTracked();
            // DeleteMany();
            // AddChildToExistingObjectWhileTracked();
            // AddChildToExistingObjectWhileNotTracked();
            // InsertNewPKFKGraph();
            // EagerLoadNingaWithQuotes();
            // ProjectSomeProperties();
            //FilterWithRelatedData();
            ModifyingRelatedDataWhenNotTracked();
            CreateHostBuilder(args).Build().Run();
        }

        private static void ModifyingRelatedDataWhenNotTracked()
        {
            var ninga = _context.Ningas.Include(e => e.Quotes).FirstOrDefault();
            var quote = ninga.Quotes[0];
            quote.Text += "Did you hear that?";
            using var newContext = new NingaContext();
            // newContext.Quotes.Update(quote);
            newContext.Entry(quote).State = EntityState.Modified;
            newContext.SaveChanges();
        }

        private static void FilterWithRelatedData()
        {
            var ninga = _context.Ningas.Where(e => e.Quotes.Any(e => e.Text.Contains("Not"))).ToList();
        }

        public struct IdAndName
        {
            public IdAndName(int id, string name)
            {
                Id = id;
                Name = name;
            }
            public int Id;
            public string Name;
        }
        private static void ProjectSomeProperties()
        {
            var someProperties = _context.Ningas.Select(s => new 
            { 
                s.Id, 
                s.Name, 
                HappyQuote = s.Quotes.Where(q=>q.Text.Contains("NOT"))
            }).ToList();
            // var someProperties = _context.Ningas.Select(s => new IdAndName(s.Id, s.Name)).ToList();
        }

        private static void EagerLoadNingaWithQuotes()
        {
            var NingaWithQuotes = _context.Ningas.Include(e => e.Quotes)
                                                  .Include(e => e.SecretIdentity)
                                                  .FirstOrDefault(e => e.Name.Contains("AIZ"));
        }

        private static void AddChildToExistingObjectWhileNotTracked()
        {
            var ninga = _context.Ningas.First();
            ninga.Quotes.Add(new NingaApp.Domain.Quote
            {
                Text = "Now try another Not Tracked Method"
            });
            using (var newContext = new NingaContext())
            {
                newContext.Ningas.Add(ninga);
                //_context.SaveChanges();
            }
        }

        private async static void InsertNewPKFKGraph()
        {
            var Ninga = new NingaApp.Domain.Ninga
            {
                Name = "AIZ",
                Quotes = new List<NingaApp.Domain.Quote> {
                    new NingaApp.Domain.Quote {
                        Text = "I've come to save you"
                    }
                }
            };
            _context.Ningas.Add(Ninga);
            await _context.SaveChangesAsync();
        }

        private static async void AddChildToExistingObjectWhileTracked()
        {
            var ninga = _context.Ningas.First();
            ninga.Quotes.Add(new NingaApp.Domain.Quote { Text = "I bet you're happy that I've saved you" });
            _ = await _context.SaveChangesAsync();
        }

        private async static void DeleteMany()
        {
            var ningas = _context.Ningas.Where(s => s.Name.Contains("A"));
            _context.Ningas.RemoveRange(ningas);
            // alternative: _context.RemoveRange(ningas);
            _ = await _context.SaveChangesAsync();
        }

        private static void DeleteWhileNotTracked()
        {
            var ninga = _context.Ningas.FirstOrDefault(x => x.Name == "ALIA");
            using (var contextNewAppInstance = new NingaContext())
            {
                contextNewAppInstance.Ningas.Remove(ninga);
                contextNewAppInstance.SaveChanges();
            }
        }

        private static void DeleteWhileTracked()
        {
            var ninga = _context.Ningas.FirstOrDefault(e => e.Name == "Nego");
            _context.Ningas.Remove(ninga);
            _context.SaveChanges();
        }

        private async static void AddSomeNingas()
        {
            await _context.Ningas.AddRangeAsync(
                new NingaApp.Domain.Ninga { Name = "Nego" },
                new NingaApp.Domain.Ninga { Name = "ALIA" },
                new NingaApp.Domain.Ninga { Name = "ADHN" },
                new NingaApp.Domain.Ninga { Name = "AUIO" },
                new NingaApp.Domain.Ninga { Name = "NJSK" }
                );
            await _context.SaveChangesAsync();
        }

        private static void QueryAndUpdateBattle_Disconnected()
        {
            var battle = _context.Battles.FirstOrDefault();
            battle.EndDate = new DateTime(1555, 5, 5);
            // open new connection
            using (var newContextInstance = new NingaContext())
            {
                newContextInstance.Battles.Update(battle);
                newContextInstance.SaveChanges();
            }
        }

        private static void InsertBattle()
        {
            _context.Battles.Add(new NingaApp.Domain.Battle
            {
                Name = "Hetten",
                StartDate = new DateTime(1232, 2, 2),
                EndDate = new DateTime(1223, 2, 1)
            });
            _context.SaveChanges();
        }

        private static void MultipleDatabaseOperations()
        {
            var ninga = _context.Ningas.FirstOrDefault();
            ninga.Name += "Hero";
            _context.Ningas.Add(new NingaApp.Domain.Ninga { Name = "Fego" });
            _context.SaveChanges();
        }

        private async static void RetriveAndUpdateMultipleNinga()
        {
            var Ningas = _context.Ningas.ToList();
            Ningas.ForEach(e => e.Name += "San");
            await _context.SaveChangesAsync();
        }

        private async static void RetriveAndUpdateNinga()
        {
            var ninga = _context.Ningas.FirstOrDefault();
            ninga.Name += "San";
            await _context.SaveChangesAsync();
        }

        private static void MoreQueries()
        {
            var name = "Rami";
            var ninga = _context.Ningas.Where(e => e.Name == name).ToList();
        }

        private static void SimpleNingaQuery()
        {
            var ningas = _context.Ningas.ToList();
            // var query = _context.Ningas;
            foreach (var item in ningas)
            {
                Console.WriteLine(item);
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

            await _context.AddRangeAsync(NewNinga, NewBattle);
            await _context.SaveChangesAsync();
        }

        private async static void InsertMultibleNinga()
        {
            var NingaFaragallah = new NingaApp.Domain.Ninga { Name = "Faragallah" };
            var NingaRami = new NingaApp.Domain.Ninga { Name = "Rami" };

            await _context.Ningas.AddRangeAsync(NingaFaragallah, NingaRami);
            await _context.SaveChangesAsync();
        }

        private async static void InsertNinga()
        {
            var Ninga = new NingaApp.Domain.Ninga { Name = "Faragallah" };
            await _context.Ningas.AddAsync(Ninga);
            await _context.SaveChangesAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
