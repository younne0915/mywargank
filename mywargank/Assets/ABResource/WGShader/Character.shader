Shader "WG/Character"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	_Mask1("Mask1",2D) = "black"{}
	_Scale("Rim_Power",range(1,8)) = 1.7
		_RimColor("RimColor",Color) = (1,1,1,1)
		_Shininess("Shininess", Range(0.01, 1)) = 0.5
		_SpecRatio("SpecRatio", Range(0, 1)) = 1
		_IlluminScale("IlluminScale",range(0,10)) = 1
		_SpecColor1("SpecColor",Color) = (1,1,1,1)
		_Color("MainColor",Color) = (1,1,1,1)

	}

		SubShader
	{
		tags{ "lightmode" = "ForwardBase"   }
		LOD 100

		Pass
	{
		CGPROGRAM
#include "UnityCG.cginc"
#include "Lighting.cginc"
#pragma multi_compile_fwdbase
#include "autolight.cginc"
#pragma vertex vert  
#pragma fragment frag  
#pragma fragmentoption ARB_precision_hint_fastest  
#pragma multi_compile_fwdbase  

		uniform fixed _Scale;
	uniform sampler2D _MainTex;
	uniform sampler2D _Mask1;
	uniform fixed4 _RimColor;
	uniform fixed4 _SpecColor1;
	uniform fixed4 _MainTex_ST;
	uniform fixed _Specular;
	uniform fixed _Shininess;
	uniform fixed _SpecRatio;
	uniform fixed _IlluminScale;
	uniform fixed4 _Color;

	struct a2v
	{
		fixed4 vertex : POSITION;
		fixed3 normal : NORMAL;
		fixed4 tangent : TANGENT;
		fixed4 texcoord : TEXCOORD0;
	};

	struct v2f
	{
		fixed4 pos : SV_POSITION;
		fixed3 normal : TEXCOORD0;
		fixed2 uv : TEXCOORD1;
		fixed3 viewDir : TEXCOORD2;
		fixed3 lightDir : TEXCOORD3;
	};

	v2f vert(a2v v)
	{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
			TANGENT_SPACE_ROTATION;
		o.normal = normalize(mul(rotation, v.normal));
		o.viewDir = normalize(mul(rotation, ObjSpaceViewDir(v.vertex)));
		o.lightDir = normalize(mul(rotation, ObjSpaceLightDir(v.vertex)));

	//	o.normal = normalize(mul((float3x3)unity_ObjectToWorld, SCALED_NORMAL));
	//	o.viewDir = normalize(WorldSpaceViewDir(v.vertex));
	//	o.lightDir = normalize(_WorldSpaceLightPos0.xyz);
		return o;
	}

	fixed4 frag(v2f i) : color
	{

		fixed4 col = tex2D(_MainTex,i.uv) * _Color;
	fixed4 mask1 = tex2D(_Mask1, i.uv);
	half3 h = normalize(i.lightDir + i.viewDir);
	fixed diff = max(0, dot(i.normal, i.lightDir));
	fixed nh = max(0, dot(i.normal, h));
	fixed3 spec = _SpecColor1.rgb * mask1.a * pow(nh, _Shininess*128.0);
	fixed3 light = _LightColor0.rgb * diff;
	//fixed atten = LIGHT_ATTENUATION(i);//光照衰减
	fixed atten = 1;
	light += UNITY_LIGHTMODEL_AMBIENT;
	col.rgb *= light * atten;
	col.rgb += spec * _SpecRatio ;
	col.rgb += col.rgb * mask1.rgb* _IlluminScale;
	//fixed bright = mask1.r * 0.299 + mask1.g * 0.587 + mask1.b * 0.114;
	 _RimColor = _RimColor * saturate(1 - saturate(dot(i.normal,i.viewDir))*_Scale);
	_RimColor.a = 0;
	col += _RimColor;
	return col;
	}
		ENDCG
	}

	}
}