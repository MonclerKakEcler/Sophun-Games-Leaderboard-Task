using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;
using UnityEngine;

namespace Leaderboard
{
    public interface IPlayerTypeStyleProvider
    {
        Color GetColor(PlayerType type);
        float GetScale(PlayerType type);
    }

    [CreateAssetMenu(fileName = "PlayerTypeStyles", menuName = "Leaderboard/PlayerTypeStyles")]
    public class PlayerTypeStyleProvider : ScriptableObject, IPlayerTypeStyleProvider
    {
        [SerializeField] private PlayerTypeStyleMapping[] _playerTypeMappings;

        public Color GetColor(PlayerType type)
        {
            var mapping = _playerTypeMappings.FirstOrDefault(x => x.Type == type);
            var color = mapping.Color;

            return color;
        }

        public float GetScale(PlayerType type)
        {
            var mapping = _playerTypeMappings.FirstOrDefault(x => x.Type == type);
            var scale = mapping.Scale;
            return scale;
        }
    }

    [System.Serializable]
    public class PlayerTypeStyleMapping
    {
        public PlayerType Type;
        public Color Color;
        [Range(0.5f, 2f)]
        public float Scale;
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum PlayerType
    {
        Default = 0,
        Bronze = 1,
        Silver = 2,
        Gold = 3,
        Diamond = 4,
    }
}