using Godot;
using System;

public abstract partial class InterfaceComponent : Node
{ 
    [Export] protected CanvasLayer interfaceGroup;

    public void SetUIState(bool state)
    {
        interfaceGroup.Visible = state;
    }

    public virtual void FindNodes()
    {
        
    }
     
}
