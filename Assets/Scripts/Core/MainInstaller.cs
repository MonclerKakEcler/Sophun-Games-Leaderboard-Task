using Zenject;
using Leaderboard;
using SimplePopupManager;
using UnityEngine;

namespace Core
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private PlayerTypeStyleProvider _colorTypeProvider;
        public override void InstallBindings()
        {
            Container.Bind<IPlayerTypeStyleProvider>().FromInstance(_colorTypeProvider).AsSingle();

            Container.Bind<LeaderboardPopupController>().To<LeaderboardPopupController>().AsTransient();
            Container.Bind<ILeaderboardPopupMediator>().To<LeaderboardPopupMediator>().AsSingle();
            Container.Bind<IPopupManagerService>().To<PopupManagerServiceService>().AsSingle();
            Container.Bind<ILeaderboardDataProvider>().To<LeaderboardDataProvider>().AsSingle();
        }
    }
}
