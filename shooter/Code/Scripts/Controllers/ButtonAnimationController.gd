class_name ButtonAnimationController extends Node

@export var from_center :  bool = true;
@export var hover_scale : Vector2 = Vector2(1,1)
@export var time : float = 0.1
@export var transition_type : Tween.TransitionType

var target : Control
var default_scale : Vector2

func _ready() -> void:
	target = get_parent()
	ConnectSignals()
	call_deferred("Setup")

func ConnectSignals() -> void:
	target.mouse_entered.connect(OnHover)
	target.mouse_exited.connect(OnUnhover)

func Setup() -> void:
	if from_center:
		target.pivot_offset = target.size / 2
	default_scale = target.scale

func OnHover() -> void:
	AddTween("scale", hover_scale, time)

func OnUnhover() -> void:
	AddTween("scale", default_scale, time)

func AddTween(property: String, value, seconds: float) -> void:
	if get_tree() != null:
		var tween = get_tree().create_tween()
		tween.tween_property(target, property, value, seconds).set_trans(transition_type)
