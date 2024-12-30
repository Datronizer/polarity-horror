extends Node2D

signal light_hit(monster)
signal light_aim()

var is_aiming = false
var light_strength = 0.0

var body_inside

var is_body_inside = false

func _ready():
	$Area2D/LightConeCollision.disabled = true

	$SpotLight.visible = false
	$AreaLight.visible = true

func _process(_delta):
	var brightness_threshold = 5.0
	var brightness_cap = 0.95

	if light_strength > brightness_threshold:
		$SpotLight.energy = brightness_cap
		$AreaLight.energy = brightness_cap
	else:
		$SpotLight.energy = light_strength / (brightness_threshold / brightness_cap)
		$AreaLight.energy = light_strength / (brightness_threshold / brightness_cap)

	if is_body_inside:
		light_hit.emit(body_inside)

	if not is_aiming:
		$Area2D/LightConeCollision.disabled = true

		$SpotLight.visible = false
		$AreaLight.visible = true

		$FlashlightSprite.visible = false
		$LanternSprite.visible = true

		body_inside = null
		is_body_inside = false

	is_aiming = false

func _on_player_aim(_mouse_pos: Variant) -> void:
	print("Player is aiming")

	$Area2D/LightConeCollision.disabled = false

	$SpotLight.visible = true
	$AreaLight.visible = false

	$FlashlightSprite.visible = true
	$LanternSprite.visible = false

	is_aiming = true

	light_aim.emit()

func _on_Area2D_body_entered(body):
	if body.is_in_group("Monster"):
		body_inside = body
		is_body_inside = true
		print("Light hit monster: ", body.name)

func _on_Area2D_body_exited(body):
	if body.is_in_group("Monster"):
		body_inside = null
		is_body_inside = false
		print("Monster left light: ", body.name)
