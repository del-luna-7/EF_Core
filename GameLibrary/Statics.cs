using GameLibrary.Data;
using GameLibrary.Entities;
using GameLibrary.Services;
using Microsoft.Extensions.Configuration;

namespace GameLibrary;

public sealed class Statics
{
	private Statics()
	{
	}

	private IConfiguration? _configuration;

	public IConfiguration GetConfiguration()
	{
		_configuration ??= new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

		return _configuration;
	}

	private ILibraryManager? _libraryManager;

	public ILibraryManager GetLibraryManager()
	{
		_libraryManager ??= new LibraryManager(this);

		return _libraryManager;
	}

	private IUserLibraryManager? _userLibraryManager;

	public IUserLibraryManager GetUserLibraryManager()
	{
		_userLibraryManager ??= new UserLibraryManager(this);

		return _userLibraryManager;
	}

	private IUserManager? _userManager;

	public IUserManager GetUserManager()
	{
		_userManager ??= new UserManager(this);

		return _userManager;
	}

	public DefaultDbContext CreateDbContext()
	{
		return new DefaultDbContext(this);
	}

	public void EnsureDbCreated()
	{
		using var dbContext = CreateDbContext();

		dbContext.Database.EnsureCreated();
	}

	private static Statics? _instance;
	public static Statics Instance => _instance ??= new Statics();
}