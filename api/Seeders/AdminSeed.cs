using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Seeders
{
    public class AdminSeed : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _config;
        private readonly ILogger<AdminSeed> _logger;

        public AdminSeed(IServiceProvider serviceProvider, IConfiguration config,
         ILogger<AdminSeed> logger)
        {
            _serviceProvider = serviceProvider;
            _config = config;
            _logger = logger;

        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

            var email = _config["SeedAdmin:Email"];
            var password = _config["SeedAdmin:Password"];

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                _logger.LogWarning("SeedAdmin skipped: SeedAdmin:Email/Password not configured.");
                return;
            }

            try
            {
                await userService.EnsureAdminExistsAsync(email, password);
                _logger.LogInformation("AdminEnsuredSuccessfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to seed admin");
                throw;
            }

        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    }
}