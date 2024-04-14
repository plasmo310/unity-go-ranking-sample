package main

import (
	"vps-general-api/repository"
	"vps-general-api/server"
)

func main() {
	// Initialize.
	repository.Init()
	server.Init()

	// Start listen server.
	server.Listen()
}
