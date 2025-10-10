using Godot;
using System;

public partial class Pause_IC : InterfaceComponent
{
    
    public Button ResumeButton;

    public override void FindNodes()
    {
        ResumeButton = interfaceGroup.GetNode<Button>("CenterContainer/PanelContainer/VBoxContainer/Resume Button");
    }
}
