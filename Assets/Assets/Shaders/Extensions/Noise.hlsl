#ifndef NOISE_EXTENSIONS_INCLUDED
#define NOISE_EXTENSIONS_INCLUDED

inline half RandomValueNoise (float2 uv)
{
	return frac(sin(dot(uv, float2(12.9898, 78.233)))*43758.5453);
}

inline half InterpolateNoise (half a, half b, half t)
{
	return (1.0 - t) * a + (t * b);
}

inline half ValueNoise (float2 uv)
{
	float2 i = floor(uv);
	float2 f = frac(uv);
	f = f * f * (3.0 - 2.0 * f);
	
	float2 c0 = i + float2(0.0, 0.0);
	float2 c1 = i + float2(1.0, 0.0);
	float2 c2 = i + float2(0.0, 1.0);
	float2 c3 = i + float2(1.0, 1.0);
	half r0 = RandomValueNoise(c0);
	half r1 = RandomValueNoise(c1);
	half r2 = RandomValueNoise(c2);
	half r3 = RandomValueNoise(c3);

	half bottom_of_grid = InterpolateNoise(r0, r1, f.x);
	half top_of_grid = InterpolateNoise(r2, r3, f.x);
	half t = InterpolateNoise(bottom_of_grid, top_of_grid, f.y);
	return t;
}

half SimpleNoise(float2 uv, half scale)
{
	half t = 0.0;

	half freq = pow(2.0, half(0));
	half amp = pow(0.5, half(3 - 0));
	t += ValueNoise(float2(uv.x * scale / freq, uv.y * scale / freq)) * amp;

	freq = pow(2.0, half(1));
	amp = pow(0.5, half(3 - 1));
	t += ValueNoise(float2(uv.x * scale / freq, uv.y * scale / freq)) * amp;

	freq = pow(2.0, half(2));
	amp = pow(0.5, half(3 - 2));
	t += ValueNoise(float2(uv.x * scale / freq, uv.y * scale / freq)) * amp;

	return t;
}

inline half2 GradientNoiseDirection(float2 p)
{
	p = p % 289;
	float x = (34 * p.x + 1) * p.x % 289 + p.y;
	x = (34 * x + 1) * x % 289;
	x = frac(x / 41) * 2 - 1;
	return normalize(half2(x - floor(x + 0.5), abs(x) - 0.5));
}

inline half GradientNoiseValue(const float2 p)
{
	half2 ip = floor(p);
	half2       fp = frac(p);
	const half  d00 = dot(GradientNoiseDirection(ip), fp);
	const half  d01 = dot(GradientNoiseDirection(ip + half2(0, 1)), fp - half2(0, 1));
	const half  d10 = dot(GradientNoiseDirection(ip + half2(1, 0)), fp - half2(1, 0));
	const half  d11 = dot(GradientNoiseDirection(ip + half2(1, 1)), fp - half2(1, 1));
	fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
	return lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x);
}

half GradientNoise(const float2 uv, const half scale)
{
	return GradientNoiseValue(uv * scale) + 0.5;
}

#endif