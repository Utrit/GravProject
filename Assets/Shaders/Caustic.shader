Shader "Custom/Caustic "
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        Cull off
        ZWrite on
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        half mod(half x, half y)
        {
            return x - y * floor(x/y);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            half TAU = 6.28318530718;
            int MAX_ITER = 5;
            half time = _Time.y * .5+23.0;
            fixed2 uv = IN.uv_MainTex.xy;

            fixed2 p = fixed2(mod(uv.x*TAU, TAU), mod(uv.y*TAU, TAU)) - 250.0;
        	fixed2 i = fixed2(p);
        	half c = 1.0;
        	half inten = .005;

        	for (int n = 0; n < MAX_ITER; n++)
        	{
        		half t = time * (1.0 - (3.5 / half(n+1)));
        		i = p + fixed2(cos(t - i.x) + sin(t + i.y), sin(t - i.y) + cos(t + i.x));
        		c += 1.0 / length(fixed2(p.x / (sin(i.x + t) / inten), p.y / (cos(i.y + t) / inten)));
        	}

        	c /= half(MAX_ITER);
        	c = 1.17 - pow(c, 1.4);

            half color = pow(abs(c), 8.0);
        	fixed3 colour = fixed3(color, color, color);
            colour = clamp(colour + fixed3(0.0, 0.35, 0.5), 0.0, 1.0);

        	o.Albedo = colour;
        	o.Alpha = pow((colour.x + colour.y + colour.z) / 3.0, 2);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
