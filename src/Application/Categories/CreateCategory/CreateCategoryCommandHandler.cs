﻿using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories.Categories;
using Domain.Categories;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Categories.CreateCategory;

public class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUserAuthenticated userAuthenticated,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCategoryCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var companyId = userAuthenticated.GetCompanyId();

        var category = new Category(companyId, request.Name);

        await categoryRepository.Add(category);

        await unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}