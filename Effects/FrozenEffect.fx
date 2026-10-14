sampler uImage0 : register(s0);
float2 uImageSize;

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
		return float4(0.61, 0.81, 0.92, 1);
	float3 mc = float3(0.30, 0.19, 0.54);
	float v = RGBtoV(oc);
	if (v > 0.8)
		mc = float3(0.61, 0.81, 0.92);
	else if (v > 0.5)
		mc = float3(0.34, 0.6, 0.89);
	else if (v > 0.3)
		mc = float3(0.31, 0.33, 0.78);
	float3 color = (mc * 3 + oc.rgb * 2) / 5;
	return float4(color, oc.a);
}

float4 frozen(float2 coords : TEXCOORD0) : COLOR0
{
	float4 color = tex2D(uImage0, coords);
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
		return fcolor(coords);
	return color;
}

technique Technique1
{
	pass Frozen
	{
		PixelShader = compile ps_2_0 frozen();
	}
}