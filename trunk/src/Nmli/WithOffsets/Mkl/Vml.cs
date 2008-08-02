using System;

namespace Nmli.WithOffsets.Mkl
{
    using Externs = Nmli.Mkl.UnsafeExterns;


    unsafe class Vml : IVml
    {
        #region IVml<OA<float>> Members

        public void Ln(int n, OA<float> x, OA<float> y)
        {
            fixed (float* px = &x.array[x.offset], py = &y.array[y.offset])
                Externs.vsLn(n, px, py);
        }

        public void Exp(int n, OA<float> x, OA<float> y)
        {
            fixed (float* px = &x.array[x.offset], py = &y.array[y.offset])
                Externs.vsExp(n, px, py);
        }

        public void Sqr(int n, OA<float> x, OA<float> y)
        {
            fixed (float* px = &x.array[x.offset], py = &y.array[y.offset])
                Externs.vsSqr(n, px, py);
        }

        public void Inv(int n, OA<float> a, OA<float> y)
        {
            fixed (float* pa = &a.array[a.offset], py = &y.array[y.offset])
                Externs.vsInv(n, pa, py);
        }

        public void Sqrt(int n, OA<float> a, OA<float> y)
        {
            fixed (float* pa = &a.array[a.offset], py = &y.array[y.offset])
                Externs.vsSqrt(n, pa, py);
        }

        public void Add(int n, OA<float> a, OA<float> b, OA<float> y)
        {
            fixed (float* pa = &a.array[a.offset], pb = &b.array[b.offset], py = &y.array[y.offset])
                Externs.vsAdd(n, pa, pb, py);
        }

        public void Sub(int n, OA<float> a, OA<float> b, OA<float> y)
        {
            fixed (float* pa = &a.array[a.offset], pb = &b.array[b.offset], py = &y.array[y.offset])
                Externs.vsSub(n, pa, pb, py);
        }

        public void Mul(int n, OA<float> a, OA<float> b, OA<float> y)
        {
            fixed (float* pa = &a.array[a.offset], pb = &b.array[b.offset], py = &y.array[y.offset])
                Externs.vsMul(n, pa, pb, py);
        }

        public void Div(int n, OA<float> a, OA<float> b, OA<float> y)
        {
            fixed (float* pa = &a.array[a.offset], pb = &b.array[b.offset], py = &y.array[y.offset])
                Externs.vsDiv(n, pa, pb, py);
        }

        #endregion


        #region IVml<OA<double>> Members

        public void Ln(int n, OA<double> x, OA<double> y)
        {
            fixed (double* px = &x.array[x.offset], py = &y.array[y.offset])
                Externs.vdLn(n, px, py);
        }

        public void Exp(int n, OA<double> x, OA<double> y)
        {
            fixed (double* px = &x.array[x.offset], py = &y.array[y.offset])
                Externs.vdExp(n, px, py);
        }

        public void Sqr(int n, OA<double> a, OA<double> y)
        {
            fixed (double* pa = &a.array[a.offset], py = &y.array[y.offset])
                Externs.vdSqr(n, pa, py);
        }

        public void Inv(int n, OA<double> x, OA<double> y)
        {
            fixed (double* px = &x.array[x.offset], py = &y.array[y.offset])
                Externs.vdInv(n, px, py);
        }

        public void Sqrt(int n, OA<double> x, OA<double> y)
        {
            fixed (double* px = &x.array[x.offset], py = &y.array[y.offset])
                Externs.vdSqrt(n, px, py);
        }

        public void Add(int n, OA<double> a, OA<double> b, OA<double> y)
        {
            fixed (double* pa = &a.array[a.offset], pb = &b.array[b.offset], py = &y.array[y.offset])
                Externs.vdAdd(n, pa, pb, py);
        }

        public void Sub(int n, OA<double> a, OA<double> b, OA<double> y)
        {
            fixed (double* pa = &a.array[a.offset], pb = &b.array[b.offset], py = &y.array[y.offset])
                Externs.vdSub(n, pa, pb, py);
        }

        public void Mul(int n, OA<double> a, OA<double> b, OA<double> y)
        {
            fixed (double* pa = &a.array[a.offset], pb = &b.array[b.offset], py = &y.array[y.offset])
                Externs.vdMul(n, pa, pb, py);
        }

        public void Div(int n, OA<double> a, OA<double> b, OA<double> y)
        {
            fixed (double* pa = &a.array[a.offset], pb = &b.array[b.offset], py = &y.array[y.offset])
                Externs.vdDiv(n, pa, pb, py);
        }

        #endregion
    
    }
}
