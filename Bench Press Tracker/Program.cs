﻿
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Bench_Press_Tracker.Models;
using Bench_Press_Tracker.Repositories;
using Microsoft.EntityFrameworkCore;

class Program
{
    static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
    }


    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
                services.AddDbContext<BenchPressContext>(options =>
                    options.UseSqlServer("Server=(localdb)\\Practice;Database=exercises;Trusted_Connection=True;"))
                .AddScoped < IRepository<BenchPressItem>, Repository<BenchPressItem> >());

}