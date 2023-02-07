using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.QueryStore;

namespace Questao5.Infrastructure.Services.Controllers;

[ApiController]
[Route("[controller]")]
public class IdempotenciasController : ControllerBase
{
    private IdempotenciaDataClass _dataClass;
    public IdempotenciasController(IdempotenciaDataClass dataClass)
    {
        _dataClass = dataClass;
    }
    
    [HttpGet(Name = "GetIdempotencia")]
    public IEnumerable<Idempotencia> Get()
    {
        var data = _dataClass.SelectQuery();
        return data;
    }
}