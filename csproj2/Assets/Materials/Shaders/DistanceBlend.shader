// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "New Amplify Shader"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_TransitionDistance("Transition Distance", Float) = 0.7
		_TransitionFalloff("Transition Falloff", Float) = 30
		_Normal("Normal", 2D) = "bump" {}
		_NormalIntensity("Normal Intensity", Float) = 0.81
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Metallic("Metallic", 2D) = "white" {}
		_Roughness("Roughness", 2D) = "white" {}
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_MetallicMutliply("Metallic Mutliply", Float) = 0
		_RoughnessMultiply("Roughness Multiply", Float) = 0
		_Albedo2("Albedo2", 2D) = "white" {}
		_Normal2("Normal2", 2D) = "bump" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform float _NormalIntensity;
		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _Normal2;
		uniform float4 _Normal2_ST;
		uniform float _TransitionDistance;
		uniform float _TransitionFalloff;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform sampler2D _Albedo2;
		uniform float4 _Albedo2_ST;
		uniform sampler2D _Metallic;
		uniform float4 _Metallic_ST;
		uniform float _MetallicMutliply;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform sampler2D _Roughness;
		uniform float4 _Roughness_ST;
		uniform float _RoughnessMultiply;
		uniform sampler2D _TextureSample1;
		uniform float4 _TextureSample1_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float clampResult47 = clamp( _NormalIntensity , 0.0 , 1.0 );
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float2 uv_Normal2 = i.uv_texcoord * _Normal2_ST.xy + _Normal2_ST.zw;
			float3 ase_worldPos = i.worldPos;
			float clampResult4_g51 = clamp( pow( ( distance( ase_worldPos , _WorldSpaceCameraPos ) / _TransitionDistance ) , _TransitionFalloff ) , 0.0 , 1.0 );
			float3 lerpResult48 = lerp( UnpackScaleNormal( tex2D( _Normal, uv_Normal ), clampResult47 ) , UnpackScaleNormal( tex2D( _Normal2, uv_Normal2 ), clampResult47 ) , clampResult4_g51);
			o.Normal = lerpResult48;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float2 uv_Albedo2 = i.uv_texcoord * _Albedo2_ST.xy + _Albedo2_ST.zw;
			float clampResult4_g50 = clamp( pow( ( distance( ase_worldPos , _WorldSpaceCameraPos ) / _TransitionDistance ) , _TransitionFalloff ) , 0.0 , 1.0 );
			float4 lerpResult12 = lerp( tex2D( _Albedo, uv_Albedo ) , tex2D( _Albedo2, uv_Albedo2 ) , clampResult4_g50);
			o.Albedo = lerpResult12.rgb;
			float2 uv_Metallic = i.uv_texcoord * _Metallic_ST.xy + _Metallic_ST.zw;
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float clampResult4_g49 = clamp( pow( ( distance( ase_worldPos , _WorldSpaceCameraPos ) / _TransitionDistance ) , _TransitionFalloff ) , 0.0 , 1.0 );
			float4 lerpResult53 = lerp( ( tex2D( _Metallic, uv_Metallic ) * _MetallicMutliply ) , ( tex2D( _TextureSample0, uv_TextureSample0 ) * _MetallicMutliply ) , clampResult4_g49);
			o.Metallic = lerpResult53.r;
			float2 uv_Roughness = i.uv_texcoord * _Roughness_ST.xy + _Roughness_ST.zw;
			float2 uv_TextureSample1 = i.uv_texcoord * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
			float clampResult4_g29 = clamp( pow( ( distance( ase_worldPos , _WorldSpaceCameraPos ) / _TransitionDistance ) , _TransitionFalloff ) , 0.0 , 1.0 );
			float4 lerpResult61 = lerp( ( tex2D( _Roughness, uv_Roughness ) * _RoughnessMultiply ) , ( tex2D( _TextureSample1, uv_TextureSample1 ) * _RoughnessMultiply ) , clampResult4_g29);
			o.Smoothness = ( 1.0 - lerpResult61 ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15500
-61.6;105.6;1522;789;1835.565;910.5511;1.748822;True;True
Node;AmplifyShaderEditor.RangedFloatNode;46;-1261.494,5.469585;Float;False;Property;_NormalIntensity;Normal Intensity;7;0;Create;True;0;0;False;0;0.81;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;55;-214.3663,879.6078;Float;True;Property;_Roughness;Roughness;10;0;Create;True;0;0;False;0;None;753e631767d377c4bbb36bff11db761d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;72;-408.2514,1284.27;Float;True;Property;_TextureSample1;Texture Sample 1;10;0;Create;True;0;0;False;0;None;ca0915f53d1abc749a59001b58114d30;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;44;-1305.869,465.2208;Float;False;Property;_TransitionDistance;Transition Distance;4;0;Create;True;0;0;False;0;0.7;86.91;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;64;-532.9568,1179.465;Float;False;Property;_RoughnessMultiply;Roughness Multiply;12;0;Create;True;0;0;False;0;0;3.77;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;43;-1295.928,358.8492;Float;False;Property;_TransitionFalloff;Transition Falloff;5;0;Create;True;0;0;False;0;30;0.8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;73;-23.67015,1422.485;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;62;92.74518,1064.033;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;60;-952.2092,1191.652;Float;False;DistanceBlend;-1;;29;7ac8c59ef95714146b1aa83ecd62dfd8;0;2;14;FLOAT;0;False;13;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;51;-876.2942,503.0001;Float;True;Property;_Metallic;Metallic;9;0;Create;True;0;0;False;0;None;0cfc732b5ebf017458f65ecaccceb019;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;57;-1229.792,754.2346;Float;False;Property;_MetallicMutliply;Metallic Mutliply;11;0;Create;True;0;0;False;0;0;0.91;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;71;-1152.393,840.1143;Float;True;Property;_TextureSample0;Texture Sample 0;8;0;Create;True;0;0;False;0;None;ce9b09b9a11abd342aa9320aaeb5b2e2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;47;-1046.555,75.10884;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;42;-774.7806,-184.4082;Float;False;DistanceBlend;-1;;50;7ac8c59ef95714146b1aa83ecd62dfd8;0;2;14;FLOAT;0;False;13;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;66;-859.9919,-718.5043;Float;True;Property;_Albedo2;Albedo2;13;0;Create;True;0;0;False;0;None;80b63a6838e3ab84ead17f5baa92cbf8;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;52;-751.2061,991.8153;Float;False;DistanceBlend;-1;;49;7ac8c59ef95714146b1aa83ecd62dfd8;0;2;14;FLOAT;0;False;13;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;69;-919.1168,197.2272;Float;True;Property;_Normal2;Normal2;14;0;Create;True;0;0;False;0;None;7eed51ed450973d44a82627c150fae36;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;56;-524.2816,659.6874;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;16;-493.7441,-742.7162;Float;True;Property;_Albedo;Albedo;3;0;Create;True;0;0;False;0;None;761729aba4be63547820423cfe997b8a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;70;-606.0273,825.5534;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;61;152.2303,1223.04;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;50;-775.9001,382.6293;Float;False;DistanceBlend;-1;;51;7ac8c59ef95714146b1aa83ecd62dfd8;0;2;14;FLOAT;0;False;13;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;45;-877.8502,-1.226337;Float;True;Property;_Normal;Normal;6;0;Create;True;0;0;False;0;None;f5c2fc00fe9ffc84da549ecd99406104;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;67;-805.0605,-342.921;Float;False;Property;_Color1;Color 1;1;0;Create;True;0;0;False;0;0,0,0,0;0.8584906,0.1012371,0.1012371,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;53;-464.7965,818.6942;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;12;-488.3716,-357.5293;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;48;-489.4908,209.5082;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;68;-795.061,-521.921;Float;False;Property;_Color4;Color 4;2;0;Create;True;0;0;False;0;0,0,0,0;0.3348614,0.745283,0.2777233,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;65;-181.8777,549.4953;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;New Amplify Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;73;0;72;0
WireConnection;73;1;64;0
WireConnection;62;0;55;0
WireConnection;62;1;64;0
WireConnection;60;14;43;0
WireConnection;60;13;44;0
WireConnection;47;0;46;0
WireConnection;42;14;43;0
WireConnection;42;13;44;0
WireConnection;52;14;43;0
WireConnection;52;13;44;0
WireConnection;69;5;47;0
WireConnection;56;0;51;0
WireConnection;56;1;57;0
WireConnection;70;0;71;0
WireConnection;70;1;57;0
WireConnection;61;0;62;0
WireConnection;61;1;73;0
WireConnection;61;2;60;0
WireConnection;50;14;43;0
WireConnection;50;13;44;0
WireConnection;45;5;47;0
WireConnection;53;0;56;0
WireConnection;53;1;70;0
WireConnection;53;2;52;0
WireConnection;12;0;16;0
WireConnection;12;1;66;0
WireConnection;12;2;42;0
WireConnection;48;0;45;0
WireConnection;48;1;69;0
WireConnection;48;2;50;0
WireConnection;65;0;61;0
WireConnection;0;0;12;0
WireConnection;0;1;48;0
WireConnection;0;3;53;0
WireConnection;0;4;65;0
ASEEND*/
//CHKSM=DAC0FB2B414B1CE405D32A3E35E61A65A0B74CEB