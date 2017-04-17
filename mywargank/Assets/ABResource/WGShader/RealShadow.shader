Shader "WG/Shadow/Realshadow" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}

	SubShader {
	    Tags { "Queue"="Geometry"  "RenderType"="Opaque" "ShadowCaster" = "Opaque"}
	  
	    Pass {
	    	ZTest Less ZWrite On Cull off
	        Fog { Mode Off }
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

			struct v2f {
			    float4 pos : POSITION;
			    float2 uv : TEXCOORD1;
			};
			
			sampler2D _ShadowBorder;
			float4x4 _DirLight_VP;

			v2f vert (appdata_base v) {
			    v2f o;
			    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			    o.uv = (o.pos.xy / o.pos.w + 1.0) * 0.5 ;
			    return o;
			}

			fixed4 frag(v2f i) : COLOR {
			   fixed4 border = tex2D(_ShadowBorder , i.uv);
			   fixed c = saturate(1-border.a);
			   return fixed4(c,c,c,1) ;
			}
			ENDCG
	    }
	}
}
