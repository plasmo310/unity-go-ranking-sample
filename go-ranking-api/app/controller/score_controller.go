package controller

import (
	"fmt"
	"github.com/gin-gonic/gin"
	"net/http"
	"strconv"
	"vps-general-api/repository"
)

type ScoreController struct{}

func (ctrl ScoreController) Routes() []Route {
	return Routes{
		Route{
			GroupType:   Ranking,
			MethodType:  GET,
			Path:        "/scores",
			HandlerFunc: ctrl.Index,
		},
		Route{
			GroupType:   Ranking,
			MethodType:  POST,
			Path:        "/scores",
			HandlerFunc: ctrl.Create,
		},
	}
}

func (ctrl ScoreController) Index(c *gin.Context) {
	// リクエストパラメータ取得
	appId, err := strconv.Atoi(c.Query("app_id"))
	if err != nil {
		c.AbortWithStatus(http.StatusBadRequest)
		fmt.Println(err)
		return
	}
	scoreType, err := strconv.Atoi(c.Query("type"))
	if err != nil {
		c.AbortWithStatus(http.StatusBadRequest)
		fmt.Println(err)
		return
	}
	limitCount, err := strconv.Atoi(c.Query("limit_count"))
	if err != nil {
		c.AbortWithStatus(http.StatusBadRequest)
		fmt.Println(err)
		return
	}
	// 任意項目
	orderDesc, err := strconv.Atoi(c.Query("order_desc"))
	if err != nil {
		orderDesc = 0
	}

	// AppIdが認証キーと一致しているか？
	authUserId := c.MustGet(gin.AuthUserKey).(string)
	if authUserId != strconv.Itoa(appId) {
		c.AbortWithStatus(http.StatusUnauthorized)
		return
	}

	// パラメータからレコードを取得
	var r repository.ScoreRepository
	scores, err := r.Get(appId, scoreType, limitCount, orderDesc)
	if err != nil {
		c.AbortWithStatus(http.StatusBadRequest)
		fmt.Println(err)
	} else {
		c.JSON(http.StatusOK, gin.H{"scores": scores})
	}
}

func (ctrl ScoreController) Create(c *gin.Context) {
	var r repository.ScoreRepository
	book, err := r.Create(c)
	if err != nil {
		c.AbortWithStatus(http.StatusBadRequest)
		fmt.Println(err)
	} else {
		c.JSON(http.StatusOK, book)
	}
}
