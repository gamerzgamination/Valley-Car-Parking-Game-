Shader "Custom/GR3DCarPaintinverse" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Color ("Main Color", Vector) = (1,1,1,1)
		_ColOut ("Outer Color", Vector) = (1,1,1,1)
		_GlossOut ("Outer Gloss", Range(0, 1)) = 0.97
		_ColIn ("Inner Color", Vector) = (1,1,1,1)
		_GlossIn ("Inner Gloss", Range(0, 1)) = 0.95
		_Highlight ("Highlight", Range(0, 0.5)) = 0.1
		_DecalTex ("Decal (RGBA)", 2D) = "black" {}
	}
	
	//DummyShaderTextExporter - One Sided
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard fullforwardshadows
#pragma target 3.0
		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
		}
		ENDCG
	}
	Fallback "Diffuse"
}