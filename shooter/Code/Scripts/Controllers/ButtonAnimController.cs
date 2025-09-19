using Godot;
using System;

public partial class ButtonAnimController : Node
{

    [Export] private bool fromCenter;
    [Export] private Vector2 hoverScale = new Vector2(1, 1);
    [Export] private float time = 0.1f;
    [Export] private Tween.TransitionType transitionType;
    
    private Control target;
    private Vector2 defaultScale;


    public override void _Ready()
    {

        target = GetParent() as Control;
        ConnectSignals();
        CallDeferred("Setup");

    }

    private void Setup()
    {
        if (fromCenter)
        {
            target.PivotOffset = target.Size / 2;
        }

        defaultScale = target.Scale;
    }
    
    private void ConnectSignals()
    {
        target.MouseEntered += OnMouseEnter;
        target.MouseExited += OnMouseExit;
    }

    private void OnMouseEnter()
    {
        AddTween("scale", hoverScale, time);
    }

    private void OnMouseExit()
    {
        AddTween("scale", defaultScale, time);
    }

    private void AddTween(String property, Vector2 value, float seconds)
    {
        if (GetTree() != null)
        {
            Tween tween = GetTree().CreateTween();
            tween.TweenProperty(target, property, value, seconds).SetTrans(transitionType);
        }
    }
    
}
