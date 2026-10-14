sampler uImage0 : register(s0);
float2 uImageSize;

float4 scale(float2 coords : TEXCOORD0) : COLOR0
{
	float4 color = tex2D(uImage0, coords);
    float dx = 1 / uImageSize.x;
    float dy = 1 / uImageSize.y;
    int x = coords.x / dx / 2;
    float2 cords = coords;
    if (floor(coords.x / dx - x * 2) == 1.0)
        cords.x -= dx;
    int y = coords.y / dy / 2;
    if (floor(coords.y / dy - y * 2) == 1.0)
        cords.y -= dy;
    color = tex2D(uImage0, cords);
	return color;
}

technique Technique1
{
	pass Scale
	{
		PixelShader = compile ps_2_0 scale();
	}
}