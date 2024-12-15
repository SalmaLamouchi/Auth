using DAL.Config;
using DAL.Entities;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{

    public class DbContextFactory : IDbContextFactory, IDisposable
        {
            /// <summary>
            /// Create Db context with connection string
            /// </summary>
            /// <param name="settings"></param>
            public DbContextFactory(IOptions<DbContextSettings> settings)
            {
                var options = new DbContextOptionsBuilder<AuthDbContext>().UseNpgsql(settings.Value.DbConnectionString).EnableSensitiveDataLogging().Options;
                //,
                //npgsqlOptionsAction: s =>
                //{
                //  s.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                //}).Options;
                DbContext = new AuthDbContext(options);
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            /// <summary>
            /// Call Dispose to release DbContext
            /// </summary>
            ~DbContextFactory()
            {
                Dispose();
            }

            public AuthDbContext DbContext { get; private set; }
            /// <summary>
            /// Release DB context
            /// </summary>
            public void Dispose()
            {
                DbContext?.Dispose();
            }
        }


    }

