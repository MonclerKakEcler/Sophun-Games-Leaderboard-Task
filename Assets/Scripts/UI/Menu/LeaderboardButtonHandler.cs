using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Leaderboard
{
    public class LeaderboardButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [Inject] private ILeaderboardPopupMediator _mediator;

        private void OnEnable()
        {
            _button.onClick.AddListener(OpenLeaderboardPopup);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OpenLeaderboardPopup);
        }

        private void OpenLeaderboardPopup()
        {
            _mediator.CreatePopup();
        }
    }
}