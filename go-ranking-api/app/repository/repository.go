package repository

import (
	"fmt"
	"gorm.io/driver/mysql"
	"gorm.io/gorm"
	"os"
	"vps-general-api/model"
)

var (
	Db  *gorm.DB
	err error
)

func Init() {
	// Get DB settings.
	dbUser := os.Getenv("MYSQL_USER")
	dbPassword := os.Getenv("MYSQL_PASSWORD")
	dbName := os.Getenv("MYSQL_DATABASE")
	dbHost := "mysql" // Db container name
	dbPort := "3306"

	// Connect DB.
	dsn := fmt.Sprintf(
		"%s:%s@tcp(%s:%s)/%s?charset=utf8mb4&parseTime=True&loc=Local",
		dbUser, dbPassword, dbHost, dbPort, dbName)
	Db, err = gorm.Open(mysql.Open(dsn))
	if err != nil {
		panic(err)
	}

	// Migration.
	err = AutoMigrate()
	if err != nil {
		panic(err)
	}
}

func AutoMigrate() error {
	err := Db.AutoMigrate(&model.MstApp{}, &model.Score{})
	if err != nil {
		return err
	}
	return nil
}
