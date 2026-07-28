.PHONY: help restore build run test format lint clean docker-up docker-down migrate watch

help:
	@echo "Available commands:"
	@echo " make restore      Restore NuGet packages"
	@echo " make build        Build the solution"
	@echo " make run          Run the API"
	@echo " make watch        Run with dotnet watch"
	@echo " make test         Run tests"
	@echo " make format       Format code"
	@echo " make clean        Clean build artifacts"
	@echo " make docker-up    Start local infrastructure"
	@echo " make docker-down  Stop local infrastructure"
	@echo " make migrate      Apply EF Core migrations"

restore:
	dotnet restore

build:
	dotnet build

run:
	dotnet run --project src/GlobalChat.Api

watch:
	dotnet watch --project src/GlobalChat.Api

test:
	dotnet test

format:
	dotnet format

clean:
	dotnet clean

docker-up:
	docker compose up -d

docker-down:
	docker compose down

migrate:
	dotnet ef database update --project src/GlobalChat.Infrastructure --startup-project src/GlobalChat.Api
