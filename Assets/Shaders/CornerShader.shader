Shader "Unlit/CornerShader"
{
    Properties
    {
        _CornerRadius ("Corner Radius", Range(0, 1)) = 0.1
        _BackgroundColor ("Background Color", Color) = (0,0,0,1)
    }

    SubShader
    {
        Tags { "Queue"="Overlay" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float _CornerRadius;
            half4 _BackgroundColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // Get screen coordinates in the range (0, 1)
                float2 screenPos = (i.pos.xy / i.pos.w) * 0.5 + 0.5;

                // Compute the distance from the corners
                float dist = min(min(screenPos.x, 1.0 - screenPos.x), min(screenPos.y, 1.0 - screenPos.y));

                // If we're inside the rounded corner radius, return the background color
                if (dist < _CornerRadius)
                {
                    return _BackgroundColor;
                }

                // Otherwise, discard the pixel to make the corners transparent
                discard;

                return half4(1, 1, 1, 1); // Return white color (or whatever color you'd like)
            }
            ENDCG
        }
    }
}
