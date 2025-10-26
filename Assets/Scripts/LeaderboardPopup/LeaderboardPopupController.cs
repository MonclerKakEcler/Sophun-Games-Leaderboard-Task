using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using System;

namespace Leaderboard
{
    public interface ILeaderboardInputHandler
    {
        void ClosePopup();
    }

    public class LeaderboardPopupController : ILeaderboardInputHandler, ILeaderboardPopupModel
    {
        private readonly ILeaderboardDataProvider _dataProvider;
        private readonly IPlayerTypeStyleProvider _styleProvider;

        private ILeaderboardPopupView _view;
        private List<IPlayerInfoView> _playerInfoItems;
        private readonly Dictionary<string, Sprite> _avatarCache = new();

        public event Action OnClose;

        public LeaderboardPopupController(ILeaderboardDataProvider dataProvider, IPlayerTypeStyleProvider styleProvider)
        {
            _dataProvider = dataProvider;
            _styleProvider = styleProvider;
        }

        public async Task Init(ILeaderboardPopupView view)
        {
            _view = view;
            var model = await BuildModel();
            _playerInfoItems = new();

            foreach (var player in model.Players)
            {
                var item = view.GetPlayerInfoItem();
                item.SetNameText(player.Name);
                item.SetScoreText(player.Score);

                var type = player.Type;
                item.SetPlayerTypeText(type.ToString());

                var color = _styleProvider.GetColor(type);
                item.SetColor(color);

                var scale = _styleProvider.GetScale(type);
                item.SetScaleBackground(scale);

                _ = LoadAndSetAvatar(player.Avatar, item);
                 
                _playerInfoItems.Add(item);
            }

            view.InitInputHandler(this);
            view.SubscribeButtons();
        }

        public void ClosePopup()
        {
            _playerInfoItems.Clear();
            _view.UnsubscribeButtons();
            _view.Release();

            OnClose?.Invoke();
        }

        private async Task<LeaderboardPopupModel> BuildModel()
        {
            var model = new LeaderboardPopupModel();
            var data = _dataProvider.LoadData();
            model.Players = data;
            return model;
        }

        private async Task LoadAndSetAvatar(string url, IPlayerInfoView item)
        {
            item.ShowLoadingText(true);
            var sprite = await LoadAvatarAsync(url);

            item.SetIcon(sprite);
            item.ShowLoadingText(false);
        }

        private async Task<Sprite> LoadAvatarAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            if (_avatarCache.TryGetValue(url, out var cachedSprite))
            {
                return cachedSprite;
            }

            using UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            var op = www.SendWebRequest();
            while (!op.isDone)
            {
                await Task.Yield();
            }

            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

                _avatarCache[url] = sprite;
                return sprite;
            }
            else
            {
                Debug.LogWarning($"Failed to load avatar: {url} ({www.error})");
                return null;
            }
        }
    }
}
