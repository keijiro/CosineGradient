// Preview shader for custom property drawer
Shader "Hidden/Klak/Chromatics/CosineGradient/Preview"
{
    CGINCLUDE

    #include "UnityCG.cginc"
    #include "Packages/jp.keijiro.klak.cosinegradient/Runtime/CosineGradient.hlsl"

    float4x4 _Gradient;

    float4 frag(v2f_img i) : SV_Target
    {
        return float4(CosineGradient(_Gradient, i.uv.x), 1);
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
