   M S B A M L   `   `   `  ����>  :GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GUIGUI  5          GUI.Page_Gebruiker   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      #localclr-namespace:GUI     5        N� Title$  Page_Gebruiker��5	      .��5      ]� .+�5   
   �� $
��Left=�6   $	��315��60   $��15q�6<   .+�5      �� $��20(�6   $
��Bold"�6&   B� 69   .�6C   
Gebruiker:6O   6[   �� 5       �� Orientation$ Vertical=�6   $��0 5 0 0q�60   .+�5      �� .��6   Filter Naam:6&   �� 5      -   5         ��Name$ txtFilterNaam��5   R   $
��Left=�6   $��5 0 0 0q�65   $	��200��6F   6i   5      �� 5      -   5         ��Name$ lvGebruiker��5      $��0 10 0 0q�6,    �� SelectionMode$ Single=�6>   $	��310��6U    �� View 5      �� 5      .P�5      �� $��ID��6)    �� Width$
 auto��5   )    �� DisplayMemberBinding 5   )   �� *Id+�� 5      $
��Naam��6)   $
 auto��5   )    5   )   �� *Naam+�� 5      $��Voornaam��6)   $
 auto��5   )    5   )   �� *
Voornaam+5      5       5!      �� 5"   
   $��Right=�6   $	��315��61   $��15q�6=   .+�5#      �� $	��Top=�6   $	��350��61   .+�5$      �� $��20(�6   $
��Bold"�6*   B� 6=   .�6G   Select Datum:6V   6b   �� 5%      -   5         ��Name$ cmbDatum��5%   -   $��0 10 0 0q�6   51      52      �� 53      $��Bottom=�6   $��50��64   .+�54      �� -   5        $��30��54      $	��200��6%   $��	0 10 0 10q�61   
����d�$��14(�6`   $��Medium"�6n   ���#324b77$��2q�6�   	 � 	Resources	 55      
 �� 
TargetType �� Property �� Value��%_   '��       �� 56      "
 ��6    .�57      ��  �@��CornerRadius$ 10��6=   58      59      .��6(   Maak Dagplan5;      5<      5=      5>      5?      