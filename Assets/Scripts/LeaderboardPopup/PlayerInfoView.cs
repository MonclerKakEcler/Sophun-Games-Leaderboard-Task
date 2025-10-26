using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Leaderboard
{
    public interface IPlayerInfoView
    {
        void SetIcon(Sprite sprite);
        void SetNameText(string text);
        void SetScoreText(int score);
        void SetPlayerTypeText(string text);
        void ShowLoadingText(bool isActive);
        void SetColor(Color color);
        void SetScaleBackground(float scale);
    }

    public class PlayerInfoView  : MonoBehaviour, IPlayerInfoView
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _playerTypeText;
        [SerializeField] private TMP_Text _loadingText;

        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        public void SetNameText(string text)
        {
            _nameText.text = text;
        }

        public void SetScoreText(int score)
        {
            _scoreText.text = score.ToString();
        }

        public void SetPlayerTypeText(string text)
        {
            _playerTypeText.text = text;
        }

        public void ShowLoadingText(bool isActive)
        {
            _loadingText.gameObject.SetActive(isActive);
        }

        public void SetColor(Color color)
        {
            _background.color = color;
        }

        public void SetScaleBackground(float scale)
        {
            _background.transform.localScale = Vector3.one * scale;
        }
    }
}
