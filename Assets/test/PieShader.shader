Shader "PieShader/PieShader" 
{
	Properties{
		_Color("Color", Color)	 = (1.0,1.0,1.0,1.0)
		_SubDivisions("SubDivision",Int) = 1
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
				//pragmas
				//we tell unity where to look for the vertex function and the fragment function
				#pragma vertex vert
				#pragma fragment frag 

				//user defined variables
				 uniform float4 _Color;

				//base input structs
				struct vertexInput{
					float4 vertex: POSITION;
					
				};
				struct vertexOutput{
					//take the objects input position and take the space that unity can understand
					float4 pos : SV_POSITION; 
				};
				//vertex functions
				vertexOutput vert(vertexInput v)
				{
					//we have the vertex output we see and this is what we write to 
					//from the vertexInput, we assign it to v
					vertexOutput o;
					//this is where we process everything with regards to the vertices
					o.pos = mul(UNITY_MATRIX_MVP,v.vertex); // the matrix mvp 
					return o;
				}
				
				//fragment function
				
				float4 frag(vertexOutput i): COLOR
				{
					return _Color;
				}
			ENDCG
		}
	}
	//fallback, comment out during development
	Fallback "Diffuse"
}