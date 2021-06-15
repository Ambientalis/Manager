using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Web.Mail;
using System.IO;
using Modelo;
using System.Globalization;
using Persistencia.Fabrica;

namespace Utilitarios
{
    /// <summary>
    /// Summary description for Email
    /// </summary>
    public class Email
    {
        public delegate void EmailEnviadoEventHandler(Email email);
        public delegate void EmailNaoEnviadoEventHandler(Email email);

        public event EmailEnviadoEventHandler EmailEnviado;
        public event EmailNaoEnviadoEventHandler EmailNaoEnviado;

        private System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();

        #region _____________ TEMPLATES ____________


        public string GetTemplateBasico(string titulo, string conteudo)
        {
            string template = @"
            <div style='width:600px; height:auto; border-radius:10px; border:1px solid silver; background-color:white'>
                <div style='float:left; margin-left:20px; margin-top:10px;'><img src='http://ambientalismanager.com.br/imagens/logo_amb_manager.png' />
                </div>
                <div style='float:left; margin-left:40px; width:250px; font-family:arial; font-size:18px; font-weight:bold; margin-top:40px;'>
                   " + titulo + @"
                </div>
                <div style='height:5px; clear:both; text-align:center; font-family:Arial, Helvetica, sans-serif; font-size:12px; padding:3px; font-weight:bold; color:Red'></div>
                <div style='margin-left:20px;border-radius:10px; margin-right:20px; font-family:Arial, Helvetica, sans-serif; font-size: 13px; padding:7px; background-color:#E9E9E9; text-align:left; height:auto'>
    
                    " + conteudo + @"</div>
                <div style='height:10px; font-family:Arial, Helvetica, sans-serif; font-size:14px; margin-top:10px; margin-left:20px; margin-right:20px; border-bottom:1px solid silver;'>
                </div>
                <div style='text-align:center;font-weight:bold;font-family:Arial, Helvetica, sans-serif; font-size:14px;'>
                    " + DateTime.Now.ToString() + @"
                </div>
                <div style='width:100%; height:20px;'></div>
                </div>";

            return template;
        }

        public string GetTemplateAberturaPedido(Pedido pedido)
        {
            return @"<!DOCTYPE html>
                <html xmlns='http://www.w3.org/1999/xhtml'>
                <head>
                    <title></title>
                </head>
                <body style='background: #b2b2b2'>
                    <div style='width: 700px; margin: 0 auto; background-color: #fff; font-family: Consolas'>
                        <header style='padding: 10px;'>
                            <a href='http://ambientalis-es.com.br/website/site/Index.aspx' style='text-decoration: none; font-size: 12px; margin-right: 310px;'>www.ambientalis-es.com.br</a>
                        </header>
                        <nav style='text-align: center'>
                            <img src='http://ambientalismanager.com.br/imagens/topo_email_pedido.png' />"
                            + "<p style=\"font-size: 15px; font-family: 'Adobe Fangsong Std'\">Castelo - ES, " + (DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy", CultureInfo.CreateSpecificCulture("pt-BR"))) + @"</p>
                        </nav>
                        <section>
                            <article style='padding: 0px 20px 0px 20px; text-align: justify; color:#00885C'>
                                <h1 style='font-size: 25px; color:#00885C'>Prezado(a), " + (pedido.GetNomeCliente) + @".</h1>
                                <h3 style='font-size: 17px;'>Gostaríamos de agradecer a confiança em nossos serviços.</h3>
                                <p>
                                    Foi gerado no sistema Ambientalis Manager o pedido nº <b>" + (pedido.Codigo) + @"</b>, referente ao(s) serviço(s) de
                                    <b>" + (pedido.GetDescricaoTipo) + @"</b>. "
                                         + (pedido.Cliente.Login.IsNotNullOrEmpty() ?
                                         @"Acompanhe o andamento do serviço pelo sistema 
                                            <a href='http://ambientalismanager.com.br/Account/LoginCliente.aspx' target='_blank' style='text-decoration: none'>www.ambientalismanager.com.br</a>
                                            com o login <b>" + (pedido.Cliente.Login) + @"</b> e a senha <b>" + (Utilitarios.Criptografia.Criptografia.Decrypt(pedido.Cliente.Senha, true)) + @"</b>." :
                                         @"Solicite seus dados de acesso pelo site <a href='http://ambientalis-es.com.br/website/site/Contato.aspx' target='_blank' style='text-decoration: none'>www.ambientalismanager.com.br</a> para acompanhar seu pedido.")
                                    + @"</p>
                                <div style='text-align:center'>
                                    <img src='http://ambientalismanager.com.br/imagens/atendimento_email_pedido.png' />
                                </div>
                            </article>
                        </section>

                        <footer style='padding: 20px; font-size: 12px; text-align: center;'>
                            <p>
                                AMBIENTALIS © 2015. Todos os direitos reservados
                            </p>
                            <p>
                                Nosso endereço de correspondência é:<br />
                                comercial@ambientalis-es.com.br
                            </p>
                        </footer>
                    </div>
                </body>
            </html>";
        }

        #endregion

        #region _____________ ATRIBUTOS ____________

        private MailAddressCollection emailsDestino;
        private MailAddressCollection comCopia;
        private MailAddressCollection comCopiaOculta;
        private string assunto;
        private string mensagem;
        private bool bodyHtml = true;
        private string erro;
        private IList<string> anexos;

        #endregion

        #region _____________ PROPRIEDADES ____________

        /// <summary>
        /// Se o corpo do email vai ser no formato HTML
        /// </summary>
        public bool BodyHtml
        {
            get { return bodyHtml; }
            set { bodyHtml = value; }
        }

        /// <summary>
        /// Endereço de destino para o envio do email
        /// </summary>
        public MailAddressCollection EmailsDestino
        {
            get
            {
                if (this.emailsDestino == null)
                    this.emailsDestino = new MailAddressCollection();
                return emailsDestino;
            }
        }

        /// <summary>
        /// Endereços de destino para o envio do email como CC
        /// </summary>
        public MailAddressCollection ComCopia
        {
            get
            {
                if (this.comCopia == null)
                    this.comCopia = new MailAddressCollection();
                return comCopia;
            }
        }

        /// <summary>
        /// Endereços de destino para o envio do email como CCO
        /// </summary>
        public MailAddressCollection ComCopiaOculta
        {
            get
            {
                if (this.comCopiaOculta == null)
                    this.comCopiaOculta = new MailAddressCollection();
                return this.comCopiaOculta;

            }
        }

        /// <summary>
        /// Assunto a ser colocado no email
        /// </summary>
        public string Assunto
        {
            get
            {
                return assunto;
            }
            set
            {
                assunto = value;
            }
        }

        /// <summary>
        /// Mensagem a ser colocado no email
        /// </summary>
        public string Mensagem
        {
            get
            {
                return mensagem;
            }
            set
            {
                mensagem = value;
            }
        }

        /// <summary>
        /// Gerada quando ocorre erro no envio
        /// </summary>
        public string Erro
        {
            get { return erro; }
            set { erro = value; }
        }

        #endregion

        #region _____________ CONTRUTOR ____________

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        public Email()
        {

        }

        #endregion

        #region _____________ EVENTOS ____________

        void smpt_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
            {
                if (this.EmailEnviado != null)
                    this.EmailEnviado(this);
            }
            else
            {
                if (this.EmailNaoEnviado != null)
                {
                    this.Erro = e.Error.Message;
                    this.EmailNaoEnviado(this);
                }
            }
        }

        #endregion

        #region _____________ MÉTODOS ____________

        /// <summary>
        /// Envia este e-mail
        /// </summary>
        /// <returns>true caso consiga e false caso não</returns>
        public bool Enviar(String nomeEmail, Funcionario funcionario, Pedido pedido)
        {
            try
            {
                if (ConfigurationManager.AppSettings["implantado"].ToString() != "true")
                    return false;

                this.CriarEmail();
                //Servidor SMTP tem que estar configurado no WebConfig
                if (ConfigurationManager.AppSettings["servidorSMTP"] == null)
                {
                    this.Erro = "Servidor SMTP não definido no arquivo de configurações da aplicação";
                    return false;
                }

                SmtpClient smpt = new SmtpClient(ConfigurationManager.AppSettings["servidorSMTP"].ToString());
                smpt.Port = 465;
                smpt.EnableSsl = true;
                smpt.UseDefaultCredentials = true;
                smpt.Send(this.email);

                String destinatarios = String.Join(";", this.EmailsDestino.Select(mail => mail.Address).Distinct().ToArray());
                String copia = (this.ComCopia == null && this.ComCopia.Count < 0 ? string.Empty : (String.Join(";", this.ComCopia.Select(mail => mail.Address).Distinct().ToArray())));

                //Salvar o e-mail enviado
                EmailEnviado emailEnviado = new EmailEnviado();
                emailEnviado.Assunto = this.Assunto;
                emailEnviado.DataEnvio = DateTime.Now;
                emailEnviado.Destinatarios = destinatarios;
                emailEnviado.EmailsCopia = copia;
                emailEnviado.Enviado = true;
                emailEnviado.NomeEmail = nomeEmail;
                emailEnviado.TextoEmail = this.Mensagem;
                emailEnviado.FuncionarioEnvio = funcionario;
                emailEnviado.Pedido = pedido;
                emailEnviado.Salvar();
                return true;
            }
            catch (Exception ex)
            {
                this.Erro = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Envia este e-mail autenticado no servidor SMTP
        /// </summary>
        /// <returns>true caso consiga e false caso não</returns>
        public bool EnviarAutenticado(String nomeEmail, Funcionario funcionario, Pedido pedido, int porta, bool useSSL)
        {
            try
            {
                if (ConfigurationManager.AppSettings["implantado"].ToString() != "true")
                    return false;

                this.CriarEmail();
                if (ConfigurationManager.AppSettings["servidorSMTP"] == null ||
                    ConfigurationManager.AppSettings["userNameEmail"] == null ||
                    ConfigurationManager.AppSettings["passwordEmail"] == null)
                {
                    this.Erro = "Configuração do servidor SMTP ou de usuário ou de senha está(ão) faltando no arquivo de configurações da aplicação";
                    return false;
                }

                SmtpClient cliente = new SmtpClient(ConfigurationManager.AppSettings["servidorSMTP"].ToString());
                cliente.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["userNameEmail"].ToString(), ConfigurationManager.AppSettings["passwordEmail"].ToString());
                cliente.Port = porta;
                cliente.EnableSsl = useSSL;
                cliente.Send(this.email);

                String destinatarios = String.Join(";", this.EmailsDestino.Select(mail => mail.Address).Distinct().ToArray());
                String copia = (this.ComCopia == null && this.ComCopia.Count < 0 ? string.Empty : (String.Join(";", this.ComCopia.Select(mail => mail.Address).Distinct().ToArray())));

                //Salvar o e-mail enviado
                EmailEnviado emailEnviado = new EmailEnviado();
                emailEnviado.Assunto = this.Assunto;
                emailEnviado.DataEnvio = DateTime.Now;
                emailEnviado.Destinatarios = destinatarios;
                emailEnviado.EmailsCopia = copia;
                emailEnviado.Enviado = true;
                emailEnviado.NomeEmail = nomeEmail;
                emailEnviado.TextoEmail = this.Mensagem;
                emailEnviado.FuncionarioEnvio = funcionario;
                emailEnviado.Pedido = pedido;
                emailEnviado.Salvar();
                return true;
            }
            catch (Exception ex)
            {
                this.Erro = ex.Message + (ex.InnerException != null ? " - " + ex.InnerException.ToString() : "");
                return false;
            }
        }

        /// <summary>
        /// Envia o e-mail assincronamente, acionando o evento "EmailEnviado" quando concluído
        /// </summary>
        public void EnviarAssincrono(String nomeEmail, Funcionario funcionario, Pedido pedido)
        {
            try
            {
                if (ConfigurationManager.AppSettings["implantado"].ToString() != "true")
                    return;

                this.CriarEmail();
                //Servidor SMTP tem que estar configurado no WebConfig
                if (ConfigurationManager.AppSettings["servidorSMTP"] == null)
                {
                    this.Erro = "Servidor SMTP não definido no arquivo de configurações da aplicação";
                    if (this.EmailNaoEnviado != null)
                        this.EmailNaoEnviado(this);
                    return;
                }

                SmtpClient smpt = new SmtpClient(ConfigurationManager.AppSettings["servidorSMTP"].ToString());
                smpt.UseDefaultCredentials = true;
                smpt.SendCompleted += new SendCompletedEventHandler(smpt_SendCompleted);
                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
                smpt.SendAsync(this.email, mailMessage);

                String destinatarios = String.Join(";", this.EmailsDestino.Select(mail => mail.Address).Distinct().ToArray());
                String copia = (this.ComCopia == null && this.ComCopia.Count < 0 ? string.Empty : (String.Join(";", this.ComCopia.Select(mail => mail.Address).Distinct().ToArray())));

                //Salvar o e-mail enviado
                EmailEnviado emailEnviado = new EmailEnviado();
                emailEnviado.Assunto = this.Assunto;
                emailEnviado.DataEnvio = DateTime.Now;
                emailEnviado.Destinatarios = destinatarios;
                emailEnviado.EmailsCopia = copia;
                emailEnviado.Enviado = true;
                emailEnviado.NomeEmail = nomeEmail;
                emailEnviado.TextoEmail = this.Mensagem;
                emailEnviado.FuncionarioEnvio = funcionario;
                emailEnviado.Pedido = pedido;
                emailEnviado.Salvar();
            }
            catch (Exception ex)
            {
                this.Erro = ex.Message;
                if (this.EmailNaoEnviado != null)
                    this.EmailNaoEnviado(this);
            }
        }

        public void EnviarAssincronoAutenticado(String nomeEmail, Funcionario funcionario, Pedido pedido, int porta, bool useSSL)
        {
            try
            {
                if (ConfigurationManager.AppSettings["implantado"].ToString() != "true")
                    return;

                this.CriarEmail();
                if (ConfigurationManager.AppSettings["servidorSMTP"] == null ||
                    ConfigurationManager.AppSettings["userNameEmail"] == null ||
                    ConfigurationManager.AppSettings["passwordEmail"] == null)
                {
                    this.Erro = "Configuração do servidor SMTP ou de usuário ou de senha está(ão) faltando no arquivo de configurações da aplicação";
                    if (this.EmailNaoEnviado != null)
                        this.EmailNaoEnviado(this);
                    return;
                }

                SmtpClient smpt = new SmtpClient(ConfigurationManager.AppSettings["servidorSMTP"].ToString());
                smpt.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["userNameEmail"].ToString(), ConfigurationManager.AppSettings["passwordEmail"].ToString());
                smpt.Port = porta;
                smpt.EnableSsl = useSSL;

                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
                smpt.SendAsync(this.email, mailMessage);

                String destinatarios = String.Join(";", this.EmailsDestino.Select(mail => mail.Address).Distinct().ToArray());
                String copia = (this.ComCopia == null && this.ComCopia.Count < 0 ? string.Empty : (String.Join(";", this.ComCopia.Select(mail => mail.Address).Distinct().ToArray())));

                //Salvar o e-mail enviado
                EmailEnviado emailEnviado = new EmailEnviado();
                emailEnviado.Assunto = this.Assunto;
                emailEnviado.DataEnvio = DateTime.Now;
                emailEnviado.Destinatarios = destinatarios;
                emailEnviado.EmailsCopia = copia;
                emailEnviado.Enviado = true;
                emailEnviado.NomeEmail = nomeEmail;
                emailEnviado.TextoEmail = this.Mensagem;
                emailEnviado.FuncionarioEnvio = funcionario;
                emailEnviado.Pedido = pedido;
                emailEnviado.Salvar();
            }
            catch (Exception ex)
            {
                this.Erro = ex.Message;
                if (this.EmailNaoEnviado != null)
                    this.EmailNaoEnviado(this);
            }
        }

        public bool AdicionarAnexo(String caminhoAnexo)
        {
            if (this.anexos == null)
                this.anexos = new List<string>();
            if (File.Exists(caminhoAnexo))
            {
                this.anexos.Add(caminhoAnexo);
                return true;
            }
            return false;
        }

        public void AdicionarDestinatario(String destinatario)
        {
            try
            {
                if (!string.IsNullOrEmpty(destinatario))
                    this.EmailsDestino.Add(new MailAddress(destinatario));
            }
            catch (Exception)
            {
            }
        }

        public void AdicionarDestinatario(params String[] destinatario)
        {
            foreach (string s in destinatario)
            {
                try
                {
                    if (!string.IsNullOrEmpty(s))
                        this.EmailsDestino.Add(new MailAddress(s));
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        public void AdicionarCC(String destinatario)
        {
            try
            {
                if (!string.IsNullOrEmpty(destinatario))
                    this.ComCopia.Add(new MailAddress(destinatario));
            }
            catch (Exception)
            {
            }
        }

        public void AdicionarCC(params String[] destinatario)
        {
            foreach (string s in destinatario)
                try
                {
                    if (!string.IsNullOrEmpty(s))
                        this.ComCopia.Add(new MailAddress(s));
                }
                catch (Exception)
                {
                    continue;
                }
        }

        public void AdicionarCCO(String destinatario)
        {
            try
            {
                if (!string.IsNullOrEmpty(destinatario))
                    this.ComCopiaOculta.Add(new MailAddress(destinatario));
            }
            catch (Exception)
            {
            }
        }

        public void AdicionarCCO(params String[] destinatario)
        {
            foreach (string s in destinatario)
                try
                {
                    if (!string.IsNullOrEmpty(s))
                        this.ComCopiaOculta.Add(new MailAddress(s));
                }
                catch (Exception)
                {
                    continue;
                }
        }

        private void CriarEmail()
        {
            if (ConfigurationManager.AppSettings["emailContato"] == null)
                throw new Exception("E-mail de contato para envio de e-mail não definido no arquivo de configurações da aplicação");

            this.email = new System.Net.Mail.MailMessage();
            if (this.anexos != null && this.anexos.Count > 0)
                foreach (string caminhoArquivo in this.anexos)
                    this.email.Attachments.Add(new Attachment(caminhoArquivo) { Name = caminhoArquivo.Substring(caminhoArquivo.LastIndexOf('/') + 1) });

            this.email.From = new MailAddress(ConfigurationManager.AppSettings["emailContato"], "Ambientalis Manager");
            this.email.To.AddRange<MailAddress>(this.EmailsDestino);
            this.email.CC.AddRange<MailAddress>(this.ComCopia);
            this.email.Bcc.AddRange<MailAddress>(this.ComCopiaOculta);
            this.email.Subject = this.Assunto;
            this.email.Body = this.Mensagem;
            this.email.IsBodyHtml = this.BodyHtml;
            //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
            this.email.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
        }

        #endregion
    }
}
