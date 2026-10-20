Shader "ScreenSpace/Glitch"
{
    HLSLINCLUDE

    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
    #include "Assets/Assets/Shaders/Extensions/Noise.hlsl"
    #include "Assets/Assets/Shaders/Extensions/ShaderExtensions.hlsl"

    bool _UsePeriod;
    half _ShaderStrength;
    
    float _SquareGlitchingStrength;
    float _SquareGlitchingNoiseAmount;
    half _SquareGlitchingFlickeringStrength;
    half _SquareGlitchingOnOffBorder;
    half _SquareGlitchingFrequencyModifier;
    half _SquareGlitchingLengthModifier;
    
    float _WaveGlitchingStrength;
    float _WaveGlitchingNoiseAmount;
    half _WaveGlitchingNoiseStrength;
    half _WaveGlitchingFlickeringStrength;
    half _WaveGlitchingFrequencyModifier;
    half _WaveGlitchingLengthModifier;
    
    half2 _AberrationCenter;
    half _AberrationsStrength;
    half _AberrationOpacity;
    half _AberrationFrequencyModifier;
    half _AberrationLengthModifier;

    bool _UseWhiteNoise;
    half _WhiteNoiseScale;
    half _WhiteNoiseDissolve;

    half2 SquareGlitches (const float2 texcoord)
    {
        const half glitching_noise = GlitchingNoise(texcoord, _SquareGlitchingStrength, _SquareGlitchingNoiseAmount);
        half glitching = glitching_noise > _SquareGlitchingOnOffBorder ? 1 : 0;
        glitching = 1 - glitching;

        half time = _Time.y;
        const half chess_grid = ChessGrid(texcoord, half2(1, 1), half2(time, 0));

        glitching *= chess_grid;

        if (_UsePeriod)
        {
            const half period = SinTimePeriod(_SquareGlitchingFrequencyModifier, half2(1 - _SquareGlitchingLengthModifier, 1));
            glitching *= period;
        }

        const half flickering = Flickering(_SquareGlitchingFlickeringStrength);

        glitching *= flickering;

        const half2 glitching_offset = half2(glitching, 0);

        half2 uv = texcoord + glitching_offset * _ShaderStrength;
        
        return uv;
    }
    
    half2 WaveGlitches (const half2 texcoord)
    {
        half glitching_noise = GlitchingNoise(texcoord, _WaveGlitchingStrength, _WaveGlitchingNoiseAmount);
        glitching_noise *= _WaveGlitchingNoiseStrength;

        const half flickering = Flickering(_WaveGlitchingFlickeringStrength);

        half glitching = glitching_noise * flickering;

        if (_UsePeriod)
        {
            const half period = SinTimePeriod(_WaveGlitchingFrequencyModifier, half2(1 - _WaveGlitchingLengthModifier, 1));
            glitching *= period;
        }

        const half2 glitching_offset = half2(glitching, 0);

        half2 uv = texcoord + glitching_offset * _ShaderStrength;
        
        return uv;
    }
    
    half3 ChromaticAberrations (half2 uv)
    {
        const half distance = DistanceOnScreen(uv, _AberrationCenter);
        const half aberration = distance * _AberrationsStrength;

        const half2 red_channel_uv = half2(uv.x - aberration, uv.y);
        const half2 green_channel_uv = half2(uv.x + aberration, uv.y);
        const half2 blue_channel_uv = half2(uv.x, uv.y - aberration);

        half red_aberration = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, red_channel_uv).x;
        half green_aberration = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, green_channel_uv).y;
        half blue_aberration = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, blue_channel_uv).z;

        half3 aberration_color = half3(red_aberration, green_aberration, blue_aberration);

        if (_UsePeriod)
        {
            const half period = SinTimePeriod(_AberrationFrequencyModifier, half2(1 - _AberrationLengthModifier, 1));
            aberration_color *= period;
        }
        
        return aberration_color;
    }
    
    half WhiteNoise (const half2 texcoord)
    {
        const half time = _Time.y;

        const half random = RandomValue(time, half(0), half(1));

        const half2 uv = texcoord * half2(1, 1) + random;

        half noise = SimpleNoise(uv, _WhiteNoiseScale);

        noise /= _WhiteNoiseDissolve;
        
        return noise;
    }

    half4 Glitches (const Varyings input) : SV_Target
    {
        half2 uv = SquareGlitches(input.texcoord);
        uv = WaveGlitches(uv);
        
        half3 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, uv).xyz;

        const half3 aberration = ChromaticAberrations(uv);

        color = BlendScreen(color, aberration, _AberrationOpacity * _ShaderStrength);

        if (_UseWhiteNoise)
        {
            const half noise = WhiteNoise(uv);
            color = color + noise * _ShaderStrength;
        }
        
        return half4(color, 1);
    }

    ENDHLSL
    
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
        LOD 100
        ZWrite Off Cull Off

        Pass
        {
            Name "Glitches"
            
            HLSLPROGRAM
            
            #pragma vertex Vert
            #pragma fragment Glitches

            ENDHLSL
        }
    }
}
