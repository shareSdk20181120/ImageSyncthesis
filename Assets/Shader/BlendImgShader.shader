Shader "Example/BlendImgShader"
{
	Properties
	{
		_MainTex ("Main Texture", 2D) = "white" {}
		_DesTex("Des Texture",2D) = "white"{}
		_XPos("XPox",Range(0,2208))=1
		_YPos("YPos",Range(0,1242))=1
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 100

		Pass
		{
			Tags{"LightMode"="ForwardBase"}
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _DesTex;
			float4 _DesTex_ST;
			fixed _XPos;
			fixed _YPos;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv.xy = v.uv.xy*_MainTex_ST.xy + _MainTex_ST.zw;
				//o.uv.zw = v.uv*_DesTex_ST.xy + _DesTex_ST.zw;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv.xy);
				i.uv.zw = i.vertex.xy-float2( _XPos,_YPos);
				fixed4 desColor = tex2D(_DesTex, i.uv.zw);
				fixed4 des;
				if (i.vertex.x > _XPos && i.vertex.y > _YPos)
					des = desColor;
				else
					des =  col;
				return des;
			}
			ENDCG
		}
	}
}
