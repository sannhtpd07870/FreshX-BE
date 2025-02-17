using Microsoft.AspNetCore.Authentication.JwtBearer; // Sử dụng cho việc xác thực JWT
using Microsoft.IdentityModel.Tokens; // Sử dụng cho Token Validation Parameters
using Microsoft.EntityFrameworkCore; // Sử dụng cho Entity Framework Core
using System.Text; // Sử dụng cho Encoding
using API; // Namespace chứa ApplicationDbContext
using API.Server.Interfaces; // Namespace chứa các interface dịch vụ
using API.Server.Services;
using API.Services;
using API.Hubs;
using API.Interfaces; // Namespace chứa các implement dịch vụ
using API.Interfaces; // Namespace chứa các implement dịch vụ
using API.Interfaces;
using System.Text.Json.Serialization; // Namespace chứa các implement dịch vụ
using API.Interfaces;
using System.Text.Json.Serialization;
using Quartz.Spi;
using Quartz;
using Quartz.Impl;
using API.EmailAotu; // Namespace chứa các implement dịch vụ
using SendGrid.Helpers.Mail; // Namespace chứa các implement dịch vụ

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Cấu hình SignalR
        builder.Services.AddSignalR();

        // Set up configuration
        builder.Configuration.AddJsonFile("appsettings.json"); // Đọc cấu hình từ appsettings.json
        var connectionString = builder.Configuration.GetConnectionString("DbContext");

        // Register database context
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString); // Đăng ký ApplicationDbContext với chuỗi kết nối từ appsettings.json
        });

        // Đọc cấu hình từ appsettings.json
        var jwtSettings = builder.Configuration.GetSection("Jwt");

        var secretKey = jwtSettings["Key"];
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];
        var key = Encoding.ASCII.GetBytes(secretKey); // Mã hóa secret key thành mảng byte

        // Đăng ký JwtTokenService với DI container
        builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>(); // Đăng ký JwtTokenService với DI container

        // Register AutoMapper
        builder.Services.AddAutoMapper(typeof(Program).Assembly);


        builder.Services.AddControllers(); // Đăng ký các controller


        // Cấu hình xác thực JWT
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Đặt mặc định cho xác thực sử dụng JWT Bearer
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Đặt mặc định cho thách thức xác thực sử dụng JWT Bearer
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, // Không cần xác thực issuer
                ValidateAudience = false, // Không cần xác thực audience
                ValidateLifetime = true, // Xác thực thời gian sống của token
                ValidateIssuerSigningKey = true, // Xác thực khóa ký token
                ValidIssuer = issuer, // Cấu hình Issuer hợp lệ
                ValidAudience = audience, // Cấu hình Audience hợp lệ
                IssuerSigningKey = new SymmetricSecurityKey(key), // Khóa ký token
                ClockSkew = TimeSpan.Zero // Không cho phép chênh lệch thời gian
            };
        });

        // Register HttpClient
        builder.Services.AddHttpClient(); // Đăng ký HttpClient cho DI container

        // Register services and repositories
        builder.Services.AddScoped<IRoleService, RoleService>(); // Đăng ký dịch vụ AccountService
        builder.Services.AddScoped<IAccountEmpService, AccountEmpService>(); // Đăng ký dịch vụ AccountService
        builder.Services.AddScoped<IChatMessageService, ChatMessageService>();                                                                     // Đăng ký dịch vụ ChatMessageService                                                                                                            // Đăng ký dịch vụ ChatSessionService
        builder.Services.AddScoped<IChatSessionService, ChatSessionService>();
        builder.Services.AddScoped<IAppointmentService, AppointmentService>();
        builder.Services.AddScoped<IAccountCusService, AccountCusService>();


        builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>(); // MedicalRecordService
                                                                                   //Đoạn mã trên cấu hình các tùy chọn serialize JSON cho ứng dụng ASP.NET Core(khó hiểu quá thì copy tra chat nhé ae) 
        builder.Services.AddScoped<IFeedbackService, FeedbackService>();
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
        });

        // Add controllers and Swagger
        builder.Services.AddControllers(); // Đăng ký các controller
        builder.Services.AddEndpointsApiExplorer(); // Thêm API Explorer cho Swagger
        builder.Services.AddSwaggerGen(); // Thêm Swagger để tạo tài liệu API



        // Add Quartz services
        builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
        builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
        builder.Services.AddSingleton<CallApiJob>();
        builder.Services.AddHostedService<QuartzHostedService>();

        builder.Services.AddSingleton(new JobSchedule(
            jobType: typeof(CallApiJob),
            cronExpression: "0 17 13 * * ?" // Lịch trình gửi email lúc 17:30 hàng ngày
        ));

        builder.Services.AddSingleton(new JobSchedule(
            jobType: typeof(CallApiJob),
            cronExpression: "0 * * * * ?" // Lịch trình gửi email mỗi phút
        ));

        var app = builder.Build(); // Xây dựng ứng dụng

        app.UseCors(builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader(); // Cấu hình CORS
        }); // Kích hoạt chính sách CORS

        // Configure middleware pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(); // Sử dụng Swagger trong môi trường phát triển
            app.UseSwaggerUI(); // Sử dụng giao diện Swagger UI
            app.UseDeveloperExceptionPage(); // Sử dụng trang lỗi dành cho nhà phát triển
        }
        else
        {
            app.UseHsts(); // Sử dụng HSTS trong môi trường sản xuất
        }
        app.UseSwagger(); // Sử dụng Swagger

        app.UseSwaggerUI(); // Sử dụng giao diện Swagger UI
        app.UseHttpsRedirection(); // Chuyển hướng các yêu cầu HTTP sang HTTPS
        app.UseStaticFiles(); // Phục vụ các tệp tĩnh
        app.UseRouting(); // Kích hoạt định tuyến



        app.UseAuthentication(); // Kích hoạt middleware xác thực
        app.UseAuthorization(); // Kích hoạt middleware phân quyền

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Định tuyến đến các controller
        });
        // Cấu hình SignalR
        app.MapHub<ChatHub>("/chathub");

        app.Run(); // Chạy ứng dụng
    }
}
