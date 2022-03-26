<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 
<nav class="navbar navbar-expand-lg navbar-light bg-light">
  <div class="container">
    <a class="navbar-brand" href="#"><img class="img-brand" src="Content/ulpgc.png" /></a>
  </div>
   <div class="mx-5">
         <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
           <asp:ListItem text="Español" value="es" ></asp:listitem>
           <asp:listitem text="English" value="en"></asp:listitem>
           <asp:listitem text="Polski" value="pl"></asp:listitem>
      </asp:DropDownList>
   </div>
</nav>

    <!-- Page Content -->
    <div class="container">
        <div class="card border-0 shadow my-5">
            <div class="card-body p-5 w-100">
                <h1 class="fw-light text-center">
                    <asp:Label ID="Label1" runat="server" Text="Label"> Conversor de números a texto en polaco</asp:Label>
                   </h1>
                <div class="mb-3 mt-5">
                    <asp:Label class="form-label" ID="Label2" runat="server" Text="Label">Escriba su número:</asp:Label>
                    <asp:TextBox class="form-control" ID="userinput" runat="server" OnTextChanged="UserInput_TextChanged" Width="100%"></asp:TextBox>
                </div>
                <asp:Button class="btn btn-primary" ID="convertBtn" runat="server" Text="Convertir" OnClick="Convert_Click" />
                <div class="text-center mt-5">
                    <hr class="divider"/>
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
