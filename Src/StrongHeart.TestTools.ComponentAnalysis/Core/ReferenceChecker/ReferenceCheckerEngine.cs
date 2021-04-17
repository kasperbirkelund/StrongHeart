using System;
using System.Collections.Generic;
using System.Linq;

namespace StrongHeart.TestTools.ComponentAnalysis.Core.ReferenceChecker
{
    internal class ReferenceCheckerEngine
    {
        //public static void A(Component component, Component mustNotReferenceMe, ICollection<Project> projects)
        //{
        //    var v1 = projects.SelectMany(x => x.References).Select(x=> x.Name).ToArray();
        //    var set1 =
        //        from p in projects.SelectMany(x => x.References)
        //        join c in component.ChildItems on p.Name equals c.Name
        //        select p.References;

        //    var set2 =
        //        from p in projects.SelectMany(x => x.References)
        //        join c in mustNotReferenceMe.ChildItems on p.Name equals c.Name
        //        select p.References;

        //    var set3 = set1.SelectMany(x => x).Select(x => x.Name);
        //    var set4 = set2.SelectMany(x => x).Select(x => x.Name);
        //    Print(set3);
        //    Print(set4);
        //    var v = set3.Intersect(set4).ToArray();
        //    Print(v);
        //}

        public static void B(Component component, Component mustNotReferenceMe, ICollection<Project> projects)
        {
            var set2 =
                from p in projects.SelectMany(x => x.References)
                join c in mustNotReferenceMe.ChildItems on p.Name equals c.Name
                select p.References;

            var set1 = from a in set2.SelectMany(x => x).Select(x => x.Name)
                join c in mustNotReferenceMe.ChildItems on a equals c.Name
                select c;

            var any = set1.Any();
            Console.Out.WriteLine(any);
        }

        public string CorrectiveAction => "Fix project references";

        public bool DoVerifyItem(string item)
        {
            throw new NotImplementedException();
        }

        public bool IsValid(string item, Action<string> output)
        {
            throw new NotImplementedException();
        }

        public bool DoFailIfNoItemsToVerify => true;
    }
}