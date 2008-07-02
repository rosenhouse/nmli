using System;

namespace Nmli
{

    public interface IVml<T> : CollectionAgnostic.IVml<T[]> { }

    public interface IVml : IVml<float>, IVml<double> { }

}
