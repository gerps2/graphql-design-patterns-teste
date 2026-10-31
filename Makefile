.PHONY: run build medir clean

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
	k6 run --quiet scripts/medir-aula-01.js
	$(ABRIR) relatorio-aula-01.html

clean:
	dotnet clean Corretora.sln
