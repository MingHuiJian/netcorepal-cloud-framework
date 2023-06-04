using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace NetCorePal.Extensions.Mappers.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void AddMapperPrivider_Test()
        {
            ServiceCollection services = new ServiceCollection();

            var assembly = Assembly.GetExecutingAssembly();

            services.AddMapperPrivider(assembly);
            Assert.Equal(2, services.Count());
            var serviceProvider = services.BuildServiceProvider();
            var mapperProvider = serviceProvider.GetService<IMapperProvider>();
            Assert.NotNull(mapperProvider);
            var myMapper = serviceProvider.GetService<IMapper<From, To>>();
            
            Assert.NotNull(myMapper);
        }


        [Fact]
        public void ABc()
        {
            var I = typeof(MyMapper).GetInterfaces();
        }

        class From
        {
            public string? Name { get; init; }
        }

        class To
        {
            public string? Name2 { get; init; }
        }


        public class MyMapper : IMapper<From, To>
        {
            To IMapper<From, To>.To(From from)
            {
                if (from == null) throw new ArgumentNullException(nameof(from));
                return new To { Name2 = from.Name };
            }
        }
    }
}