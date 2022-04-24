using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync();
                await Seed.SeedData(context);
                
            }
            catch(Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "This error occured because your migration is sucks");
            }
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
/*


public class UndergroundSystem {
    public List<CheckData> CheckList = new List<CheckData>();
    public UndergroundSystem() {
        
    }
    
    public void CheckIn(int id, string stationName, int t) {
        CheckList.Add(
        new CheckData{
            Id = id,
            StationName = stationName,
            Time = Convert.ToDouble(t),
            IsIn = true
        }
        );
        
    }
    
    public void CheckOut(int id, string stationName, int t) {
        CheckList.Add(
        new CheckData{
            Id = id,
            StationName = stationName,
            Time = Convert.ToDouble(t),
            IsIn = false
        }
        );
    }
    
    public double GetAverageTime(string startStation, string endStation) {
        
        var allCheckInCurrentStation = CheckList.Where(c => c.StationName == startStation && c.IsIn == true).ToList();
        
        var allCheckOutCurrentStation = CheckList.Where(c => c.StationName == endStation && c.IsIn == false).ToList();
        
        List<double> resList = new List<double>();
        
        foreach(var startSt in allCheckInCurrentStation)
        {
            var currentIdCheckOut = allCheckOutCurrentStation.Where(c => c.Id == startSt.Id).FirstOrDefault();
            if(currentIdCheckOut == null)
                return resList.Sum() / resList.Count;
            
            var currentTime = currentIdCheckOut.Time - startSt.Time;
            resList.Add(currentTime);
        }
        
        /*double sum = Convert.ToDouble(resList.Sum());
        double count = Convert.ToDouble(resList.Count);
        return  sum / count;*/
        
        return resList.Sum() / resList.Count;
            
    }
}

public class CheckData{
    
    public int Id {get; set;}
    
    public string StationName {get; set;}

    public double Time {get; set;}

    public bool IsIn {get; set;}

}

 */
