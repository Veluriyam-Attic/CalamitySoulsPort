sampler uImage0 : register(s0);

float4 cosmic(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    if (any(color))
        return float4(0.37, 0.12, 0.63, 1.0);
    return color;
}

technique Technique1
{
    pass Cosmic
    {
        PixelShader = compile ps_2_0 cosmic();
    }
}