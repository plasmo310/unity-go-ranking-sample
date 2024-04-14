using Ranking;
using UI;
using UnityEngine;

/// <summary>
/// ランキングリスト管理クラス
/// </summary>
public class RankingListManager : MonoBehaviour
{
    /// <summary>
    /// UI: スクロールエリア
    /// </summary>
    [SerializeField] private GameObject _uiRankingScrollRect;

    /// <summary>
    /// UI: ランキングアイテムPrefab
    /// </summary>
    [SerializeField] private UiRankingItemContent _uiRankingItemPrefab;

    /// <summary>
    /// ランキングサービス
    /// </summary>
    private IRankingApiService _rankingApiService;

    /// <summary>
    /// スコア種別
    /// </summary>
    private const int RankingScoreType = 1;

    /// <summary>
    /// スコア最大取得数
    /// </summary>
    private const int RankingLimitCount = 100;

    /// <summary>
    /// 開始処理
    /// </summary>
    private void Start()
    {
        // サービス初期化
        var rankingApiSettings = Resources.Load<RankingApiSettings>("Ranking/RankingSettings");
        _rankingApiService = new RankingApiApiService(rankingApiSettings);

        // ランキング情報を取得
        GetRankingList();
    }
    
    /// <summary>
    /// ランキング情報を追加する
    /// </summary>
    /// <param name="uiRankingItemContent">追加するランキング情報</param>
    private void AddRankingItem(UiRankingItemContent uiRankingItemContent)
    {
        var displayName = uiRankingItemContent.nameText.text;
        var score = uiRankingItemContent.scoreText.text;
        if (string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(score))
        {
            UnityEngine.Debug.LogError("please input text.");
            return;
        }

        _rankingApiService.AddScore(RankingScoreType, displayName, int.Parse(score), (isSuccess, response) =>
        {
            if (!isSuccess)
            {
                UnityEngine.Debug.LogError("failed add score.");
                return;
            }
            // 情報の再取得
            ClearRankingList();
            GetRankingList();
        });
    }

    /// <summary>
    /// 全てのランキング情報を取得する
    /// </summary>
    private void GetRankingList()
    {
        _rankingApiService.GetScores(RankingScoreType, RankingLimitCount, true, (isSuccess, scores) =>
        {
            if (!isSuccess)
            {
                UnityEngine.Debug.LogError("failed get scores.");
                return;
            }
            // 取得したランキング情報をリストに追加
            foreach (var score in scores)
            {
                var rankingItemContent = Instantiate(_uiRankingItemPrefab, _uiRankingScrollRect.transform);
                rankingItemContent.idText.text = score.Id.ToString();
                rankingItemContent.nameText.text = score.Name;
                rankingItemContent.scoreText.text = score.Score.ToString();
            }
        });
    }

    /// <summary>
    /// ランキングリストをクリアする
    /// </summary>
    private void ClearRankingList()
    {
        foreach (Transform child in _uiRankingScrollRect.transform)
        {
            Destroy(child.transform.gameObject);
        }
    }

    // ---------- 各ボタン押下処理 ----------
    public void PushReloadButton()
    {
        ClearRankingList();
        GetRankingList();
    }

    public void PushAddButton(UiRankingItemContent uiRankingItemContent)
    {
        AddRankingItem(uiRankingItemContent);
    }
}
