Shader "Custom/SpriteRotate"
{
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" {}
        _Rotation("Rotation", Float) = 0.0
    }

    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            fixed4    _MainTex_ST;
            float     _Rotation;
            float     _OffsetX;
            float     _OffsetY;

            struct appdata {
                float4 vertex : POSITION;
                float4 texcoord : TEXCOORD0;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                float    s = sin(_Rotation);
                float    c = cos(_Rotation);

                // Rotation Matrix [[Cos, -Sin], [Sin, Cos]] 
                float2x2 rotationMatrix = float2x2(c, -s, s, c);

                // Vertex to Fragment Position
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                float offsetX = .5; 
                float offsetY = .5;

                float x = v.texcoord.x - offsetX;
                float y = v.texcoord.y - offsetY; 
                o.uv = mul(float2(x, y), rotationMatrix) + float2(offsetX, offsetY);

                return o;
            }

            fixed4 frag(v2f i) : COLOR
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}