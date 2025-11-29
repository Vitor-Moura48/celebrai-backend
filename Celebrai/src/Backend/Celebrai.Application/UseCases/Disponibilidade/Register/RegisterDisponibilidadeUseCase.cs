using Celebrai.Communication.Requests.Disponibilidade;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Disponibilidade;
using Celebrai.Domain.Repositories.Fornecedor;
using Celebrai.Domain.Services.LoggedUser;
using Celebrai.Exceptions.ExceptionsBase;

namespace Celebrai.Application.UseCases.Disponibilidade.Register;

public class RegisterDisponibilidadeUseCase : IRegisterDisponibilidadeUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IFornecedorReadOnlyRepository _supplierReadOnlyRepository;
    private readonly IDisponibilidadeWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterDisponibilidadeUseCase(
        ILoggedUser loggedUser,
        IFornecedorReadOnlyRepository supplierReadOnlyRepository,
        IDisponibilidadeWriteOnlyRepository repository,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _supplierReadOnlyRepository = supplierReadOnlyRepository;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestRegistedDisponibilidadeJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.User();

        var supplier = await _supplierReadOnlyRepository.GetByUserId(loggedUser.IdUsuario);

        var newAvailability = request.Horarios.Select(h => new Domain.Entities.Disponibilidade
        {
            IdFornecedor = supplier!.IdFornecedor, 
            DiaSemana = h.DiaSemana,
            HoraInicio = h.HoraInicio,
            HoraFim = h.HoraFim,
        }).ToList();

        await _repository.DeleteByFornecedorId(supplier!.IdFornecedor);

        if (newAvailability.Any())
            await _repository.AddRange(newAvailability);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestRegistedDisponibilidadeJson request)
    {
        var validator = new RegisterDisponibilidadeValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorValidationException(errors);
        }
    }
}
