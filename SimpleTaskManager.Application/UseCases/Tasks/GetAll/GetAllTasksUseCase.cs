using Microsoft.EntityFrameworkCore;
using SimpleTaskManager.Communication.Enums;
using SimpleTaskManager.Communication.Requests;
using SimpleTaskManager.Communication.Responses;
using SimpleTaskManager.Exception.ExceptionsBase;
using SimpleTaskManager.Infrastructure.Data;

namespace SimpleTaskManager.Application.UseCases.Tasks.GetAll;

public class GetAllTasksUseCase
{
    private readonly AppDbContext _dbContext;
    
    public GetAllTasksUseCase(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ResponseGetAllTasksJson> Execute(RequestGetAllTasksJson request)
    {
        Validate(request);

        var query = _dbContext.Tasks.AsQueryable(); // Inicia a query com todos os books do banco de dados
        
        if (!string.IsNullOrEmpty(request.Search))
        {
            query = query.Where(task => 
                EF.Functions.Like(task.Name, $"%{request.Search}%") || 
                EF.Functions.Like(task.Description, $"%{request.Search}%")
            ); // Filtra por nome ou descrição, se o parâmetro de busca não for nulo ou vazio
        }
        
        // Especificando Ordenação
        if (request.SortDirection == SortDirectionType.Desc)
        {
            query = query
                .OrderByDescending(task => task.Name)
                .ThenByDescending(task => task.DueDate); // Ordena por nome em ordem decrescente, se o parâmetro de ordenação for Desc
        }
        else
        {
            query = query
                .OrderBy(task => task.Name)
                .ThenBy(task => task.DueDate); // Ordena por nome em ordem crescente, se o parâmetro de ordenação for Asc ou nulo
        }

        // Filtrando por Prioridade
        if (request.Priority.HasValue)
        {
            query = query
                .Where(task => task.Priority == (Domain.Enums.PriorityType)request.Priority.Value); // Filtra por prioridade, se o parâmetro de prioridade não for nulo
        }
        
        // Filtrando por Status da Task
        if (request.Status.HasValue)
        {
            query = query
                .Where(task => task.TaskStatus == (Domain.Enums.TaskStatus)request.Status.Value); // Filtra por status da task, se o parâmetro de status não for nulo
        }
        
        // Filtrando por DueDate
        if (request.DueDate.HasValue)
        {
            query = query
                .Where(task => task.DueDate.Date == request.DueDate.Value.Date); // Filtra por DueDate, se o parâmetro de DueDate não for nulo
        }
        
        var tasks = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
        
        var totalItems = await query.CountAsync(); // Conta o total de tasks que correspondem à busca, sem considerar paginação
        
        var totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize);
        
        var hasPreviousPage = request.Page > 1;
        var hasNextPage = request.Page < totalPages;

        return new ResponseGetAllTasksJson
        {
            Data = tasks.Select(task => new ResponseTaskJson
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = (PriorityType)task.Priority,
                TaskStatus = (Communication.Enums.TaskStatus)task.TaskStatus,
            }).ToList(),
            Pagination = new PaginationMetadata
            {
                Page = request.Page,
                PageSize = request.PageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                HasPreviousPage = hasPreviousPage,
                HasNextPage = hasNextPage
            }
        };
    }

    private void Validate(RequestGetAllTasksJson request)
    {
        var validator = new GetAllTasksValidator();
        
        var  validationResults = validator.Validate(request);

        if (!validationResults.IsValid)
        {
            var errorMessages = validationResults.Errors.Select(x => x.ErrorMessage).ToList();
            
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}