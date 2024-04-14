## go-ranking-api
* Go言語で実装したランキング処理のREST APIサンプルです。
* 構成
  * フレームワーク
    * Gin
  * ORM
    * GORM
  * DB
    * MySQL

### テーブル構成

##### mst_apps
| Id | Name | ClientKey |
| -- | -- | -- |
| アプリID | アプリ名 | クライアントキー |
| integer | string | string |

##### scores
| Id | AppId | Type | Name | Score |
| -- | -- | -- | -- | -- |
| スコアID | アプリID | スコア種類 | プレイヤー名 | スコア |
| integer | integer | integer | string | integer |

### API定義

| URL | Method | Description | Requests |
| -- | -- | -- | -- |
| ranking/scores | GET | ランキング情報取得 | app_id: アプリID<br>type: スコア種別<br>limit_count: 取得最大数<br>order_desc: 値降順で取得するか？ |
| ranking/scores | POST | ランキング情報登録 | <a href="app/model/score_model.go">mst_appsのmodel定義</a>を参照 |

### 使用方法

* <a href=".env_rename_me">.env_rename_me</a>ファイルをコピーして.envファイルを作成します。
* <code>docker compose up -d</code>を実行して localhost:8080 に対して各APIを実行します。
  * 初回起動時は<code>mst_apps</code>のデータを挿入する必要があります。sqlフォルダ内の下記コマンドでサンプルデータのSQLをimportできます。
     ```
     sh sql/_db_data_import.sh
     ```
  * デバッグ時など詳細なログを確認したい場合、<a href="/build/app/Dockerfile">Dockerfile</a>の<code>CMD ["go", "run", "main.go"]</code>をコメントアウトして、下記コマンドで直接実行できます。
     ```
     docker compose exec app go run main.go
     ```
  
