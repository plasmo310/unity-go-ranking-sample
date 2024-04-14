package controller

import (
	"github.com/gin-gonic/gin"
	"vps-general-api/repository"
)

type Controller struct{}

type MethodType int

const (
	GET MethodType = iota
	POST
	PUT
	DELETE
)

type Route struct {
	GroupType   RouteGroupType
	MethodType  MethodType
	Path        string
	HandlerFunc gin.HandlerFunc
}
type Routes []Route

// GetAllRoutes all controller routes.
func GetAllRoutes() Routes {
	var routes Routes
	routes = append(routes, ScoreController{}.Routes()...)
	return routes
}

type RouteGroupType int

const (
	None RouteGroupType = iota
	Ranking
)

type RouteGroup struct {
	Path         string
	AuthAccounts map[string]string
}
type RouteGroupMap map[RouteGroupType]RouteGroup

var routeGroupMap RouteGroupMap

// GetRouteGroupMap all controller route groups.
func GetRouteGroupMap() RouteGroupMap {
	var r repository.MstAppRepository

	// routeグループと認証情報を定義
	routeGroupMap = RouteGroupMap{}
	routeGroupMap[Ranking] = RouteGroup{
		Path:         "/ranking",
		AuthAccounts: r.GetRankingAuthAccounts(),
	}
	return routeGroupMap
}
