Shader "Hidden/GradientOverlay"
{
    Properties
    {
        _MainTex("", 2D) = "white" {}
    }

    CGINCLUDE

    #include "UnityCG.cginc"
    #include "Packages/jp.keijiro.noiseshader/Shader/SimplexNoise3D.hlsl"
    #include "Packages/jp.keijiro.klak.cosinegradient/Runtime/CosineGradient.hlsl"

    sampler2D _MainTex;

    float4x4 _Gradient;
    float _Opacity;
    float2 _Direction;
    float _Frequency;
    float _Scroll;
    float _NoiseStrength;
    float _NoiseAnimation;

    float4 frag(v2f_img i) : SV_Target
    {
        // Base animation
        float2 uv = i.uv - 0.5;
        uv.x *= _ScreenParams.x * (_ScreenParams.w - 1);
        uv += _Direction * _Scroll;
        uv *= _Frequency;

        float p = dot(uv, _Direction);

        // Noise field
        p = lerp(p, snoise(float3(uv, _NoiseAnimation)), _NoiseStrength);

        // Pick color from the gradient.
        float3 rgb = CosineGradient(_Gradient, p);

        #if !defined(UNITY_COLORSPACE_GAMMA)
        rgb = GammaToLinearSpace(rgb);
        #endif

        // Mix with the source.
        float4 col = tex2D(_MainTex, i.uv);
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
