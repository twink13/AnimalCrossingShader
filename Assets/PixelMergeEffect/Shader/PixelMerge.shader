Shader "Twink/AnimalCrossing/PixelMerge"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _IDTex ("ID Texture", 2D) = "white" {}
        _PixelMaskTex ("Pixel Mask Texture", 2D) = "white" {}
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
            sampler2D _PixelMaskTex;
			uint _TestBitScroll;

            uint colorIDByCornerIDList[5];
            uint styleIDByCornerIDList[4];
            inline void DecodeCodes(uint4 codes)
            {
                // a -> r -> g -> b
                // a: color0(4bit)      color1(4bit)
                // r: color2(4bit)      color3(4bit)
                // g: colorMain(4bit)   styleID0(3bit) styleID1_part1(1bit)
                // b: styleID1_part2(2bit) styleID2(3bit) styleID3(3bit)

				colorIDByCornerIDList[0] = codes.a >> 4;
				colorIDByCornerIDList[1] = codes.a & 0x0f;
				colorIDByCornerIDList[2] = codes.r >> 4;
				colorIDByCornerIDList[3] = codes.r & 0x0f;
				colorIDByCornerIDList[4] = codes.g >> 4;

                styleIDByCornerIDList[0] = (codes.g >> 1) & 0x7;
                styleIDByCornerIDList[1] = ((codes.g & 0x3) << 2) | (codes.b >> 6);
                styleIDByCornerIDList[2] = (codes.b >> 3) & 0x7;
                styleIDByCornerIDList[3] = codes.b & 0x7;
            }

            // this is one corner data (will reset 4 times)
            fixed cornerVisiblityByStyleID[6];
            inline void DecodeMask(fixed4 mask)
            {
                cornerVisiblityByStyleID[0] = 0;
                cornerVisiblityByStyleID[1] = mask.r;
                cornerVisiblityByStyleID[2] = mask.g;
                cornerVisiblityByStyleID[3] = mask.b;
                cornerVisiblityByStyleID[4] = mask.a;
                cornerVisiblityByStyleID[5] = max(mask.b, mask.a);
            }

            inline fixed CalcCornerVisiblity(uint cornerID, float2 maskUV)
            {
				fixed4 mask = tex2D(_PixelMaskTex, maskUV);
				DecodeMask(mask);

                // todo
                uint styleID = styleIDByCornerIDList[cornerID];
				return cornerVisiblityByStyleID[styleID];
            }

			// x
			inline uint CalcCornerID(float2 uv)
			{
				uint ID1 = step(uv.y, 0.5);
				uint ID2 = step(uv.x * uv.y, 0);
				return ID1 * 2 + ID2;
			}

            fixed4 frag (v2f i) : SV_Target
            {
                // get main code
                fixed4 col = tex2D(_MainTex, i.uv);
                uint4 codes = floor(col * 255.0 + 0.5);
				DecodeCodes(codes);

                // get mask
                float2 maskUV = (i.uv * _MainTex_TexelSize.zw) % 1;

				// calc visibility
				fixed visibility0 = CalcCornerVisiblity(0, maskUV);
				fixed visibility1 = CalcCornerVisiblity(1, float2(1 - maskUV.y, maskUV.x));
				fixed visibility2 = CalcCornerVisiblity(2, float2(1 - maskUV.x, 1 - maskUV.y));
				fixed visibility3 = CalcCornerVisiblity(3, float2(maskUV.y, 1 - maskUV.x));
				fixed visibilityTotal = min(1, visibility0 + visibility1 + visibility2 + visibility3);

				fixed4 cornerColor = 0;
				cornerColor += visibility0 * _MainColors[colorIDByCornerIDList[0]];
				cornerColor += visibility1 * _MainColors[colorIDByCornerIDList[1]];
				cornerColor += visibility2 * _MainColors[colorIDByCornerIDList[2]];
				cornerColor += visibility3 * _MainColors[colorIDByCornerIDList[3]];


				fixed4 mainColor = _MainColors[colorIDByCornerIDList[4]];
                fixed4 result = 0;
                result = lerp(mainColor, cornerColor, visibilityTotal);

                return result;
            }
            ENDCG
        }
    }
}
