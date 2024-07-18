using SHARMemory.Memory;
using System;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(Matrix4x4Struct))]
    public struct Matrix4x4 : IEquatable<Matrix4x4>
    {
        public const int Size = sizeof(float) * 4 * 4;

        public float M11;
        public float M12;
        public float M13;
        public float M14;

        public float M21;
        public float M22;
        public float M23;
        public float M24;

        public float M31;
        public float M32;
        public float M33;
        public float M34;

        public float M41;
        public float M42;
        public float M43;
        public float M44;

        public Matrix4x4(float value)
        {
            M11 = value;
            M12 = value;
            M13 = value;
            M14 = value;

            M21 = value;
            M22 = value;
            M23 = value;
            M24 = value;

            M31 = value;
            M32 = value;
            M33 = value;
            M34 = value;

            M41 = value;
            M42 = value;
            M43 = value;
            M44 = value;
        }

        public Matrix4x4(float M11, float M12, float M13, float M14,  float M21, float M22, float M23, float M24, float M31, float M32, float M33, float M34, float M41, float M42, float M43, float M44)
        {
            this.M11 = M11;
            this.M12 = M12;
            this.M13 = M13;
            this.M14 = M14;

            this.M21 = M21;
            this.M22 = M22;
            this.M23 = M23;
            this.M24 = M24;

            this.M31 = M31;
            this.M32 = M32;
            this.M33 = M33;
            this.M34 = M34;

            this.M41 = M41;
            this.M42 = M42;
            this.M43 = M43;
            this.M44 = M44;
        }

        public void Identity()
        {
            M11 = 1f;
            M12 = 0f;
            M13 = 0f;
            M14 = 0f;

            M21 = 0f;
            M22 = 1f;
            M23 = 0f;
            M24 = 0f;

            M31 = 0f;
            M32 = 0f;
            M33 = 1f;
            M34 = 0f;

            M41 = 0f;
            M42 = 0f;
            M43 = 0f;
            M44 = 1f;
        }

        public void IdentityRotation()
        {
            M11 = 1f;
            M22 = 1f;
            M33 = 1f;

            M12 = 0f;
            M13 = 0f;
            M21 = 0f;
            M23 = 0f;
            M31 = 0f;
            M32 = 0f;
        }

        public void IdentityTranslation()
        {
            M41 = 0f;
            M42 = 0f;
            M43 = 0f;
        }

        public void IdentityProjection()
        {
            M14 = 0f;
            M24 = 0f;
            M34 = 0f;
            M44 = 1f;
        }

        public void FillRotateX(float angle)
        {
            M11 = 1f;
            M12 = 0f;
            M13 = 0f;
            M21 = 0f;
            M31 = 0f;

            float sinX = (float)Math.Sin(angle);
            float cosX = (float)Math.Cos(angle);

            M22 = cosX;
            M33 = cosX;
            M32 = -sinX;
            M23 = sinX;
        }

        public void FillRotateY(float angle)
        {
            M22 = 1f;
            M12 = 0f;
            M21 = 0f;
            M23 = 0f;
            M32 = 0f;

            float sinY = (float)Math.Sin(angle);
            float cosY = (float)Math.Cos(angle);

            M11 = cosY;
            M33 = cosY;
            M13 = -sinY;
            M31 = sinY;
        }

        public void FillRotateZ(float angle)
        {
            M33 = 1f;
            M13 = 0f;
            M23 = 0f;
            M31 = 0f;
            M32 = 0f;

            float sinZ = (float)Math.Sin(angle);
            float cosZ = (float)Math.Cos(angle);

            M11 = cosZ;
            M22 = cosZ;
            M21 = -sinZ;
            M12 = sinZ;
        }

        public void FillRotation(Vector3 axis, float theta)
        {
            axis.Normalize();

            float halfTheta = theta * 0.5f;
            float sinHalfTheta = (float)Math.Sin(halfTheta);
            float cosHalfTheta = (float)Math.Cos(halfTheta);

            float qx = axis.X * sinHalfTheta;
            float qy = axis.Y * sinHalfTheta;
            float qz = axis.Z * sinHalfTheta;
            float qw = cosHalfTheta;

            float xs = qx * 2;
            float ys = qy * 2;
            float zs = qz * 2;

            float wx = qw * xs;
            float wy = qw * ys;
            float wz = qw * zs;

            float xx = qx * xs;
            float xy = qx * ys;
            float xz = qx * zs;

            float yy = qy * xs;
            float yz = qy * ys;

            float zz = qz * zs;

            M11 = 1f - (yy + zz);
            M12 = xy - wz;
            M13 = xz + wy;

            M21 = xy + wz;
            M22 = 1f - (xx + zz);
            M23 = yz - wx;

            M31 = xz - wy;
            M32 = yz + wx;
            M33 = 1f - (xx + yy);
        }

        public void FillRotateXYZ(float angleX, float angleY, float angleZ)
        {
            float sinX = (float)Math.Sin(angleX);
            float cosX = (float)Math.Cos(angleX);
            float sinY = (float)Math.Sin(angleY);
            float cosY = (float)Math.Cos(angleY);
            float sinZ = (float)Math.Sin(angleZ);
            float cosZ = (float)Math.Cos(angleZ);

            float sinXsinY = sinX * sinY;
            float cosXsinY = cosX * sinY;

            M11 = cosY * cosZ;
            M12 = cosY * sinZ;
            M13 = -sinY;
            M21 = sinXsinY * cosZ - cosX * sinZ;
            M22 = sinXsinY * sinZ + cosX * cosZ;
            M23 = sinX * cosY;
            M31 = cosXsinY * cosZ + sinX * sinZ;
            M32 = cosXsinY * sinZ - sinX * cosZ;
            M33 = cosX * cosY;
        }

        public void FillTranslate(Vector3 vector)
        {
            M41 = vector.X;
            M42 = vector.Y;
            M43 = vector.Z;
        }

        public void Transform(Vector3 src, ref Vector3 dest)
        {
            float x = M11 * src.X + M21 * src.Y + M31 * src.Z + M41;
            float y = M12 * src.X + M22 * src.Y + M32 * src.Z + M42;
            float z = M13 * src.X + M23 * src.Y + M33 * src.Z + M43;

            dest.Set(x, y, z);
        }

        public void Transpose()
        {
            float t1 = M12;
            float t2 = M13;
            float t3 = M14;
            float t4 = M23;
            float t5 = M24;
            float t6 = M34;

            M12 = M21;
            M13 = M31;
            M14 = M41;
            M23 = M32;
            M24 = M42;
            M34 = M43;

            M21 = t1;
            M31 = t2;
            M41 = t3;
            M32 = t4;
            M42 = t5;
            M43 = t6;
        }

        public void InvertOrtho()
        {
            float t0 = -M41;
            float t1 = -M42;
            float t2 = -M43;

            Transpose();
            IdentityProjection();
            M41 = t0 * M11 + t1 * M21 + t2 * M31;
            M42 = t0 * M12 + t1 * M22 + t2 * M32;
            M43 = t0 * M13 + t1 * M23 + t2 * M33;
        }


        public void Invert()
        {
            float det;
            det  = M11 * M22 * M33;
            det += M12 * M23 * M31;
            det += M13 * M21 * M32;
            det += M13 * M22 * M31;
            det += M12 * M21 * M33;
            det += M11 * M23 * M31;

            if (Globals.RadMathUtil.Epsilon(det, 1f))
            {
                InvertOrtho();
            }
            else
            {
                Matrix4x4 tmp = new(0);
                float inverseDet = 1f / det;

                tmp.M11 = ((M22 * M33) - (M23 * M32)) * inverseDet;
                tmp.M21 = -((M21 * M33) - (M23 * M31)) * inverseDet;
                tmp.M31 = ((M21 * M32) - (M22 * M31)) * inverseDet;
                tmp.M41 = M41;
                tmp.M12 = -((M12 * M33) - (M13 * M32)) * inverseDet;
                tmp.M22 = ((M11 * M33) - (M13 * M31)) * inverseDet;
                tmp.M32 = -((M11 * M32) - (M12 * M31)) * inverseDet;
                tmp.M42 = M42;
                tmp.M13 = ((M12 * M23) - (M13 * M22)) * inverseDet;
                tmp.M23 = -((M11 * M23) - (M13 * M21)) * inverseDet;
                tmp.M33 = ((M11 * M22) - (M12 * M21)) * inverseDet;
                tmp.M43 = M43;

                tmp.M14 = -((M41 * tmp.M11) + (M42 * tmp.M21) + (M43 * tmp.M31));
                tmp.M24 = -((M41 * tmp.M12) + (M42 * tmp.M22) + (M43 * tmp.M32));
                tmp.M34 = -((M41 * tmp.M13) + (M42 * tmp.M23) + (M43 * tmp.M33));
                tmp.M44 = M44;

                Set(tmp);

                IdentityProjection();
            }
        }

        public void Set(Matrix4x4 matrix)
        {
            M11 = matrix.M11;
            M12 = matrix.M12;
            M13 = matrix.M13;
            M14 = matrix.M14;

            M21 = matrix.M21;
            M22 = matrix.M22;
            M23 = matrix.M23;
            M24 = matrix.M24;

            M31 = matrix.M31;
            M32 = matrix.M32;
            M33 = matrix.M33;
            M34 = matrix.M34;

            M41 = matrix.M41;
            M42 = matrix.M42;
            M43 = matrix.M43;
            M44 = matrix.M44;
        }

        public bool SameMatrix(Matrix4x4 matrix, float epsilon = 0.000001f) => SameTranslation(matrix, epsilon) && SameRotation(matrix, epsilon);

        public bool SameTranslation(Matrix4x4 matrix, float epsilon = 0.000001f)
        {
            if (Globals.RadMathUtil.Fabs(M41 - matrix.M41) > epsilon)
                return false;

            if (Globals.RadMathUtil.Fabs(M42 - matrix.M43) > epsilon)
                return false;

            if (Globals.RadMathUtil.Fabs(M42 - matrix.M43) > epsilon)
                return false;

            return true;
        }

        public bool SameRotation(Matrix4x4 matrix, float epsilon = 0.000001f)
        {
            if (Globals.RadMathUtil.Fabs(M11 - matrix.M11) > epsilon)
                return false;

            if (Globals.RadMathUtil.Fabs(M22 - matrix.M22) > epsilon)
                return false;

            if (Globals.RadMathUtil.Fabs(M33 - matrix.M33) > epsilon)
                return false;

            return true;
        }

        public Vector3 GetPosition() => new(M41, M42, M43);

        public override string ToString() => $"{{ {{M11:{M11} M12:{M12} M13:{M13} M14:{M14}}} {{M21:{M21} M22:{M22} M23:{M23} M24:{M24}}} {{M31:{M31} M32:{M32} M33:{M33} M34:{M34}}} {{M41:{M41} M42:{M42} M43:{M43} M44:{M44}}} }}";

        public override bool Equals(object obj) => obj is Matrix4x4 x && Equals(x);

        public bool Equals(Matrix4x4 other)
        {
            return M11 == other.M11 &&
                   M12 == other.M12 &&
                   M13 == other.M13 &&
                   M14 == other.M14 &&
                   M21 == other.M21 &&
                   M22 == other.M22 &&
                   M23 == other.M23 &&
                   M24 == other.M24 &&
                   M31 == other.M31 &&
                   M32 == other.M32 &&
                   M33 == other.M33 &&
                   M34 == other.M34 &&
                   M41 == other.M41 &&
                   M42 == other.M42 &&
                   M43 == other.M43 &&
                   M44 == other.M44;
        }

        public static bool operator ==(Matrix4x4 matrix1, Matrix4x4 matrix2) => matrix1.Equals(matrix2);

        public static bool operator !=(Matrix4x4 matrix1, Matrix4x4 matrix2) => !matrix1.Equals(matrix2);

        public override int GetHashCode()
        {
            int hashCode = -1955208504;
            hashCode = hashCode * -1521134295 + M11.GetHashCode();
            hashCode = hashCode * -1521134295 + M12.GetHashCode();
            hashCode = hashCode * -1521134295 + M13.GetHashCode();
            hashCode = hashCode * -1521134295 + M14.GetHashCode();
            hashCode = hashCode * -1521134295 + M21.GetHashCode();
            hashCode = hashCode * -1521134295 + M22.GetHashCode();
            hashCode = hashCode * -1521134295 + M23.GetHashCode();
            hashCode = hashCode * -1521134295 + M24.GetHashCode();
            hashCode = hashCode * -1521134295 + M31.GetHashCode();
            hashCode = hashCode * -1521134295 + M32.GetHashCode();
            hashCode = hashCode * -1521134295 + M33.GetHashCode();
            hashCode = hashCode * -1521134295 + M34.GetHashCode();
            hashCode = hashCode * -1521134295 + M41.GetHashCode();
            hashCode = hashCode * -1521134295 + M42.GetHashCode();
            hashCode = hashCode * -1521134295 + M43.GetHashCode();
            hashCode = hashCode * -1521134295 + M44.GetHashCode();
            return hashCode;
        }
    }

    internal class Matrix4x4Struct : Struct
    {
        public override int Size => Matrix4x4.Size;

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
        {
            float M11 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M12 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M13 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M14 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M21 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M22 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M23 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M24 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M31 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M32 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M33 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M34 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M41 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M42 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M43 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M44 = BitConverter.ToSingle(Bytes, Offset);
            return new Matrix4x4(M11, M12, M13, M14, M21, M22, M23, M24, M31, M32, M33, M34, M41, M42, M43, M44);
        }

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not Matrix4x4 Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Matrix4x4)}'.", nameof(Value));

            BitConverter.GetBytes(Value2.M11).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M12).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M13).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M14).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M21).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M22).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M23).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M24).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M31).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M32).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M33).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M34).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M41).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M42).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M43).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M44).CopyTo(Buffer, Offset);
        }
    }
}