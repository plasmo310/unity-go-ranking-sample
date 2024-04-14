#!/bin/bash

##############################
# dataンポート処理
# docker compose upしている状態で実行する
##############################

# DBコンテナ名
CONTAINER_NAME="go-ranking-api-mysql-1"

#dataファイルパス
DATA_DIR_NAME="data/"

# DB接続情報
MYSQL_DB="sample"
MYSQL_USER="user"
MYSQL_PASSWORD="password"

#作業フォルダに移動
WORK_DIR=$(dirname $0)
cd ${WORK_DIR}

#インポートするdumpを選択させる
echo `ls ${DATA_DIR_NAME}`
echo "select import dump: "
read file_name

#ファイル存在チェック
if [[ ! -e "${DATA_DIR_NAME}${file_name}" ]]; then
  echo "not exists file: ${file_name}"
  exit 1
fi

#import開始
echo "*** start import"
echo "file_name: ${file_name}"
docker exec -i "${CONTAINER_NAME}" mysql ${MYSQL_DB} -u${MYSQL_USER} -p${MYSQL_PASSWORD} < "${DATA_DIR_NAME}${file_name}"
echo "*** finish import"
