   M S B A M L   `   `   `  ����  TangibleStories2(    "SurfaceApplication1.SurfaceWindow1   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      e aMicrosoft.Surface.Presentation, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e351s)http://schemas.microsoft.com/surface/2008  5        � Title$  SurfaceApplication1��5       � 	Resources 5         ImageAndCaptionTemplate  ScatterTemplate  WindowBackground j� Stretch �� Opacity��%�  &	        &	     &	 �    �� 5	   
   .R�5
      �� .+�5      �� -   5        $	��230��5   2   $	��130��6>   $��UniformToFill=�6K   ��6   �� *Source+6c   �� 5      $	��220��65   $
��Wrap=�6A   $��Center=�6U   ��6   �� *	Caption+6U   5      5      �� 5   
   .R�5      �� .+�5      �� $��UniformToFill=�63   ��6   �� *Source+63   5      5      �� 5   
   $
 None=�5   	   $	 0.6Q�6   5      .��5"      � #��[ 6    � RowDefinitions 5#   
   �� 5%      $	��165 �6   �� 5&      $��* �6   .+�5+   
   <  6Microsoft.Surface.Presentation.Controls.SurfaceListBox  -   5          Name$ viewList��5+      $	��300��6-   $
��Auto��6:    �� SelectedIndex$ 0��6G   #��[  5,      ��5.      �� 5/      .R�50      ��  �� Orientation$ 
Horizontal=�6!   $��Stretch=�6:   $	��190��6X   $	��Top=�6e   51      54      9  3Microsoft.Surface.Presentation.Controls.ScatterView  59   
   -   5          Name$ scatter��59   %   $��1��6   
����� �#��[ 6O   $��4q�5:      5<      >  8Microsoft.Surface.Presentation.Controls.SurfaceInkCanvas  5>   
   -   5        $��myInkCanvas��5>      $��3��60   	   
Background
	 ���� $y�Hidden=�6W   $��Stretch=�5?      $��Stretch=�6;   
   DefaultDrawingAttributes
 5@      *  $System.Windows.Ink.DrawingAttributes  5A         Color$ 	#FF808080��6$   5C      ;  5Microsoft.Surface.Presentation.Controls.SurfaceButton  5F   
   -   5        $��
SaveButton��5F   '   $��3��6   
�����_�$
��SAVE��6Q   $��50��5G      $	��100��6)   $	��Top=�69   $��Right=�6Q   $��15,10,15,10q�5H      5I        5K   
   -   5        $��
UndoButton��5K   '   $��3��6   
�����_�$�� UNDO��5L      $��50��5M      $	��300��6)   $	��Top=�69   $��Right=�6Q   $��15,10,150,10q�5N      5O        5Q   
   -   5        $��VideoButton��5Q   '   $��3��6   
�����_�$��
 SEE VIDEO��5R      $��50��5S      $	��300��6)   $	��Top=�69   $
��Left=�6Q   $��15,10,15,10q�5T      5U        5W   
   -   5        $��ClearButton��5W   '   $��3��6   $��10q�6:   
�����_�$��CLEAR��6f   $��50��5X      $	��100��6)   $	��Top=�69   $��Right=�6Q   $��160,10,80,10q�5Y      5Z      @  :Microsoft.Surface.Presentation.Controls.SurfaceRadioButton  5]   
   -	   5        $��	inkButton��5]   ,   $��3��6   $��Center=�6=   $��Turn on Paintbrush��5^      $��10,10,10,10q�6>   5_   <     5`   
   -
   5        $��inkOffButton��5`   ,   $��3��6   $��Center=�6@   $��Turn off Paintbrush��5a      $��
10,40,10,0q�6?   5b   =   5e      5g      