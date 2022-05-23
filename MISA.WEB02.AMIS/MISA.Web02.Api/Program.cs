using MISA.Infrastructure.Respository;
using MISA.Web02.Core.Interfaces.Service;
using MISA.Web02.Core.Interfaces.Repository;
using MISA.Web02.Core.Interfaces.Base;
using MISA.Web02.Core.Services;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//cấu hình DI(Dependence Injection)
//employee
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
//department
builder.Services.AddScoped<IDepartmentRepository, DepartmentRespository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
//vendor
builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddScoped<IVendorService, VendorService>();
//payment
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
//payment
builder.Services.AddScoped<IPaymentDetailRepository, PaymentDetailRepository>();
builder.Services.AddScoped<IPaymentDetailService, PaymentDetailService>();
//payment
builder.Services.AddScoped<ITableOptionRepository, TableOptionRepository>();
builder.Services.AddScoped<ITableOptionService, TableOptionService>();
//payment
builder.Services.AddScoped<IVendorGroupRepository, VendorGroupRepository>();
builder.Services.AddScoped<IVendorGroupService, VendorGroupService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});
builder.Services.AddMvc()
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
