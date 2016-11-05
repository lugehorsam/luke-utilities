Shader "Unlit/UnlitAlpha"
{
	Properties
	{
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Sprite Texture", 2D) = "white" {}

	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
        Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			Cull Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			float4 _Color;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float4 col = _Color;
				return col;
			}
			ENDCG
		}
	}
}
