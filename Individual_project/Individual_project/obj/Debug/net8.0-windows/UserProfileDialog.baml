   M S B A M L   `   `   `  ����  Individual_project*    $Individual_project.UserProfileDialog   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$!  Личный кабинет��5   	   $	��250��6    $	��400��6-    � WindowStartupLocation$ CenterOwner=�5   	    � 
ResizeMode$ NoResize=�6-    � WindowStyle$ SingleBorderWindow=�5   	    � ShowInTaskbar$ False��6*   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $	��120 �6   �� 5      $��* �6   .+�5   
   �� $��Логин:��6   $��Center=�6"   �� 5   
   -   5         ��Name$ LoginBox��5      $��1��6$   $��4q�64   $	��160��6?   �� 5   
   $��Пароль:��6   $��Center=�6#   $��1��6>   G� 5   
   -   5         G�Name$ PasswordBox��5      $��1��6+   $��1��6;   $��4q�6H   $	��160��6S   �� 5   
   -   5        	 ��Name$	 	ErrorText��5      $��2��6'   $��2��64   
���  ��$��4,10,4,4q�6Y   $
��Wrap=�6k   �� 5   
   $��3��6   $��2��6"   
 �� Orientation$
 
Horizontal=�66   $��Right=�6O   $��0,10,0,0q�6k   .+�5      �� -   5        $��Сохранить��5      $	��100��6)   $��0,0,8,0q�65    �� 	IsDefault$
 True��6F   6W   �� 5      -   5        $��Отменить��5      $	��100��6(    �� IsCancel$
 True��64   6D   5      5      5      