using System;
using System.Collections.Generic;
using System.Text;

namespace Nmli.Extended
{
    public class MultinormalPdfExponent<N> : ExtendingFunc<N>
    {
        public MultinormalPdfExponent(IMathLibrary<N> ml): base(ml)
        {

        }

    }
}
