#ifndef CURVED_WORLD_DEFORMER_INCLUDED
#define CURVED_WORLD_DEFORMER_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

float _CurvedWorldBendType;

float3 CurvedWorld_ClassicRunner_Z_Positive_ID1_PivotPoint;
float2 CurvedWorld_ClassicRunner_Z_Positive_ID1_BendSize;    
float2 CurvedWorld_ClassicRunner_Z_Positive_ID1_BendOffset;

float3 CurvedWorld_CylindricalRolloff_Z_ID1_PivotPoint;
float CurvedWorld_CylindricalRolloff_Z_ID1_BendSize;    
float CurvedWorld_CylindricalRolloff_Z_ID1_BendOffset;

float3 CurvedWorld_TwistedSpiral_X_Positive_ID1_PivotPoint;
float3 CurvedWorld_TwistedSpiral_X_Positive_ID1_RotationAxis;
float3 CurvedWorld_TwistedSpiral_X_Positive_ID1_BendSize;    
float3 CurvedWorld_TwistedSpiral_X_Positive_ID1_BendOffset;

float3 CurvedWorld_TwistedSpiral_Z_Positive_ID1_PivotPoint;
float3 CurvedWorld_TwistedSpiral_Z_Positive_ID1_RotationAxis;
float3 CurvedWorld_TwistedSpiral_Z_Positive_ID1_BendSize;    
float3 CurvedWorld_TwistedSpiral_Z_Positive_ID1_BendOffset;

struct PositionAndNormal
{
    float4 position;
    float3 normal;
};

float3 CurvedWorld_ObjectToWorld(float4 vertexOS)
{
    return GetAbsolutePositionWS(TransformObjectToWorld(vertexOS.xyz));
}

float3 CurvedWorld_ObjectToWorldNormal(float3 normalOS)
{
    return TransformObjectToWorldNormal(normalOS);
}

float3 CurvedWorld_ObjectToWorldTangent(float3 tangentOS)
{
    return TransformObjectToWorldDir(tangentOS);
}

float3 CurvedWorld_WorldToObject(float4 vertexWS, float HDRPCoef)
{
    #if (SHADEROPTIONS_CAMERA_RELATIVE_RENDERING != 0)
    vertexWS.xyz -= _WorldSpaceCameraPos * HDRPCoef;	//HDRPCoef is always 1 for URP. In HDRP for Spiral bend types = 1, for Cylindricals = 0. 
    #endif

    return mul(GetWorldToObjectMatrix(), vertexWS).xyz;
}

float3 CurvedWorld_WorldToObjectNormal(float3 normalWS)
{
    return TransformWorldToObjectNormal(normalWS);
}

inline float CurvedWorld_SmoothTwistedPositive(float x, float scale)
{
    float d = x / scale;
    float s = d * d;
    float smooth = lerp(s, d, s) * scale;

    return x < scale ? smooth : x;
}

inline float3 CurvedWorld_SmoothTwistedPositive(float3 x, float scale)
{
    float3 d = x / scale;
    float3 s = d * d;
    float3 smooth = lerp(s, d, s) * scale;

    return float3(x.x < scale ? smooth.x : x.x, x.y < scale ? smooth.y : x.y, x.z < scale ? smooth.z : x.z);
}

inline void CurvedWorld_RotateVertex(inout float3 vertex, float3 pivot, float3 axis, float angle)
{
    //degree to rad / 2
    angle *= 0.00872664625;


    float sinA, cosA;
    sincos(angle, sinA, cosA);

    float3 q = axis * sinA;

    //vertex
    vertex -= pivot;
    vertex += cross(q, cross(q, vertex) + vertex * cosA) * 2;
    vertex += pivot;		
}

void CurvedWorld_ClassicRunner_Z_Positive(inout float4 inVertexOS, inout float3 normalOS, float4 tangentOS, float3 pivotPoint, float2 bendSize, float2 bendOffset)
{
    float3 positionWS = CurvedWorld_ObjectToWorld(inVertexOS);
    float3 normalWS   = CurvedWorld_ObjectToWorldNormal(normalOS);
    float3 tangentWS  = CurvedWorld_ObjectToWorldTangent(tangentOS.xyz);
    float3 binormalWS = cross(tangentWS, normalWS);


    float3 v0 = positionWS - pivotPoint;
    float3 v1 = v0 + tangentWS;
    float3 v2 = v0 + binormalWS;
 

    float2 offset = max(float2(0, 0), v0.zz - bendOffset.xy);
    offset *= offset;
    float3 transformedVertex = float3(bendSize.y * offset.y, bendSize.x * offset.x, 0.0f) * 0.001;
    v0 += transformedVertex;
			

    offset = max(float2(0, 0), v1.zz - bendOffset.xy);
    offset *= offset;
    v1 += float3(bendSize.y * offset.y, bendSize.x * offset.x, 0.0f) * 0.001; 

			
    offset = max(float2(0, 0), v2.zz - bendOffset.xy);
    offset *= offset;
    v2 += float3(bendSize.y * offset.y, bendSize.x * offset.x, 0.0f) * 0.001; 


	
    inVertexOS.xyz += CurvedWorld_WorldToObject(float4(transformedVertex, 0), 0);

    normalOS = CurvedWorld_WorldToObjectNormal(normalize(cross(v2 - v0, v1 - v0)));
}

void CurvedWorld_CylindricalRolloff_Z(inout float4 inVertexOS, inout float3 normalOS, float4 tangentOS, float3 pivotPoint, float3 bendSize, float3 bendOffset)
{
    float3 positionWS = CurvedWorld_ObjectToWorld(inVertexOS);
    float3 normalWS   = CurvedWorld_ObjectToWorldNormal(normalOS);
    float3 tangentWS  = CurvedWorld_ObjectToWorldTangent(tangentOS.xyz);
    float3 binormalWS = cross(tangentWS, normalWS);


    float3 v0 = positionWS - pivotPoint;
    float3 v1 = v0 + tangentWS;
    float3 v2 = v0 + binormalWS;
 

    float2 offset = max(float2(0, 0), abs(v0.zx) - bendOffset.xx);
    offset *= step(float2(0, 0), v0.zx) * 2 - 1;
    offset *= offset;
    float3 transformedVertex = float3(0, -(bendSize.x * offset.x) * 0.001, 0.0f); 
    v0 += transformedVertex;
						
				
    offset = max(float2(0, 0), abs(v1.zx) - bendOffset.xx);
    offset *= step(float2(0, 0), v1.zx) * 2 - 1;
    offset *= offset; 		
    v1.y += -(bendSize.x * offset.x) * 0.001;
					
				
    offset = max(float2(0, 0), abs(v2.zx) - bendOffset.xx);
    offset *= step(float2(0, 0), v2.zx) * 2 - 1;
    offset *= offset; 		
    v2.y += -(bendSize.x * offset.x) * 0.001;


	
    inVertexOS.xyz += CurvedWorld_WorldToObject(float4(transformedVertex, 0), 0);

    normalOS = CurvedWorld_WorldToObjectNormal(normalize(cross(v2 - v0, v1 - v0)));
}

void CurvedWorld_TwistedSpiral_X_Positive(inout float4 inVertexOS, inout float3 inNormalOS, float4 tangentOS, float3 pivotPoint, float3 rotationAxis, float3 bendSize, float3 bendOffset)
{
    float3 positionWS = CurvedWorld_ObjectToWorld(inVertexOS);
    float3 normalWS   = CurvedWorld_ObjectToWorldNormal(inNormalOS);
    float3 tangentWS  = CurvedWorld_ObjectToWorldTangent(tangentOS.xyz);
    float3 binormalWS = cross(tangentWS, normalWS);

    positionWS -= pivotPoint;
 

    float3 v0 = positionWS;
    float3 v1 = v0 + tangentWS;
    float3 v2 = v0 + binormalWS;


    float3 d = max(0, float3(v0.x, v1.x, v2.x) - bendOffset.xxx);
    d = CurvedWorld_SmoothTwistedPositive(d, 100);
    float3 angle = bendSize.xxx * d;               

    CurvedWorld_RotateVertex(v0, pivotPoint, rotationAxis, angle.x);
    float2 offset = max(float2(0, 0), v0.xx - bendOffset.yz);
    offset *= offset;
    v0 += float3(0.0f, bendSize.y * offset.x, -bendSize.z * offset.y) * 0.001;


    CurvedWorld_RotateVertex(v1, pivotPoint, rotationAxis, angle.y);
    offset = max(float2(0, 0), v1.xx - bendOffset.yz);
    offset *= offset;
    v1 += float3(0.0f, bendSize.y * offset.x, -bendSize.z * offset.y) * 0.001;
      

    CurvedWorld_RotateVertex(v2, pivotPoint, rotationAxis, angle.z);
    offset = max(float2(0, 0), v2.xx - bendOffset.yz);
    offset *= offset;
    v2 += float3(0.0f, bendSize.y * offset.x, -bendSize.z * offset.y) * 0.001;



    v0 += pivotPoint;
    v1 += pivotPoint;
    v2 += pivotPoint;

    inVertexOS.xyz = CurvedWorld_WorldToObject(float4(v0, 1), 1);

    inNormalOS = CurvedWorld_WorldToObjectNormal(normalize(cross(v2 - v0, v1 - v0)));
}

void CurvedWorld_TwistedSpiral_Z_Positive(inout float4 inVertexOS, inout float3 inNormalOS, float4 tangentOS, float3 pivotPoint, float3 rotationAxis, float3 bendSize, float3 bendOffset)
{
    float3 positionWS = CurvedWorld_ObjectToWorld(inVertexOS);
    float3 normalWS   = CurvedWorld_ObjectToWorldNormal(inNormalOS);
    float3 tangentWS  = CurvedWorld_ObjectToWorldTangent(tangentOS.xyz);
    float3 binormalWS = cross(tangentWS, normalWS);

    positionWS -= pivotPoint;


    float3 v0 = positionWS;
    float3 v1 = v0 + tangentWS;
    float3 v2 = v0 + binormalWS;


    float3 d = max(0, float3(v0.z, v1.z, v2.z) - bendOffset.xxx);
    d = CurvedWorld_SmoothTwistedPositive(d, 100);
    float3 angle = bendSize.xxx * d;         
            

    CurvedWorld_RotateVertex(v0, pivotPoint, rotationAxis, angle.x);
    float2 offset = max(float2(0, 0), v0.zz - bendOffset.yz);
    offset *= offset;
    v0 += float3(bendSize.z * offset.y, bendSize.y * offset.x, 0.0f) * 0.001;


    CurvedWorld_RotateVertex(v1, pivotPoint, rotationAxis, angle.y);
    offset = max(float2(0, 0), v1.zz - bendOffset.yz);
    offset *= offset;
    v1 += float3(bendSize.z * offset.y, bendSize.y * offset.x, 0.0f) * 0.001;
      

    CurvedWorld_RotateVertex(v2, pivotPoint, rotationAxis, angle.z);
    offset = max(float2(0, 0), v2.zz - bendOffset.yz);
    offset *= offset;
    v2 += float3(bendSize.z * offset.y, bendSize.y * offset.x, 0.0f) * 0.001;



    v0 += pivotPoint;
    v1 += pivotPoint;
    v2 += pivotPoint;

    inVertexOS.xyz = CurvedWorld_WorldToObject(float4(v0, 1), 1);

    inNormalOS = CurvedWorld_WorldToObjectNormal(normalize(cross(v2 - v0, v1 - v0)));
}

void CurvedWorld_ClassicRunner_Z_Positive_ID1(inout float4 vertexOS, inout float3 normalOS, float4 tangent)
{
    CurvedWorld_ClassicRunner_Z_Positive(vertexOS, 
                            normalOS, 
                            tangent,
                            CurvedWorld_ClassicRunner_Z_Positive_ID1_PivotPoint,
                            CurvedWorld_ClassicRunner_Z_Positive_ID1_BendSize,
                            CurvedWorld_ClassicRunner_Z_Positive_ID1_BendOffset);
}    

void CurvedWorld_CylindricalRolloff_Z_ID1(inout float4 vertexOS, inout float3 normalOS, float4 tangent)
{
    CurvedWorld_CylindricalRolloff_Z(vertexOS, 
                            normalOS, 
                            tangent,
                            CurvedWorld_CylindricalRolloff_Z_ID1_PivotPoint,
                            CurvedWorld_CylindricalRolloff_Z_ID1_BendSize,
                            CurvedWorld_CylindricalRolloff_Z_ID1_BendOffset);
}

void CurvedWorld_TwistedSpiral_X_Positive_ID1(inout float4 vertexOS, inout float3 normalOS, float4 tangent)
{
    CurvedWorld_TwistedSpiral_X_Positive(vertexOS, 
                            normalOS, 
                            tangent,
                            CurvedWorld_TwistedSpiral_X_Positive_ID1_PivotPoint,
                            CurvedWorld_TwistedSpiral_X_Positive_ID1_RotationAxis,
                            CurvedWorld_TwistedSpiral_X_Positive_ID1_BendSize,
                            CurvedWorld_TwistedSpiral_X_Positive_ID1_BendOffset);
}

void CurvedWorld_TwistedSpiral_Z_Positive_ID1(inout float4 vertexOS, inout float3 normalOS, float4 tangent)
{
    CurvedWorld_TwistedSpiral_Z_Positive(vertexOS, 
                            normalOS, 
                            tangent,
                            CurvedWorld_TwistedSpiral_Z_Positive_ID1_PivotPoint,
                            CurvedWorld_TwistedSpiral_Z_Positive_ID1_RotationAxis,
                            CurvedWorld_TwistedSpiral_Z_Positive_ID1_BendSize,
                            CurvedWorld_TwistedSpiral_Z_Positive_ID1_BendOffset);
}    

PositionAndNormal CurvedWorldDeformer(float4 positionOS, float3 normalOS, float4 tangentOS)
{
    PositionAndNormal result;
    
    [branch] switch(_CurvedWorldBendType)
    {
        case 2:
            CurvedWorld_ClassicRunner_Z_Positive_ID1(positionOS, normalOS, tangentOS);
            break;
        case 10:
            CurvedWorld_CylindricalRolloff_Z_ID1(positionOS, normalOS, tangentOS);
            break;
        case 27:
            CurvedWorld_TwistedSpiral_X_Positive_ID1(positionOS, normalOS, tangentOS);
            break;
        case 29:
            CurvedWorld_TwistedSpiral_Z_Positive_ID1(positionOS, normalOS, tangentOS);
            break;
        default:
            break;
    }

    result.position = positionOS;
    result.normal = normalOS;
    
    return result;
}

#endif //CURVED_WORLD_DEFORMER_INCLUDED