using Celebrai.Application.UseCases.Kit.Register;
using Celebrai.Communication.Requests.Kit;
using Celebrai.Communication.Responses.Kit;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Kit;
using Celebrai.Domain.Repositories.Produto;
using Celebrai.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Celebrai.Application.UseCases.Kit.Register;

public class RegisterKitUseCase : IRegisterKitUseCase
{
    private readonly IKitWriteOnlyRepository _kitWriteOnlyRepository;
    private readonly IProdutoReadOnlyRepository _produtoReadOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterKitUseCase(
        IKitWriteOnlyRepository kitWriteOnlyRepository,
        IProdutoReadOnlyRepository produtoReadOnlyRepository, 
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _kitWriteOnlyRepository = kitWriteOnlyRepository;
        _produtoReadOnlyRepository = produtoReadOnlyRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisteredKitJson> Execute(RequestKitJson request)
    {
        await Validate(request);

        var kit = _mapper.Map<Domain.Entities.Kit>(request);

        var produtosDoKit = request.ProdutosIds.Select(produtoId => new Domain.Entities.ProdutoKit
        {
            Kit = kit,
            IdProduto = produtoId   
        }).ToList();

       await _kitWriteOnlyRepository.Add(kit, produtosDoKit);
       await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisteredKitJson>(kit);
    }

    private async Task Validate(RequestKitJson request)
    {
        var validator = new RegisterKitValidator(_produtoReadOnlyRepository);
        var result = await validator.ValidateAsync(request);

        if (result.IsValid == false)
            throw new ErrorValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());     
    }
}