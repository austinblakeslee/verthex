Shader "Electricity" {

    Properties {

        _Color ("Main Color", Color) = (1,1,1,1)

        _MainTex ("Base (RGB)", 2D) = "white" {}

        _Noise ("Noise (RGB)", 2D) = "white" {}

        _Ramp ("Ramp (RGBA)", 2D) = "white" {}

        _Speed ("Scroll Speed", float) = 0.1

        _FallOff("FallOff", float) = 0.85

        _Width("Width", float) = 0.2

        _OutlineColor ("Outline Color", Color) = (1,1,1,1)

        _OutlineColorFallOff("Outline Color FallOff", float) = 1.1

    }

    SubShader {

        // copy&paste from Normal-Diffuse

        CGPROGRAM

        #pragma surface surf Lambert

 

        sampler2D _MainTex;

        float4 _Color;

 

        struct Input {

            float2 uv_MainTex;

        };

 

        void surf (Input IN, inout SurfaceOutput o) {

            half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

            o.Albedo = c.rgb;

            o.Alpha = c.a;

        }

        ENDCG

        

        // lightning

        Pass{

            Tags {"Queue" = "Transparent" }

            Cull Off

            Lighting Off

            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM

            #include "UnityCG.cginc"

            #pragma vertex vert

            #pragma fragment frag

            

            sampler2D _Noise;

            float4 _Noise_ST;

            sampler2D _Ramp;

            float _Speed;

            float _FallOff;

            float _Width;

            half4 _OutlineColor;

            float _OutlineColorFallOff;

            

            struct data{

                float4 vertex : POSITION;

                float4 texcoord : TEXCOORD0;

                float3 normal : NORMAL;

            };

            

            struct v2f{

                float4 position : POSITION;

                float2 uv : TEXCOORD0;

                float viewDir : TEXCOORD1;

            };

            

            v2f vert(data i){

                v2f o;

                float4 vertex = i.vertex + float4(i.normal * _Width, 0);

                o.position = mul(UNITY_MATRIX_MVP, vertex);

                o.uv = TRANSFORM_TEX(i.texcoord, _Noise);

                o.viewDir = 1 - abs(dot(i.normal, normalize(ObjSpaceViewDir(vertex)))); 

                return o;

            }

            

            half4 frag(v2f i) : COLOR{

                half4 noise1 = tex2D(_Noise, i.uv + _Time.xy * _Speed);

                half4 noise2 = tex2D(_Noise, i.uv - _Time.yw * _Speed);

                float x = pow(i.viewDir, _FallOff)*(dot(noise1, noise2));

                half4 col = tex2D(_Ramp, float2(x, 0));

                _OutlineColor.a *= pow(i.viewDir, _OutlineColorFallOff);

                return col+_OutlineColor;

            }

            

            ENDCG

        }

    }

}