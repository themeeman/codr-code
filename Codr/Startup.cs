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
                new TextComponent(@"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut fermentum imperdiet hendrerit. Ut et dignissim elit. Fusce turpis tortor, fermentum nec pellentesque nec, sollicitudin a est. Donec sed tempor nibh. Mauris quis aliquam metus. Sed laoreet nisl vitae erat suscipit pellentesque. Fusce ac neque et tortor mollis tristique ut id magna. Morbi feugiat turpis id nulla lobortis congue. Nullam a molestie enim.
Morbi ut ex in mi facilisis placerat quis sit amet urna. Pellentesque sodales et nisi ac vestibulum. Vestibulum sodales tellus mauris, vitae blandit tellus commodo ut. Nunc aliquam velit eu blandit laoreet. Proin luctus non metus vitae faucibus. Etiam aliquet ac urna in tristique. Mauris eu pulvinar sem. Morbi efficitur enim id nunc porttitor, nec maximus nisl mollis. Sed porta turpis non tincidunt cursus. Vestibulum nibh eros, maximus at turpis vitae, aliquam posuere tortor.
Praesent quis ligula hendrerit, tempus nisl id, cursus nibh. Curabitur nec consequat metus. Curabitur vestibulum lorem tellus, et blandit nisi blandit vitae. Phasellus suscipit ante eleifend lacus rutrum cursus. Curabitur tempus, lorem ut convallis tempus, nulla mi efficitur mauris, eget ornare nisl neque ac justo. Vestibulum congue nulla at elit mollis, sit amet dictum mi pharetra. Vestibulum vel sagittis turpis.
Cras porta varius libero non vestibulum. Aliquam erat volutpat. Phasellus metus metus, pulvinar at porttitor non, vestibulum eu massa. Ut congue lectus eget diam convallis convallis. Cras elementum ipsum lectus. Nulla augue massa, volutpat ut mauris vel, consequat malesuada elit. Duis consequat volutpat blandit. Nunc nisl risus, rutrum nec nulla eu, consectetur sagittis nibh. Integer sollicitudin tempor massa vel feugiat. Proin sit amet orci eu mauris blandit viverra. In sed quam sit amet purus scelerisque faucibus. Nam iaculis libero sit amet leo aliquam accumsan. Proin et vulputate risus, sit amet cursus erat.
Sed a nulla turpis. Pellentesque id sagittis ante, quis accumsan mauris. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Aenean auctor consequat tellus sit amet rhoncus. Cras ultrices pharetra tortor, vel vestibulum eros bibendum non. Nunc imperdiet aliquet orci, at feugiat lacus porta non. Nam vel nunc velit. Aenean tristique justo id risus tristique, at condimentum mi pulvinar. Praesent quis libero pulvinar, volutpat nisi a, lobortis mi. Morbi auctor ante felis, non dictum tellus lacinia id. Nunc iaculis finibus odio quis tempus. Vivamus eu vestibulum erat. Phasellus condimentum imperdiet lacinia. Vivamus finibus tortor id ligula egestas blandit. Maecenas neque libero, posuere at augue ac, sollicitudin convallis nisl."
                )
            }));
            post.Components.Add(new Code(@"#include <iostream>
auto main() -> int {
    std::cout << ""This is C++ Code\n"";
}", Models.Language.CPP));
            post.Components.Add(new Link("https://www.example.com/", "Example URL"));
            post.Components.Add(new Image("https://upload.wikimedia.org/wikipedia/commons/thumb/8/81/Parque_Eagle_River%2C_Anchorage%2C_Alaska%2C_Estados_Unidos%2C_2017-09-01%2C_DD_02.jpg/1280px-Parque_Eagle_River%2C_Anchorage%2C_Alaska%2C_Estados_Unidos%2C_2017-09-01%2C_DD_02.jpg"));
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
