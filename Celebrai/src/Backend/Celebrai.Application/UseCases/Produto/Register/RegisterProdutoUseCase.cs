using Celebrai.Communication.Requests.Produto;
using Celebrai.Communication.Responses.Produto;
using Celebrai.Domain.Entities;
using Celebrai.Domain.Enums;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Fornecedor;
using Celebrai.Domain.Repositories.Produto;
using Celebrai.Domain.Repositories.SubCategoria;
using Celebrai.Domain.Repositories.Usuario;
using Celebrai.Domain.Services.Cloudinary;
using Celebrai.Domain.Services.LoggedUser;
using Celebrai.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Celebrai.Application.UseCases.Produto.Register;
public class RegisterProdutoUseCase : IRegisterProdutoUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IProdutoWriteOnlyRepository _repository;
    private readonly IFornecedorUpdateOnlyRepository _userUpdateOnlyRepository;
    private readonly ISubCategoriaReadOnlyRepository _subCategoryReadOnlyRepository;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterProdutoUseCase(
        ILoggedUser loggedUser,
        IProdutoWriteOnlyRepository repository,
        IFornecedorUpdateOnlyRepository userUpdateOnlyRepository,
        ISubCategoriaReadOnlyRepository subCategoryReadOnlyRepository,
        ICloudinaryService cloudinaryService,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _userUpdateOnlyRepository = userUpdateOnlyRepository;
        _subCategoryReadOnlyRepository = subCategoryReadOnlyRepository;
        _cloudinaryService = cloudinaryService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisteredProdutoJson> Execute(RequestRegisterProdutoFormData request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.User();

        var product = _mapper.Map<Domain.Entities.Produto>(request);

        var subCategory = await _subCategoryReadOnlyRepository.GetSubCategoriaById((SubCategoriaEnum)request.SubCategoria);
        product.IdSubcategoria = (int)request.SubCategoria;
        product.SubCategoria = subCategory;

        var fileStream = request.Imagem.OpenReadStream();
        fileStream.Position = 0;

        var resultImage = await _cloudinaryService.UploadImage(fileStream, request.Imagem.Name);

        product.ImagemUrl = resultImage.ImageUrl;
        product.ImagemPublicId = resultImage.ImagemPublicId;

        var user = await _userUpdateOnlyRepository.GetById(loggedUser.IdUsuario);

        var entity = new FornecedorProduto
        {
            IdProduto = product.IdProduto,
            IdFornecedor = user!.IdFornecedor,
            Fornecedor = user,
            Produto = product
        };

        await _repository.Add(product, entity);
        await _unitOfWork.Commit();

        return new ResponseRegisteredProdutoJson
        {
            Nome = request.Nome,
            Descricao = request.Descricao,
            ImagemUrl = resultImage.ImageUrl,
            SubCategoria = request.SubCategoria.ToString()
        };
    }

    private static void Validate(RequestRegisterProdutoFormData request)
    {
        var result = new RegisterProdutoValidator().Validate(request);

        if (result.IsValid == false)
            throw new ErrorValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
    }
}
