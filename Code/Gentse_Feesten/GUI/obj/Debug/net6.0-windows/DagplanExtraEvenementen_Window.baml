   M S B A M L   `   `   `  ����>  :GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GUIGUI  5   	   (    "GUI.DagplanExtraEvenementen_Window   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:GUI     5   	     � Title$$  DagplanExtraEvenementen_Window��5   	   $	��450��60   $	��800��6=   .��5	      ]� .+�5
   
   �� .+�5      �� $	��Top=�6   $��
20 10 20 5q�61   $	��760��6E   $	��345��6Q   .+�5      �� $��20(�6   $
��Bold"�6*   B� 6=   .�6G   
Dagplan:6Q   6]   �� 5       �� Orientation$ 
Horizontal=�6   $��0 10 0 0q�66   .+�5      �� $ Vertical=�6!   .+�5      �� .��6    Filter Titel:6/   �� 5      -   5         ��Name$ txtFilterTitel��5   Z   $
��Left=�6"   $��5 0 0 0q�6=   $	��200��6N   6r   5      5      �� 5      -   5         ��Name$ lvEvenement��5      $��0 15 0 0q�60    �� SelectionMode$ Single=�6B   $	��245��6Y   $��Visible=�6f    �� View 5      �� 5      .P�5      �� $��ID��6-    �� Width$
 auto��5   )    �� DisplayMemberBinding 5   )   �� *Id+�� 5      $��Title��6-   $
 auto��5   )    5   )   �� *Titel+�� 5      $��	Starttijd��6-   $
 auto��5   )    5   )   �� *	Starttijd+�� 5      $��Eindtijd��6-   $
 auto��5    )    5!   )   �� *
Eindtijd+�� 5"      $��Prijs��6-   $
 auto��5#   )    5$   )   �� *Prijs+�� 5%      $��beschrijving��6-   $
 auto��5&   )    5'   )   �� *Beschrijving+5(      5*      5+      �� 5,      $��Bottom=�6   $	��760��64   $��50��6@   $ 
Horizontal=�6L   .+�5-      �� -   5        $	��250��5-      $��40��6%   $��	80 0 50 0q�61   $��18(�6D   $��Medium"�6R   
����d����#324b77$��3q�6�    � 	Resources 5.      	 �� 
TargetType
 �� Property �� Value��%_   '��       �� 5/      "	 ��6    .�50      �� 
 �@��CornerRadius$ 10��6=   51      52      .��6(   Evenement Toevoegen54      �� 55      -   5        $	��250��55      $��40��6%   $��	50 0 80 0q�61   $��18(�6D   $��Medium"�6R   
����d����#324b77$��3q�6�    56      ��%_   '��       �� 57      "	 ��6    .�58      �� 
 �@��CornerRadius$ 10��6=   59      5:      .��6(   
Ga terug5<      5=      5>      5?      5@      