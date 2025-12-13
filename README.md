# Student Management System

## Mô tả dự án

Student Management System là một ứng dụng console đơn giản được xây dựng bằng C# và .NET 10.0, được thiết kế để quản lý thông tin sinh viên. Dự án này được phát triển nhằm mục đích học tập và thực hành kiến trúc **Clean Architecture** trong .NET, giúp người học hiểu rõ về cách tổ chức code, phân tách trách nhiệm và áp dụng các nguyên tắc SOLID.

Ứng dụng cung cấp các chức năng cơ bản:

- ✅ Thêm sinh viên mới
- ✅ Cập nhật thông tin sinh viên
- ✅ Xóa sinh viên
- ✅ Xem danh sách tất cả sinh viên
- ✅ Tìm kiếm sinh viên theo tên
- ✅ Sắp xếp sinh viên theo năm sinh (tăng dần/giảm dần)
- ✅ **Lưu trữ dữ liệu linh hoạt**: 
  - Lưu vào **MySQL Database** (mặc định)
  - Lưu vào **file JSON** (tùy chọn)

## Clean Architecture

Dự án này áp dụng mô hình **Clean Architecture** để đảm bảo code dễ bảo trì, dễ test và dễ mở rộng. Clean Architecture chia ứng dụng thành các lớp (layers) độc lập, với nguyên tắc phụ thuộc một chiều từ ngoài vào trong.

![Clean Architecture Diagram](https://camo.githubusercontent.com/e9589f543d14da74faa12e2fbdfa50a2efc32b20ac260d5227658ea14d9e6c6c/68747470733a2f2f626c6f672e636c65616e636f6465722e636f6d2f756e636c652d626f622f696d616765732f323031322d30382d31332d7468652d636c65616e2d6172636869746563747572652f436c65616e4172636869746563747572652e6a7067)

### Cấu trúc dự án

Dự án được tổ chức thành các layer/project sau:

```
StudentManagement/
│
├── Models/                     # Domain Layer - Entities
│   ├── Student.cs             # Domain model của sinh viên
│   └── Models.csproj
│
├── Interfaces/                # Application Layer - Abstractions
│   ├── IStudentRepository.cs  # Interface cho Data Access
│   ├── IStudentService.cs     # Interface cho Business Logic
│   ├── IStoreData.cs          # Interface cho File Storage
│   ├── IDbContext.cs          # Interface cho Database Context
│   └── Interfaces.csproj
│
├── Services/                  # Application Layer - Business Logic
│   ├── StudentService.cs      # Implement business rules và validation
│   └── Services.csproj
│
├── Repositories/              # Infrastructure Layer - Data Access
│   ├── FileStore/             # File-based storage
│   │   ├── StudentFileRepository.cs   # Implement JSON file storage
│   │   └── StudentRepository.cs       # In-memory repository với file sync
│   ├── DatabaseStore/         # Database storage (MySQL)
│   │   ├── DbContext.cs              # Database context và connection
│   │   └── StudentDbRepository.cs    # Implement MySQL data access
│   └── Repositories.csproj
│
├── Helpers/                   # Infrastructure Layer - Utilities
│   ├── InputHelper.cs         # Utility cho input validation
│   └── Utils.csproj
│
├── StudentManagement/         # Presentation Layer - UI
│   ├── Program.cs             # Console UI và menu điều khiển
│   ├── appsettings.json       # Cấu hình database connection string
│   └── StudentManagerment.csproj
│
└── database.sql               # SQL script tạo database và table
```

### Các tầng trong Clean Architecture

#### 1. **Domain Layer (Models)**

- Chứa các entity và business objects cốt lõi
- Không phụ thuộc vào bất kỳ layer nào khác
- `Student.cs`: Định nghĩa thông tin sinh viên (StudentCode, FullName, BirthYear, Major)

#### 2. **Application Layer (Interfaces & Services)**

- **Interfaces**: Định nghĩa các contract (interface) cho repository và service
  - `IStudentRepository`: Định nghĩa các phương thức truy cập dữ liệu
  - `IStudentService`: Định nghĩa các nghiệp vụ xử lý
- **Services**: Implement business logic và validation rules
  - `StudentService`: Xử lý logic nghiệp vụ, validation (StudentCode format, age validation)

#### 3. **Infrastructure Layer (Repositories & Helpers)**

- **Repositories**: Implement data access với nhiều phương án lưu trữ
  - **FileStore**:
    - `StudentRepository`: Quản lý danh sách sinh viên trong memory với đồng bộ file
    - `StudentFileRepository`: Xử lý lưu trữ và đọc dữ liệu từ file JSON (`students.json`)
  - **DatabaseStore** (mới):
    - `DbContext`: Quản lý kết nối MySQL database thông qua connection string từ appsettings.json
    - `StudentDbRepository`: Implement CRUD operations với MySQL database
      - Sử dụng ADO.NET và MySql.Data connector
      - Thực hiện các câu lệnh SQL: SELECT, INSERT, UPDATE, DELETE
      - Parameterized queries để tránh SQL Injection
- **Helpers**: Các utility functions
  - `InputHelper`: Xử lý và validate input từ console

#### 4. **Presentation Layer (StudentManagement)**

- Tầng giao diện người dùng (Console UI)
- `Program.cs`: Hiển thị menu và xử lý tương tác với người dùng
- Dependency Injection: Khởi tạo và inject dependencies vào services

### Ưu điểm của Clean Architecture trong dự án này

- **Separation of Concerns**: Mỗi layer có trách nhiệm riêng biệt
- **Testability**: Dễ dàng unit test từng layer độc lập nhờ interface
- **Maintainability**: Code dễ bảo trì và mở rộng
- **Independence**: Domain layer hoàn toàn độc lập với framework và UI
- **Flexibility**: Dễ dàng chuyển đổi giữa các phương án lưu trữ (JSON file ⇄ MySQL database) mà không ảnh hưởng business logic
  - Chỉ cần thay đổi implementation của `IStudentRepository` trong [Program.cs](StudentManagement/Program.cs)
  - Từ `StudentFileRepository` sang `StudentDbRepository` hoặc ngược lại

## Lưu trữ dữ liệu linh hoạt

Dự án hỗ trợ **2 phương án lưu trữ** dữ liệu, có thể chuyển đổi dễ dàng:

### 1. **MySQL Database** (mặc định)
- Sử dụng `StudentDbRepository` để tương tác với MySQL
- Connection string được cấu hình trong [appsettings.json](StudentManagement/appsettings.json)
- Sử dụng **MySql.Data** package và ADO.NET
- CRUD operations được thực hiện qua SQL commands
- Dữ liệu được lưu trữ vĩnh viễn trong database `student_management`

**Kích hoạt MySQL storage** (hiện tại đang bật):
```csharp
// Trong Program.cs
IStudentRepository repo = new StudentDbRepository();
IStudentService service = new StudentService(repo);
```

### 2. **JSON File** (tùy chọn)
- Sử dụng `StudentRepository` với `StudentFileRepository`
- Dữ liệu được lưu trong file `students.json`
- Tự động serialize/deserialize với System.Text.Json
- Phù hợp cho testing hoặc môi trường không có database

**Chuyển sang JSON storage**:
```csharp
// Trong Program.cs, bỏ comment và comment dòng database
IStoreData storeData = new StudentFileRepository();
IStudentRepository repo = new StudentRepository(storeData.ReadDataToFile());
// IStudentRepository repo = new StudentDbRepository(); // Comment dòng này
```

## Yêu cầu hệ thống

- **.NET SDK 10.0** hoặc cao hơn
- **Visual Studio 2022** hoặc **Visual Studio Code** với C# extension
- **MySQL Server 5.7+** hoặc **MySQL 8.0+** (nếu sử dụng database storage)
- **Git** để clone repository

## Hướng dẫn cài đặt và chạy

### Bước 1: Clone repository từ GitHub

Mở terminal/command prompt và chạy lệnh sau:

```bash
git clone https://github.com/hoangtuanqn/student-management-csharp.git
```

### Bước 2: Cấu hình MySQL Database (nếu sử dụng database storage)

#### 2.1. Cài đặt MySQL Server

Nếu chưa có MySQL Server, tải và cài đặt từ:
- [MySQL Community Server](https://dev.mysql.com/downloads/mysql/)
- Hoặc sử dụng XAMPP/WAMP/MAMP

#### 2.2. Tạo database và table

Mở MySQL Workbench hoặc MySQL Command Line và chạy script từ file `database.sql`:

```bash
mysql -u root -p < database.sql
```

Hoặc copy nội dung file `database.sql` và execute trong MySQL Workbench.

Script sẽ tạo:
- Database: `student_management`
- Table: `students` với các cột: `id`, `student_code`, `full_name`, `birth_year`, `major`

#### 2.3. Cấu hình connection string

Mở file `StudentManagement/appsettings.json` và cập nhật connection string:

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost; Database=student_management; User ID=root; Password=your_password"
  }
}
```

Thay `your_password` bằng mật khẩu MySQL của bạn.

### Bước 3: Restore packages (cài đặt dependencies)

Trước khi build project, cần cài đặt các NuGet packages cần thiết:

```bash
cd student-management-csharp
dotnet restore
```

Lệnh này sẽ tự động cài đặt tất cả packages cần thiết:
- **MySql.Data** (9.5.0) - MySQL connector cho .NET
- **Microsoft.Extensions.Configuration.Json** (10.0.1) - Đọc appsettings.json
- **System.Text.Json** - Xử lý JSON file

### Bước 4: Build và chạy project

#### Cách 1: Sử dụng Visual Studio

1. Mở **Visual Studio 2022**
2. Chọn **File** → **Open** → **Project/Solution**
3. Điều hướng đến thư mục vừa clone và chọn file `StudentManagement.slnx`
4. Click **Open**
5. Visual Studio sẽ tự động restore packages (kiểm tra Output window)
6. Trong **Solution Explorer**, click chuột phải vào project `StudentManagement` và chọn **Set as Startup Project**
7. Nhấn **F5** hoặc click nút **Start** (▶️) để chạy

#### Cách 2: Sử dụng Command Line

```bash
cd student-management-csharp

# Build solution
dotnet build

# Chạy project StudentManagement
cd StudentManagement
dotnet run
```

#### Cách 3: Sử dụng Visual Studio Code

1. Mở thư mục dự án trong VS Code
2. Mở terminal (**Terminal** → **New Terminal**)
3. Chạy lệnh:

```bash
cd StudentManagement
dotnet run
```

## Sử dụng ứng dụng

Sau khi chạy thành công, bạn sẽ thấy menu như sau:

```
--- STUDENT MANAGEMENT SYSTEM ---
1. Add student
2. Update student
3. Delete student
4. View all students
---------------------------------
5. Search student
6. Sort students
---------------------------------
7. Exit program
```

#### Hướng dẫn sử dụng các chức năng:

**1. Add student (Thêm sinh viên)**

- Nhập họ tên đầy đủ
- Nhập mã sinh viên (format: SExxxxxx, ví dụ: SE123456)
- Nhập năm sinh (tuổi phải từ 18-30)
- Nhập chuyên ngành

**2. Update student (Cập nhật sinh viên)**

- Nhập mã sinh viên cần cập nhật
- Nhập thông tin mới (có thể bỏ qua để giữ nguyên giá trị cũ)

**3. Delete student (Xóa sinh viên)**

- Nhập mã sinh viên cần xóa

**4. View all students (Xem danh sách)**

- Hiển thị toàn bộ danh sách sinh viên

**5. Search student (Tìm kiếm)**

- Nhập tên (hoặc một phần tên) để tìm kiếm

**6. Sort students (Sắp xếp)**

- Chọn sắp xếp tăng dần hoặc giảm dần theo năm sinh

**7. Exit program (Thoát)**

- Thoát khỏi chương trình

### Lưu trữ dữ liệu

Ứng dụng tự động lưu dữ liệu sinh viên vào file `students.json` sau mỗi thao tác thêm, sửa, hoặc xóa. File JSON được lưu tại thư mục gốc của ứng dụng với định dạng:

```json
[
  {
    "StudentCode": "SE200947",
    "FullName": "Pham Hoang Tuan",
    "BirthYear": 2006,
    "Major": "KTPM"
  },
  {
    "StudentCode": "SE200948",
    "FullName": "Nguyen Thanh Tuyen",
    "BirthYear": 2002,
    "Major": "KTPM"
  }
]
```

**Đặc điểm:**

- Dữ liệu được lưu tự động, không cần thao tác thủ công
- Khi khởi động lại ứng dụng, dữ liệu sẽ được load từ file `students.json`
- File JSON sử dụng UTF-8 encoding, hỗ trợ hiển thị tiếng Việt có dấu
- Nếu file không tồn tại, hệ thống sẽ tạo mới khi có dữ liệu đầu tiên

## Quy tắc validation

Ứng dụng có các quy tắc validation sau:

- **Mã sinh viên**: Phải có định dạng `SExxxxxx` (SE + 6 chữ số)
  - Ví dụ hợp lệ: SE123456, SE200947
  - Ví dụ không hợp lệ: SE12345, ABC123456
- **Tuổi sinh viên**: Phải từ 18 đến 30 tuổi
  - Được tính dựa trên năm sinh và năm hiện tại
- **Mã sinh viên duy nhất**: Không được trùng lặp trong hệ thống

## Cấu trúc Dependencies

```
StudentManagement (Presentation)
    ↓
    ├─→ Services (Business Logic)
    ├─→ Repositories (Data Access)
    ├─→ Helpers (Utilities)
    ├─→ Interfaces (Contracts)
    └─→ Models (Domain)

Services
    └─→ Interfaces

Repositories
    └─→ Interfaces

Interfaces
    └─→ Models
```

## Công nghệ sử dụng

- **Ngôn ngữ**: C# 12
- **Framework**: .NET 10.0
- **Architecture Pattern**: Clean Architecture
- **Database**: MySQL 8.0+ (MySql.Data 9.5.0)
- **Data Storage**: 
  - MySQL Database (primary)
  - JSON File với System.Text.Json (alternative)
- **Configuration**: Microsoft.Extensions.Configuration.Json
- **Design Patterns**:
  - Repository Pattern
  - Dependency Injection
  - Interface Segregation
  - Strategy Pattern (cho việc chuyển đổi storage)

## Tác giả

**Pham Hoang Tuan**

- GitHub: [@hoangtuanqn](https://github.com/hoangtuanqn)
- Repository: [student-management-csharp](https://github.com/hoangtuanqn/student-management-csharp)

## Mục đích học tập

Dự án này được tạo ra với mục đích:

- Học và thực hành Clean Architecture trong .NET
- Hiểu về Dependency Injection và Inversion of Control
- Áp dụng Repository Pattern với nhiều implementation
- Thực hành SOLID principles
- Làm quen với cấu trúc multi-project solution trong .NET
- Học cách làm việc với MySQL database trong C#
- Thực hành ADO.NET và parameterized queries
- Hiểu về Strategy Pattern để chuyển đổi data source linh hoạt

## Tính năng nổi bật

✨ **Dual Storage Support**: Chuyển đổi dễ dàng giữa MySQL và JSON file chỉ bằng vài dòng code

🏗️ **Clean Architecture**: Tách biệt rõ ràng các layer, dễ test và maintain

🔒 **Security**: Sử dụng parameterized queries để tránh SQL Injection

⚙️ **Configuration-based**: Database connection string được quản lý qua appsettings.json

🎯 **SOLID Principles**: Code được tổ chức theo các nguyên tắc SOLID

## Lưu ý khi sử dụng

### Chuyển đổi giữa MySQL và JSON

Mở file [Program.cs](StudentManagement/Program.cs) và thay đổi code khởi tạo repository:

**Sử dụng MySQL** (mặc định):
```csharp
IStudentRepository repo = new StudentDbRepository();
IStudentService service = new StudentService(repo);
```

**Sử dụng JSON File**:
```csharp
IStoreData storeData = new StudentFileRepository();
IStudentRepository repo = new StudentRepository(storeData.ReadDataToFile());
IStudentService service = new StudentService(repo);
```

### Troubleshooting

**Lỗi kết nối MySQL**:
- Kiểm tra MySQL Server đã chạy chưa
- Kiểm tra connection string trong appsettings.json
- Đảm bảo database `student_management` đã được tạo
- Kiểm tra username/password MySQL

**Lỗi "Students table doesn't exist"**:
- Chạy lại script `database.sql` để tạo table

**Lỗi package MySql.Data**:
```bash
cd Helpers
dotnet restore
```

## License

Dự án này được tạo ra cho mục đích học tập và có thể tự do sử dụng.

---

**Ghi chú**: Đây là một dự án học tập về Clean Architecture và quản lý dữ liệu. Một số điểm cần cải thiện cho production:

- ✅ **Đã implement**: MySQL database với ADO.NET và parameterized queries
- ✅ **Đã implement**: Dual storage (MySQL + JSON file)
- ✅ **Đã implement**: Configuration management với appsettings.json
- 🔄 **Có thể mở rộng**: 
  - Sử dụng Entity Framework Core thay vì ADO.NET
  - Implement async/await cho database operations
  - Thêm comprehensive error handling và logging
  - Implement unit tests và integration tests
  - Sử dụng dependency injection container
  - Thêm data validation phức tạp hơn
  - Connection pooling và retry logic
  - Database migration management
