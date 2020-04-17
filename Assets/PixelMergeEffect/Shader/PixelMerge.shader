﻿Shader "Twink/AnimalCrossing/PixelMerge"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_TestBitScroll ("Test Bit Scroll", Range(0, 8)) = 0
    }
    SubShader
    {
        Pass
        {
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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
			uint _TestBitScroll;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                uint xxx = floor(col.r * 255.0 + 0.5);

                fixed4 result = 0;
                result.a = 1;
                result.r = (xxx >> _TestBitScroll) & 1;
                return result;
            }
            ENDCG
        }
    }
}