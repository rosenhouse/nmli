using System;
namespace Nmli
{
    public interface IMathLibrary<T>
    {
        IBlas<T> Blas { get; }
        ILapack<T> Lapack { get; }
        IVml<T> Vml { get; }
        ISml<T> Sml { get; }
    }

    public interface IMathLibrary : IMathLibrary<float>, IMathLibrary<double>
    {
        new IBlas Blas { get; }
        new ILapack Lapack { get; }
        new IVml Vml { get; }
        new ISml Sml { get; }
    }



}
