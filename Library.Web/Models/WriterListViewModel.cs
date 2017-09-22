using Library.Domain.Entities;
using System.Collections.Generic;

namespace Library.Web.Models
{
    public class WriterListViewModel
    {
        public IEnumerable<Writer> Writers { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}