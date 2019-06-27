using Apka2.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace Apka2.Services.Backgrand
{
    internal class PushEmail : IHostedService, IDisposable
    {

        private readonly ILogger _logger;
        private Timer _timer;
        private readonly Context _context;


        public PushEmail(ILogger<PushEmail> logger, Context context)
        {
            _logger = logger;
            _context = context;

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromHours(6));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var userToEmail = _context.Users.Select(x => x.Username).ToList();

            var timeEnd = _context.Laws.ToList();

            foreach (var item in timeEnd)
            {
                if(item.DateEnd>DateTime.Now)
                {
                    var credentials = new NetworkCredential("kamilonsz3@gmail.com", "euvictest");


                    var mail = new MailMessage()
                    {
                        From = new MailAddress("kamilonsz3@gmail.com"),
                        Subject = "Wyniki ustawy " + item.Name,
                        Body = "Głosowanie na ustgawe "+ item.Name+ " dobiegło konca sprawdz wyniki na stronie współdzielni"
                    };


                    foreach (var user in userToEmail)
                    {
                        mail.To.Add(new MailAddress($"{user}"));

                    }

                    // Smtp client
                    var client = new System.Net.Mail.SmtpClient()
                    {
                        Port = 587,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Host = "smtp.gmail.com",
                        EnableSsl = true,
                        Credentials = credentials
                    };

                    // Send it...         
                    client.Send(mail);
                }
            }


            



        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
