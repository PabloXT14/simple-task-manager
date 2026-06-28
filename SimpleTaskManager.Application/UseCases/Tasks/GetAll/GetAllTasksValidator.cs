using FluentValidation;
using SimpleTaskManager.Communication.Requests;

namespace SimpleTaskManager.Application.UseCases.Tasks.GetAll;

public class GetAllTasksValidator : AbstractValidator<RequestGetAllTasksJson>
{
    public  GetAllTasksValidator()
    {
        RuleFor(request => request.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("O número da pagina deve ser maior ou igual a 1.");
        
        RuleFor(request => request.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("O tamanho da pagina deve ser maior ou igual a 1.");

        RuleFor(request => request.SortDirection)
            .IsInEnum()
            .WithMessage("O valor de SortDirection deve ser 'Asc' ou 'Desc'.");

        When(request => request.Priority.HasValue, () =>
        {
            RuleFor(request => request.Priority)
                .IsInEnum()
                .WithMessage("O valor de Priority deve ser 'Low', 'Medium' ou 'High'.");
        });
        
        When(request => request.Status.HasValue, () =>
        {
            RuleFor(request => request.Status)
                .IsInEnum()
                .WithMessage("O valor de Status deve ser 'Pending', 'InProgress' ou 'Completed'.");
        });
    }
}