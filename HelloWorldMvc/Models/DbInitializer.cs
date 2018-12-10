using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using HelloWorldMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldMvc.Models
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            AppDbContext context = applicationBuilder.ApplicationServices.GetRequiredService<AppDbContext>();

            //Seed the database with the initial welcome message if no messages exist
            if (!context.Messages.Any())
            {
                context.Add
                (
                    new Message { GreetingMessageId = 1, GreetingMessage = "Hello World!"}
                );
                context.SaveChanges();
            }
        }
    }
}
