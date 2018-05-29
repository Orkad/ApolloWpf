using ApolloWpfCore.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using System.Collections.Generic;

namespace ApolloWpfCore.Test
{
    [TestClass]
    public class ObjectExtensionTest
    {
        public class SimpleClass
        {
            public int Primitive { get; set; }
            public string Str { get; set; }
        }

        public class ComplexClass
        {
            public SimpleClass Reference { get; set; }
        }

        public class DeepClass
        {
            public List<ComplexClass> List { get; set; }
        }

        [TestMethod]
        public void SimpleCopyTest()
        {
            var obj = new SimpleClass(){Primitive = 1, Str = "apollo"};
            var copy = obj.Copy();

            Check.That(obj).IsNotEqualTo(copy);
            Check.That(obj.Primitive).IsEqualTo(copy.Primitive);
            Check.That(obj.Str).IsEqualTo(copy.Str);
            obj.Primitive = 3;
            Check.That(obj.Primitive).IsNotEqualTo(copy.Primitive);
            obj.Str = "apollo ssc";
            Check.That(obj.Str).IsNotEqualTo(copy.Str);
        }

        [TestMethod]
        public void ComplexCopyTest()
        {
            var simple = new SimpleClass() { Primitive = 1, Str = "apollo ssc" };
            var obj = new ComplexClass{Reference = simple};
            var copy = obj.Copy();

            Check.That(obj).IsNotEqualTo(copy);
            Check.That(obj.Reference).IsNotEqualTo(copy.Reference);
            Check.That(obj.Reference.Str).IsEqualTo("apollo ssc");
        }

        [TestMethod]
        public void DeepCopyTest()
        {
            var simple = new SimpleClass() { Primitive = 1, Str = "apollo ssc" };
            var complex = new ComplexClass { Reference = simple };
            var obj = new DeepClass{List = new List<ComplexClass>{complex}};
            var copy = obj.Copy();

            Check.That(obj).IsNotEqualTo(copy);
            Check.That(obj.List).IsNotEqualTo(copy.List);
            Check.That(copy.List).HasSize(1);
            Check.That(obj.List[0]).IsNotEqualTo(copy.List[0]);
            Check.That(obj.List[0].Reference).IsNotEqualTo(copy.List[0].Reference);
            Check.That(obj.List[0].Reference.Str).IsEqualTo(copy.List[0].Reference.Str);

        }
    }
}
