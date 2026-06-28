using FluentValidation;
using SimpleTaskManager.Communication.Requests;

namespace SimpleTaskManager.Application.UseCases.Tasks.Register;

public class RegisterTaskValidator : AbstractValidator<RequestRegisterTaskJson>
{
    public RegisterTaskValidator()
    {
        RuleFor(task => task.Name)
            .NotEmpty().WithMessage("O nome da tarefa é obrigatório.")
            .MaximumLength(100).WithMessage("O nome da tarefa não pode exceder 100 caracteres.");
        
        RuleFor(task => task.DueDate)
            .NotEmpty().WithMessage("A data de vencimento é obrigatória.")
            .GreaterThan(DateTimeOffset.UtcNow).WithMessage("A data de vencimento deve ser uma data futura.");
        
        RuleFor(task => task.Priority)
            .IsInEnum().WithMessage("Valor de prioridade não suportado.");
        
        RuleFor(task => task.TaskStatus)
            .IsInEnum().WithMessage("Valor de status não suportado.");
    }
}