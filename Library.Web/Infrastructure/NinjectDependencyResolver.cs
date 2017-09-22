using Library.Domain.Abstract;
using Library.Domain.Concrete;
using Library.Domain.Entities;
using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Library.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IBookTypeRepository>().To<EFBookTypeRepository>();
            kernel.Bind<IWriterRepository>().To<EFWriterRepository>();
        }
    }
}