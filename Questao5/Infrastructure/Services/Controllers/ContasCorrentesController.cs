using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.QueryStore;

namespace Questao5.Infrastructure.Services.Controllers;

[ApiController]
[Route("[controller]")]
public class ContasCorrentesController : ControllerBase
{

    private ContaCorrenteDataClass _dataClass;
    private readonly MovimentoDataClass _movimentoDataClass;

    public ContasCorrentesController(ContaCorrenteDataClass dataClass, MovimentoDataClass movimentoDataClass)
    {
        _dataClass = dataClass;
        _movimentoDataClass = movimentoDataClass;
    }
    
    [HttpGet("GetContasCorrentes")]
    public IEnumerable<ContaCorrente> Get()
    {
        var data = _dataClass.SelectQuery();
        return data;
    }
    
    [HttpGet("GetSaldo/{id}")]
    public ActionResult<ContaCorrenteDTO> GetSaldo(string id)
    {
        var data = _dataClass.SelectById(id);
        if (data == null)
            return StatusCode(400, "INVALID_ACCOUNT: Nenhuma conta encontrada com o id informado");
        if (!data.Ativo)
            return StatusCode(400, "INACTIVE_ACCOUNT: Conta encontrada com o id informado está inativa");

        var response = _movimentoDataClass.SelectByIdContaCorrente(id).ToList();
        var saldo = response.Where(m => m.TipoMovimento == TipoMovimento.Credito).Select(x => x.Valor).Sum() -
                    response.Where(m => m.TipoMovimento == TipoMovimento.Debito).Select(x => x.Valor).Sum();

        
        var dto = new ContaCorrenteDTO
        {
            Numero = data.Numero,
            Nome = data.Nome ?? "",
            Data = DateTime.Now,
            Saldo = saldo
        };

        return StatusCode(200, dto);
    }
    
    
}