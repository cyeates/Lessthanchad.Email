using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lessthanchad.Email
{
  public class EmailSettings
  {
    public string SmtpServer { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string FromEmailAddress { get; set; }
    public string FromDisplayName { get; set; }
  }
}
