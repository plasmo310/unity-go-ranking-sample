package repository

import (
	"strconv"
	"vps-general-api/model"
)

type MstAppRepository struct{}
type MstApp model.MstApp

func (r MstAppRepository) GetRankingAuthAccounts() map[string]string {
	var apps []MstApp

	// 1000番台がランキング対象アプリ
	err := Db.Find(&apps, "id BETWEEN ? AND ?", 1000, 1999).Error
	if err != nil {
		panic(err)
	}

	// mapにして返却
	authAccounts := map[string]string{}
	for _, app := range apps {
		key := strconv.FormatUint(uint64(app.Id), 10)
		authAccounts[key] = app.ClientKey
	}
	return authAccounts
}
