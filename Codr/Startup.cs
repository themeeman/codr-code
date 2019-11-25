using Codr.Models.Posts;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codr {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            using var session = DocumentStoreHolder.Store.OpenSession();
            var post = new Post(author: "12345678");
            post.Components.Add(new Heading(content: "Hello World"));
            post.Components.Add(new Text(new List<TextComponent> {
                new TextComponent("Text can be "),
                new TextComponent("bold and italics", TextStyle.Bold | TextStyle.Italics),
            }));
            post.Components.Add(new Code(@"#include <iostream>
int main() {
    std::cout << ""This is C++ Code\n"";
}", Models.Language.CPP));
            string s = Newtonsoft.Json.JsonConvert.SerializeObject(post);

            session.Store(post);
            session.SaveChanges();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
