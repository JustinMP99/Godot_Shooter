using Godot;
using System;

public partial class Options_IC : InterfaceComponent
{
    
    private Label cheatDescriptionLabel;
    private LineEdit cheatIF;
    private Slider masterVolumeSlider;
    private Slider sfxVolumeSlider;
    private Slider musicVolumeSlider;
    private Panel deleteSavePanel;
    public Button YesDeleteButton;
    
    public override void FindNodes()
    {
        cheatDescriptionLabel = interfaceGroup.GetNode<Label>("Cheat Description Label");
        cheatIF = interfaceGroup.GetNode<LineEdit>("Settings Panel/Cheat Box/Cheat Line Edit");
        masterVolumeSlider = interfaceGroup.GetNode<Slider>("Settings Panel/Core Settings/Master Slider");
        sfxVolumeSlider = interfaceGroup.GetNode<Slider>("Settings Panel/Core Settings/SFX Slider");
        musicVolumeSlider = interfaceGroup.GetNode<Slider>("Settings Panel/Core Settings/Music Slider");
        deleteSavePanel = interfaceGroup.GetNode<Panel>("Delete Save Panel");
        YesDeleteButton = interfaceGroup.GetNode<Button>("Delete Save Panel/Yes Delete Button");
    }

    public void SetDeleteSavePanelState(bool state)
    {
        deleteSavePanel.Visible = state;
    }

    public void SetMasterSliderValue(float value)
    {
        masterVolumeSlider.Value = value;
    }

    public void SetSFXSliderValue(float value)
    {
        sfxVolumeSlider.Value = value;
    }

    public void SetMusicSliderValue(float value)
    {
        musicVolumeSlider.Value = value;
    }

    public void SetCheatDescriptionLabelText(string description)
    {
        cheatDescriptionLabel.Text = description;
    }

    public string GetCheatIFString()
    {
        return cheatIF.Text;
    }
    
}
