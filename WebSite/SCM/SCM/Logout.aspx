<%@ Page Language="C#" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Session.Clear();
        }
        catch (Exception ex) { }
        Response.Redirect("login.aspx",false);
    }
</script>
