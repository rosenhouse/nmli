using System;

namespace Nmli
{
    /// <summary>
    /// Standard LAPACK (http://www.netlib.org/lapack/)
    /// </summary>
    /// <typeparam name="T">The number type, eg Single or Double</typeparam>
    public interface ILapack<T> : CollectionAgnostic.ILapack<T[]> { }

    /// <summary>
    /// Standard LAPACK for Single and Double types
    /// </summary>
    public interface ILapack : ILapack<double>, ILapack<float> { }
}
