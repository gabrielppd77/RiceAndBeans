using Contracts.Services.Email.Templates;

namespace Infrastructure.Email.Templates;

public class PasswordRecoveryEmailTemplate: IPasswordRecoveryEmailTemplate
{
    public string Generate(string recoveryLink)
    {
        return $@"
        <!DOCTYPE html>
        <html lang='pt-BR'>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>Recuperação de Senha</title>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f5f6fa;
                    color: #2f3640;
                    padding: 20px;
                }}
                .container {{
                    max-width: 600px;
                    margin: auto;
                    background-color: #ffffff;
                    padding: 30px;
                    border-radius: 10px;
                    box-shadow: 0 0 10px rgba(0,0,0,0.1);
                }}
                .button {{
                    display: inline-block;
                    margin-top: 20px;
                    padding: 12px 25px;
                    background-color: #4CAF50;
                    color: white;
                    text-decoration: none;
                    border-radius: 5px;
                    font-weight: bold;
                }}
                .footer {{
                    margin-top: 30px;
                    font-size: 0.9em;
                    color: #888;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <h2>Recuperação de Senha</h2>
                <p>Olá,</p>
                <p>Recebemos uma solicitação para redefinir a sua senha. Para continuar, clique no botão abaixo:</p>
                <a href='{recoveryLink}' class='button'>Redefinir Senha</a>
                <p>Se você não solicitou a redefinição de senha, ignore este e-mail.</p>
                <div class='footer'>
                    <p>Atenciosamente,<br>Sua equipe de suporte</p>
                </div>
            </div>
        </body>
        </html>";
    }
}
