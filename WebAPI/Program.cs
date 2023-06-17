using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/Coupon/GetAllCoupons", (ILogger<Program> _logger) =>
{
    _logger.LogInformation("=>>>>>>> api/Coupon =>>>>>>> Getting all coupons");

    return Results.Ok(CouponStore.Coupons.ToList());

}).Produces<IEnumerable<Coupon>>(200);

app.MapGet("/api/Coupon/{id:int}", (int id,ILogger<Program> _logger) =>
{
    var coupon = CouponStore.Coupons.FirstOrDefault(x => x.Id == id);

    if (coupon == null)
    {
        _logger.LogInformation("=>>>>>>> api/Coupon =>>>>>>> get coupon by id not found !");
        return Results.NotFound("Data not found !");
    }

    _logger.LogInformation("=>>>>>>> api/Coupon =>>>>>>> Getting coupon by Id");

    return Results.Ok(coupon);

}).WithName("GetCouponById");

app.MapPost("/api/Coupon/AddNewCoupon", ([FromBody]Coupon coupon, ILogger<Program> _logger) =>
{
    if(coupon.Id!=0 || coupon.Name == string.Empty)
    {
        _logger.LogError("=>>>>>>> api/Coupon =>>>>>>> add new coupon invalid id or Name");
        return Results.BadRequest("Invalid id or Name");
    }
    _logger.LogInformation("=>>>>>>> api/Coupon =>>>>>>> Add new coupon");

    coupon.Id = CouponStore.Coupons.LastOrDefault().Id+1;
    coupon.CreatedAt = DateTimeOffset.UtcNow;

    CouponStore.Coupons.Add(coupon);

    return Results.CreatedAtRoute("GetCouponById", new {id=coupon.Id},coupon);

}).WithName("AddNewCoupon").Accepts<Coupon>("application/json").Produces<Coupon>(201).Produces<Coupon>(400);

app.MapPut("/api/Coupon/UpdateCoupon", ([FromBody]Coupon model, ILogger<Program> _logger) =>
{
    var coupon = CouponStore.Coupons.FirstOrDefault(x => x.Id == model.Id);

    if (coupon == null)
    {
        _logger.LogInformation("=>>>>>>> api/Coupon =>>>>>>> update coupon by id, coupon not found");
        return Results.NotFound("Data not found !");
    }

    _logger.LogInformation("=>>>>>>> api/Coupon =>>>>>>> update coupon");

    coupon.UpdatedAt = DateTimeOffset.UtcNow;
    coupon.Name = model.Name;
    coupon.Percent = model.Percent;
    coupon.IsActive = model.IsActive;

    return Results.Ok("Data successfully updated !");
});

app.MapDelete("/api/Coupon/DeleteCoupon", (int id, ILogger<Program> _logger) =>
{
    var coupon = CouponStore.Coupons.FirstOrDefault(x => x.Id == id);

    if (coupon == null)
    {
        _logger.LogInformation("=>>>>>>> api/Coupon =>>>>>>> delete coupon by id, coupon not found");
        return Results.NotFound("Data not found !");
    }

    _logger.LogInformation("=>>>>>>> api/Coupon =>>>>>>> delete coupon");

    CouponStore.Coupons.Remove(coupon);
    return Results.Ok("Data successfully deleted !");
});

app.UseHttpsRedirection();
app.Run();
