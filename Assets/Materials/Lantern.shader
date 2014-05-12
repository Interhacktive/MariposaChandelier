Shader "Little Polygon/Vertex Color" {
	Properties {
		_Color ("Main Color", Color) = (1.0, 1.0, 1.0, 1.0)
	}
	SubShader {
		Lighting Off Fog { Mode Off }
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
	fixed4 vertexColor : COLOR;
};

FragmentInput vert(float4 vertex : POSITION, fixed4 color : COLOR) {						
	FragmentInput o;
	o.pos = mul(UNITY_MATRIX_MVP, vertex);
	o.vertexColor = color;
	return o;
}

half4 frag(FragmentInput i) : COLOR {
	return i.vertexColor * _Color;
}

ENDCG

		} 	
	}
}
