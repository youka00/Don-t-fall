using Godot;

public partial class AudioManager : Node
{
    [Export] private AudioStreamPlayer2D backgroundMusicPlayer;

    public override void _Ready()
    {
        if (backgroundMusicPlayer != null)
        {
            backgroundMusicPlayer.ProcessMode = Node.ProcessModeEnum.Always;
            PlayBackgroundMusic();
        }
    }

    private void PlayBackgroundMusic()
    {
        if (backgroundMusicPlayer != null && !backgroundMusicPlayer.Playing)
        {
            backgroundMusicPlayer.Play();
        }
    }

    // public void StopBackgroundMusic()
    // {
    //     if (backgroundMusicPlayer != null && backgroundMusicPlayer.Playing)
    //     {
    //         backgroundMusicPlayer.Stop();
    //     }
    // }

    public override void _Process(double delta)
    {
        if (backgroundMusicPlayer != null && !backgroundMusicPlayer.Playing)
        {
            backgroundMusicPlayer.Play();
        }
    }
}
