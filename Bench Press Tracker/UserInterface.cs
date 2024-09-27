using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Bench_Press_Tracker.Repositories;
using Bench_Press_Tracker.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Bench_Press_Tracker.Helpers;
namespace Bench_Press_Tracker
{
    public class UserInterface
    {
        IHost _host;
        public UserInterface(IHost host) 
        {
            _host = host;
            var repository = host.Services.GetRequiredService<IRepository<BenchPressItem>>();
            while (true)
            {
                var options = new Dictionary<string, Action>
                {
                    {"Add", () => Add() },
                    {"Update", () => Update() },
                    {"Delete", () => Delete() },
                    {"Read by Id", () => ReadById() },
                    {"Read all", () => ReadAll() }
                };

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Welcome to the Bench Press Tracker, please choose an option")
                        .PageSize(10)
                        .AddChoices(options.Keys));

                options[choice].Invoke();

            }
        }

        public void Add()
        {
            Console.WriteLine("Start date");
            var sessionStart = DateHelper.getUserDate();
            Console.WriteLine("End date");
            var sessionEnd = DateHelper.getUserDate();
            var duration = DateHelper.calculateDuration(sessionStart, sessionEnd);
            var setsXReps = AnsiConsole.Ask<string>("Enter [green]Sets x Reps[/] (e.g., 3x12):");
            var comments = AnsiConsole.Ask<string>("Enter any [green]Comments[/]:");

            var newItem = new BenchPressItem
            {
                SessionStart = sessionStart,
                SessionEnd = sessionEnd,
                Duration = duration,
                SetsXReps = setsXReps,
                Comments = comments
            };

            var repository = _host.Services.GetRequiredService<IRepository<BenchPressItem>>();
            repository.Add(newItem);
            repository.SaveChanges();

            AnsiConsole.MarkupLine("[green]Item added successfully![/]");
        }

        public void Update()
        {
            var repository = _host.Services.GetRequiredService<IRepository<BenchPressItem>>();

            int id = AnsiConsole.Ask<int>("Enter the [green]ID[/] of the item to update:");
            var item = repository.GetById(id);

            if (item == null)
            {
                AnsiConsole.MarkupLine("[red]Item not found![/]");
                return;
            }

            Console.WriteLine("Start date");
            var sessionStart = DateHelper.getUserDate();
            Console.WriteLine("End date");
            var sessionEnd = DateHelper.getUserDate();
            var duration = DateHelper.calculateDuration(sessionStart, sessionEnd);
            var setsXReps = AnsiConsole.Ask<string>($"Enter [green]Sets x Reps[/]");
            var comments = AnsiConsole.Ask<string>($"Enter [green]Comments[/]");


            repository.Update(item);
            repository.SaveChanges();

            AnsiConsole.MarkupLine("[green]Item updated successfully![/]");
        }


        public void Delete()
        {
            var repository = _host.Services.GetRequiredService<IRepository<BenchPressItem>>();

            int id = AnsiConsole.Ask<int>("Enter the [red]ID[/] of the item to delete:");
            var item = repository.GetById(id);

            if (item == null)
            {
                AnsiConsole.MarkupLine("[red]Item not found![/]");
                return;
            }

            repository.Delete(id);
            repository.SaveChanges();

            AnsiConsole.MarkupLine("[green]Item deleted successfully![/]");
        }

        public void ReadById()
        {
            var repository = _host.Services.GetRequiredService<IRepository<BenchPressItem>>();

            int id = AnsiConsole.Ask<int>("Enter the [green]ID[/] of the item to read:");
            var item = repository.GetById(id);

            if (item == null)
            {
                AnsiConsole.MarkupLine("[red]Item not found![/]");
                return;
            }

            AnsiConsole.MarkupLine($"[yellow]ID:[/] {item.Id}");
            AnsiConsole.MarkupLine($"[yellow]Session Start:[/] {item.SessionStart}");
            AnsiConsole.MarkupLine($"[yellow]Session End:[/] {item.SessionEnd}");
            AnsiConsole.MarkupLine($"[yellow]Duration:[/] {item.Duration}");
            AnsiConsole.MarkupLine($"[yellow]Sets x Reps:[/] {item.SetsXReps}");
            AnsiConsole.MarkupLine($"[yellow]Comments:[/] {item.Comments}");
        }

        public void ReadAll()
        {
            var repository = _host.Services.GetRequiredService<IRepository<BenchPressItem>>();
            var items = repository.GetAll();

            if (!items.Any())
            {
                AnsiConsole.MarkupLine("[red]No items found![/]");
                return;
            }

            foreach (var item in items)
            {
                AnsiConsole.MarkupLine($"[yellow]ID:[/] {item.Id}");
                AnsiConsole.MarkupLine($"[yellow]Session Start:[/] {item.SessionStart}");
                AnsiConsole.MarkupLine($"[yellow]Session End:[/] {item.SessionEnd}");
                AnsiConsole.MarkupLine($"[yellow]Duration:[/] {item.Duration}");
                AnsiConsole.MarkupLine($"[yellow]Sets x Reps:[/] {item.SetsXReps}");
                AnsiConsole.MarkupLine($"[yellow]Comments:[/] {item.Comments}");
                AnsiConsole.MarkupLine("------------------------------------");
            }
        }

    }
}
