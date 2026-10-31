.PHONY: run build medir clean

run:
	dotnet run --project src/Corretora.Api --urls http://localhost:5000

build:
	dotnet build Corretora.sln

medir:
	k6 run --quiet scripts/medir-aula-01.js

clean:
	dotnet clean Corretora.sln
