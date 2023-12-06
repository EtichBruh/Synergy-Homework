#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

// from https://iquilezles.org/articles/distfunctions
float roundedBoxSDF(float2 CenterPosition, float2 Size, float Radius)
{
    return length(max(abs(CenterPosition) - Size + Radius, 0.0)) - Radius;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    // The pixel space scale of the rectangle.
    
    // the pixel space location of the rectangle.

    // How soft the edges should be (in pixels). Higher values could be used to simulate a drop shadow.
    float edgeSoftness = 1.0f;
    
    // The radius of the corners (in pixels).
    float radius = 30.0f;
    
    // Calculate distance to edge.   
    float distance = roundedBoxSDF(input.TextureCoordinates - (input.TextureCoordinates / 2.0f), input.TextureCoordinates / 2.0f, radius);
    
    // Smooth the result (free antialiasing).
    float smoothedAlpha = 1.0f - smoothstep(0.0f, edgeSoftness * 2.0f, distance);
    
    // Return the resultant shape.
    return lerp(tex2D(SpriteTextureSampler, input.TextureCoordinates) * input.Color, float4(0.0f, 0.f, 0.0f, smoothedAlpha), smoothedAlpha);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};