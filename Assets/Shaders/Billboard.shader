Shader "Unlit/Billboard"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "DisableBatching" = "True" }

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 pos : SV_POSITION;
			};
			
			sampler2D _MainTex;
			float4 _MainTex_ST;
			int _FlipX;
			int _FlipY;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				
			    float3 vpos = mul((float3x3)unity_ObjectToWorld, v.vertex.xyz);
			    float4 worldCoord = float4(unity_ObjectToWorld._m03, unity_ObjectToWorld._m13, unity_ObjectToWorld._m23, 1);
			    float4 viewPos = mul(UNITY_MATRIX_V, worldCoord) + float4(vpos, 0);
			    o.pos = mul(UNITY_MATRIX_P, viewPos);

				float flipFactorX = _FlipX == 1 ? -1.0 : 1.0;
				float flipFactorY = _FlipY == 1 ? -1.0 : 1.0;
				o.uv = (v.uv - 0.5) * float2(flipFactorX, flipFactorY) + 0.5;
				
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}