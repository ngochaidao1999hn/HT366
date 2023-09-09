using AutoFixture;
using HT366.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace HT366.Test.Utils
{
    public static class Helper
    {
        private static readonly Fixture fixture = new();
        private static Guid id;

        public static Guid GenerateId()
        {
            id = new Guid();
            return id;
        }

        public static ControllerContext CreateUserContext(string userId, bool? isAdmin = null)
        {
            var principal = new Mock<ClaimsPrincipal>();
            principal.Setup(p => p.FindFirst(It.IsAny<string>())).Returns(new Claim("sub", userId));
            if (isAdmin.HasValue && isAdmin.Value)
            {
                principal.Setup(p => p.FindAll(ClaimTypes.Role)).Returns(new List<Claim>() { new Claim(ClaimTypes.Role, "Admin") });
            }
            var contextMock = new Mock<HttpContext>();
            contextMock.ExpectGet(ctx => ctx.User)
                    .Returns(principal.Object);

            var controllerContext = new ControllerContext
            {
                HttpContext = contextMock.Object
            };
            return controllerContext;
        }

        public static ApplicationUser CreateUser()
        {
            return fixture.Build<ApplicationUser>()
                .With(x => x.UserName, "Test User")
                .With(x => x.Email, "test@gmail.com")
                .With(x => x.PhoneNumber, "12345678")
                .With(x => x.EmailConfirmed, true)
                .With(x => x.PhoneNumberConfirmed, true)
                .Create();
        }
    }
}