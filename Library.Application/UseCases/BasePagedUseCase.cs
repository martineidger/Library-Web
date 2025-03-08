using AutoMapper;
using Library.Application.Models;
using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases
{
    public abstract class BasePagedUseCase<T, U>
    {
        public PagedItems<T> MapPagedItems(PagedItems<U> source, IMapper _mapper)
        {
            var result = new PagedItems<T>
            {
                Items = _mapper.Map<List<T>>(source.Items),
                TotalCount = source.TotalCount,
                PageSize = source.PageSize,
                CurrentPage = source.CurrentPage,
                TotalPages = source.TotalPages
            };

            return result;
        }
    }
}
