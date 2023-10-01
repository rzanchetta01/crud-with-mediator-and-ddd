namespace domain.Dto;

public record PlayerDto
(
    Guid Id,
    string Name,
    short Age,
    Guid TeamId,
    decimal Salary
);