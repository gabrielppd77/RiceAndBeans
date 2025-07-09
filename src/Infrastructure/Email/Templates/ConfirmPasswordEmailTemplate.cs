using Contracts.Services.Email.Templates;

namespace Infrastructure.Email.Templates;
public class ConfirmPasswordEmailTemplate : IConfirmPasswordEmailTemplate
{
    public string Generate(string userName, string link)
    {
        return $@"
        <!DOCTYPE html>
        <html lang='pt-BR'>
        <head>
          <meta charset='UTF-8'>
          <title>Confirmação de E-mail</title>
          <style>
            body {{
              font-family: Arial, sans-serif;
              background-color: #f6f8fa;
              margin: 0;
              padding: 0;
            }}
            .container {{
              max-width: 600px;
              margin: 40px auto;
              background-color: #ffffff;
              border-radius: 8px;
              box-shadow: 0 0 10px rgba(0,0,0,0.05);
              padding: 30px;
            }}
            .header {{
              text-align: center;
              margin-bottom: 20px;
            }}
            .header h1 {{
              color: #2c3e50;
              font-size: 24px;
            }}
            .content {{
              font-size: 16px;
              color: #333333;
              line-height: 1.5;
            }}
            .button-container {{
              text-align: center;
              margin: 30px 0;
            }}
            .confirm-button {{
              background-color: #3498db;
              color: white;
              padding: 12px 24px;
              border-radius: 6px;
              text-decoration: none;
              font-weight: bold;
              display: inline-block;
            }}
            .footer {{
              font-size: 12px;
              color: #999999;
              text-align: center;
              margin-top: 20px;
            }}
            .footer a {{
              color: #999999;
              text-decoration: none;
            }}
          </style>
        </head>
        <body>
          <div class='container'>
            <div class='header'>
              <h1>Confirme seu cadastro</h1>
            </div>
            <div class='content'>
              <p>Olá {userName},</p>
              <p>Obrigado por se cadastrar! Para ativar sua conta, clique no botão abaixo:</p>
            </div>
            <div class='button-container'>
              <a href='{link}' class='confirm-button'>Confirmar e-mail</a>
            </div>
            <div class='content'>
              <p>Se você não se cadastrou, apenas ignore este e-mail.</p>
            </div>
            <div class='footer'>
              <p>Este e-mail foi enviado automaticamente. Não responda a esta mensagem.</p>
              <p><a href='https://riceandbeans.online'>riceandbeans.online</a></p>
            </div>
          </div>
        </body>
        </html>";
    }
}
