//using System;
//using System.Threading.Tasks;
//using FluentAssertions;
//using Microsoft.Extensions.DependencyInjection;
//using StrongHeart.Features.Core;
//using StrongHeart.Features.Test.Helpers;
//using StrongHeart.Features.Test.SampleDecorator.SimpleLog;
//using StrongHeart.Features.Test.SampleFeatures.EventHandler.TestEventHandler;
//using Xunit;

//namespace StrongHeart.Features.Test
//{
//    public class FeatureEventHandlerTest
//    {
//        [Fact]
//        public async Task TestFullFeatureWithFullPipeline()
//        {
//            PipelineExtensionsStub extensions = new PipelineExtensionsStub();
//            using IServiceScope scope = extensions.CreateScope();

//            var meta = new EventMetadata("messageType", "publisherApplicationName", DateTime.Now, Guid.NewGuid(), "publishedByUserName");
//            var sut = scope.ServiceProvider.GetRequiredService<IEventHandlerFeature<TestEvent>>();
//            IResult result = await sut.Handle(new EventMessage<TestEvent>(new TestAdminCaller(), new TestEvent("newValue"), meta));
//            result.IsSuccess.Should().BeTrue();

//            extensions.AuditRepoSpy.Audits.Count.Should().Be(1);
//            extensions.ExceptionLoggerSpy.Exceptions.Count.Should().Be(0);
//        }

//        [Fact]
//        public async Task TestCustomDecorator()
//        {
//            SimpleLogSpy logSpy = new SimpleLogSpy();
//            SimpleLogExtension extension = new SimpleLogExtension(() => logSpy);
//            using IServiceScope scope = extension.CreateScope();

//            var meta = new EventMetadata("messageType", "publisherApplicationName", DateTime.Now, Guid.NewGuid(), "publishedByUserName");
//            var sut = scope.ServiceProvider.GetRequiredService<IEventHandlerFeature<TestEvent>>();
//            IResult result = await sut.Handle(new EventMessage<TestEvent>(new TestAdminCaller(), new TestEvent("newValue"), meta));
//            result.IsSuccess.Should().BeTrue();

//            logSpy.Messages.Count.Should().Be(2);
//        }
//    }
//}