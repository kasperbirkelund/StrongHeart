rem Generate a solution with all projects in subfolders named StrongHeart.sln

dotnet new sln --force
FOR /R %%i IN (*.csproj) DO dotnet sln add "%%i"