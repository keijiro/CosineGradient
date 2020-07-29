Shader "Hidden/GradientOverlay"
{
    Properties
    {
        _MainTex("", 2D) = "white" {}
    }

    CGINCLUDE

    #include "UnityCG.cginc"
    #include "SimplexNoise3D.cginc"

    half3 _CoeffsA;
    half3 _CoeffsB;
    half3 _CoeffsC;
    half3 _CoeffsD;

    half3 CosineGradient(float p)
    {
        half3 rgb = saturate(_CoeffsA + _CoeffsB * cos(_CoeffsC * p + _CoeffsD));
        #if !defined(UNITY_COLORSPACE_GAMMA)
        rgb = GammaToLinearSpace(rgb);
        #endif
        return rgb;
    }

    sampler2D _MainTex;

    half _Opacity;
    half2 _Direction;
    float _Frequency;
    float _Scroll;
    float _NoiseStrength;
    float _NoiseAnimation;

    half4 frag(v2f_img i) : SV_Target
    {
        // Base animation.
        float2 uv = i.uv - 0.5;
        uv.x *= _ScreenParams.x * (_ScreenParams.w - 1);
        uv += _Direction * _Scroll;
        uv *= _Frequency;

        float p = dot(uv, _Direction);

        // Noise field.
        p = lerp(p, snoise(float3(uv, _NoiseAnimation)), _NoiseStrength);

        // Pick color from the gradient.
        half3 rgb = CosineGradient(p);

        // Mix with the source.
        half4 col = tex2D(_MainTex, i.uv);
        col.rgb = lerp(col.rgb, rgb, _Opacity);
        return col;
    }

    ENDCG

    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            CGPROGRAM
            #pragma multi_compile __ UNITY_COLORSPACE_GAMMA
            #pragma vertex vert_img
            #pragma fragment frag
            ENDCG
        }
    }
}
