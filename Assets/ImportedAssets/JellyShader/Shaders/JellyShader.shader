Shader "Custom/JellyWaterDropShader" {
    Properties {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0, 1)) = 0.5
        _Metallic ("Metallic", Range(0, 1)) = 0.0

        _ControlTime ("Time", Range(0, 10)) = 0
        _ModelOrigin ("Model Origin", Vector) = (0, 0, 0)
        _ImpactOrigin ("Impact Origin", Vector) = (-5, 0, 0)

        _Frequency ("Frequency", Range(0, 1000)) = 10
        _Amplitude ("Amplitude", Range(0, 5)) = 0.1
        _WaveFalloff ("Wave Falloff", Range(1, 8)) = 4
        _MaxWaveDistortion ("Max Wave Distortion", Range(0.1, 2.0)) = 1
        _ImpactSpeed ("Impact Speed", Range(0, 10)) = 0.5
        _WaveSpeed ("Wave Speed", Range(-10, 10)) = -5

        _Transparency ("Transparency", Range(0, 1)) = 1

        _Emission ("Emission", Range(0, 1)) = 0
        _WaveDirection ("Wave Direction", Vector) = (1, 0, 0)

        _RefractionStrength ("Refraction Strength", Range(0, 1)) = 0.1
        _ReflectionStrength ("Reflection Strength", Range(0, 1)) = 0.1
        _WaveSpeed ("Water Wave Speed", Range(0, 5)) = 1
    }
    SubShader {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 200

        Pass {
            CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members uv_MainTex,viewDir,normalDir)
#pragma exclude_renderers d3d11
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;

            struct appdata_t {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float4 texcoord1 : TEXCOORD0;
                float4 texcoord2 : TEXCOORD1;
            };

            struct v2f {
                float2 uv_MainTex;
                float3 viewDir;
                float3 normalDir;
                float4 texcoord1 : TEXCOORD0;
                float4 texcoord2 : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            half _Glossiness;
            half _Metallic;
            fixed4 _Color;

            float _ControlTime;
            float3 _ModelOrigin;
            float3 _ImpactOrigin;

            half _Frequency;
            half _Amplitude;
            half _WaveFalloff;
            half _MaxWaveDistortion;
            half _ImpactSpeed;
            half _WaveSpeed;

            half _Transparency;

            half _Emission;
            float3 _WaveDirection;

            half _RefractionStrength;
            half _ReflectionStrength;
            half _WaterWaveSpeed;

            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv_MainTex = v.texcoord1.xy;
                o.viewDir = normalize(UnityWorldSpaceViewDir(v.vertex));
                o.normalDir = normalize(UnityObjectToWorldNormal(v.normal));
                o.texcoord1 = v.texcoord1;
                o.texcoord2 = v.texcoord2;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // Water drop distortion
                float2 waterDistortion = tex2D(_MainTex, i.uv_MainTex).rg;
                float2 refractionOffset = waterDistortion * _RefractionStrength;
                float2 reflectionOffset = reflect(i.uv_MainTex - 0.5, float2(0, 1)) * _ReflectionStrength;
                float waterWaveOffset = sin(_Time.y * _WaterWaveSpeed);
                float2 finalOffset = refractionOffset + reflectionOffset + float2(0, waterWaveOffset);

                // Original Jelly distortion
                fixed4 c = tex2Dlod(_MainTex, float4(i.uv_MainTex + finalOffset, 0, 0));
                half3 displacement = normalize(mul(unity_WorldToObject, _WaveDirection));
                i.normalDir = lerp(i.normalDir, displacement, _Amplitude);

                // Combine water and jelly distortion
                SurfaceOutputStandard o;
                o.Albedo = c.rgb * _Color.rgb;
                o.Specular = _Metallic;
                o.Smoothness = _Glossiness;
                o.Normal = i.normalDir;
                o.Alpha = c.a * _Transparency;

                return LightingStandard(i, o);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
