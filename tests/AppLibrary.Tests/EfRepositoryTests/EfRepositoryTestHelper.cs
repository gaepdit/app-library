using AppLibrary.Tests.EfRepositoryTests.BaseRepositoryTests;
using AppLibrary.Tests.EfRepositoryTests.IncludePropertiesTests;
using AppLibrary.Tests.EfRepositoryTests.NamedEntityProjectToTests;
using AppLibrary.Tests.EfRepositoryTests.NamedEntityTests;
using AppLibrary.Tests.EfRepositoryTests.ProjectDerivedEntityTests;
using AppLibrary.Tests.EfRepositoryTests.ProjectToTests;
using AppLibrary.Tests.RepositoryTestHelpers;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TestSupport.EfHelpers;

namespace AppLibrary.Tests.EfRepositoryTests;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TestEntity> TestEntities => Set<TestEntity>();
    public DbSet<TestNamedEntity> TestNamedEntities => Set<TestNamedEntity>();
    public DbSet<NamedEntityWithChildProperty> NamedEntitiesWithChildProperty => Set<NamedEntityWithChildProperty>();
    public DbSet<EntityWithNavigationProperty> EntitiesWithNavigationProperty => Set<EntityWithNavigationProperty>();
    public DbSet<EntityWithChildProperty> EntitiesWithChildProperty => Set<EntityWithChildProperty>();

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
    //     optionsBuilder.LogTo(Console.WriteLine);
}

public sealed class EfRepositoryTestHelper : IDisposable, IAsyncDisposable
{
    private AppDbContext Context { get; set; } = null!;
    private readonly DbContextOptions<AppDbContext> _options;
    private readonly AppDbContext _context;

    /// Constructor used by <see cref="CreateRepositoryHelper"/>.
    private EfRepositoryTestHelper()
    {
        _options = SqliteInMemory.CreateOptions<AppDbContext>();
        _context = new AppDbContext(_options);
        _context.Database.EnsureCreated();
    }

    /// Constructor used by <see cref="CreateSqlServerRepositoryHelper"/>.
    private EfRepositoryTestHelper(object callingClass, string callingMember)
    {
        _options = callingClass.CreateUniqueMethodOptions<AppDbContext>(callingMember: callingMember,
            builder: opts => opts.UseSqlServer());
        _context = new AppDbContext(_options);
        _context.Database.EnsureClean();
    }

    /// Creates a new Sqlite database and returns a RepositoryHelper.
    public static EfRepositoryTestHelper CreateRepositoryHelper() => new();

    /// Creates a SQL Server database and returns a RepositoryHelper. Use of this method requires that
    /// an "appsettings.json" exists in the project root with a connection string named "UnitTestConnection".
    /// 
    /// The `callingClass` and `callingMember` parameters are used to generate a unique database for each unit test method.
    /// 
    /// `callingClass`: Enter `this`. The class of the unit test method requesting the Repository Helper.
    /// `callingMember`: Do not enter. The unit test method requesting the Repository Helper. This is filled in by the compiler.
    public static EfRepositoryTestHelper CreateSqlServerRepositoryHelper(
        object callingClass, [CallerMemberName] string callingMember = "") =>
        new(callingClass, callingMember);

    /// Stops tracking all currently tracked entities.
    /// See https://github.com/JonPSmith/EfCore.TestSupport/wiki/Using-SQLite-in-memory-databases#1-best-approach-one-instance-and-use-changetrackerclear
    public void ClearChangeTracker() => Context.ChangeTracker.Clear();

    /// Deletes all data from the EF database table for the specified entity.
    public async Task ClearTableAsync<TEntity>() where TEntity : class
    {
        Context.RemoveRange(Context.Set<TEntity>());
        await Context.SaveChangesAsync();
        ClearChangeTracker();
    }

    public TestRepository GetRepository(List<TestEntity> testData)
    {
        if (!_context.TestEntities.Any())
        {
            _context.TestEntities.AddRange(testData);
            _context.SaveChanges();
        }

        Context = new AppDbContext(_options);
        return new TestRepository(Context);
    }

    public NamedEntityRepository GetNamedEntityRepository(List<TestNamedEntity> testData)
    {
        if (!_context.TestNamedEntities.Any())
        {
            _context.TestNamedEntities.AddRange(testData);
            _context.SaveChanges();
        }

        Context = new AppDbContext(_options);
        return new NamedEntityRepository(Context);
    }

    public NamedEntityWithChildPropertyRepository GetNamedEntityWithChildPropertyRepository(
        List<NamedEntityWithChildProperty> testData)
    {
        if (!_context.NamedEntitiesWithChildProperty.Any())
        {
            _context.NamedEntitiesWithChildProperty.AddRange(testData);
            _context.SaveChanges();
        }

        Context = new AppDbContext(_options);
        return new NamedEntityWithChildPropertyRepository(Context);
    }

    public NavigationPropertiesRepository GetNavigationPropertiesRepository(
        List<EntityWithNavigationProperty> testData)
    {
        if (!_context.EntitiesWithNavigationProperty.Any())
        {
            _context.EntitiesWithNavigationProperty.AddRange(testData);
            _context.SaveChanges();
        }

        Context = new AppDbContext(_options);
        return new NavigationPropertiesRepository(Context);
    }

    public ProjectToRepository GetProjectToRepository(List<EntityWithChildProperty> testData)
    {
        if (!_context.EntitiesWithChildProperty.Any())
        {
            _context.EntitiesWithChildProperty.AddRange(testData);
            _context.SaveChanges();
        }

        Context = new AppDbContext(_options);
        return new ProjectToRepository(Context);
    }

    public ProjectDerivedEntityRepository GetProjectDerivedEntityRepository(List<TestEntity> testData)
    {
        if (!_context.EntitiesWithChildProperty.Any())
        {
            _context.TestEntities.AddRange(testData);
            _context.SaveChanges();
        }

        Context = new AppDbContext(_options);
        return new ProjectDerivedEntityRepository(Context);
    }

    public void Dispose()
    {
        _context.Dispose();
        Context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
        await Context.DisposeAsync();
    }
}
