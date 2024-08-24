
namespace WebMain.App_Start
{
    using SimpleInjector;
    using SimpleInjector.Lifestyles;
    using SimpleInjector.Integration.WebApi;
    using Core.Dominio;
    using FluentValidation;
    using System.Web.Http;

    using Tareo.Persistencia.UnitOfWork;
    using Tareo.Dominio.Entidades;
    using Tareo.Aplicacion.Dto;
    using Tareo.Persistencia;
    using Web.Sockets;
    using Tareo.Persistencia.Repositorio;
    using Tareo.Transversal;
    using Tareo.Dominio.Entidades.SCode.EntidadesReporte;

    public class InjectorConfig
    {
        public static void RegisterInjector()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            /**
             * Registrar las Unidades de Trabajo 
             */
            container.Register<Tareo_UnitOfWork, Tareo_UnitOfWork>(Lifestyle.Scoped); 

            //Item
            container.Register<IRepository<Item>, Tareo_Repository<Item>>(Lifestyle.Scoped);
            container.Register<IService<Item>, ServiceAbstract<Item>>(Lifestyle.Scoped);
            container.Register<IValidator<ItemDto>, ItemDtoValidator>(Lifestyle.Scoped);



            //TareoReporte
            container.Register<IRepositoryReporte<ProyectoReporte>, Reporte_Repository<ProyectoReporte>>(Lifestyle.Scoped);
            container.Register<IServiceReporte<ProyectoReporte>, ServiceAbstractReporte<ProyectoReporte>>(Lifestyle.Scoped);

            /**
             * Ultimas configuraciones del container 
             */
            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}