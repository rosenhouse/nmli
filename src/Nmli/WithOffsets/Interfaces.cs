using System;


namespace Nmli.WithOffsets
{
    public interface IBlas<T> : CollectionAgnostic.IBlas<T, OA<T>> { }
    
    //public interface ILapack<T> : CollectionAgnostic.ILapack<OA<T>> { }

    //public interface IVml<T> : CollectionAgnostic.IVml<OA<T>> { }


    public interface IBlas : IBlas<float>, IBlas<double> { }

    //public interface ILapack : ILapack<float>, ILapack<double> { }

    //public interface IVml : IVml<float>, IVml<double> { }


}
