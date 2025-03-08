using AutoMapper;
using Library.Application.Models;
using Library.Core.Abstractions;
using Library.Core.Entities;
using Library.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Authors
{
    public class GetAllAuthorsUseCase: BasePagedUseCase<AuthorModel, AuthorEntity>
    {
        private readonly ILibraryUnitOfWork db;
        private readonly IMapper mapper;

        public GetAllAuthorsUseCase(ILibraryUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<PagedItems<AuthorModel>> ExecuteAsync(int page, int size)
        {
            var authEntities =  await db.authorRepository.GetAllAsync(page, size) ??
                throw new ObjectNotFoundException($"Error on GetAllAuthorsUseCase: list was empty");

            //return mapper.Map<List<AuthorModel>>(authEntities);
            return MapPagedItems(authEntities, mapper);
        }

        /*private PagedItems<AuthorModel> MapPagedItems(PagedItems<AuthorEntity> source)
        {
            var result = new PagedItems<AuthorModel>
            {
                Items = mapper.Map<List<AuthorModel>>(source.Items),
                TotalCount = source.TotalCount,
                PageSize = source.PageSize,
                CurrentPage = source.CurrentPage,
                TotalPages = source.TotalPages
            };

            return result;
        }*/
    }
}
