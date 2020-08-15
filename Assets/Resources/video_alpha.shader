// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2417,x:32942,y:32733,varname:node_2417,prsc:2|custl-3475-OUT,alpha-469-OUT;n:type:ShaderForge.SFN_TexCoord,id:2377,x:31624,y:32705,varname:node_2377,prsc:2,uv:0;n:type:ShaderForge.SFN_Append,id:1035,x:32152,y:32762,varname:node_1035,prsc:2|A-305-OUT,B-2377-V;n:type:ShaderForge.SFN_Multiply,id:305,x:31969,y:32844,varname:node_305,prsc:2|A-2377-U,B-7158-OUT;n:type:ShaderForge.SFN_Vector1,id:7158,x:31570,y:32927,varname:node_7158,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Tex2dAsset,id:9305,x:32131,y:33011,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_9305,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5733,x:32390,y:32869,varname:node_5733,prsc:2,ntxv:0,isnm:False|UVIN-1035-OUT,TEX-9305-TEX;n:type:ShaderForge.SFN_Multiply,id:3475,x:32432,y:32681,varname:node_3475,prsc:2|A-6255-OUT,B-4881-OUT,C-7416-OUT,D-5733-RGB;n:type:ShaderForge.SFN_ValueProperty,id:6255,x:32222,y:32547,ptovrint:False,ptlb:ScaleY,ptin:_ScaleY,varname:node_6255,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:7416,x:32222,y:32659,ptovrint:False,ptlb:ScaleX,ptin:_ScaleX,varname:node_7416,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:4881,x:32222,y:32602,ptovrint:False,ptlb:FillMode,ptin:_FillMode,varname:node_4881,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:3091,x:32380,y:33028,varname:node_3091,prsc:2,ntxv:0,isnm:False|UVIN-1064-OUT,TEX-9305-TEX;n:type:ShaderForge.SFN_Multiply,id:1566,x:31806,y:32996,varname:node_1566,prsc:2|A-2377-U,B-7158-OUT;n:type:ShaderForge.SFN_Add,id:1492,x:31789,y:33167,varname:node_1492,prsc:2|A-1566-OUT,B-5561-OUT;n:type:ShaderForge.SFN_Append,id:1064,x:31979,y:33167,varname:node_1064,prsc:2|A-1492-OUT,B-2377-V;n:type:ShaderForge.SFN_Slider,id:5561,x:31447,y:33275,ptovrint:False,ptlb:offset,ptin:_offset,varname:node_5561,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.45,cur:0.5,max:0.55;n:type:ShaderForge.SFN_If,id:469,x:32605,y:33148,varname:node_469,prsc:2|A-3091-R,B-810-OUT,GT-3091-R,EQ-3091-R,LT-811-OUT;n:type:ShaderForge.SFN_Vector1,id:811,x:32380,y:33321,varname:node_811,prsc:2,v1:0;n:type:ShaderForge.SFN_Slider,id:810,x:32149,y:33238,ptovrint:False,ptlb:clip,ptin:_clip,varname:node_810,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:0.2;proporder:9305-6255-7416-4881-5561-810;pass:END;sub:END;*/

Shader "Unlit/video_alpha" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _ScaleY ("ScaleY", Float ) = 1
        _ScaleX ("ScaleX", Float ) = 1
        _FillMode ("FillMode", Float ) = 1
        _offset ("offset", Range(0.45, 0.55)) = 0.5
        _clip ("clip", Range(0, 0.2)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _ScaleY;
            uniform float _ScaleX;
            uniform float _FillMode;
            uniform float _offset;
            uniform float _clip;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
                float node_7158 = 0.5;
                float2 node_1035 = float2((i.uv0.r*node_7158),i.uv0.g);
                float4 node_5733 = tex2D(_MainTex,TRANSFORM_TEX(node_1035, _MainTex));
                float3 finalColor = (_ScaleY*_FillMode*_ScaleX*node_5733.rgb);
                float2 node_1064 = float2(((i.uv0.r*node_7158)+_offset),i.uv0.g);
                float4 node_3091 = tex2D(_MainTex,TRANSFORM_TEX(node_1064, _MainTex));
                float node_469_if_leA = step(node_3091.r,_clip);
                float node_469_if_leB = step(_clip,node_3091.r);
                return fixed4(finalColor,lerp((node_469_if_leA*0.0)+(node_469_if_leB*node_3091.r),node_3091.r,node_469_if_leA*node_469_if_leB));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
