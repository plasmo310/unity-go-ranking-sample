package server

import (
	"github.com/gin-gonic/gin"
	"vps-general-api/controller"
)

var router *gin.Engine

func Init() {
	router = CreateRouter(controller.GetAllRoutes(), controller.GetRouteGroupMap())
}

func CreateRouter(routes controller.Routes, routeGroupMap controller.RouteGroupMap) *gin.Engine {
	r := gin.Default()

	routerGroup := map[controller.RouteGroupType]*gin.RouterGroup{}
	for key, value := range routeGroupMap {
		routerGroup[key] = r.Group(value.Path, gin.BasicAuth(value.AuthAccounts))
	}

	for _, route := range routes {
		if group, ok := routerGroup[route.GroupType]; ok {
			switch route.MethodType {
			case controller.GET:
				group.GET(route.Path, route.HandlerFunc)
			case controller.POST:
				group.POST(route.Path, route.HandlerFunc)
			case controller.PUT:
				group.PUT(route.Path, route.HandlerFunc)
			case controller.DELETE:
				group.DELETE(route.Path, route.HandlerFunc)
			}
		} else {
			switch route.MethodType {
			case controller.GET:
				r.GET(route.Path, route.HandlerFunc)
			case controller.POST:
				r.POST(route.Path, route.HandlerFunc)
			case controller.PUT:
				r.PUT(route.Path, route.HandlerFunc)
			case controller.DELETE:
				r.DELETE(route.Path, route.HandlerFunc)
			}
		}

	}
	return r
}

func Listen() {
	err := router.Run(":8080")
	if err != nil {
		panic(err)
	}
}
