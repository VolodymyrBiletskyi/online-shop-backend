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

            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var _passwordHash = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

            var hasAdmin = await db.Users.AsNoTracking()
                .AnyAsync(u => u.Role == UserRole.Admin);
            
            if(hasAdmin) return;

            var email = _config["SeedAdmin:Email"];
            var password = _config["SeedAdmin:Password"];

            if(string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                _logger.LogWarning("SeedAdmin skipped: SeedAdmin:Email/Password not configured.");
                return;
            }
            
            var existing =  await db.Users.SingleOrDefaultAsync(u => u.Email == email);
            if(existing is not null)
            {
                existing.Role = UserRole.Admin;
                await db.SaveChangesAsync();

                _logger.LogWarning("Seed admin: existing user promoted to Admin. Email ={Emai}",email);
                return;
            }
            
            var admin = new User
            {
                Id = Guid.NewGuid(),
                FullName = "Admin",
                Email = email,
                Role = UserRole.Admin,
                PasswordHash = _passwordHash.Hash(password)
            };

            db.Users.Add(admin);
            await db.SaveChangesAsync();

            _logger.LogWarning("Seed admin created. Email={Email}",email);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        
    }
}