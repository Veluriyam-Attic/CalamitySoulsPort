sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float uOpacity;
float3 uSecondaryColor;
float uTime;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uImageOffset;
float uIntensity;
float uProgress;
float2 uDirection;
float2 uZoom;
float2 uImageSize0;
float2 uImageSize1;

float RGBtoV(float4 rgb)
{
	float c = max(rgb.r, rgb.g);
	c = max(c, rgb.b);
	return c;
}

float4 fcolor(float2 coords : TEXCOORD0) : COLOR0
{
	float4 oc = tex2D(uImage0, coords);
	if (!any(oc))
		return oc;
	float3 mc = float3(0.30, 0.19, 0.54);
	float v = RGBtoV(oc);
	if (v > 0.8)
		mc = float3(0.61, 0.81, 0.92);
	else if (v > 0.5)
		mc = float3(0.34, 0.6, 0.89);
	else if (v > 0.3)
		mc = float3(0.31, 0.33, 0.78);
	float3 color = (mc + oc.rgb * uProgress) / (1 + uProgress);
	return float4(color, 1.0);
}

float radius(float2 coords : TEXCOORD0) : COLOR0
{
    float2 rpos = (coords - float2(0.5, 0.5));
    rpos.x *= uScreenResolution.x / uScreenResolution.y;
    float rad = length(rpos) * 2;
    rad = 1.0 - clamp(rad * uIntensity, 0.0, 1.0);
    return rad;
}

float4 fearmonger(float2 coords : TEXCOORD0) : COLOR0
{
    float4 oc = tex2D(uImage0, coords);
    if (!any(oc))
        return oc;
    float4 color = fcolor(coords) * radius(coords);
    color.a = 1.0;
    return color;
}

technique Technique1
{
    pass Fearmonger
    {
        PixelShader = compile ps_2_0 fearmonger();
    }
}