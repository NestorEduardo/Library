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
            Mock<IBookTypeRepository> mock = new Mock<IBookTypeRepository>();
            mock.Setup(bt => bt.BookTypes).Returns(new List<BookType>
            {
                new BookType { Description = "Story" },
                new BookType { Description = "Novel" },
                new BookType { Description = "Biography" }
            });

            kernel.Bind<IBookTypeRepository>().To<EFBookTypeRepository>();
        }
    }
}