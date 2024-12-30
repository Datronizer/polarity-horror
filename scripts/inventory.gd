@tool
extends Node2D

## docstring
# This class handles the inventory system and light strength based on crank rotations.

## signals
signal light_strength(strength)

## enums
# No enums in this script

## constants
# No constants in this script

## @export variables
@export var rotations: float = 0
@export var light_strength_diminish: float = 0.0015

## public variables
# No public variables in this script

## private variables
var has_mouse: bool = false
var is_clicking: bool = false
var max_rotations: float = 15

## @onready variables
# No @onready variables in this script

## optional built-in virtual _init method
# No _init method in this script

## optional built-in virtual _enter_tree() method
# No _enter_tree method in this script

## built-in virtual _ready method
func _ready():
	pass

## remaining built-in virtual methods
func _process(_delta):
	var brightness_threshold: float = 5.0
	var brightness_cap: float = 0.95

	if rotations > brightness_threshold:
		$Light.energy = brightness_cap
	else:
		$Light.energy = rotations / (brightness_threshold / brightness_cap)

	if has_mouse and is_clicking:
		var initial_rotation = $Area2D.rotation
		$Area2D.look_at(get_global_mouse_position())
		var new_rotation = $Area2D.rotation

		rotations += abs(new_rotation - initial_rotation)

	if rotations > max_rotations:
		rotations = max_rotations
	elif rotations < 0.05:
		rotations = 0.05

	rotations -= light_strength_diminish
	light_strength.emit(rotations)

func _input(event):
	if event is InputEventMouseButton:
		if event.button_index == MOUSE_BUTTON_LEFT and event.pressed:
			is_clicking = true
		elif event.button_index == MOUSE_BUTTON_LEFT and not event.pressed:
			is_clicking = false

## public methods
# No additional public methods in this script

## private methods
func _on_area2d_mouse_entered():
	has_mouse = true
	print("Mouse entered")

func _on_area2d_mouse_exited():
	has_mouse = false
	print("Mouse exited")

## subclasses
# No subclasses in this script
