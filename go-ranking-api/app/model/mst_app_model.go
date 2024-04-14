package model

import "time"

type MstApp struct {
	Id        uint      `gorm:"primary_key"`
	Name      string    `gorm:"size:255"`
	ClientKey string    `gorm:size:255`
	CreatedAt time.Time `gorm:default:CURRENT_TIMESTAMP`
	UpdatedAt time.Time `gorm:default:CURRENT_TIMESTAMP`
}
