extends Node


@export var mob_scene: PackedScene


var _score: int
var light_strength: float = 0.5


# Called when the node enters the scene tree for the first time.
func _ready():
	new_game()


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	print(light_strength)
	if Input.is_action_just_pressed("inventory"):
		$InventoryHud.visible = not $InventoryHud.visible
	
	$HUD.update_mouse_view_pos(get_viewport().get_mouse_position())
	$HUD.update_player_pos($Player.global_position)
	$HUD.update_camera_pos($Player/Camera2D.global_position)
	# hud.update_player_pos(get_node("Player/Camera2D").get_target_position())

	inform_player_location(get_node("Player").position)


func game_over():
	print("You died")


func new_game():
	_score = 0
	
	var start_position = get_node("StartPosition")

	set_player_spawn()
	$Player.start(start_position.position)

	$Wendigo/HuntTimer.start()
	$Wendigo.search($Player.global_position)


func on_light_hit(monster: Node2D):
	monster.call("on_hit_by_light")


func inform_player_location(player_pos: Vector2):
	get_node("Wendigo").search(player_pos)


func _on_inventory_hud_light_strength(strength: float):
	light_strength = strength
	$Player/Lantern.light_strength = light_strength


func set_player_spawn():
	var area = get_map_size()
	var random = RandomNumberGenerator.new()
	var x = random.randi_range(0, int(area.x))
	var y = random.randi_range(0, int(area.y))

	$StartPosition.position = Vector2(x, y)


func get_map_size() -> Vector2:
	var area = $Map/PlainGrassLayer.rendering_quadrant_size
	return Vector2(sqrt(area), sqrt(area))
