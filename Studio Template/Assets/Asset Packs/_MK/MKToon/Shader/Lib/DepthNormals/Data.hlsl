//////////////////////////////////////////////////////
// MK Toon Depth Normals Data				       	//
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de                            //
// Copyright © 2021 All rights reserved.            //
//////////////////////////////////////////////////////

#ifndef MK_TOON_DEPTH_NORMALS_IO
	#define MK_TOON_DEPTH_NORMALS_IO

	#include "../Core.hlsl"
	
	/////////////////////////////////////////////////////////////////////////////////////////////
	// INPUT
	/////////////////////////////////////////////////////////////////////////////////////////////
	struct VertexInputDepthNormals
	{
		float4 vertex : POSITION;
		#if defined(MK_VERTEX_ANIMATION_PULSE) || defined(MK_VERTEX_ANIMATION_NOISE) || defined(MK_PARALLAX) || defined(MK_NORMAL)
			half3 normal : NORMAL;
		#endif
		#ifdef MK_TCM
			float2 texcoord0 : TEXCOORD0;
		#endif
		#if defined(MK_PARALLAX) || defined(MK_TBN)
			half4 tangent : TANGENT;
		#endif

		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	/////////////////////////////////////////////////////////////////////////////////////////////
	// OUTPUT
	/////////////////////////////////////////////////////////////////////////////////////////////
	struct VertexOutputDepthNormals
	{
		float4 svPositionClip : SV_POSITION;
		#ifdef MK_TCM
			float2 uv : TEXCOORD0;
		#endif

		#if defined(MK_PARALLAX)
			half3 viewTangent : TEXCOORD1;
		#endif

		#ifdef MK_LIT
			#ifdef MK_NORMAL
				half3 normalWorld : TEXCOORD2;
			#endif
			#if defined(MK_TBN)
				half3 tangentWorld : TEXCOORD3;
				half3 bitangentWorld : TEXCOORD4;
			#endif
		#endif

		#ifdef MK_POS_CLIP
			float4 positionClip : TEXCOORD5;
		#endif
		#ifdef MK_POS_NULL_CLIP
			float4 nullClip : TEXCOORD6;
		#endif

		UNITY_VERTEX_INPUT_INSTANCE_ID
		UNITY_VERTEX_OUTPUT_STEREO
	};
#endif