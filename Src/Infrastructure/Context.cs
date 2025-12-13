using Common.Domain;
using Domain.NotificationAgg;
using Domain.OrderAgg;
using Domain.ProductAgg;
using Domain.ProfitAgg;
using Domain.PurchaseReportAgg;
using Domain.RoleAgg;
using Domain.StockAgg;
using Domain.UserAgg;
using Infrastructure.Persistent.Ef.Product.Configuration;
using Infrastructure.Persistent.Ef.User.Configuration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics.Contracts;

namespace Infrastructure
{
    public class Context : DbContext
    {
        private readonly IMediator _mediator;
        public Context(DbContextOptions<Context> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<PurchaseReport> PurchaseReports { get; set; }
        public DbSet<Profit> Profits { get; set; }
        public DbSet<Domain.StockAgg.Stock> Stocks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users => Set<User>();
        public DbSet<Domain.Contract.ContractAgg> Contracts => Set<Domain.Contract.ContractAgg>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntities = GetModifiedEntities();

            await DispatchDomainEventsAsync();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private List<AggregateRoot> GetModifiedEntities() =>
             ChangeTracker.Entries<AggregateRoot>()
                 .Where(x => x.State != EntityState.Detached)
                 .Select(c => c.Entity)
                 .Where(c => c.DomainEvents.Any()).ToList();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            //optionsBuilder
            //    .UseSqlServer("your-connection-string")
            //    .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);

            //optionsBuilder.UseSqlServer(connectionString)
            //    .EnableSensitiveDataLogging()
            //    .LogTo(Console.WriteLine, LogLevel.Information);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
            //modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);


         
        }

        private async Task DispatchDomainEventsAsync()
        {
            var domainEntities = ChangeTracker.Entries<AggregateRoot>()
                .Where(x => x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }

    }
}
