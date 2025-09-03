shader_type spatial;

uniform vec3 baseColor = vec3(0.0f);
uniform vec3 customColor = vec3(0.0f);
uniform float fresnelAmplifier = 0.0f;
uniform float transparency : hint_range(0.0, 1.0, 0.01);

float fresnel(float amount, vec3 normal, vec3 view)
{
	vec3 normalizedNormal = normalize(normal);
	vec3 normalizedView = normalize(view);
	float dotResult = dot(normalizedNormal, normalizedView);
	float clampedResult = clamp(dotResult, 0.1f, 1.0f);
	float powResult = pow(1.0f - clampedResult, amount);
	return powResult;
		
	//return pow((1.0 - clamp(dot(normalize(normal), normalize(view)), 0.0, 1.0)), amount);
}

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	// Called for every pixel the material is visible on.
	float basic_fresnel = fresnel(3.0, NORMAL, VIEW);
	ALBEDO = baseColor + basic_fresnel * fresnelAmplifier;
	ALPHA = transparency;
}


