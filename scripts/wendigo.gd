@tool
extends RootMonster


## docstring
# This script controls the behavior of the Wendigo enemy, including hunting, stalking, and fleeing from the player.


# signals


# enums


# constants


# @export variables


# public variables
var flee_speed = 250


# private variables
var _player_global_pos = Vector2()
var _delta = 0.0


# @onready variables


# optional built-in virtual _init method


# optional built-in virtual _enter_tree() method


# built-in virtual _ready method
func _ready():
	speed = 50
	detection_radius = 500
	kill_radius = 100


# remaining built-in virtual methods
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	_delta = delta
	hunt(_player_global_pos)


# public methods
func _on_body_entered(body):
	if body.name == "Player":
		print("Wendigo collided with Player")


func _on_hunt_timer_timeout():
	# print("Wendigo hunting Player at last known location: ", _player_global_pos)
	hunt(_player_global_pos)


func hunt(player_pos):
	if global_position.distance_to(player_pos) < detection_radius:
		stalk(player_pos)
	else:
		approach_player(player_pos, _delta, 50)


func stalk(player_pos):
	if global_position.distance_to(player_pos) < kill_radius:
		approach_player(player_pos, _delta, 999)
	if global_position.distance_to(player_pos) < 250:
		position += Vector2.ZERO


func flee(player_pos):
	if global_position.distance_to(player_pos) < 200:
		approach_player(player_pos, _delta, -flee_speed)


# The wendigo always knows where the player is.
func search(player_pos = null):
	_player_global_pos = player_pos if player_pos != null else Vector2.ZERO


func on_hit_by_light():
	print("Wendigo hit by light")
	flee(_player_global_pos)


func approach_player(player_pos, delta, approach_speed = null):
	var screen_size = get_viewport_rect().size
	var velocity = player_pos - global_position
	velocity = velocity.normalized() * (approach_speed if approach_speed != null else speed)
	
	position += velocity * delta
	position.x = clamp(position.x, 0, screen_size.x)
	position.y = clamp(position.y, 0, screen_size.y)

	var animated_sprite_2d = $AnimatedSprite2D
	if velocity.length() > 0:
		velocity = velocity.normalized() * speed
		animated_sprite_2d.play()
		animated_sprite_2d.animation = "walk"
		animated_sprite_2d.flip_h = velocity.x < 0
	else:
		animated_sprite_2d.animation = "idle"
		animated_sprite_2d.play()


# private methods


# subclasses
