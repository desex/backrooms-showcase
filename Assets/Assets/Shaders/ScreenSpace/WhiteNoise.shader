Shader "ScreenSpace/WhiteNoise"
{
    HLSLINCLUDE
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
    #include "Assets/Assets/Shaders/Extensions/Noise.hlsl"
    #include "Assets/Assets/Shaders/Extensions/ShaderExtensions.hlsl"

    bool _UseGradient;
    bool _UseBlendScreen;
    half _ShaderStrength;
    half _WhiteNoiseScale;
    half _WhiteNoiseDissolve;

    half3 _NoiseColor;

    half4 WhiteNoise(Varyings input) : SV_Target
    {
        float time = _Time.y;

        half random = RandomValue(time, half(0), half(1));

        half2 uv = input.texcoord * half2(1, 1) + random;
        
        half noise = _UseGradient ? GradientNoise(uv, _WhiteNoiseScale) : SimpleNoise(uv, _WhiteNoiseScale);

        noise /= _WhiteNoiseDissolve;
        
        half3 coloredNoise = noise * _NoiseColor;

        half3 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, input.texcoord).xyz;

        half3 result;

        if (_UseBlendScreen)
            result = BlendScreen(color, coloredNoise, noise * _ShaderStrength);
        else
            result = BlendMultiply(color, coloredNoise, noise * _ShaderStrength);

        result = saturate(result);

        return half4(result, 1);
    }
    ENDHLSL

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"
        }
        LOD 100
        ZWrite Off Cull Off

        Pass
        {
            Name "WhiteNoise"

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment WhiteNoise
            ENDHLSL
        }
    }
}