<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="iframe_AddDepartOffice.aspx.cs"
    Inherits="SQS.UI.Pages.iframe_AddDepartOffice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <x:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
    <x:Panel ID="Panel1" runat="server" BodyPadding="0px" ShowBorder="false" ShowHeader="false"
        Title="Panel" AutoScroll="true">
        <Items>
            <x:Toolbar ID="Toolbar1" runat="server">
                <Items>
                    <x:Button ID="Button_AddNew" runat="server" Text="新增" ConfirmText="确定新增？" OnClick="Button_AddNew_Click">
                    </x:Button>
                    <x:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                    </x:ToolbarSeparator>
                    <x:Button ID="Button_Cancel" runat="server" Text="取消">
                    </x:Button>
                </Items>
            </x:Toolbar>
            <x:Panel ID="Panel2" runat="server" BodyPadding="5px" ShowBorder="false" ShowHeader="false"
                Title="Panel">
                <Items>
                    <x:Panel ID="Panel3" runat="server" BodyPadding="5px" ShowBorder="false" ShowHeader="false"
                        Title="Panel" Layout="HBox">
                        <Items>
                            <x:RadioButton ID="RadioButton_SelectDepart" GroupName="AddType" runat="server" Label="" Text="选择部系"
                                AutoPostBack="true" OnCheckedChanged="RadioButton_AddType_CheckedChanged" Checked="true">
                            </x:RadioButton>
                            <x:DropDownList ID="DropDownList_Depart" runat="server" Label="Label">
                            </x:DropDownList>
                        </Items>
                    </x:Panel>
                    <x:Panel ID="Panel4" runat="server" BodyPadding="5px" ShowBorder="false" ShowHeader="false"
                        Title="Panel" Layout="HBox">
                        <Items>
                            <x:RadioButton ID="RadioButton_TypeDepart" GroupName="AddType" runat="server" Label="" Text="新增部系"
                                AutoPostBack="true">
                            </x:RadioButton>
                            <x:TextBox ID="TextBox_Depart" runat="server" Label="Label" Text="" Enabled="false">
                            </x:TextBox>
                        </Items>
                    </x:Panel>
                    <x:TextArea ID="TextArea_Office" runat="server" Height="50px" Label="Label" Text="" EmptyText="请输入处室名称（多个处室以“；”隔开）"
                        Width="400px">
                    </x:TextArea>
                </Items>
            </x:Panel>
        </Items>
    </x:Panel>
    </form>
</body>
</html>
