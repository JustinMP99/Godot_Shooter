using Godot;
using System;

public partial class AnimationControllerComponent : Node
{
    [ExportCategory("Animators")]
    [Export] private AnimationTree shipAnimationTree;
    [Export] private AnimationPlayer shipAnimationPlayer;
    [Export] private AnimationTree reticleAnimationTree;
    [Export] private AnimationPlayer reticleAnimationPlayer;
    
    public void SetShipFlyInTrue()
    {
        shipAnimationTree.Set("parameters/conditions/Fly In", true);
    }

    public void SetShipFlyInFalse()
    {
        shipAnimationTree.Set("parameters/conditions/Fly In", false);
    }

    public void SetReticleFlyInTrue()
    {
        reticleAnimationTree.Set("parameters/conditions/Fly In", true);
    }

    public void SetReticleFlyInFalse()
    {
        reticleAnimationTree.Set("parameters/conditions/Fly In", false);
    }

    public void SetReticleLockOnTrue()
    {
        reticleAnimationTree.Set("parameters/conditions/Lock On", true);
    }

    public void SetReticleLockOnFalse()
    {
        reticleAnimationTree.Set("parameters/conditions/Lock On", false);
    }

    public void SetReticleReturnToIdleTrue()
    {
        reticleAnimationTree.Set("parameters/conditions/Return To Idle", true);
    }

    public void SetReticleReturnToIdleFalse()
    {
        reticleAnimationTree.Set("parameters/conditions/Return To Idle", false);
    }
    
}
