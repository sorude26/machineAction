Shader "Custom/Scanline"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_LineSpeed("Scan Line Speed", Float) = 1.0
		_TrajectorySize("Scan Trajectory Size", Float) = 1.0
		_IntervalSec("Scan Interval", Float) = 2.0
	}
		SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
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
				float3 worldPos : TEXCOORD2;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			fixed4 _LineColor; // color of line
			half _LineSpeed; // speed of line movement
			fixed4 _TrajectoryColor; // color of trajectory
			half _TrajectorySize; // size of trajectory

			half _IntervalSec; // interval(sec)

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o, o.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{  
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
			    // apply fog
			    UNITY_APPLY_FOG(i.fogCoord, col);

			    float z = i.worldPos.z;
				z -= _Time.w * _LineSpeed;
				z = fmod(z, _IntervalSec);
			    col.a = step(0, z) * step(z, 0.1);
				col.a += step(z, 0) * smoothstep(-_TrajectorySize, 0.0, z);
			    return col;
			}
			ENDCG
		}
	}
}
