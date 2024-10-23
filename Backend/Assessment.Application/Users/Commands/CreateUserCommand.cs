using Assessment.Data;
using Assessment.Data.Entities;
using Assessment.Shared.Models;
using FluentValidation;
using MediatR;

namespace Assessment.Application.Users.Commands;

public class CreateUserCommand : IRequest<Result<CreateUserResult>>
{
    public string Username { get; set; }
}

public class CreateUserResult
{
    public Guid Id { get; set; }
}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required");
        
        RuleFor(x => x.Username)
            .MaximumLength(255)
            .WithMessage("Username must not exceed 255 characters");
    }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<CreateUserResult>>
{
    private readonly ApplicationDbContext _context;

    public CreateUserCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CreateUserResult>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = new User
        {
            Username = request.Username,
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<CreateUserResult>.Success(new CreateUserResult
        {
            Id = user.Id,
        });
    }
}
