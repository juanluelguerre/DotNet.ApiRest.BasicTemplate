using ElGuerre.Items.Api.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ElGuerre.Items.Api.Infrastructure
{
	public class ItemsContextSeed
    {
        public async Task SeedAsync(ItemsContext dbContext,
            IWebHostEnvironment env,
            IOptions<AppSettings> settings,
            ILogger<ItemsContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(ItemsContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                if (dbContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                {
                    dbContext.Database.Migrate();
                }

                SeedData(dbContext);
                await dbContext.SaveChangesAsync();
            });
        }

        private void SeedData(ItemsContext dbContext)
        {
            dbContext.AddRange(new[] {
                new ItemEntity() { Id = 1, Name = "Peugeot 3008", Description = "My new future car" },
                new ItemEntity() { Id = 2, Name = "Big House", Description = "My Dream home" },
                new ItemEntity() { Id = 3, Name = "Bruno", Description = "My happy son" },
                new ItemEntity() { Id = 4, Name = "SSD", Description = "My new Mac Hard Disk" }
            });
        }

        private AsyncRetryPolicy CreatePolicy(ILogger<ItemsContextSeed> logger, string prefix, int retries = 3)
        {
            var policy = Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception,
                            "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}",
                            prefix, exception.GetType().Name,
                            exception.Message,
                            retry,
                            retries);
                    }
                );

            return policy;
        }
    }
}
