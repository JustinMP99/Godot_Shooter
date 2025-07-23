shader_type spatial;

uniform vec3 custom_color = vec3(0.0);
uniform bool show_glow = false;

float fresnel(float amount, vec3 normal, vec3 view)
{
	return pow((1.0 - clamp(dot(normalize(normal), normalize(view)), 0.0, 1.0)), amount);
}

vec3 fresnel_glow(float amount, float intensity, vec3 color, vec3 normal, vec3 view)
{
	return pow((1.0 - dot(normalize(normal), normalize(view))), amount) * color * intensity;
}

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	// Called for every pixel the material is visible on.
	vec3 base_color = vec3(0.0);
	vec3 fresnel_color = vec3(0.0, 0.7, 0.9);
	if(show_glow)
	{
		vec3 fresnel = fresnel_glow(4.0, 4.5, custom_color, NORMAL, VIEW);
		ALBEDO = base_color + fresnel;
		EMISSION = base_color + fresnel;
	}
	else
	{
		float basic_fresnel = fresnel(3.0, NORMAL, VIEW);
		ALBEDO = base_color + basic_fresnel;
	}
	
}

//void light() {
//	// Called for every pixel for every light affecting the material.
//	// Uncomment to replace the default light processing function with this one.
//}
