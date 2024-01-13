using Zenject;

public class SignalsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<PlayAudio>();
        Container.DeclareSignal<SwipeDetectionSignal>();
        Container.DeclareSignal<GameStateChanged>();
        Container.DeclareSignal<CoinsCollected>();
        Container.DeclareSignal<PlayerHitObstacle>();
    }
}
