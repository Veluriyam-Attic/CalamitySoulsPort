sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
float uTime;
float2 uPosition;
float2 uImageSize1;
float2 uImageSize2;

float4 water(float2 coords : TEXCOORD0) : COLOR0
{
    float dis = distance(coords, float2(0.5, 0.5));
    if (dis > 0.5)
        return float4(0, 0, 0, 0);
    if (dis + 1.0 / 640.0 > 0.499)
        return float4(0.50, 1, 0.99, 1.0) * 0.5;
    float r = sin((coords.x + (uTime % 100.0) / 100.0) * 6.2831852);
    float2 r0 = coords * float2(uImageSize1.y / uImageSize1.x, 1.0);
    float a = tex2D(uImage1, r0 * 0.5
    + uPosition + float2(0, r * 0.01)).r;
    float b = tex2D(uImage1, -r0 * 0.7
    + uPosition * 0.5 + float2(0, r * 0.03)).r;
    float c = tex2D(uImage2, r0
    + uPosition + float2(0, r * 0.02)).r;
    float3 d = float3(0.14, 0.24, 0.44) * 3.0 + a * float3(0.42, 0.51, 0.47) + b * float3(0.33, 0.59, 0.77) + c * float3(0.26, 0.48, 0.25);
    return float4(d, 3.0) / 3.0 * clamp(dis * 2.0 - 0.1, 0.0, 0.6);
}

technique Technique1
{
    pass Water
    {
        PixelShader = compile ps_2_0 water();
    }
}