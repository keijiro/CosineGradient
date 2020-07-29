using UnityEngine;
using Unity.Mathematics;

namespace Klak.Chromatics
{
    [System.Serializable]
    public struct CosineGradient
    {
        #region Public members

        public float4 R, G, B;

        #endregion

        #region Default value

        public static CosineGradient DefaultGradient
          => new CosineGradient
            { R = math.float4(0.5f, 0.5f, 1, 0),
              G = math.float4(0.5f, 0.5f, 1, 1.0f / 3),
              B = math.float4(0.5f, 0.5f, 1, 2.0f / 3) };

        #endregion

        #region Accessor properties

        public float3 CoeffsA => math.float3(R.x, G.x, B.x);
        public float3 CoeffsB => math.float3(R.y, G.y, B.y);
        public float3 CoeffsC => math.float3(R.z, G.z, B.z);
        public float3 CoeffsD => math.float3(R.w, G.w, B.w);

        public float3 CoeffsC2 => CoeffsC * math.PI * 2;
        public float3 CoeffsD2 => CoeffsD * math.PI * 2;

        #endregion

        #region Conversion operator

        public static implicit operator float4x3(CosineGradient g)
          => math.float4x3(g.R, g.G, g.B);

        public static implicit operator Matrix4x4(CosineGradient g)
          => new Matrix4x4(g.R, g.G, g.B, Vector4.zero);

        #endregion

        #region Evaluator method

        public float3 EvaluateAsFloat3(float t)
          => CoeffsA + CoeffsB * math.cos(CoeffsC2 * t + CoeffsD2);

        public Color Evaluate(float t)
        {
            var c = EvaluateAsFloat3(t);
            return new Color(c.x, c.y, c.z);
        }

        #endregion
    }
}
