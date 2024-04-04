@tool
extends EditorPlugin

var plugin

func _enter_tree():
	plugin = preload("res://addons/vs22dp/vs22dp_plugin.gd").new()
	add_inspector_plugin(plugin)

func _exit_tree():
	remove_inspector_plugin(plugin)
