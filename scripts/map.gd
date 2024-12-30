extends Node2D

@export var noise_texture: NoiseTexture2D
@export var tree_noise_texture: NoiseTexture2D
@export var plain_grass_layer_index: int = -1
@export var grass_brush_layer_index: int = 0
@export var tree_layer_index: int = 1

var noise: Noise
var tree_noise: Noise
var plain_grass_layer: TileMapLayer
var plain_grass_layer2: TileMapLayer
var grass_brush_layer: TileMapLayer
var tree_layer: TileMapLayer

var width: int = 64
var height: int = 64

var plain_grass_atlas: Vector2 = Vector2(7, 0)
var grass_atlas_arr: Array = [
	Vector2(0, 5), Vector2(0, 6), Vector2(1, 4), Vector2(3, 2),
	Vector2(4, 5), Vector2(4, 7), Vector2(6, 0), Vector2(6, 3),
	Vector2(7, 0), Vector2(7, 1), Vector2(8, 4), Vector2(8, 7),
	Vector2(9, 3), Vector2(14, 3), Vector2(14, 7)
]
var bush_atlas1: Vector2 = Vector2(0, 0)
var bush_atlas2: Vector2 = Vector2(0, 0)
var tree_atlas1: Vector2 = Vector2(0, 1)
var tree_atlas2: Vector2 = Vector2(1, 1)
var tree_atlas3: Vector2 = Vector2(0, 0)
var tree_atlas4: Vector2 = Vector2(0, 0)

var source_id: int = 1
var bush_atlas1_id: int = 2
var bush_atlas2_id: int = 4
var tree_atlas1_id: int = 3
var tree_atlas2_id: int = 5
var tree_atlas3_id: int = 6
var tree_atlas4_id: int = 7

func _ready():
	noise = noise_texture.noise
	tree_noise = tree_noise_texture.noise

	plain_grass_layer = $PlainGrassLayer
	plain_grass_layer2 = $PlainGrassLayer2
	grass_brush_layer = $GrassBrushLayer
	tree_layer = $TreeLayer

	generate_map()

func _process(_delta):
	pass

func generate_map():
	var random = RandomNumberGenerator.new()

	# var min_value = 0.0
	# var max_value = 0.0

	for x in range(-width * 1.0 / 2, width * 1.0 / 2):
		for y in range(-height * 1.0 / 2, height * 1.0 / 2):
			var value = noise.get_noise_2d(x, y)
			var tree_value = tree_noise.get_noise_2d(x, y)

			# if value < min_value:
			# 	min_value = value
			# if value > max_value:
			# 	max_value = value

			if value >= 0.2:
				if value >= 0.14:
					if value >= 0.17:
						if tree_value >= 0.8:
							if random.randf() > 0.5:
								tree_layer.set_cell(Vector2(x, y), tree_atlas4_id, tree_atlas4)
							else:
								tree_layer.set_cell(Vector2(x, y), tree_atlas3_id, tree_atlas3)

					if tree_value >= 0.55 and tree_value < 0.65:
						grass_brush_layer.set_cell(Vector2(x, y), bush_atlas1_id, bush_atlas1)

				if tree_value >= 0.4 and tree_value < 0.5:
					grass_brush_layer.set_cell(Vector2(x, y), bush_atlas2_id, bush_atlas2)

				plain_grass_layer2.set_cell(Vector2(x, y), source_id, grass_atlas_arr[randi() % grass_atlas_arr.size()])
			else:
				plain_grass_layer.set_cell(Vector2(x, y), source_id, plain_grass_atlas)

	# print("Min tree value: %s" % min_value)
	# print("Max tree value: %s" % max_value)
