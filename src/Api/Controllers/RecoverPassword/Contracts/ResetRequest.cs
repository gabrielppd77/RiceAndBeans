namespace Api.Controllers.RecoverPassword.Contracts;

public record ResetRequest(Guid Token, string NewPassword);
