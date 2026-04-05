# WebReview API (.NET 8)

API quản lý **User** và **Post** theo mô hình **Clean Architecture + CQRS + MediatR**.

## Công nghệ sử dụng

- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core + SQL Server
- MediatR (CQRS)
- FluentValidation
- JWT Authentication + Role-based Authorization
- BCrypt (hash mật khẩu)
- Swagger (OpenAPI)

## Chức năng chính

- Đăng ký, đăng nhập (JWT)
- Quản lý bài viết (Post)
  - Xem danh sách, xem chi tiết (public)
  - Tạo, sửa, xóa (cần đăng nhập)
- Quản lý người dùng (User)
  - Chỉ **Admin** được xem/sửa/xóa user
- Phân quyền rõ ràng:
  - **User**: thao tác bài viết của chính mình
  - **Admin**: quản lý toàn bộ bài viết và user
- Thông báo lỗi tiếng Việt, response thống nhất

## Cấu trúc dự án

- `WebReview.Domain`  
  Entity, interface repository, hằng số role

- `WebReview.Application`  
  Commands/Queries/Handlers, Validators, DTOs, Exceptions

- `WebReview.Infrastructure`  
  EF Core DbContext, repository implementation, JWT service, password hasher

- `WebReview.API`  
  Controllers, contracts request/response, middleware, cấu hình auth/DI/swagger

## Quan hệ dữ liệu

- 1 User có nhiều Post
- 1 Post thuộc về 1 User
- Xóa User sẽ xóa Post liên quan (cascade delete)

## Chạy dự án

### 1) Cập nhật database

```powershell
dotnet ef database update --project WebReview.Infrastructure --startup-project WebReview.API
```

### 2) Chạy API

```powershell
dotnet run --project WebReview.API
```

### 3) Test trên Swagger

- Mở Swagger sau khi chạy project
- Gọi `POST /api/auth/register` hoặc `POST /api/auth/login`
- Copy token và bấm **Authorize** để test API cần quyền

## Endpoint chính

### Auth
- `POST /api/auth/register`
- `POST /api/auth/login`

### Posts
- `GET /api/posts`
- `GET /api/posts/{id}`
- `POST /api/posts`
- `PUT /api/posts/{id}`
- `DELETE /api/posts/{id}`

### Users (Admin)
- `GET /api/users`
- `GET /api/users/{id}`
- `PUT /api/users/{id}`
- `DELETE /api/users/{id}`

## Ghi chú

- Dự án dùng `Id` kiểu số nguyên (`int identity`).
- Response thành công theo dạng:
  - `message`: thông báo
  - `data`: dữ liệu
- Response lỗi trả thông báo tiếng Việt rõ ràng.
