

## معرفی کلی

پروژه CRUDTest یک API ساده مبتنی بر ASP.NET Core است که عملیات احراز هویت و مدیریت محصولات (CRUD) را با معماری تمیز (Clean Architecture) و الگوی CQRS پیاده‌سازی می‌کند. این پروژه از لایه‌های Core (دامنه)، Infrastructure (زیرساخت) و Presentation (ارائه API) تشکیل شده است.

---

## کنترلرها

### 1. ProductController

آدرس پایه:  
`/Product`

تمام متدها نیاز به احراز هویت (JWT یا مشابه) دارند.

#### متدها:

- **GET /Product**  
  دریافت یک محصول بر اساس کوئری.
  ```http
  GET /Product?id=1
  ```
  ورودی: پارامترهای GetProductQuery
  خروجی:  
  ```json
  {
    "isSuccess": true,
    "value": { "id": 1, "name": "..." }
  }
  ```

- **GET /Product (لیست)**  
  دریافت لیست محصولات با فیلترهای کوئری.
  ```http
  GET /Product?category=book
  ```
  ورودی: پارامترهای GetProductsQuery
  خروجی:  
  ```json
  {
    "isSuccess": true,
    "value": [ { "id": 1, ... }, ... ]
  }
  ```

- **POST /Product**  
  ایجاد محصول جدید.
  ```http
  POST /Product
  Content-Type: application/json

  {
    "name": "Book",
    "category": "Book",
    ...
  }
  ```
  ورودی: CreateProductCommand
  خروجی:  
  ```json
  { "isSuccess": true }
  ```

- **PUT /Product**  
  ویرایش محصول.
  ```http
  PUT /Product
  Content-Type: application/json

  {
    "id": 1,
    "name": "Updated name",
    ...
  }
  ```
  ورودی: UpdateProductCommand
  خروجی:  
  ```json
  { "isSuccess": true }
  ```

- **DELETE /Product**  
  حذف محصول.
  ```http
  DELETE /Product
  Content-Type: application/json

  {
    "id": 1
  }
  ```
  ورودی: DeleteProductCommand
  خروجی:  
  ```json
  { "isSuccess": true }
  ```

---

### 2. UserController

آدرس پایه:  
`/User`

#### متدها:

- **GET /User**  
  ورود کاربر (login).
  ```http
  GET /User?username=test&password=123
  ```
  ورودی: LoginUserQuery
  خروجی:  
  ```json
  {
    "isSuccess": true,
    "value": "JWT-TOKEN"
  }
  ```

- **POST /User**  
  ثبت‌نام کاربر (register).
  ```http
  POST /User
  Content-Type: application/json

  {
    "username": "test",
    "password": "123",
    ...
  }
  ```
  ورودی: RegisterUserCommand
  خروجی:  
  ```json
  { "isSuccess": true }
  ```

---

## ساختار ورودی و خروجی

- ورودی متدها بر اساس مدل‌های Command/Query در Dtoهای Application layer هستند.
- خروجی تمام متدها با FluentResults و ساختار:
  ```json
  {
    "isSuccess": true,
    "value": ...,
    "errors": []
  }
  ```

---

## مثال‌های curl

**دریافت توکن:**
```bash
curl "http://localhost:5000/User?username=admin&password=123"
```

**ثبت نام:**
```bash
curl -X POST "http://localhost:5000/User" -H "Content-Type: application/json" -d '{"username":"test","password":"123"}'
```

**ایجاد محصول:**
```bash
curl -X POST "http://localhost:5000/Product" -H "Authorization: Bearer {token}" -H "Content-Type: application/json" -d '{"name":"Book"}'
```

---

## سایر نکات پروژه

- **احراز هویت:** دسترسی به ProductController نیازمند توکن است. ابتدا از طریق UserController توکن بگیرید.
- **معماری:** لایه‌بندی تمیز (Core، Infrastructure، Presentation) رعایت شده است.
- **CQRS:** منطق هر عملیات در کلاس‌های Command/Query در Application پیاده شده است.
- **Swagger:** مستندات و تست آنلاین endpointها معمولاً از طریق Swagger UI در دسترس است.
- **توسعه‌دهندگان:** افزودن قابلیت جدید از طریق تعریف Command/Query، هندلر و افزودن endpoint در کنترلر مربوطه.

---

## مدیریت خطاها

در صورت خطا، خروجی با isSuccess=false و لیست errors برگردانده می‌شود:
```json
{
  "isSuccess": false,
  "errors": ["شرح خطا"]
}
```

---
