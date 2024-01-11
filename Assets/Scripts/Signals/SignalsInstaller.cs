using Zenject;

public class SignalsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<GameStarted>();
        Container.DeclareSignal<PlayAudio>();
    }

}
