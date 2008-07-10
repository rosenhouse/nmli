using System;

namespace Nmli
{

    public interface IBlas<T> : CollectionAgnostic.IBlas<T, T[]> { }

    public interface ILapack<T> : CollectionAgnostic.ILapack<T[]> { }

    public interface IVml<T> : CollectionAgnostic.IVml<T[]> { }

    

    public interface IBlas : IBlas<double>, IBlas<float> { }

    public interface ILapack : ILapack<double>, ILapack<float> { }
    
    public interface IVml : IVml<float>, IVml<double> { }




    public interface IIO<N> : CollectionAgnostic.IIO<N[]> { }
    public interface IIO : IIO<float>, IIO<double> { }
}
