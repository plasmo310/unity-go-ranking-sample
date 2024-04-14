using UnityEngine;

namespace Ranking
{
    [CreateAssetMenu(fileName = "RankingSettings", menuName = "Ranking/RankingSettings")]
    public class RankingApiSettings : ScriptableObject
    {
        /// <summary>
        /// ランキングAPI URL
        /// </summary>
        [SerializeField]
        private string _apiUrl = "http://localhost:8080";
        public string ApiUrl => _apiUrl;

        /// <summary>
        /// アプリケーションID
        /// </summary>
        [SerializeField]
        private int _appId = 1001;
        public int AppId => _appId;

        /// <summary>
        /// クライアントキー
        /// </summary>
        [SerializeField]
        private string _appClientKey = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
        public string AppClientKey => _appClientKey;
    }
}