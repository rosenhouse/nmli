using System;
using System.IO;
namespace Nmli.IO
{
    public class ManagedIO : IIO
    {

        #region Single
        public void Write(BinaryWriter bw, int n, float[] buffer)
        {
            for (int i = 0; i < n; i++)
                bw.Write(buffer[i]);
        }

        public void WriteF32(BinaryWriter bw, int n, float[] buffer) { Write(bw, n, buffer); }

        public void WriteF64(BinaryWriter bw, int n, float[] buffer)
        {
            for (int i = 0; i < n; i++)
                bw.Write((double)buffer[i]);
        }
        

        public void Read(BinaryReader br, int n, float[] buffer)
        {
            for (int i = 0; i < n; i++)
                buffer[i] = br.ReadSingle();
        }

        public void ReadF32(BinaryReader br, int n, float[] buffer) { Read(br, n, buffer); }

        public void ReadF64(BinaryReader br, int n, float[] buffer)
        {
            for (int i = 0; i < n; i++)
                buffer[i] = (float)(br.ReadDouble());
        }
        #endregion



        #region Double
        public void Write(BinaryWriter bw, int n, double[] buffer)
        {
            for (int i = 0; i < n; i++)
                bw.Write(buffer[i]);
        }

        public void WriteF32(BinaryWriter bw, int n, double[] buffer)
        {
            for (int i = 0; i < n; i++)
                bw.Write((float)buffer[i]);
        }

        public void WriteF64(BinaryWriter bw, int n, double[] buffer) { Write(bw, n, buffer); }
        

        public void Read(BinaryReader br, int n, double[] buffer)
        {
            for (int i = 0; i < n; i++)
                buffer[i] = br.ReadDouble();
        }


        public void ReadF32(BinaryReader br, int n, double[] buffer)
        {
            for (int i = 0; i < n; i++)
                buffer[i] = (double)(br.ReadSingle());
        }

        public void ReadF64(BinaryReader br, int n, double[] buffer) { Read(br, n, buffer); }
        #endregion

    }
}
