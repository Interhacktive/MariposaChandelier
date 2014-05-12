Shader "Little Polygon/Rim" {
	Properties {
		_Color ("Main Color", Color) = (1.0, 1.0, 1.0, 1.0)
	}
	SubShader {
		Tags { "Queue" = "Transparent" }
		Lighting Off Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha
		Pass {
			
		
CGPROGRAM
#pragma exclude_renderers ps3 xbox360 flash
#pragma fragmentoption ARB_precision_hint_fastest
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

uniform fixed4 _Color;

struct FragmentInput {
	float4 pos : SV_POSITION;
	float rim : TEXCOORD1;
};

FragmentInput vert(
	float4 vertex : POSITION,
	float3 normal : NORMAL
) {						
	FragmentInput o;
	o.pos = mul(UNITY_MATRIX_MVP, vertex);
	normal = mul( (float3x3)UNITY_MATRIX_IT_MV, normal);  
//	o.rim = 1.0 - normal.z;

	o.rim = dot(normal, float3(0,0,1));
	o.rim = 1.0 - o.rim * o.rim; // bright
	
//	o.rim = 1.0 - o.rim * o.rim; // bright
//	o.rim = (1.0 - o.rim) * (1.0 - o.rim); // eased
	return o;
}

half4 frag(FragmentInput i) : COLOR {
	return (i.rim * _Color.a) * _Color;
}

ENDCG

		} 	
	}
	FallBack "Diffuse"
}
