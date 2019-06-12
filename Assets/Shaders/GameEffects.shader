Shader "Unlit/GameEffects"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	    _Saturation("Saturation", Range(0.0, 1.0)) = 1.0
		_Blur("Blur", Int) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
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
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			float _Saturation;
			float _Blur;

			float interpolateColorValue(float start, float end, float percentage) {
				return start + (end - start) * percentage;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 pixel = tex2D(_MainTex, i.uv);

				float desaturatedColorValue = (pixel.r + pixel.g + pixel.b) / 3.0;
				float desaturationPercentage = 1.0 - (float)_Saturation;

				float redComponent = interpolateColorValue(pixel.r, desaturatedColorValue, desaturationPercentage);
				float greenComponent = interpolateColorValue(pixel.g, desaturatedColorValue, desaturationPercentage);
				float blueComponent = interpolateColorValue(pixel.b, desaturatedColorValue, desaturationPercentage);

				return fixed4(redComponent, greenComponent, blueComponent, 1);
				//return fixed4(_Saturation * 255, _Saturation * 255, _Saturation * 255, 1);
			}
			ENDCG
		}
	}
}
