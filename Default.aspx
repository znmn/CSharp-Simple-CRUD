<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASP_Project.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <title>PBO B Zain</title>
</head>
<body>
    <form id="formMhs" class="form-group" runat="server">
        <div class="container" style="width: 90%;" />
        <h1 class="text-center">MASUKKAN DATA</h1>
        <div class="mb-3">
            <label for="txtEmpNIM" class="form-label">NIM</label>
            <input type="text" class="form-control" id="txtEmpNIM" runat="server" />
        </div>
        <div class="mb-3">
            <label for="txtEmpFname" class="form-label">Nama Depan Mahasiswa</label>
            <input type="text" class="form-control" id="txtEmpFname" runat="server" />
        </div>
        <div class="mb-3">
            <label for="txtEmpLname" class="form-label">Nama Belakang Mahasiswa</label>
            <input type="text" class="form-control" id="txtEmpLname" runat="server" />
        </div>
        <div class="mb-3">
            <label for="txtEmpPhone" class="form-label">No HP Mahasiswa</label>
            <input type="tel" class="form-control" id="txtEmpPhone" runat="server" />
        </div>
        <div class="text-center">
            <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Insert" OnClick="btnInsertion_Click" />
            <%--Style="width: 48px"--%>
            <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="Update" OnClick="btnUpdation_Click" />
            <br />
            <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <br />

        <h1 class="text-center">PENGHAPUSAN DATA</h1>
        <div class="mb-3">
            <label for="txtMhsNIM" class="form-label">NIM</label>
            <input type="number" class="form-control" id="txtMhsNIM" runat="server" />
        </div>
        <div class="text-center">
            <asp:Button ID="Button3" class="btn btn-danger" runat="server" Text="Delete" OnClick="btnDelete_Click" />
            <br />
            <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
        </div>

        <br />
        <div class="text-center">
            <h1>PENGAMBILAN DATA</h1>
            <asp:Button ID="Button4" class="btn btn-primary" runat="server" Text="Ambil Semua Data" OnClick="btnSelect_Click" />
            <br />
            <br />
            <asp:GridView ID="GridView1" class="table table-striped table-hover" runat="server"></asp:GridView>
        </div>
    </form>
    <br />
</body>
</html>
