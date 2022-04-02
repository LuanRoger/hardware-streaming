package logger

import (
	"os"
	"time"

	"github.com/rs/zerolog"
	"github.com/rs/zerolog/log"
)

func InitLogger() {
	log.Logger = log.Output(zerolog.ConsoleWriter{Out: os.Stderr, TimeFormat: time.Kitchen})
	log.Info().Msg("Logger inted")
}

func LogInformation(message string) {
	log.Info().Msg(message)
}
func LogError(err error) {
	log.Error().Msg(err.Error())
}
func LogFatal(err error) {
	log.Fatal().Msg(err.Error())
}
