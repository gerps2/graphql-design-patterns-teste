.PHONY: run build medir test clean

AULA ?= 02

ifeq ($(OS),Windows_NT)
ABRIR = powershell -NoProfile -Command Start-Process
else ifeq ($(shell uname),Darwin)
ABRIR = open
else
ABRIR = xdg-open
endif

run:
	dotnet run --project src/Corretora.Api --urls http://localhost:5000

build:
	dotnet build Corretora.sln

medir:
	k6 run --quiet scripts/medir-aula-$(AULA).js
	$(ABRIR) relatorio-aula-$(AULA).html

test:
	dotnet test Corretora.sln

clean:
	dotnet clean Corretora.sln
