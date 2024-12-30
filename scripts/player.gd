extends Area2D

signal hit
signal light_hit(monster_hit)
signal aim(mouse_pos)

@export var speed = 100
var screen_size
var is_aiming = false
var last_turn_direction = false
var is_turn_direction_changed = false

func _ready():
	screen_size = get_viewport_rect().size
	hide()

func _process(delta):
	$Lantern.rotation = 0

	var velocity = Vector2.ZERO
	if is_aiming:
		compute_camera_position()
		rotate_arm()
		speed = 30
	else:
		$Camera2D.global_position = global_position
		speed = 100

	# Player Inputs
	if Input.is_action_pressed("move_right"):
		velocity.x += 1

	if Input.is_action_pressed("move_left"):
		velocity.x -= 1

	if Input.is_action_pressed("move_down"):
		velocity.y += 1

	if Input.is_action_pressed("move_up"):
		velocity.y -= 1

	if Input.is_action_pressed("aim"):
		is_aiming = true
		aim.emit(get_viewport().get_mouse_position())
	else:
		is_aiming = false

	# Animation
	var animated_sprite_2d = $AnimatedSprite2D

	if velocity.length() > 0:
		velocity = velocity.normalized() * speed
		animated_sprite_2d.play()
		animated_sprite_2d.animation = "walk_3"
		animated_sprite_2d.flip_h = get_player_turn_direction(velocity)
	else:
		animated_sprite_2d.animation = "idle"
		animated_sprite_2d.play()
		animated_sprite_2d.flip_h = get_player_turn_direction(velocity)

	if !animated_sprite_2d.flip_h != ($Lantern.position.x > 0):
		$Lantern.position.x = -$Lantern.position.x
	if last_turn_direction != get_player_turn_direction(velocity):
		$Lantern.position.x = -$Lantern.position.x

	position += velocity * delta
	position.x = clamp(position.x, 0, screen_size.x)
	position.y = clamp(position.y, 0, screen_size.y)
	last_turn_direction = get_player_turn_direction(velocity)


func start(start_position) -> void:
	self.position = start_position
	show()
	$CollisionShape2D.disabled = false

func _on_body_entered(_body: Node2D) -> void:
	hide()
	hit.emit()
	$CollisionShape2D.set_deferred("disabled", true)

func get_player_turn_direction(velocity):
	var mouse_pos = get_global_mouse_position()
	var player_global_pos = global_position
	var middle_point = (mouse_pos + player_global_pos) / 2
	var direction = middle_point.x - player_global_pos.x

	if is_aiming:
		return direction < 0
	else:
		return velocity.x < 0

func compute_camera_position():
	var mouse_pos = get_viewport().get_mouse_position()
	if mouse_pos.x < 0:
		mouse_pos.x = 0
	elif mouse_pos.x > screen_size.x:
		mouse_pos.x = screen_size.x

	if mouse_pos.y < 0:
		mouse_pos.y = 0
	elif mouse_pos.y > screen_size.y:
		mouse_pos.y = screen_size.y

	var player_global_pos = global_position
	var middle_point = (mouse_pos + player_global_pos) / 2

	$Camera2D.global_position = middle_point

func rotate_arm():
	var direction = Vector2(cos(PI / 2), sin(PI / 2)).normalized()
	$Lantern.look_at(get_global_mouse_position() - direction)

func on_light_hit(monster_hit):
	light_hit.emit(monster_hit)
