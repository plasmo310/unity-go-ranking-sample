using System;

namespace Ranking
{
    /// <summary>
    /// ランキングサービス interface
    /// </summary>
    public interface IRankingApiService
    {
        [Serializable]
        public class ScoreSchema
        {
            /// <summary>
            /// ID
            /// </summary>
            public int Id;

            /// <summary>
            /// アプリケーションID
            /// </summary>
            public int AppId;

            /// <summary>
            /// スコア種別
            /// </summary>
            public int Type;

            /// <summary>
            /// プレイヤー名
            /// </summary>
            public string Name;

            /// <summary>
            /// スコア
            /// </summary>
            public int Score;
        }

        [Serializable]
        public class ScoreSchemaArray
        {
            public ScoreSchema[] scores;
        }

        /// <summary>
        /// スコアレコードの取得
        /// </summary>
        /// <param name="type">スコア種別</param>
        /// <param name="limitCount">最大取得数</param>
        /// <param name="isOrderDesc">値降順で取得するか？</param>
        /// <param name="callback">コールバック</param>
        public void GetScores(int type, int limitCount, bool isOrderDesc, Action<bool, ScoreSchema[]> callback);

        /// <summary>
        /// スコアレコードの追加
        /// </summary>
        /// <param name="type">スコア種別</param>
        /// <param name="name">プレイヤー名</param>
        /// <param name="score">スコア</param>
        /// <param name="callback">コールバック</param>
        public void AddScore(int type, string name, int score, Action<bool, string> callback = null);
    }
}