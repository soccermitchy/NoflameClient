   M S B A M L   `   `   `  ����H  DNoFlameClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication1WpfApplication1  5   	        WpfApplication1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfApplication1     5   	     � Title$  NoFlame VPN Client��5   	   $	��500��6$   $	��800��61    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowStyle$
 None=�6.    � 
ResizeMode$ CanMinimize=�5
   	    � Icon$ Resources/icon.ico��6"    � AllowsTransparency .  � UseLayoutRounding . .��5      �� ���	#FF1C1C1C���	#FF0F0F0F$��10,40,10,10q�6<    � 	Resources 5   
    �� 
TargetType	 �� Property
 �� Value��%e   '��       �� 5      " ��6   .�5      �� 	 �@^�InitialShowDelay$

 2000��6D   5      5      ._�5   
   � .+�5      � -   5         �Name$ titleBar��5      $	��Top=�6%   $��40��6=   $��	0,-40,0,0q�6I   ���	#FF0F0F0F � Cursor$
 Hand��6s   .+�5      �� $ ��Resources/noflame_icon.png��6   $��UniformToFill=�6<   $��20��6T   $
��Left=�6_   $��20��6z   $��10,0,0,0q�6�   �� 5      
�������$<��6/WpfApplication1;component/Resources/#Montserrat Light)�6/   $��18(�6s   $
��Left=�6�   $��40,6,0,0q�6�   $��20��6�   NOFLAME VPN CLIENT6�   �� 5       �� Orientation$ 
Horizontal=�6   $	��120��66   $��Right=�6B   .+�5      �� -   5         ��Name$ btnMinimize��5       � ToolTip$ Minimize The Client��62   ���	#FF0F0F0F
�������$��60��6�   $��Right=�6�   ��6g   Z� ��5      �� 5      " ��6$   .�5   "   �� 	 � ��
 5   &   �� 5   *    �� 
TargetType" ��6:   .R�5   .   ��  ��Name$ Panel��65   #��z��6D   ._�5   2   �� $��Resources/btnMinimize.png��68   $��20��6[   $��20��6f   $��Center=�6r   $��Center=�5   4   $
��None=�6O   5    /    �� Triggers 5!   .   P� 5"   2    P� Property �@J�IsMouseOver P� Value . . �5#   6   ��  �� 
TargetName$ Panel��6=   	 � ��
 �	#FF1C1C1C6f   5$   3   5&   +   5(   #   5)      5+      �� 5,      -   5        $ btnClose��5,      $ Close the client��6/   ���	#000F0F0F
�������$��60��6�   $��Right=�6�   ��6a   Z� ��5-      �� 5.      " ��6$   .�5/   "   �� 	 � ��
 50   &   �� 51   *   " ��6:   .R�52   .   �� $ Panel��65   #��z��6D   ._�53   2   �� $��Resources/btnClose.png��68   $��20��6X   $��20��6c   $��Center=�6o   $��Center=�54   4   $
��None=�6O   55   /    56   .   P� 57   2    �@J�IsMouseOver . . �58   6   �� $ Panel��6=   	 � ��
 �	#FF1C1C1C6f   59   3   5;   +   5=   #   5>      5@      5A      5B      � 5C       � RowDefinitions 5D      �� 5E      $
��136* �6$   �� 5F      $	��89* �6$   .+�5H      �� $!��Resources/logo_with_sub.png��6   $��68,0,0,0q�6=   $	��480��6O   $��1��6[   $
��Left=�6l   � 5I      $��
20,0,20,20q�6   $��1��6+   .+�5J      �� $ 
Horizontal=�6!   .+�5K      �� $	��Top=�6%   .+�5L      �� -   5         ��Name$ labelUsername��5L   (   
�������$:��4/NoFlameClient;component/Resources/#Montserrat Light)�6R   $��18(�6�   $��0q�6�   $��1��6�   $
��Left=�6�   $	��Top=�6�   
USERNAME6�   �� 5M      -   5         ��Name$ textboxUsername��5M   &   $
��Left=�6?   $	��Top=�6Z   $	��360��6r   $��45��6~   $��0,20,0,0q�6�   $:��4/NoFlameClient;component/Resources/#Montserrat Light)�6�   $��16(�6�   $��
10,13,10,0q�6�   $��0q�6  5N      �� 5O      $��20,0,0,0q�6%   $	��Top=�67   .+�5P      �� -   5        $ labelPassword��5P   (   
�������$:��4/NoFlameClient;component/Resources/#Montserrat Light)�6R   $��18(�6�   $��0q�6�   $��1��6�   $
��Left=�6�   $	��Top=�6�   
PASSWORD6�   G� 5Q      -   5         G�Name$ textboxPassword��5Q   *   $
��Left=�6C   $	��Top=�6^   $	��360��6v   $��45��6�   $��0,20,0,0q�6�   $:��4/NoFlameClient;component/Resources/#Montserrat Light)�6�   $��16(�6�   $��
10,13,10,0q�6�   $��0q�6  5R      5S      5U      5V      5W      5X      5Y      