using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Leaderboard
{
    public interface ILeaderboardDataProvider
    {
        List<LeaderboardPlayer> LoadData();
    }

    public class LeaderboardDataProvider : ILeaderboardDataProvider
    {
        private const string kLeaderboardPath = "Leaderboard";

        public List<LeaderboardPlayer> LoadData()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>(kLeaderboardPath);
            if (jsonFile == null)
            {
                Debug.LogError($"Leaderboard JSON '{kLeaderboardPath}' not found!");
                return null;
            }

            try
            {
                var holder = JsonConvert.DeserializeObject<LeaderboardData>(jsonFile.text);
                return holder.Leaderboard;
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error parsing leaderboard JSON: {ex.Message}");
                return null;
            }
        }
    }

    [System.Serializable]
    public class LeaderboardData
    {
        public List<LeaderboardPlayer> Leaderboard;
    }

    [System.Serializable]
    public class LeaderboardPlayer
    {
        public string Name;
        public int Score;
        public string Avatar;
        public PlayerType Type;
    }
}