package repository

import (
	"github.com/gin-gonic/gin"
	"vps-general-api/model"
)

type ScoreRepository struct{}
type Score model.Score

func (r ScoreRepository) GetAll() ([]Score, error) {
	var scores []Score
	err := Db.Find(&scores).Error
	if err != nil {
		return nil, err
	}
	return scores, nil
}

func (r ScoreRepository) Get(appId int, scoreType int, limitCount int, orderDesc int) ([]Score, error) {
	var scores []Score
	scoreOrder := "score"
	if orderDesc == 1 {
		scoreOrder += " desc"
	}
	err := Db.Order(scoreOrder).Limit(limitCount).Find(&scores, "app_id = ? AND type = ? ", appId, scoreType).Error
	if err != nil {
		return nil, err
	}
	return scores, nil
}

func (r ScoreRepository) Create(c *gin.Context) (Score, error) {
	var score Score
	err := c.ShouldBindJSON(&score)
	if err != nil {
		return score, err
	}
	err = Db.Create(&score).Error
	if err != nil {
		return score, err
	}
	return score, nil
}
