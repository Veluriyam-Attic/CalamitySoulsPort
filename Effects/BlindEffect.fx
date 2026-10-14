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

float gauss[3][3] =
{
    0.075, 0.124, 0.075,
    0.124, 0.204, 0.124,
    0.075, 0.124, 0.075
};
float radius(float2 coords : TEXCOORD0) : COLOR0
{
    float2 rpos = (coords - float2(0.5, 0.5));
    rpos.x *= uScreenResolution.x / uScreenResolution.y;
    float rad = length(rpos) * 2;
    rad = clamp(rad * uProgress, 0.0, 1.0);
    return rad;
}
float4 blind(float2 coords : TEXCOORD0) : COLOR0
{
    float4 oc = tex2D(uImage0, coords);
    if (!any(oc))
        return oc;
    float dx = 2 / uScreenResolution.x;
    float dy = 2 / uScreenResolution.y;
   float4 color = float4(0, 0, 0, 0);
    for (int i = -1; i <= 1; i++)
    {
        for (int j = -1; j <= 1; j++)
        {
            color += gauss[i + 1][j + 1] * tex2D(uImage0, float2(coords.x + dx * i, coords.y + dy * j));
        }
    }
    float rad = radius(coords);
    color = rad * color + (1.0 - rad) * oc;
    color = (1.0 - rad) * color;
    return color;
}

technique Technique1
{
    pass Blind
    {
        PixelShader = compile ps_2_0 blind();
    }
}