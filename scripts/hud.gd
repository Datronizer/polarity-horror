extends CanvasLayer

signal start_game

# Called when the node enters the scene tree for the first time.
func _ready():
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	pass

func update_player_pos(pos):
	$PlayerPos.text = "Player: %s" % str(pos)

func update_camera_pos(pos):
	$CameraPos.text = "Camera: %s" % str(pos)

func update_mouse_view_pos(pos):
	$MouseViewPos.text = "Mouse: %s" % str(pos)
