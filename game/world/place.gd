@tool
extends Node

func _ready() -> void:
	var tile:PackedScene = load("res://game/world/floor_tile.glb")
	for x in range(-5, 5):
		for y in range(-5, 5):
			var tile_instance:Node3D = tile.instantiate()
			add_child(tile_instance)
			tile_instance.global_position.x = x * 50
			tile_instance.global_position.z = y * 50
