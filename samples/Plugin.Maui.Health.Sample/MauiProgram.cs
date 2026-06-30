using System.Reflection;
using CommunityToolkit.Maui;
using Plugin.Maui.Health.Sample.Services;
using Plugin.Maui.Health.Sample.ViewModels;
using Plugin.Maui.Health.Sample.Views;

namespace Plugin.Maui.Health.Sample;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.UseMauiCommunityToolkit();

		builder.Services.AddSingleton<INavigationService, NavigationService>();
		builder.Services.AddSingleton(HealthDataProvider.Default);
        builder.Services.AddSingletonWithShellRoute<MainPage, MainPageViewModel>(nameof(MainPage));
		builder.Services.AddSingletonWithShellRoute<VitaminsView, VitaminsViewViewModel>(nameof(VitaminsView));
		builder.Services.AddSingletonWithShellRoute<BodyMeasurementsView, BodyMeasurementsViewViewModel>(nameof(BodyMeasurementsView));

#if DEBUG
		PrintDotnetInfo();
#endif

		return builder.Build();
    }

    static void PrintDotnetInfo()
#pragma warning restore IDE0051 // Remove unused private members
    {
#if ANDROID
	    var root = Assembly.GetExecutingAssembly().Location;
	    var assemblyPath = Path.Combine(Path.GetDirectoryName(root)!, "Microsoft.Maui.Controls.dll");
#else
        var root = AppContext.BaseDirectory;
        var assemblyPath = Path.Combine(root, "Microsoft.Maui.Controls.dll");
#endif
	    Console.WriteLine($"Assembly path: {assemblyPath}");
	    var assembly = Assembly.LoadFile(assemblyPath);
	    var fileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(assemblyPath);
	    var productVersion = fileVersionInfo.ProductVersion;
	    Console.WriteLine($"Product version: {productVersion}");
	    var mauiVersion = assembly.GetName().Version?.ToString();
	    Console.WriteLine($"Maui version: {mauiVersion}");
	    var dotnetVersion = Environment.Version;
	    Console.WriteLine($"Dotnet version: {dotnetVersion}");
	    var runtimeVersion = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
	    Console.WriteLine($"Runtime version: {runtimeVersion}");
    }
}