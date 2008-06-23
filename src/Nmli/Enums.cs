using System;

// The following are defined equal to the MKL CBLAS Enums (pg 2964 of MKL Reference Manual, March 2007)
namespace Nmli
{

    /// <summary>
    /// The matrix data storage format.
    /// </summary>
    public enum Order
    {
        /// <summary>
        /// The matrix array uses a row-major layout.
        /// </summary>
        Row = 101,

        /// <summary>
        /// The matrix array uses a column-major layout.
        /// </summary>
        Column = 102
    }

    /// <summary>
    /// Matrix transpose type.
    /// </summary>
    public enum Transpose
    {
        /// <summary>
        /// Don't transpose the matrix.  Equivalent to trans='N'
        /// </summary>
        NoTrans = 111,

        /// <summary>
        /// Transpose the matrix.  Equivalent to trans='T'
        /// </summary>
        Trans = 112,

        /// <summary>
        /// Conjugate transpose the matrix. The only refers to complex matrices. Real matrices will just be transposed.  Equivalent to trans='C'
        /// </summary>
        ConjTrans = 113
    }

    /// <summary>
    /// Triangular matrix type.
    /// </summary>
    public enum UpLo
    {
        /// <summary>
        /// Data is stored in the upper triangle of the matrix.    Equivalent to UpLo='U'
        /// </summary>
        Upper = 121,

        /// <summary>
        /// Data is stored in the lower triangle of the matrix.    Equivalent to UpLo='L'
        /// </summary>
        Lower = 122
    }

    /// <summary>
    /// Specifies whether a matrix is unit triangular.
    /// </summary>
    public enum Diag
    {
        /// <summary>
        /// The matrix is not unit triangular.    Equivalent to diag='N'
        /// </summary>
        NonUnit = 131,

        /// <summary>
        /// The matrix is unit triangular.  Equivalent to diag='U'
        /// </summary>
        Unit = 132
    }

    /// <summary>
    /// Matrix side to apply an operation. 
    /// </summary>
    public enum Side
    {
        /// <summary>
        /// Left side of the matrix.  Equivalent to side='L'
        /// </summary>
        Left = 141,

        /// <summary>
        /// Right side of the matrix.  Equivalent to side='R'
        /// </summary>
        Right = 142
    }


    public enum LibraryImplementations { ACML, MKL }
}
