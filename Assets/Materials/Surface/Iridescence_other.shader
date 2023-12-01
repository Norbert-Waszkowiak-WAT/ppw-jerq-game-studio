Shader "Smkgames/Fragment/Iridescence/GlossyIridescence" {
    Properties {
        _Color ("Main Color", Color) = (.5,.5,.5,1)
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _Glossiness ("Glossiness", Range (0, 1)) = 0.5
    }

    CGINCLUDE
    #include "UnityCG.cginc"
    ENDCG

    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Standard

        sampler2D _MainTex;
        fixed4 _Color;
        float _Glossiness;

        struct Input {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutputStandard o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
            o.Smoothness = _Glossiness;
        }
        ENDCG
    }

    FallBack "Diffuse"
}
Shader "Smkgames/Fragment/Iridescence/GlossyIridescence" {
    Properties {
        _Color ("Main Color", Color) = (.5,.5,.5,1)
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _Glossiness ("Glossiness", Range (0, 1)) = 0.5
    }

    CGINCLUDE
    #include "UnityCG.cginc"
    ENDCG

    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Standard

        sampler2D _MainTex;
        fixed4 _Color;
        float _Glossiness;

        struct Input {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutputStandard o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
            o.Smoothness = _Glossiness;
        }
        ENDCG
    }

    FallBack "Diffuse"
}
