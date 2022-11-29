var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // ettõl lesz képes API kontrollerekre
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // ezen keresztül lehet az API kontrollereket kipróbálni ahogy volt is

var app = builder.Build();

if (app.Environment.IsDevelopment()) // csak fejlesztéshez vannak engedélyezve, ha pl feltolom Azure-be akkor nem
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDefaultFiles(); // index.html-t vagy default.html-t meg ilyeneket keres
app.MapControllers();
app.UseStaticFiles(); // van-e olyan kontroller aminek az elérési útja oda van beállítva amit kértél ha nincs default file

// SORREND FONTOS

app.Run();