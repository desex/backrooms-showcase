Shader "Game Company Shaders/FixedTilingShader"
{
    Properties
    {
        _BaseMap("Base Texture", 2D) = "white" {}
        _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        _Tiling("Tiling", Vector) = (1, 1, 0, 0)
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            HLSLPROGRAM

            #pragma target 3.5
            
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Assets/Assets/Shaders/Surface/CurvedWorld/CurvedWorldDeformer.hlsl"
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                half3 worldNormal : TEXCOORD2;
            };

            TEXTURE2D_HALF(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseMap_ST;
                half4 _BaseColor;
                float2 _Tiling;
            CBUFFER_END

            Varyings vert(Attributes input)
            {
                Varyings output;

                PositionAndNormal positionAndNormal = CurvedWorldDeformer(input.positionOS, input.normalOS, input.tangentOS);
                input.positionOS = positionAndNormal.position;
                input.normalOS = positionAndNormal.normal;
                
                float3 worldPos = TransformObjectToWorld(input.positionOS.xyz);
                
                output.worldPos = worldPos;
                output.worldNormal = TransformObjectToWorldNormal(input.normalOS);
                output.uv = input.uv * _Tiling * _BaseMap_ST.xy;
                output.positionHCS = TransformWorldToHClip(worldPos);
                
                return output;
            }
            
            half4 frag(Varyings input) : SV_Target
            {
                half4 color = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.uv) * _BaseColor;
                return color;
            }
            ENDHLSL
        }
    }
}