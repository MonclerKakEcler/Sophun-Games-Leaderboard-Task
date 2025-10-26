using SimplePopupManager;

namespace Leaderboard
{
    public interface ILeaderboardPopupMediator
    {
        void CreatePopup();
        void ClosePopup();
    }

    public class LeaderboardPopupMediator : ILeaderboardPopupMediator
    {
        private const string kViewAddressable = "LeaderboardPopup";

        private readonly IPopupManagerService _popupManagerService;
        private readonly LeaderboardPopupController _controller;

        public LeaderboardPopupMediator(IPopupManagerService popupManagerService
            , LeaderboardPopupController controller)
        {
            _popupManagerService = popupManagerService;
            _controller = controller;
        }

        public void CreatePopup()
        {
            _popupManagerService.OpenPopup(kViewAddressable, _controller);
            _controller.OnClose += Release;
        }

        public void ClosePopup()
        {
            _controller.ClosePopup();
        }

        private void Release()
        {
            _controller.OnClose -= Release;
            _popupManagerService.ClosePopup(kViewAddressable);
        }
    }
}