var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // ett�l lesz k�pes API kontrollerekre
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // ezen kereszt�l lehet az API kontrollereket kipr�b�lni ahogy volt is

var app = builder.Build();

if (app.Environment.IsDevelopment()) // csak fejleszt�shez vannak enged�lyezve, ha pl feltolom Azure-be akkor nem
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDefaultFiles(); // index.html-t vagy default.html-t meg ilyeneket keres
app.MapControllers();
app.UseStaticFiles(); // van-e olyan kontroller aminek az el�r�si �tja oda van be�ll�tva amit k�rt�l ha nincs default file

// SORREND FONTOS

app.Run();