float4x4 matView;
float4x4 matViewProjection;

struct VS_INPUT 
{
   float4 Position: POSITION0;
   float3 Normal:   NORMAL0;
};

struct VS_OUTPUT 
{
   float4 Position: POSITION0;
   float3 Normal: TEXCOORD0;
   
};

VS_OUTPUT vs_main( VS_INPUT Input )
{
   VS_OUTPUT Output;
   
   Output.Position = mul( Input.Position, matViewProjection );
   
   Output.Normal   = -Input.Normal;

   return( Output );
   
}


texture skyMap;

samplerCUBE skyMapSampler = sampler_state {
    Texture = <skyMap>;
    MinFilter = Linear;
    MipFilter = Linear;
    MagFilter = Linear;
    AddressU = Clamp;
    AddressV = Clamp;
    AddressW = Clamp;
};

struct PS_INPUT 
{
   float3 Normal:   TEXCOORD0;
};

float4 ps_main( PS_INPUT Input ) : COLOR0
{
   float4 color = texCUBE(skyMapSampler, normalize(Input.Normal));
   
   return( color );
}




technique Technique1
{
    pass Pass0
    {
        VertexShader = compile vs_2_0 vs_main();
        PixelShader = compile ps_2_0 ps_main();
    }
}
