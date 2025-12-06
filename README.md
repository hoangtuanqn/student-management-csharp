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

## Clean Architecture

Dự án này áp dụng mô hình **Clean Architecture** để đảm bảo code dễ bảo trì, dễ test và dễ mở rộng. Clean Architecture chia ứng dụng thành các lớp (layers) độc lập, với nguyên tắc phụ thuộc một chiều từ ngoài vào trong.

![Clean Architecture Diagram](https://blog.cleancoder.com/uncle-bob/images/2012-08-13-the-clean-architecture/CleanArchitecture.jpg)

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
│   └── Interfaces.csproj
│
├── Services/                  # Application Layer - Business Logic
│   ├── StudentService.cs      # Implement business rules và validation
│   └── Services.csproj
│
├── Repositories/              # Infrastructure Layer - Data Access
│   ├── StudentRepository.cs   # Implement data storage (in-memory)
│   └── Repositories.csproj
│
├── Helpers/                   # Infrastructure Layer - Utilities
│   ├── InputHelper.cs         # Utility cho input validation
│   └── Utils.csproj
│
└── StudentManagement/         # Presentation Layer - UI
    ├── Program.cs             # Console UI và menu điều khiển
    └── StudentManagerment.csproj
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
- **Repositories**: Implement data access, hiện tại sử dụng in-memory storage
  - `StudentRepository`: Quản lý danh sách sinh viên trong memory
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
- **Flexibility**: Dễ dàng thay đổi implementation (ví dụ: từ in-memory sang database) mà không ảnh hưởng business logic

## Yêu cầu hệ thống

- **.NET SDK 10.0** hoặc cao hơn
- **Visual Studio 2022** hoặc **Visual Studio Code** với C# extension
- **Git** để clone repository

## Hướng dẫn cài đặt và chạy

### Bước 1: Clone repository từ GitHub

Mở terminal/command prompt và chạy lệnh sau:

```bash
git clone https://github.com/hoangtuanqn/student-management-csharp.git
```

Sau đó di chuyển vào thư mục dự án:

```bash
cd student-management-csharp
```

### Bước 2: Mở dự án trong Visual Studio

1. Mở **Visual Studio 2022**
2. Chọn **File** → **Open** → **Project/Solution**
3. Điều hướng đến thư mục vừa clone và chọn file `StudentManagement.slnx`
4. Click **Open**

### Bước 3: Cấu hình Startup Project

**Quan trọng**: Cần đặt `StudentManagement` làm startup project

1. Trong **Solution Explorer**, tìm project `StudentManagement`
2. Click chuột phải vào project `StudentManagement`
3. Chọn **Set as Startup Project**
4. Project `StudentManagement` sẽ được highlight (in đậm)

### Bước 4: Build và chạy ứng dụng

#### Cách 1: Sử dụng Visual Studio
1. Nhấn **F5** hoặc click nút **Start** (▶️) trên toolbar
2. Hoặc chọn **Debug** → **Start Debugging**

#### Cách 2: Sử dụng Command Line
```bash
# Build solution
dotnet build

# Chạy project StudentManagement
cd StudentManagement
dotnet run
```

#### Cách 3: Sử dụng Visual Studio Code
1. Mở thư mục dự án trong VS Code
2. Mở terminal trong VS Code (**Terminal** → **New Terminal**)
3. Chạy lệnh:
```bash
cd StudentManagement
dotnet run
```

### Bước 5: Sử dụng ứng dụng

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
- **Design Patterns**: 
  - Repository Pattern
  - Dependency Injection
  - Interface Segregation

## Tác giả

**Pham Hoang Tuan**
- GitHub: [@hoangtuanqn](https://github.com/hoangtuanqn)
- Repository: [student-management-csharp](https://github.com/hoangtuanqn/student-management-csharp)

## Mục đích học tập

Dự án này được tạo ra với mục đích:
- Học và thực hành Clean Architecture trong .NET
- Hiểu về Dependency Injection và Inversion of Control
- Áp dụng Repository Pattern
- Thực hành SOLID principles
- Làm quen với cấu trúc multi-project solution trong .NET

## License

Dự án này được tạo ra cho mục đích học tập và có thể tự do sử dụng.

---

**Note**: Đây là một dự án học tập đơn giản. Trong thực tế, bạn nên:
- Sử dụng database thay vì in-memory storage
- Thêm logging và error handling tốt hơn
- Implement unit tests cho từng layer
- Sử dụng dependency injection container (như Microsoft.Extensions.DependencyInjection)
- Thêm data validation phức tạp hơn