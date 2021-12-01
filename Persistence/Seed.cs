using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Bikes.Any()) return;
            
            var activities = new List<Bike>
            {
                new Bike
                {
                    TotalTimeSpent = 0,
                    DateModified = DateTime.Now.AddDays(-1)
                },
                new Bike
                {
                    TotalTimeSpent = 0,
                    DateModified = DateTime.Now.AddDays(-2)
                },new Bike
                {
                    TotalTimeSpent = 0,
                    DateModified = DateTime.Now.AddDays(-3)
                },new Bike
                {
                    TotalTimeSpent = 0,
                    DateModified = DateTime.Now.AddDays(-4)
                },new Bike
                {
                    TotalTimeSpent = 0,
                    DateModified = DateTime.Now.AddDays(-5)
                }
            };

            await context.Bikes.AddRangeAsync(activities);
            await context.SaveChangesAsync();
        }
    }
}