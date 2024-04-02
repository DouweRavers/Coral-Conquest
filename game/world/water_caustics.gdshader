shader_type spatial;

uniform sampler2D noise_sample;
uniform float size_scale;
uniform float time_scale;
varying vec3 world_position;

uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_linear_mipmap;

void vertex() {
	world_position = (MODEL_MATRIX * vec4(VERTEX, 1.0)).xyz;
}

void fragment() {
	float color = texture(noise_sample, world_position.xz * size_scale + TIME * time_scale).r;
	color = min(0.999, color);
	color = log(0.999) / log(color);
	color *= 50.0f;
	EMISSION = vec3(color);
	ALPHA = color;
}