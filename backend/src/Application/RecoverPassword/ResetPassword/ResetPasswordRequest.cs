namespace Application.RecoverPassword.ResetPassword;

public record ResetPasswordRequest(Guid Token, string NewPassword);