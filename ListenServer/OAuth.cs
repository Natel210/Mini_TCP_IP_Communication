using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ListenServer
{
    internal class OAuth
    {
        string _identityServerUrl = "https://your-identity-server.com";
        string _clientId = "your-client-id";
        string _clientSecret = "your-client-secret";
        string _apiResource = "your-api-resource";

        // IdentityServer4를 초기화하는 메서드
        public static void InitializeIdentityServer()
        {
            // IdentityServer4 빌더 생성
            var builder = IdentityServer4.Models.IdentityServer4.CreateDefaultBuilder()
                .AddInMemoryApiResources(new List<ApiResource>())
                .AddInMemoryIdentityResources(new List<IdentityResource>())
                .AddInMemoryClients(new List<Client>())
                .AddTestUsers(new List<TestUser> {
                    new TestUser {
                        SubjectId = "1",
                        Username = "john",
                        Password = "password",
                        Claims = new List<Claim> {
                            new Claim("name", "John Doe"),
                            new Claim("email", "john@example.com"),
                            new Claim("role", "admin")
                        }
                    }
                });

            // IdentityServer4 인스턴스 생성 및 실행
            var server = builder.Build();
            server.Start();

            Console.WriteLine("IdentityServer4 started.");
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.Configure(app =>
                   {
                       // IdentityServer4를 애플리케이션에 구성
                       app.UseIdentityServer();
                       app.Run(async context =>
                       {
                           // 인증된 사용자 정보 확인
                           var user = context.User;
                           if (user.Identity.IsAuthenticated)
                           {
                               Console.WriteLine("사용자 인증됨.");
                               Console.WriteLine($"사용자명: {user.Identity.Name}");

                               // 사용자 클레임 확인
                               foreach (var claim in user.Claims)
                               {
                                   Console.WriteLine($"클레임: {claim.Type} = {claim.Value}");
                               }
                           }
                           else
                           {
                               Console.WriteLine("사용자 인증되지 않음.");
                           }

                           await context.Response.WriteAsync("IdentityServer4 Example");
                       });
                   });
               });

    }
}
