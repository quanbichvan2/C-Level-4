namespace Lab_4_1_2_Net_4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            //Hỗ trợ route, nếu không có nó thì thằng useEndpoint dưới ko đc dùng, nó đang định nghĩa cho middleware của mình
            app.UseRouting();

            //Cách sử dụng route phù hợp trên .net 6
            //Thêm nữa không dùng được ATTRIBUTE ROUTES nếu không khai báo ông nội bên dưới.
            //Bởi vì useEnpoint trả về cho mình 1 cái đầu cuối (middleware), nó đang trả về 1 cái router default. Trong trường hợp default không có enpoint thì nó sẽ không biết trả về đâu, nên sau cùng nó sẽ lỗi.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");

                //Dùng mapRoute để route nhưng thích dùng attribute routes
                // mapRoute thì được định nghĩa rõ ràng hơn attribute routes
                //endpoints.MapControllerRoute(
                //    name: "student",
                //    pattern: "{controller}/{action}/{id}",
                //    new { controller = "Home", action = "Details" }, new { id = new IntRouteConstraint() });
            });
            app.Run();
        }
    }
}