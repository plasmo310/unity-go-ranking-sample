# unity-ranking-project
* ランキング処理実行サンプル<br>
  * ランキングサーバのAPIをUnity側から実行するサンプルになります。
  * API実行処理は <a href="Assets/Scripts/Ranking">Assets/Scripts/Ranking</a> 配下に定義してあります。
<img width="808" alt="screenshot 2024-04-14 19 46 07" src="https://github.com/plasmo310/unity-go-ranking-sample/assets/77447256/d8832470-185f-4cec-a1f4-5f6ec979c5a9">

### Unityバージョン
2022.3.16f1<br>

### 使用方法
* <a href="Assets/Resources/Ranking/RankingSettings.asset">RankingSettings.asset</a> にAPIの接続情報を入力します。
* APIサーバを起動した状態で画面上にプレイヤー名、スコアを入力して「Add」ボタンを押下するとスコア情報が登録されます。
