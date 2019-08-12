using Sample.Generator;
using System;
using System.Threading.Tasks;

namespace Sample.Consumer
{
    [DuplicateWithSuffixByNameOrdered("t", ExecutionOrder = 1)]
    [TryCatchOrdered(ExecutionOrder = 2)]
    public partial class Foo
    {
        [DuplicateWithSuffixByNameOrdered("t", ExecutionOrder = 1)]
        public object Prop1;

        [DuplicateWithSuffixByNameOrdered("k", ExecutionOrder = 1)]
        public int? Prop2;

        public void Test()
        {
            var aa = 1;
            var dd = 3;
            var bb = 0;
            var cc = aa / bb;
        }
    }

    [DuplicateWithSuffixByNameOrdered("t", ExecutionOrder = 2)]
    [TryCatchOrdered(ExecutionOrder = 1)]
    public partial class OtherFoo
    {
        [DuplicateWithSuffixByNameOrdered("t")]
        public object Prop1;

        [DuplicateWithSuffixByNameOrdered("k")]
        public int? Prop2;

        public void Test()
        {
            var aa = 1;
            var dd = 3;
            var bb = 0;
            var cc = aa / bb;
        }
    }
}
