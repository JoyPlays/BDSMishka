// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// MatCap Shader, (c) 2015 Jean Moreno

Shader "MatCap/Vertex/Textured Add-new"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_MatCap ("MatCap (RGB)", 2D) = "white" {}
// Outline
        _Outline ("Outline Extrusion", Range(-1,1)) = 0.05
        _OutColor ("Outline Color", Color) = (1,1,1,1)
	}
	
	Subshader
	{
		Tags { "RenderType"="Opaque" }

		 // Outline addition starts here
        Cull Off
        ZWrite Off
        //ZTest Always // Uncomment for "see through"
 
        CGPROGRAM
            #pragma surface surf Solid vertex:vert
            struct Input {
                float4 color : COLOR;
            };
 
            fixed4 _OutColor;
            float _Outline;
 
        fixed4 LightingSolid (SurfaceOutput s, half3 lightDir, half atten) {
        return _OutColor;
        }
 
            void vert (inout appdata_full v) {
                v.vertex.xyz += v.normal * _Outline;
            }
 
            void surf (Input IN, inout SurfaceOutput o) {
                o.Albedo = _OutColor.rgb;
            }
        ENDCG
 
        Cull Back
        ZWrite On  
        ZTest LEqual
        // Outline addition ends here
		
		Pass
		{
			Tags { "LightMode" = "Always" }
			
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"
				
				struct v2f
				{
					float4 pos	: SV_POSITION;
					float2 uv 	: TEXCOORD0;
					float2 cap	: TEXCOORD1;
				};
				
				uniform float4 _MainTex_ST;
				
				v2f vert (appdata_base v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos (v.vertex);
					o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
					
					float3 worldNorm = normalize(unity_WorldToObject[0].xyz * v.normal.x + unity_WorldToObject[1].xyz * v.normal.y + unity_WorldToObject[2].xyz * v.normal.z);
					worldNorm = mul((float3x3)UNITY_MATRIX_V, worldNorm);
					o.cap.xy = worldNorm.xy * 0.5 + 0.5;
					
					return o;
				}
				
				uniform sampler2D _MainTex;
				uniform sampler2D _MatCap;
				
				fixed4 frag (v2f i) : COLOR
				{
					fixed4 tex = tex2D(_MainTex, i.uv);
					fixed4 mc = tex2D(_MatCap, i.cap);
					
					return (tex + (mc*2.0)-1.0);
				}
			ENDCG
		}
	}
	
	Fallback "VertexLit"
}