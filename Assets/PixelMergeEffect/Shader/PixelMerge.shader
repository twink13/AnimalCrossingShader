Shader "Twink/AnimalCrossing/PixelMerge"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _IDTex ("ID Texture", 2D) = "white" {}
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

            fixed4 _MainColors[16];

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            sampler2D _IDTex;
			uint _TestBitScroll;

            fixed4 frag (v2f i) : SV_Target
            {
                // get main code
                fixed4 col = tex2D(_MainTex, i.uv);
                uint4 codes = floor(col * 255.0 + 0.5);

				// set colors map
				uint colorIDByCornerIDList[4];
				colorIDByCornerIDList[0] = codes.r & 0x0f;
				colorIDByCornerIDList[1] = codes.r >> 4;
				colorIDByCornerIDList[2] = codes.g & 0x0f;
				colorIDByCornerIDList[3] = codes.g >> 4;

                // get area id
                float2 IDUV = i.uv * _MainTex_TexelSize.zw;
                fixed4 IDCol = tex2D(_IDTex, IDUV);
                uint ID = floor(IDCol.a * 255.0 + 0.5);

				// calc corner ID
				// 3 0
				// 2 1
				uint cornerID = floor(ID / 2);
				fixed4 cornerColor = _MainColors[colorIDByCornerIDList[cornerID]];
				int colorSelectionMask = (codes.a >> ID) & 1;
				cornerColor = cornerColor * colorSelectionMask;

                int mainMask = (ID >> 3) & 1;
                fixed4 mainColor = _MainColors[codes.b & 0x0f];

                fixed4 result = 0;
                result = lerp(cornerColor, mainColor, mainMask);

                return result;
            }
            ENDCG
        }
    }
}
