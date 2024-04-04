@tool
extends EditorInspectorPlugin

func _can_handle(object):
	return object is CSharpScript

func _parse_begin(object):
	var button = Button.new()
	button.text = "Link to Visual Studio"
	button.pressed.connect(self.regenerate_launch_profile)
	add_custom_control(button)

func regenerate_launch_profile():
	var active_scene:Node = EditorInterface.get_edited_scene_root()
	var name = active_scene.name
	var path = active_scene.scene_file_path.replace("res://", "./")
	var base_profile = FileAccess.open("res://addons/vs22dp/launchSettingsBase.json", FileAccess.READ).get_as_text()
	var profile = base_profile.replace("$NAME$", name).replace("$PATH$", path)
	var file = FileAccess.open("res://Properties/launchSettings.json", FileAccess.WRITE)
	file.store_string(profile)
