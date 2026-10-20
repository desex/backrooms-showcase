#ifndef SHADER_EXTENSIONS_INCLUDED
#define SHADER_EXTENSIONS_INCLUDED

#include "Assets/Assets/Shaders/Extensions/Noise.hlsl"

float Remap(float in_value, float2 in_min_max, float2 out_min_max)
{
	return out_min_max.x + (in_value - in_min_max.x) * (out_min_max.y - out_min_max.x) / (in_min_max.y - in_min_max.x);
}

half Remap(half in_value, half2 in_min_max, half2 out_min_max)
{
	return out_min_max.x + (in_value - in_min_max.x) * (out_min_max.y - out_min_max.x) / (in_min_max.y - in_min_max.x);
}

float RandomValue (float seed, float min, float max)
{
	float randomno = frac(sin(dot(seed, float2(12.9898, 78.233))) * 43758.5453);
	return lerp(min, max, randomno);
}

half RandomValue (const half seed, const half min, const half max)
{
	const half randomno = frac(sin(dot(seed, float2(12.9898, 78.233))) * 43758.5453);
	return lerp(min, max, randomno);
}

half DistanceOnScreen (half2 uv, half2 center)
{
	half offset_x = uv.x - center.x;
	half offset_y = uv.y - center.y;

	half offset_square_x = pow(offset_x, 2);
	half offset_square_y = pow(offset_y, 2);
        
	return sqrt(offset_square_x + offset_square_y);
}

half SinTimePeriod (const half frequency_modifier, const half2 positive_sin_range)
{
	const half time = _Time.y * frequency_modifier;
	const half sin_time = sin(time);

	half period = Remap(sin_time, positive_sin_range, half2(0, 1));
	period = saturate(period);

	return period;
}

half3 BlendScreen (half3 base, half3 blend, half opacity)
{
	half3 result = 1.0 - (1.0 - blend) * (1.0 - base);
	result = lerp(base, result, opacity);

	return result;
}

half3 BlendMultiply (half3 base, half3 blend, half opacity)
{
	half3 result = base * blend;
	result = lerp(base, result, opacity);

	return result;
}

half GlitchingNoise (float2 uv, const half glitch_strength, const half noise_amount)
{
	const float time = _Time.y;
	const float glitch_time = time * glitch_strength;
	float glitch_layout = uv.y + glitch_time;
	half noise = GradientNoise(float2(glitch_layout, glitch_layout), noise_amount);
	noise = Remap(noise, half2(0, 1), half2(-1, 1));

	return noise;
}

half Flickering (const half flickering_strength)
{
	float time = _Time.y;
	time *= flickering_strength;
	half noise = GradientNoise(float2(time, time), 10);

	noise = pow(noise, 4);
	noise *= 0.2;
	noise = saturate(noise);

	if (noise > 0.015)
	{
		return noise;
	}
	
	return 0;
}

half ChessGrid (float2 uv, const float2 tiling, const float2 offset)
{
	uv = uv * tiling + offset;
	uv = fmod(uv, float2(1, 1));
	const bool x = uv.x > 0.5 ? true : false;
	const bool y = uv.y > 0.5 ? true : false;

	const bool b0 = x && y;
	const bool b1 = !x && !y;
	const bool b2 = b0 || b1;

	return half(b2);
}

#endif