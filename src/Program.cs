WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

	// Add services to the container.
	IServiceCollection services = builder.Services;

	services.AddRazorPages();

	WebApplication app = builder.Build();

	// Configure the HTTP request pipeline.
	if (!app.Environment.IsDevelopment())
	{
		app.UseExceptionHandler("/Error");
		// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
		app.UseHsts();
	}
	
	app.UseDefaultFiles();
	app.UseStaticFiles();
	app.UseForwardedHeaders();
	app.UseRouting();
	app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });

	app.Run();