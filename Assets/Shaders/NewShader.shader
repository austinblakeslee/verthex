Shader "Custom/Electricity6" {
	Properties {
		_NoiseTex ("Noise (RGB)", 2D) = "white" {}
		_RampTex ("Ramp (RGB)", 2D) = "white" {}
		_FallOff ("FallOff", FLOAT) = 2
		_Color ("FallOff", COLOR) = (0.5,0.5,0.5,0.5)
		_Speed ("Speed", FLOAT) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Transparent" }
		LOD 200
		Pass {
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			Cull Off
			
			CGPROGRAM
// Upgrade NOTE: excluded shader from DX11 and Xbox360; has structs without semantics (struct appdata members vertex,normal,texcoord)
#pragma exclude_renderers d3d11 xbox360
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
	
			sampler2D _NoiseTex;
			float4 _NoiseTex_ST;
			sampler2D _RampTex;
			half4 _Color;
			float _FallOff;
			float _Speed;
	
			struct appdata {
				float4 vertex;
				float3 normal;
				float2 texcoord;
			};
			
			struct v2f {
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
				float viewAngle : TEXCOORD1;
			};
	
			v2f vert(appdata v){
				v2f o;
				o.position = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _NoiseTex);
				o.viewAngle = 1 - abs(dot(normalize(ObjSpaceViewDir(v.vertex)), v.normal));
				return o;
			}
			
			half4 frag(v2f i) : COLOR {
				half4 noise1 = tex2D(_NoiseTex, i.uv + _Time.xy*_Speed);
				half4 noise2 = tex2D(_NoiseTex, i.uv - _Time.zx*0.789*_Speed);
				half noise = (noise1.a + noise2.a) * 0.5;
				half4 ramp = tex2D(_RampTex, float2(noise,0));
				half4 col = ramp.a * pow(i.viewAngle, _FallOff) * _Color * 2;
				return col;
			}
			
			ENDCG
		}
	}
}
