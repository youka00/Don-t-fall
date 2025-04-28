using Godot;
using System;

public partial class MainMenu : Control
{
	[Export] public TextureButton StartButton;
	[Export] public TextureButton ExitButton;
	[Export] public TextureRect Background;

	public override void _Ready()
	{
		StartButton.Pressed += OnStartButtonPressed;
		ExitButton.Pressed += OnExitButtonPressed;
	}

	private void OnStartButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Main.tscn");
	}

	private void OnExitButtonPressed()
	{
		GetTree().Quit();
	}
}
