   M S B A M L   `   `   `  ����A  =Random, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:randrand  5   	       rand.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:rand     5   	     � Title$  Рандом��5   	   $	��500��6   $	��600��6%   .��5	      � .+�5
   
   �� $��Center=�6   $��Center=�62   .+�5      �� $��Center=�6   $��Random emae��65   $��25(�6H   �� 5      -   5         ��Name$ ResultTextBlock��5   +   
�������$��Center=�6D   $��61��6a   $��25(�6k   $��WrapWithOverflow=�6y   $	��300��6�   �� 5      -   5        $��5q�5   4   $ ��Сгенерировать��6?   
��� � �
�������$��30(�6}   $��Italic$�6�   �� 5      $��Center=�6   $I��CВыберите количество случайных чисел��65   $��12(�6`   $��Arial)�6n   �� 5      -   5         ��Name$ GetCountNumbersSlider��5       � Minimum$ 1Q�64    � Maximum$ 50Q�6@   5      �� 5      -   5        $ NumbersCounter��5      $��Center=�60   $1��+Выбранное кол-во чисел 1��6M   $��12(�6m   $��Arial)�6{   � 5       � ColumnDefinitions 5      �� 5      �� 5      �� 5      .+�5      	� -   5         	�Name$ RangeRadioButton��5   /    d� 	IsChecked$
 TrueY�6   $��0��6l   $��из диапазона��6|   $��15(�6�   $��Center=�6�   	� 5      -   5        $ ListRadioButton��5      $��2��6Y   $��из списка��6i   $��15(�6}   $��Center=�6�   5      � 5      -   5         �Name$ 
RangePanel��5      $
��0,10q�6'    5      �� 5      �� 5      .+�5!      �� -   5        	 ��Name$	 
StartRange��5!   �   $��1��6G   $��0��6�   $��5q�6�   

 � Tag$

 От��6�   �� 5"      -	   5        $	 FinishRange��5"   �   $	��100��6G   $��1��6�   $��5q�6�   $

 До��6�   5#      � 5$      -
   5        $ ListPanelNumbers��5$      $
��0,15q�6-   .+�5%      �� -   5        $	 ListPanelNumbersOfBox��5%   G   $	��200��6f   $	��100��6r   $R
 LВводите сюда числа, деля их пробелами (1 2 3)��6   5&      �� 5'      -   5         ��Name$ ElliminationAllNumbersBox��5'   k   $��Исключение��6�   $��Center=�6�   $��5q�6�   $��15(�6�   � 5(      -   5        $! ElliminationAllNumbersPanel��5(      $
��0,10q�68   .+�5)      �� -   5        $!	 ElliminationAllNumbersOfBox��5)   G   $	��200��6l   $	��100��6x   $R
 LВводите сюда числа, деля их пробелами (1 2 3)��6�   5*      �� 5+      -   5        $ ElliminationRepeat��5+      $!��Без повторения��63   $��Center=�6L   $��20q�6i   $��15(�6u   5,      5-      5.      