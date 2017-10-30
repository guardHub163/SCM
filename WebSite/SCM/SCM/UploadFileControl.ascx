<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadFileControl.ascx.cs"
    Inherits="SCM.Web.UploadFileControl" %>
    <input type="file" runat="server" id="aFile" style="width: 300px; height: 22px; border: 1px solid  Green;" />
    <asp:LinkButton ID="btnUpload" runat="server" Text="上传" CssClass="LinkButton2" OnClick="Process_Upload"></asp:LinkButton>
    <asp:LinkButton ID="btnCancel" runat="server" Text="取消" CssClass="LinkButton2" OnClick="Process_Cancel"></asp:LinkButton>
