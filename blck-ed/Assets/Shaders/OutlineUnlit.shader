Shader "Unlit/OutlineUnlit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color",Color) = (1,1,1,1)
        _OutlineColor ("OutlineColor", Color) = (1,1,1,1)
        _OutlineTex("Outline Texture", 2D) = "black" {}
        _OutlineWidth("Outline Width", Range(1.0,25.0)) = 1.3
    }
    SubShader
    {
        Tags { "Queue"="Transparent"}

        ZWrite Off
        Pass
        {
            Name "OUTLINE"
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
            sampler2D _MainTex;
            sampler2D _OutlineTex;
            float4 _OutlineColor;
            float _OutlineWidth;
            
            v2f vert (appdata v)
            {
                //this is the most important line to achieve the outline effect.
                v.vertex.xyz *= 1.1;
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float4 col = tex2D(_OutlineTex, i.uv);
                //return col
                return col*_OutlineColor;
            }
            ENDCG
        }
        Pass
        {
            Name "OBJECT"
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
            sampler2D _MainTex;
            float4 _Color;
            
            v2f vert (appdata v)
            {
                //it seems to me I am going to need two seperate shaders working together. 20.00 
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                //return col;
                
                return col * _Color;
                //uncomment ^^ this if you want the props true colors. It gives the props a really cool effect!
                //also comment out the return col;
            }
            ENDCG
        }
    }
}
