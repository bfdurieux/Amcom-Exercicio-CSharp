using System.Net;
using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.QueryStore;

namespace Questao5.Infrastructure.Services.Controllers;


[ApiController]
[Route("[controller]")]
public class MovimentosController : ControllerBase
{
    private MovimentoDataClass _dataClass;
    private ContaCorrenteDataClass _contaCorrenteDataClass;
    public MovimentosController(MovimentoDataClass dataClass, ContaCorrenteDataClass contaCorrenteDataClass)
    {
        _dataClass = dataClass;
        _contaCorrenteDataClass = contaCorrenteDataClass;
    }
    
    [HttpGet(Name = "GetMovimentos")]
    public IEnumerable<Movimento> Get()
    {
        var data = _dataClass.SelectQuery();
        return data;
    }

    [HttpPost(Name = "PostMovimento")]
    public ActionResult<string> Post(Movimento movimento)
    {
        if (movimento.Valor < 0)
            return StatusCode(400, "INVALID_VALUE: Valores negativos não são permitidos");

        if (movimento.TipoMovimento != TipoMovimento.Credito && movimento.TipoMovimento != TipoMovimento.Debito)
            return StatusCode(400, "INVALID_TYPE: Tipo de movimento deve ser Crédito 'C' ou Débito 'D'");
        
        var contaCorrente = _contaCorrenteDataClass.SelectById(movimento.IdContaCorrente);
        
        if (contaCorrente == null)
            return StatusCode(400, "INVALID_ACCOUNT: Conta corrente relacionada à movimentação não foi encontrada");

        if (!contaCorrente.Ativo)
            return StatusCode(400, "INACTIVE_ACCOUNT: Conta corrente relacionada à movimentação está inativa");

        var response = _dataClass.InsertQuery(movimento);

        while (response == null) 
            response = _dataClass.InsertQuery(movimento);


        return StatusCode(200, response.IdMovimento);
    }
}