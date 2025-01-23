using POC_PayMob.Binders;
using POC_PayMob.Filters;
using POC_PayMob.Services;

namespace POC_PayMob {
    public class Program {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add CORS services
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    policy.WithOrigins("https://example.com", "https://anotherexample.com") // Replace with your allowed origin(s)
                          .AllowAnyHeader() //"Content-Type", "Authorization"
                          .AllowAnyMethod()//"GET", "POST", "PUT", "DELETE"
                          .AllowCredentials();// to support credentials (e.g., cookies, authorization headers)

                     // To Handle the preflight request (HTTP OPTIONS) which sent by Browsers 
                    // policy.WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS"); 
                });
            });
          
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient<PaymobService>();
            // Register the HMAC filter
            builder.Services.AddScoped<HmacValidationFilter>();
            // Register the CallBackTransactionModel Binder Provider
            builder.Services.AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0, new CallBackTransactionModelBinderProvider());
            });


            var app = builder.Build();
            // Use CORS middleware
            app.UseCors("AllowSpecificOrigin");
            app.Use(async (context, next) =>
            {
                // Enable buffering for the request body
                context.Request.EnableBuffering();

                // Rewind the stream so the body can be read again
                context.Request.Body.Position = 0;

                // Call the next middleware
                await next();
            });
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
