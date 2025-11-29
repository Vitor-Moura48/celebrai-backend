using Celebrai.Communication.Responses.Disponibilidade;

namespace Celebrai.Application.UseCases.Disponibilidade.GetHoursFornecedor;

public interface IGetHoursFornecedorUseCase
{
    public Task<List<ResponseDisponibilidadeJson>> Execute(Guid idFornecedor);
}
