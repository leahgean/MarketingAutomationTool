
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@import namespace="System.Net"%>
<%@import namespace="System.Net.Mail"%>
<script language="C#" runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        MailMessage m = new MailMessage();
        SmtpClient sc = new SmtpClient();
         try
            {
                m.From = new MailAddress("ldiopenes@asiaonline1.com");               
                       m.To.Add("mcdrosales@mailinator.com");
                m.Subject = "This is a Test Mail";
                m.IsBodyHtml = true;
                m.Body = "test gmail";
                sc.Host = "smtp.gmail.com";
                sc.Port = 587;
                sc.Credentials = new System.Net.NetworkCredential("marketingautomationtool@gmail.com","m4rk3t1ng4ut019");
                      
                sc.EnableSsl = true;
                sc.Send(m);
                Response.Write("Email Send successfully");
            }
            catch (Exception ex)
            {
                Response.Write("<p>"+ex.Message+"</p>");
            }
    }
</script>
<html>
<body>
    <form runat="server">
        <asp:Label id="lblMessage" runat="server"></asp:Label>
    </form>
</body>
</html>
