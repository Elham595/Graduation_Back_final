using graduation_project.Data;
using graduation_project.Models;
using graduation_project.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.SignalR;
using System.Text;
using graduation_project;
using graduation_project.Service;
using IdentityModel;

var builder = WebApplication.CreateBuilder(args);
//variable for the core
string MyAllowSpecificOrigins = "";
// Add services to the container.
//DbConext Service
builder.Services.AddDbContext<FashionDesignContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("projectcon"),
        builderOptions =>
        {
            builderOptions.EnableRetryOnFailure();
        });
});
//REFERENCE LOOP HANDLING
builder.Services.AddControllers().AddNewtonsoftJson(n=>n.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);
//=======
builder.Services.AddControllers();
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped(typeof(BaseRepositoryServices<,>), typeof(IBaseRepositoryServices<>));
builder.Services.AddScoped(typeof(DbContextRepositoryServices<>));
builder.Services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//.AddJwtBearer(options =>
//{
//    //options.SaveToken = true;
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateLifetime = true,
//        ValidateAudience = false,
//        ValidateIssuer = false,
//        //ValidAudience = builder.Configuration["Jwt:Audience"],
//        //ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("user_signature_12345678")) //builder.Configuration["Jwt: Key"]

/**********************JWT Validate**********************/
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{

    var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);

    o.SaveToken = true;

    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key),
        NameClaimType = JwtClaimTypes.PreferredUserName,
        RoleClaimType = JwtClaimTypes.Role
    };
}
);

//cores service

builder.Services.AddCors(option =>
{
    //option.AddPolicy(MyAllowSpecificOrigins, policy =>
    //{
    //    policy.AllowAnyHeader();
    //    policy.AllowAnyMethod();
    //    policy.AllowAnyOrigin();
    //});
    option.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200",
            "http://localhost:4200",
            "http://angularstite.localtest.com",
            "http://angularstite.localtest.com").AllowAnyMethod()
            .AllowAnyHeader().AllowCredentials();
    });
});

builder.Services.AddSingleton(new HashSet<UserConnection>());
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
//=======


app.UseHttpsRedirection();



app.UseRouting();

//>>>>>>> Stashed changes
//cores midleware///MyAllowSpecificOrigins
app.UseCors();
//authentication & aythorization
app.UseAuthentication();
app.UseAuthorization();
//<<<<<<< Updated upstream
//=======

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.WebRootPath, "Images")),
    RequestPath = new PathString("/DesginImages")
});
//chat
app.MapHub<ChatHub>("/chat");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();
