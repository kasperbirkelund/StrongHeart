//using System.Linq;
//using StrongHeart.TestTools.ComponentAnalysis.Core;
//using StrongHeart.TestTools.ComponentAnalysis.Core.ReferenceChecker;
//using Xunit;

//namespace StrongHeart.TestTools.ComponentAnalysis.Test
//{
//    public class UnitTest1
//    {
//        private static readonly IProjectReader ProjectReader = new DotNetCoreProjectReader(@"C:\development\github\");
//        [Fact(Skip = "not implemented")]
//        public void Test1()
//        {
//            Component cWebApi = Components.GetWebApi();
//            Component cWebApp = Components.GetWebApp();

//            VerifyThat.Component(cWebApi).DoesNotReference(cWebApp).Using(ProjectReader);
//        }

//        [Fact(Skip = "not implemented")]
//        public void Test2()
//        {
//            var all = Components.GetAll().ToArray();
//            VerifyThat.Components(all).DoesNotHaveRedundancy();
//        }

//        [Fact(Skip = "not implemented")]
//        public void Test3()
//        {
//            var all = Components.GetAll().ToArray();
//            VerifyThat.Components(all).MatchAllExistingProjects(ProjectReader);
//        }
//    }
//}