using Zenject;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.Bind<GameModel>().AsSingle().NonLazy();
    }
}