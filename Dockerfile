# Используем официальный образ .NET SDK в качестве базового
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем файл проекта и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем остальные файлы проекта и строим приложение
COPY . ./
WORKDIR /src/QualificationCoursesExam
RUN dotnet publish -c Release -o /app

# Используем официальный образ ASP.NET Core Runtime в качестве базового для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

<<<<<<< HEAD
# Указываем команду для запуска 
=======
# Указываем команду для запуска приложения
>>>>>>> parent of 1d00976 (Documentation is complite)
ENTRYPOINT ["dotnet", "QualificationCoursesExam.dll"]
