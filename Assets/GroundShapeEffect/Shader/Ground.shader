Shader "Unlit/Ground"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GrassTex ("Grass Texture", 2D) = "white" {}
        _ColorInner ("Color Inner", Color) = (0, 1, 0, 1)
        _ColorOuter ("Color Outer", Color) = (1, 1, 0, 1)
        _DistanceScale ("Distance Scale", Range(1, 5)) = 2
        _HeightScale("Height Scale", Range(0, 1)) = 1
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
                float4 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _GrassTex;
            float4 _GrassTex_ST;
            float4 _ColorInner;
            float4 _ColorOuter;
            float _DistanceScale;
            float _HeightScale;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = 0;
                o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv.zw = TRANSFORM_TEX(v.uv, _GrassTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv.xy);
                fixed4 grass = tex2D(_GrassTex, i.uv.zw);

                float height = col.a * 1;
                height = height * _HeightScale + (1 - _HeightScale) / 2;

                float mask = distance(i.uv.xy, float2(0.5, 0.5)) * _DistanceScale;
                mask = min(mask, 1);

                fixed selection = step(mask, 1 - height);


                return lerp(grass, _ColorInner, selection);
            }
            ENDCG
        }
    }
}
