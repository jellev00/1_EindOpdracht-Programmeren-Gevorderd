   M S B A M L   `   `   `  ����>  :GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GUIGUI  5          GUI.Page_DagplanGebruiker   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      #localclr-namespace:GUI     5        N� Title$  Page_DagplanGebruiker��5	      .��5
      ]� .+�5   
   �� $
��Left=�6   $	��220��60   $��
15 15 0 15q�6<   .+�5      �� $��20(�6   $
��Bold"�6&   B� 69   .�6C   
Gebruiker:6O   6[   �� 5       �� Orientation$ Vertical=�6   $��0 5 0 0q�60   .+�5      �� .��6   Filter Naam:6&   �� 5      -   5         ��Name$ txtFilterNaam��5   R   $
��Left=�6   $��5 0 0 0q�65   $	��200��6F   6i   5      �� 5      -   5         ��Name$ lvGebruiker��5      $��0 10 0 0q�6,    �� SelectionMode$ Single=�6>   $	��315��6U   $��Hidden=�6b    �� View 5      �� 5      .P�5      �� $��ID��6)    �� Width$
 auto��5   )    �� DisplayMemberBinding 5   )   �� *Id+�� 5      $
��Naam��6)   $
 auto��5   )    5   )   �� *Naam+�� 5      $��Voornaam��6)   $	 100��5   )    5   )   �� *
Voornaam+5      5      5       �� 5"   
   $��Right=�6   $	��420��61   $��
0 15 15 15q�6=   .+�5#      �� $	��Top=�6   $	��350��61   .+�5$      �� $��20(�6   $
��Bold"�6*   B� 6=   .�6G   Gebruikers Dagplan:6\   6h   �� 5%      -   5        $ 	lvDagplan��5%      $��0 10 0 0q�6.   $ Single=�6@   $	��315��6W   $��Visible=�6d    5&      �� 5'      .P�5(      �� $��ID��6-   $
 auto��5)   )    5*   )   �� *Id+�� 5+      $��
GebuikerID��6-   $
 auto��5,   )    5-   )   �� *GebruikerId+�� 5.      $��Datum��6-   $
 auto��5/   )    50   )   �� *Datum+51      53      54      �� 55      $��Bottom=�6   $ 
Horizontal=�64   $��50��6M   .+�56      �� -   5        $��30��56      $	��180��6%   $��20 10 10 10q�61   
����d�$��14(�6b   $��Medium"�6p   ���#324b77$��2q�6�    � 	Resources 57      	 �� 
TargetType
 �� Property �� Value��%_   '��       �� 58      "	 ��6    .�59      �� 
 �@��CornerRadius$ 10��6=   5:      5;      .��6(   Toon Dagplan5=      �� 5?      -   5        $��30��5?      $	��180��6%   $��10 10 20 10q�61   
����d�$��14(�6b   $��Medium"�6p   ���#324b77$��2q�6�    5@      ��%_   '��       �� 5A      "	 ��6    .�5B      �� 
 �@��CornerRadius$ 10��6=   5C      5D      .��6(   Verwijder Dagplan5F      5G      5H      5I      5J      