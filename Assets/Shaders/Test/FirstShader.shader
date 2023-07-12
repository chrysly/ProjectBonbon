Shader "Unlit/FirstShader" {
    Properties{    //input data
        //_MainTex ("Texture", 2D) = "white" {}
        _Value("Value", Float) = 1.0;
    }
    SubShader {
        Tags { "RenderType"="Opaque" }  //how stuff renders in the pipeline
        //LOD 100                       //picks different subshaders based on LOD, rarely used?

        Pass {                          //explicit rendering for this pass itself, graphics
            CGPROGRAM
            #pragma vertex vert         //tells compiler what function is vertex
            #pragma fragment frag       //tells compiler what function is fragment shader


            // make fog work - IGNORE FOR NOW
            //#pragma multi_compile_fog

            #include "UnityCG.cginc"    //file containing unity specific functions for efficiency

            float _Value;

            //sampler2D _MainTex;
            //float4 _MainTex_ST;

            struct MeshData {               //per-vertex mesh data
                float4 vertex : POSITION;   // vertex position
                //float3 normals: NORMAL;
                //float4 tangent :TANGENT;
                //float4 color : COLOR;
                float2 uv0 : TEXCOORD0;      // uv coordinates
                //float2 uv1 : TEXCORRD1;
            };

            struct v2f {                    //default name for data passed from vertex shader to fragment shader
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            v2f vert (MeshData v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
