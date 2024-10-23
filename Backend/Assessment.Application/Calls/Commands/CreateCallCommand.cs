using Assessment.Data;
using Assessment.Data.Entities;
using Assessment.Shared.Models;
using FluentValidation;
using MediatR;

namespace Assessment.Application.Calls.Commands;

public class CreateCallCommand : IRequest<Result<CreateCallResult>>
{
    public Guid CallingUserId { get; set; }
    public DateTimeOffset DateCallStarted { get; set; }
}

public class CreateCallResult
{
    public Guid Id { get; set; }
}

public class CreateCallCommandValidator : AbstractValidator<CreateCallCommand>
{
    public CreateCallCommandValidator()
    {
        RuleFor(x => x.CallingUserId)
            .NotEmpty()
            .WithMessage("CallingUserId is required");

        RuleFor(x => x.DateCallStarted)
            .NotEmpty()
            .WithMessage("DateCallStarted is required");
    }
}

public class CreateCallCommandHandler : IRequestHandler<CreateCallCommand, Result<CreateCallResult>>
{
    private readonly ApplicationDbContext _context;

    public CreateCallCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CreateCallResult>> Handle(
        CreateCallCommand request,
        CancellationToken cancellationToken)
    {
        var call = new Call
        {
            CallingUserId = request.CallingUserId,
            DateCallStarted = request.DateCallStarted,
        };

        _context.Calls.Add(call);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<CreateCallResult>.Success(new CreateCallResult
        {
            Id = call.Id
        });
    }
}
