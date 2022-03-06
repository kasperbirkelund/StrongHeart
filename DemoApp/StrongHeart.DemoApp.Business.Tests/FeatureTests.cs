using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Core.Events;
using StrongHeart.Features.DependencyInjection;
using Xunit;

namespace StrongHeart.DemoApp.Business.Tests;

public class FeatureTests
{
    [Fact]
    public async Task A()
    {
        ServiceCollection services = new ServiceCollection();
        services.AddStrongHeart(_ =>{}, typeof(CreateCarRequest).Assembly);
        services.AddSingleton<IEventPublisher, DummyEventPublisher>();
        services.AddTransient<IFoo, Foo>();

        using (var executor = TestExecutor.Create(services))
        {
            //await executor.RunCommand<CreateCarRequest, CreateCarDto>(new CreateCarRequest(Guid.NewGuid(), new CreateCarDto("Skoda"), new TestAdminCaller()));
            await executor.RunQuery<GetCarRequest, GetCarResponse>(new GetCarRequest(1, new TestAdminCaller()));
            await executor.RunQuery<GetCarsRequest, GetCarsResponse>(new GetCarsRequest("abc", new TestAdminCaller()));
        }
    }
}