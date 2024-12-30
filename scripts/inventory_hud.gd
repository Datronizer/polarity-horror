extends CanvasLayer

signal light_strength(strength)

# Called when the node enters the scene tree for the first time.
func _ready():
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	pass

func _on_inventory_light_strength(strength: Variant) -> void:
	light_strength.emit(strength)
