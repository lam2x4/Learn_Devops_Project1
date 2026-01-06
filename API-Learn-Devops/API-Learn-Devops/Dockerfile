FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Sử dụng dấu * để tự động tìm file csproj bất kể tên chính xác là gì
COPY *.csproj ./
RUN dotnet restore

# Copy tất cả code còn lại
COPY . .
# Publish trực tiếp thư mục hiện tại thay vì chỉ định tên file cụ thể để tránh sai sót
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
# Thêm biến môi trường để ứng dụng chạy ổn định hơn trên Linux
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /app/publish .
EXPOSE 8080

ENTRYPOINT ["dotnet", "API-Learn-Devops.dll"]