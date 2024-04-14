package model

import "time"

type Score struct {
	Id        uint      `gorm:"primary_key"`
	AppId     uint      `gorm:default:0`
	Type      uint      `gorm:default:0`
	Name      string    `gorm:"size:255"`
	Score     uint      `gorm:default:0`
	CreatedAt time.Time `gorm:default:CURRENT_TIMESTAMP`
	UpdatedAt time.Time `gorm:default:CURRENT_TIMESTAMP`
}
