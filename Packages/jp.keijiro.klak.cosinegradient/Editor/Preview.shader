// Preview shader for custom property drawer
Shader "Hidden/Klak/Chromatics/CosineGradient/Preview"
{
    CGINCLUDE

    #include "UnityCG.cginc"
    #include "Packages/jp.keijiro.klak.cosinegradient/Runtime/CosineGradient.hlsl"

    float3 _CoeffsA;
    float3 _CoeffsB;
    float3 _CoeffsC;
    float3 _CoeffsD;

    float4 frag(v2f_img i) : SV_Target
    {
        float3 rgb = CosineGradient(_CoeffsA, _CoeffsB, _CoeffsC, _CoeffsD, i.uv.x);
        return float4(rgb, 1);
    }

    ENDCG

    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            ENDCG
        }
    }
}
