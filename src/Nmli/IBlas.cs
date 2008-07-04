using System;


namespace Nmli
{
    /// <summary>
    /// Standard BLAS (http://www.netlib.org/blas/)
    /// </summary>
    /// <typeparam name="T">The number type, eg Single or Double</typeparam>
    public interface IBlas<T> : CollectionAgnostic.IBlas<T, T[]> { }

    /// <summary>
    /// Standard BLAS for Single and Double types
    /// </summary>
    public interface IBlas : IBlas<double>, IBlas<float> { }

}
