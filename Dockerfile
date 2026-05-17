# 1. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# نسخ ملفات الـ .csproj لعمل Restore
COPY ["GreenMind.Presentation/GreenMind.Presentation.csproj", "GreenMind.Presentation/"]
COPY ["GreenMind.Domain/GreenMind.Domain.csproj", "GreenMind.Domain/"]
COPY ["GreenMind.Presistance/GreenMind.Presistance.csproj", "GreenMind.Presistance/"]
COPY ["GreenMind.Service/GreenMind.Service.csproj", "GreenMind.Service/"]
COPY ["GreenMind.ServiceAbstraction/GreenMind.ServiceAbstraction.csproj", "GreenMind.ServiceAbstraction/"]
COPY ["GreenMind.Shared/GreenMind.Shared.csproj", "GreenMind.Shared/"]

# عمل Restore
RUN dotnet restore "GreenMind.Presentation/GreenMind.Presentation.csproj"

# نسخ باقي الكود وبناء المشروع
COPY . .
WORKDIR "/src/GreenMind.Presentation"
RUN dotnet build "GreenMind.Presentation.csproj" -c Release -o /app/build

# 2. Publish Stage
FROM build AS publish
# تعديل أمر الـ publish لضمان تجميع كل الملفات المطلوبة للتشغيل
RUN dotnet publish "GreenMind.Presentation.csproj" -c Release -o /app/publish

# 3. Final Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
# نسخ الملفات النهائية من مرحلة الـ publish
COPY --from=publish /app/publish .

# تفعيل البيئة وتحديد البورت
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "GreenMind.Presentation.dll"]