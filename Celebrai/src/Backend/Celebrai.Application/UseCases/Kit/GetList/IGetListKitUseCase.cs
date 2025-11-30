using Celebrai.Communication.Responses.Kit;

namespace Celebrai.Application.UseCases.Produto.GetList;

public interface IGetListKitUseCase
{
    public Task<IList<ResponseKitJson>> Execute(int? page);
}
