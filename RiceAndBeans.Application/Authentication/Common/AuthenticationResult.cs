using RiceAndBeans.Domain.Users;

namespace RiceAndBeans.Application.Authentication.Common;

public record AuthenticationResult(
	User User,
	string Token);