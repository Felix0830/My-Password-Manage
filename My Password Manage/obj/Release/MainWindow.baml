   M S B A M L   `   `   `  ����   My Password Manage#    My_Password_Manage.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  My Password Manage��$	��600��$
��1000�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=� � 	Resources  �� 
TargetType �� Property �� Value��%�   '��       '�� '     'G� N     �� " ��.���  � ��$ Center=��� " ��.���  � ��$ Center=��� " G�.���  � ��$ Center=�.��� .+�� .+��� -   $��	tabManWin��.@��� $��浏览��.���  � RowDefinitions �� $��40 ��� .+�� .+��� -   $��labType��$��选择类型：��$��30��$��70��$
��Left=��� -   $��cbChooseType��$	��150��$��30��$
��Left=�$��70,0,0,0q��� -   $��	labPwdKey��$��请输入密钥：��$��30��$��80��$
��Left=�$��	230,0,0,0q�G� -   $��	txtPwdKey��$��30��$	��180��$
��Left=�$��	310,0,0,0q��� -   $��
btnShowPwd��$��查看��$��30��$��60��$
��Left=�$��	500,0,0,0q�]� $��1��$	��500��$	��Top=�.+�&   System.Windows.Controls.DataGrid  -   $��dgPwdInfoView��   AutoGenerateColumns .  $	��Top=�	   
IsReadOnly	 . ���� 
   Columns
 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Id��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 100    
Visibility$ Hidden=�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Id+  $ 类型��$	 100 $ Hidden=� �� *TypeID+  $ 站点��$	 190  �� *Url+  $ 	用户名��$	 150  �� *Uid+  $ 密码��$	 160  �� *Pwd+  $ 描述��$	 350  �� *	Explain+4  .System.Windows.Controls.DataGridTemplateColumn  $ 修改��$ *    CellTemplate �� .R��� $��	btnUpdate��-	   $��25��$��60��$��修改��
 � Tag �� *Id+�� $��添加��.���  � ColumnDefinitions �� $��80 ���  �� �� �� �� �� �� �� $	��100 ��� .+��� -
   $��
lbSiteType��$��类  型：��$��Right=�$��Center=��� -   $��
cbSiteType��$
��Left=�$��Center=�$��30��$	��300��$��1���� -   $��lbSite��$��网  址：��$��Right=�$��Center=�$��1���� -   $��txtSite��$��18(�$
��Left=�$��Center=�$	��450��$��30��$��1��$��1���� -   $��
lbUserName��$��用户名：��$��Right=�$��Center=�$��2���� -   $��txtUserName��$��18(�$
��Left=�$��Center=�$	��450��$��30��$��2��$��1���� -   $��	lbUserPwd��$��密  码：��$��Right=�$��Center=�$��3��G� -   $��
txtUserPwd��$��18(�$
��Left=�$��Center=�$	��450��$��30��$��3��$��1���� -   $��lbUserPwdConfirm��$��确认密码：��$��Right=�$��Center=�$��4��G� -   $��txtUserPwdConfirm��$��18(�$
��Left=�$��Center=�$	��450��$��30��$��4��$��1���� -   $��lbPasswordKey��$��密  钥：��$��Right=�$��Center=�$��5���� -   $��txtPasswordKey��$��18(�$
��Left=�$��Center=�$	��450��$��30��$��5��$��1���� -   $��	lbRemarks��$��备  注：��$��Right=�$��Center=�$��6���� -   $��txtUserRemarks��$��15(�$
��Left=�$��Center=� �� TextWrapping$
 Wrap=� �� AcceptsReturn . " �� VerticalScrollBarVisibility$ Visible=�$	��Top=�$	��600��$	��100��$��6��$��1���� -   $��
btnAddSite��$��添  加��$��80��$��40��$
��Left=�$��7��$��1���� $��配置��.��� .+��� $��添加网站类型��$
��Left=�$��	25,10,0,0q�$	��Top=�$	��125��$	��183��.���  �� $��50 ��� .+��� -   $��txtSiteType��$
��Left=�$��23��$��13,0,0,0q�$��Center=�$	��150���� -   $��btnAdd��$��1��$��30��$	��100��$
��Left=�$	��Top=�$��30,0,0,0q�$��确  定��