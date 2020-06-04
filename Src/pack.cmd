dotnet pack .\StrongHeart.Core
xcopy StrongHeart.Core\bin\Debug\*.nupkg C:\development\NugetFeed /Y
dotnet pack .\StrongHeart.Features
xcopy StrongHeart.Features\bin\Debug\*.nupkg C:\development\NugetFeed /Y
dotnet pack .\StrongHeart.Migrations
xcopy StrongHeart.Migrations\bin\Debug\*.nupkg C:\development\NugetFeed /Y
dotnet pack .\StrongHeart.EfCore
xcopy StrongHeart.EfCore\bin\Debug\*.nupkg C:\development\NugetFeed /Y

