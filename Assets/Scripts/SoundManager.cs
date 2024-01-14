using UnityEngine;

public class SoundManager : BaseView
{
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        SignalService?.Subscribe<PlayAudio>(PlaySound);
        SignalService?.Subscribe<StopAudio>(StopAudio);
        SignalService?.Subscribe<PlayAudioInLoop>(PlayInLoop);

    }

    private void PlaySound(PlayAudio val)
    {
        var audioClip = GameInfoSO.GetAudioClip(val.audioClip);
       audioSource.PlayOneShot(audioClip);
    }

    private void PlayInLoop(PlayAudioInLoop val)
    {
        var audioClip = GameInfoSO.GetAudioClip(val.audioClip);
       audioSource.PlayOneShot(audioClip);
    }

    private void StopAudio(StopAudio val)
    {
       audioSource.Stop();
    }

}
