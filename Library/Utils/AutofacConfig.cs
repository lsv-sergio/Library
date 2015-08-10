using Autofac;
using Autofac.Integration.Mvc;
using LibraryDal.EF;
using LibraryDal.Infrastructure.CommandsDal;
using LibraryDal.Infrastructure.QueriesDal.Factory;
using LibraryDal.Infrastructure.UnitOfWork;
using LibraryDomain.Commands;
using LibraryDomain.Queries;
using LibraryDomain.Queries.AbstractQuery;
using LibraryDomain.Queries.DomainQueries;
using LibraryDomain.Queries.DomainQueries.Factory;
using LibraryDomain.Queries.LinksQueries;
using LibraryDomain.Queries.LinksQueries.Factory;
using LibraryDomain.Queries.ViewsQueries;
using LibraryDomain.Queries.ViewsQueries.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Utils
{
    public class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EFLibraryDbContext>().AsSelf().InstancePerRequest();

            builder.RegisterType<UnitOfWork>().As<IQueryContext, ICommandContext>().InstancePerRequest();

            builder.RegisterType<DalQueryFactory>().As<IDalQueryFactory>().InstancePerDependency();

            builder.RegisterType<LinkFactory>().As<ILinkFactory>().InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(DalCommand<>).Assembly)
                .Where(type => type.BaseType != null && type.BaseType.IsAbstract && type.BaseType.IsGenericType)
                .Where(type => type.BaseType.GetGenericTypeDefinition() == typeof(DalCommand<>))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(DomainCommands).Assembly)
                .Where(type => type.BaseType != null)
                .Where(type => type.BaseType == typeof(DomainCommands))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(BaseViewQueries<,>).Assembly)
                .Where(type => type.BaseType != null)
                .Where(type => type.BaseType.IsAbstract && type.BaseType.IsGenericType)
                .Where(type => type.BaseType.GetGenericTypeDefinition() == typeof(BaseViewQueries<,>))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(BaseDomainQueries<,>).Assembly)
                .Where(type => type.BaseType != null)
                .Where(type => type.BaseType.IsAbstract && type.BaseType.IsGenericType)
                .Where(type => type.BaseType.GetGenericTypeDefinition() == typeof(BaseDomainQueries<,>))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}