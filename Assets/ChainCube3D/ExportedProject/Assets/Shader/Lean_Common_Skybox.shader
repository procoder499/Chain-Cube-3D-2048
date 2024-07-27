Shader "Lean/Common/Skybox" {
	Properties {
		_Color1 ("Color 1", Vector) = (1,0.5,0.5,1)
		_Color2 ("Color 2", Vector) = (0.5,0.5,1,1)
		_Scale ("Scale", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
}