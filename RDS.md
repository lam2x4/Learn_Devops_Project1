# Chuyển Database sang AWS RDS (PostgreSQL)

Tài liệu này hướng dẫn từng bước để chuyển Postgres đang chạy trong Docker (EC2) sang RDS.

## 0. Chuẩn bị
- Đã có một RDS PostgreSQL instance (lấy **endpoint**, **username**, **password**, **database name**).
- EC2 có thể SSH vào và đã cài Docker; cài thêm psql client nếu chưa có.
- Biết Security Group (SG) của EC2 và SG của RDS.

## 1. Cấu hình mạng (Security Group)
- Vào AWS Console → RDS → Chọn instance → Connectivity & security → Security group.
- Mở inbound rule cho **PostgreSQL 5432** **chỉ từ SG của EC2** (không mở 0.0.0.0/0).
  - Type: PostgreSQL
  - Port: 5432
  - Source: Security Group của EC2 (hoặc IP cố định nếu bắt buộc)
- Đảm bảo EC2 và RDS ở cùng VPC hoặc có peering phù hợp.

## 2. Cài psql client trên EC2 (nếu chưa có)
```bash
sudo apt update && sudo apt install -y postgresql-client
```

## 3. Kiểm tra kết nối RDS
Thay giá trị thực tế:
```bash
PGPASSWORD=YOUR_RDS_PASSWORD psql "host=YOUR_RDS_ENDPOINT port=5432 dbname=postgres user=YOUR_RDS_USER sslmode=require" -c "\l"
```
Nếu thất bại, kiểm tra lại SG và endpoint.

## 4. Sao lưu dữ liệu từ Postgres container cũ (nếu cần)
```bash
# Tạo dump từ container Postgres đang chạy trên EC2
CONTAINER_NAME=project-devops-postgres-1  # đổi theo thực tế
DB_NAME=TodoApp
PGUSER=postgres
PGPASSWORD=123

docker exec -t $CONTAINER_NAME \
  pg_dump -U $PGUSER -d $DB_NAME > /tmp/todo_dump.sql
```

## 5. Khôi phục dữ liệu lên RDS
```bash
PGPASSWORD=YOUR_RDS_PASSWORD psql \
  "host=YOUR_RDS_ENDPOINT port=5432 dbname=TodoApp user=YOUR_RDS_USER sslmode=require" \
  < /tmp/todo_dump.sql
```

## 6. Cập nhật connection string cho backend
- Chỉnh `docker-compose.yml` service `backend`, thay connection string:
```yaml
backend:
  environment:
    ConnectionStrings__DefaultConnection: |
      Host=YOUR_RDS_ENDPOINT;Port=5432;Database=TodoApp;Username=YOUR_RDS_USER;Password=YOUR_RDS_PASSWORD;SslMode=Require;Trust Server Certificate=true
```
- Khuyến nghị: dùng GitHub Secret `RDS_CONNECTION_STRING` và inject qua CI/CD hoặc `.env` trên EC2 (không commit mật khẩu).

## 7. Gỡ bỏ Postgres container (tuỳ chọn)
- Comment hoặc xóa service `postgres` trong `docker-compose.yml` nếu không dùng nữa.
- Sau khi chắc chắn đã migratê, có thể xóa volume cũ:
```bash
docker volume rm project-devops_pgdata
```

## 8. Khởi động lại backend
```bash
docker compose down
docker compose up --build -d
```
Backend sẽ tự chạy `context.Database.Migrate()` và dùng RDS.

## 9. Xác minh
```bash
docker compose logs -f backend
curl http://localhost:5000/api/todos
# hoặc qua domain: https://api.heeeee.me/api/todos
```
Swagger (nếu bật): `http://localhost:5000/swagger`

## 10. Mẹo bảo mật & vận hành
- Không mở 5432 ra internet; chỉ cho phép SG của EC2.
- Đổi mật khẩu Postgres mặc định; lưu trong Secrets Manager / GitHub Secrets.
- Bật backup tự động và Multi-AZ cho RDS (tuỳ ngân sách).
- Theo dõi kết nối SSL (`sslmode=require` đã bật ở connection string).

## 11. Tích hợp CI/CD
- Thêm secret `RDS_CONNECTION_STRING` trong GitHub Actions, cập nhật workflow để export vào compose env (backend):
```yaml
environment:
  ConnectionStrings__DefaultConnection: ${{ secrets.RDS_CONNECTION_STRING }}
```
- Triển khai lại: push code → GitHub Actions deploy → backend dùng RDS.

## 12. Checklist nhanh
- [ ] RDS endpoint + user + password + dbname đã sẵn sàng
- [ ] SG RDS cho phép SG EC2 trên port 5432
- [ ] Kiểm tra kết nối psql thành công
- [ ] Dump/restore dữ liệu (nếu cần)
- [ ] Cập nhật connection string backend
- [ ] Gỡ Postgres container (tuỳ chọn)
- [ ] `docker compose up --build -d`
- [ ] Kiểm tra API/Swagger
