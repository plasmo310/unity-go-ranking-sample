using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Ranking
{
    /// <summary>
    /// ランキングサービスクラス
    /// </summary>
    public class RankingApiApiService : IRankingApiService
    {
        /// <summary>
        /// ランキング設定
        /// </summary>
        private readonly RankingApiSettings _rankingApiSettings;

        /// <summary>
        /// MonoBehaviour(コルーチン実行用)
        /// </summary>
        private readonly MonoBehaviour _monoBehaviour;

        /// <summary>
        /// ランキングAPI URL
        /// </summary>
        private string ApiUrl => $"{_rankingApiSettings.ApiUrl}/ranking";

        /// <summary>
        /// アプリケーションID
        /// </summary>
        private int AppId => _rankingApiSettings.AppId;

        /// <summary>
        /// アプリケーションクライアントキー
        /// </summary>
        private string AppClientKey => _rankingApiSettings.AppClientKey;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="rankingApiSettings">ランキング設定</param>
        public RankingApiApiService(RankingApiSettings rankingApiSettings)
        {
            _rankingApiSettings = rankingApiSettings;

            // MonoBehaviourハンドラの生成
            var monoBehaviourHandlerObject = new GameObject(nameof(RankingApiMonoBehaviour));
            var monoBehaviourHandler = monoBehaviourHandlerObject.AddComponent<RankingApiMonoBehaviour>();
            UnityEngine.Object.DontDestroyOnLoad(monoBehaviourHandlerObject);
            _monoBehaviour = monoBehaviourHandler;
        }

        /// <summary>
        /// スコアレコードの取得
        /// </summary>
        /// <param name="type">スコア種別</param>
        /// <param name="limitCount">最大取得数</param>
        /// <param name="isOrderDesc">値降順で取得するか？</param>
        /// <param name="callback">コールバック</param>
        public void GetScores(int type, int limitCount, bool isOrderDesc, Action<bool, IRankingApiService.ScoreSchema[]> callback)
        {
            var queryString = System.Web.HttpUtility.ParseQueryString("");
            queryString.Add("app_id", AppId.ToString());
            queryString.Add("type", type.ToString());
            queryString.Add("limit_count", limitCount.ToString());
            if (isOrderDesc)
            {
                queryString.Add("order_desc", "1");
            }

            var requestUrl = $"{ApiUrl}/scores";
            var uriBuilder = new System.UriBuilder(requestUrl)
            {
                Query = queryString.ToString()
            };

            var request = UnityWebRequest.Get(uriBuilder.Uri);
            _monoBehaviour.StartCoroutine(SendRequestCoroutine(request, (isSuccess, response) =>
            {
                if (isSuccess)
                {
                    var schema = JsonUtility.FromJson<IRankingApiService.ScoreSchemaArray>(response);
                    callback.Invoke(true, schema.scores);
                }
                else
                {
                    callback?.Invoke(false, null);
                }
            }));
        }

        /// <summary>
        /// スコアレコードの追加
        /// </summary>
        /// <param name="type">スコア種別</param>
        /// <param name="name">プレイヤー名</param>
        /// <param name="score">スコア</param>
        /// <param name="callback">コールバック</param>
        public void AddScore(int type, string name, int score, Action<bool, string> callback = null)
        {
            var schema = new IRankingApiService.ScoreSchema()
            {
                AppId = AppId,
                Type = type,
                Name = name,
                Score = score,
            };
            var json = JsonUtility.ToJson(schema);
            var bodyRaw = Encoding.UTF8.GetBytes(json);

            var requestUrl = $"{ApiUrl}/scores";
            var request = new UnityWebRequest(requestUrl, "POST");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            _monoBehaviour.StartCoroutine(SendRequestCoroutine(request, (isSuccess, response) =>
            {
                callback?.Invoke(isSuccess, response);
            }));
        }

        /// <summary>
        /// リクエスト送信
        /// </summary>
        /// <param name="request">リクエスト</param>
        /// <param name="callback">コールバック</param>
        private IEnumerator SendRequestCoroutine(UnityWebRequest request, Action<bool, string> callback = null)
        {
            request.SetRequestHeader("Authorization", GetBasicAuthToken());
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError
                || request.result == UnityWebRequest.Result.ProtocolError)
            {
                callback?.Invoke(false, request.error);
                yield break;
            }

            callback?.Invoke(true, request.downloadHandler?.text);
        }

        /// <summary>
        /// Basic認証トークン取得
        /// </summary>
        /// <returns></returns>
        private string GetBasicAuthToken() {
            var token = $"{AppId}:{AppClientKey}";
            token = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(token));
            return $"Basic {token}";
        }
    }

    /// <summary>
    /// MonoBehaviour実行用
    /// </summary>
    public class RankingApiMonoBehaviour : MonoBehaviour
    {
    }
}