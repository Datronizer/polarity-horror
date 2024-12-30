@tool
class_name RootMonster
extends RigidBody2D

## docstring
# RootMonster is a class that represents a monster which moves towards the player.

# signals

# enums

# constants

# @export variables

# public variables
var speed = 100 # How fast the monster will move (pixels/sec).
var detection_radius = 100 # Units pixels.
var kill_radius = 25 # Units pixels.

# private variables

# @onready variables

# optional built-in virtual _init method

# optional built-in virtual _enter_tree() method

# built-in virtual _ready method
func _ready():
	pass

# remaining built-in virtual methods

# public methods
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
