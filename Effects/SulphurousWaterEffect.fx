sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float2 uImageSize;
float uTime;
float uAlpha;
float3 uColor;

float4 inner(float2 coords : TEXCOORD0) : COLOR0
{
    float r = tex2D(uImage1, float2(coords.x + uTime, coords.y)).r;
	float4 color = float4(uColor, r);
    return color * uAlpha;
}

float4 sulwater(float2 coords : TEXCOORD0) : COLOR0
{
	float4 color = tex2D(uImage0, coords);
    if (any(color))
        return inner(coords);
	float dx = 1 / uImageSize.x;
	float dy = 1 / uImageSize.y;
	bool flag = false;
	for (int i = -2; i <= 2; i++)
	{
		for (int j = -2; j <= 2; j++)
		{
			if (i * j == 0.0)
			{
				float4 c = tex2D(uImage0, coords + float2(dx * i, dy * j));
				if (any(c))
				{
					flag = true;
				}
			}
		}
	}
	if (flag)
		return float4(uColor, 1.0);
	return color;
}

technique Technique1
{
	pass SulWater
	{
		PixelShader = compile ps_2_0 sulwater();
	}
}