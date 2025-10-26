using SimplePopupManager;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Leaderboard
{
    public interface ILeaderboardPopupModel
    {
        Task Init(ILeaderboardPopupView view);
    }

    public interface ILeaderboardPopupView
    {
        void InitInputHandler(ILeaderboardInputHandler inputHandler);
        IPlayerInfoView GetPlayerInfoItem();
        void SubscribeButtons();
        void UnsubscribeButtons();
        void Release();
    }

    public class LeaderboardPopupView : MonoBehaviour, IPopupInitialization, ILeaderboardPopupView
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Transform _container;
        [SerializeField] private PlayerInfoView _playerInfoItem;

        private ILeaderboardInputHandler _inputHandler;

        public async Task Init(object param)
        {
            if (param is not ILeaderboardPopupModel model)
            {
                return;
            }

            await model.Init(this);
        }

        public void InitInputHandler(ILeaderboardInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        public IPlayerInfoView GetPlayerInfoItem()
        {
            var prefab = Instantiate(_playerInfoItem, _container);
            return prefab;
        }

        public void SubscribeButtons()
        {
            _closeButton.onClick.AddListener(OnCloseClick);
        }

        public void UnsubscribeButtons()
        {
            _closeButton.onClick.RemoveListener(OnCloseClick);
        }

        public void Release()
        {
            Destroy(this);
        }

        private void OnCloseClick()
        {
            _inputHandler.ClosePopup();
        }
    }
}