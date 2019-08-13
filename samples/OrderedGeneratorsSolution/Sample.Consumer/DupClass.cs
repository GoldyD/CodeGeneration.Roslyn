using Sample.Generator;

namespace Sample.Consumer
{
    [DuplicateWithSuffixByNameOrdered("t", ExecutionOrder = 1)]
    public class DupClass
    {
        public object Prop1;

        public void Test()
        {
            var aa = 1;
            var dd = 3;
            var bb = 0;
            var cc = aa / bb;
        }
    }
}
