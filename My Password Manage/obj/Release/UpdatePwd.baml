   M S B A M L   `   `   `  ����   My Password Manage"    My_Password_Manage.UpdatePwd   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     �Name$  UpdatePwdInfo�� � Title$ 	UpdatePwd��$	��600��$	��700�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�.���  � ColumnDefinitions �� $��80 ���  � RowDefinitions �� �� �� �� �� �� �� �� $	��100 ��� .+��� -   $��
lbSiteType��$��类  型：��$��Right=�$��Center=��� -   $��
cbSiteType��$
��Left=�$��Center=�$��30��$	��300��$��1���� -   $��lbSite��$��	网址：��$��Right=�$��Center=�$��1���� -   $��txtSite��$��18(�$
��Left=�$��Center=�$	��450��$��30��$��1��$��1�� �� 
IsReadOnly . ���	#FFF6F5F5�� -   $��
lbUserName��$��用户名：��$��Right=�$��Center=�$��2���� -   $��txtUserName��$��18(�$
��Left=�$��Center=�$	��450��$��30��$��2��$��1�� . ���	#FFF6F5F5�� -   $��lbUserOldPwd��$��旧密码：��$��Right=�$��Center=�$��3��G� -	   $��txtUserOldPwd��$��18(�$
��Left=�$��Center=�$	��450��$��30��$��3��$��1���� -
   $��	lbUserPwd��$��新密码：��$��Right=�$��Center=�$��4��G� -   $��
txtUserPwd��$��18(�$
��Left=�$��Center=�$	��450��$��30��$��4��$��1���� -   $��lbUserPwdConfirm��$��确认新密码：��$��Right=�$��Center=�$��5��G� -   $��txtUserPwdConfirm��$��18(�$
��Left=�$��Center=�$	��450��$��30��$��5��$��1���� -   $��lbPasswordKey��$��	密钥：��$��Right=�$��Center=�$��6���� -   $��txtPasswordKey��$��18(�$
��Left=�$��Center=�$	��450��$��30��$��6��$��1��$��********�����	#FFF6F5F5 . �� -   $��	lbRemarks��$��	备注：��$��Right=�$��Center=�$��7���� -   $��txtUserRemarks��$��15(�$
��Left=�$��Center=� �� TextWrapping$
 Wrap=� �� AcceptsReturn . "	 �� VerticalScrollBarVisibility$	 Visible=�$	��Top=�$	��600��$	��100��$��7��$��1���� -   
 ��Name$
 	btnUpdate��$��确认修改��$��80��$��40��$
��Left=�$��8��$��1��