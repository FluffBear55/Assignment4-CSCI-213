<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administrator.aspx.cs" Inherits="Assignment_4.Administration_Page.Administrator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
        Instructors<asp:GridView ID="InstructorGridView" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="InstructorID" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="InstructorID" HeaderText="InstructorID" ReadOnly="True" SortExpression="InstructorID" />
                <asp:BoundField DataField="InstructorFirstName" HeaderText="InstructorFirstName" SortExpression="InstructorFirstName" />
                <asp:BoundField DataField="InstructorLastName" HeaderText="InstructorLastName" SortExpression="InstructorLastName" />
                <asp:BoundField DataField="InstructorPhoneNumber" HeaderText="InstructorPhoneNumber" SortExpression="InstructorPhoneNumber" />
                <asp:ButtonField Text="Delete User" />
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
    </p>
    <p>
        Users</p>
    <p>
        <asp:GridView ID="MemberGridView" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Member_UserID" DataSourceID="SqlDataSource2" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Member_UserID" HeaderText="UserID" ReadOnly="True" SortExpression="Member_UserID" />
                <asp:BoundField DataField="MemberFirstName" HeaderText="FirstName" SortExpression="MemberFirstName" />
                <asp:BoundField DataField="MemberLastName" HeaderText="LastName" SortExpression="MemberLastName" />
                <asp:BoundField DataField="MemberDateJoined" HeaderText="DateJoined" SortExpression="MemberDateJoined" />
                <asp:BoundField DataField="MemberPhoneNumber" HeaderText="PhoneNumber" SortExpression="MemberPhoneNumber" />
                <asp:BoundField DataField="MemberEmail" HeaderText="Email" SortExpression="MemberEmail" />
                <asp:ButtonField Text="Delete User" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Member]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Instructor]"></asp:SqlDataSource>
    </p>
    <p>
        <table style="width:100%;">
            <tr>
                <td style="width: 152px">New Member:&nbsp;&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 152px">
                    <asp:TextBox ID="UserNameBox" runat="server"></asp:TextBox>
                </td>
                <td>Username</td>
            </tr>
            <tr>
                <td style="width: 152px">
                    <asp:TextBox ID="PasswordBox" runat="server"></asp:TextBox>
                </td>
                <td>Password</td>
            </tr>
            <tr>
                <td style="width: 152px">
                    <asp:TextBox ID="FirstNameBox" runat="server"></asp:TextBox>
                </td>
                <td>First Name</td>
            </tr>
            <tr>
                <td style="width: 152px; height: 27px">
                    <asp:TextBox ID="LastNameBox" runat="server"></asp:TextBox>
                </td>
                <td style="height: 27px">Last Name</td>
            </tr>
            <tr>
                <td style="width: 152px">
                    <asp:TextBox ID="PhoneBox" runat="server"></asp:TextBox>
                </td>
                <td>Phone Number</td>
            </tr>
            <tr>
                <td style="width: 152px; height: 27px">
                    <asp:TextBox ID="EmailBox" runat="server"></asp:TextBox>
                </td>
                <td style="height: 27px">Email</td>
            </tr>
            <tr>
                <td style="width: 152px; height: 22px">
                    <asp:CheckBox ID="InstructorBox" runat="server" Text=" Instructor" />
                </td>
                <td style="height: 22px"></td>
            </tr>
            <tr>
                <td style="width: 152px">
                    <asp:Button ID="SubmitButton" runat="server" OnClick="SubmitButton_Click" Text="Submit" />
                </td>
                <td>
                    <asp:Label ID="ErrorLabel" runat="server" style="color: #FF0000" Text="ErrorLabel" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    </p>
    <p>
    </p>
</asp:Content>
