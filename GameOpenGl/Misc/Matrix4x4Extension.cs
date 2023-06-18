using System.Numerics;

namespace GameOpenGl.Misc
{
    public static class Matrix4x4Extension
    {
        public static float[] ToFloatArray(this Matrix4x4 matrix)
        {
            var result = new float[16];

            result[0]  = matrix.M11;
            result[1]  = matrix.M12;
            result[2]  = matrix.M13;
            result[3]  = matrix.M14;
            result[4]  = matrix.M21;
            result[5]  = matrix.M22;
            result[6]  = matrix.M23;
            result[7]  = matrix.M24;
            result[8]  = matrix.M31;
            result[9]  = matrix.M32;
            result[10] = matrix.M33;
            result[11] = matrix.M34;
            result[12] = matrix.M41;
            result[13] = matrix.M42;
            result[14] = matrix.M43;
            result[15] = matrix.M44;

            return result;
        }

        public static void ShowMatrix(this Matrix4x4 matrix, string name)
        {
            var arrayMatrix = matrix.ToFloatArray();

            Console.Write(name + ":");
            for (int i = 0; i<arrayMatrix.Length; i++)
            {
                if (i % 4 == 0) Console.WriteLine();
                Console.Write(arrayMatrix[i] + "\t");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
