   M S B A M L   `   `   `  ����A  =Webcam, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WebcamWebcam  5   	       Webcam.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Webcam     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � WindowStartupLocation$ CenterScreen=�65   .��5	      � .+�5
   
   �� -   5         ��Name$ txtSave��5
      $
��Left=�6#   $��	27,78,0,0q�6>    �� TextWrapping$
 Wrap=�6Q   $	��Top=�6e   $	��261��6}   $��31��6�   �� 5   
   -   5         ��Name$ 
btnBrowser��5      $��Browser��6%   $
��Left=�67   $��
317,80,0,0q�6R   $	��Top=�6f   $��31��6~   $��57��6�   6�   �� 5   
   $��Webcam��6   $
��Left=�6!   $��
566,78,0,0q�6<   $	��Top=�6P   �� 5   
   -   5         ��Name$ listView��5      $��27,127,426,111q�6C   ��6%   �� *Items+ �� View 5      �� 5      .P�5      �� $
��Type��6%    �� Width$ 50��63    �� CellTemplate 5      �� 5      .R�5   "   �� $��16��6(   $��16��63   ��6?   �� *Icon+5      5      �� 5      $
��Path��6%   	 �� DisplayMemberBinding	 63   �� *Path+5      5      �� 5   
   -   5        
 ��Name$
 webcamImage��5      $
��Left=�6%   $	��231��6@   $��411,104,0,0q�6M   $	��Top=�6b   $	��354��6z   �� 5   
   -   5        $ 
btnCapture��5      $��Capture��6%   $
��Left=�67   $��566,347,0,0q�6R   $	��Top=�6g   $��31��6   $��57��6�   6�   �� 5   
   -   5        $��	DeleteBtn��5   N   $y�Hidden=�6   $��Delete��6%   $��158,352,544,30q�66    J� RenderTransformOrigin$ 
0.999,0.62&�6w   �� 5   
   -   5        $��	UploadBtn��5   N   $y�Hidden=�6   $��Upload��6%   $��231,352,470,30q�66   6_   �� 5   
   $��WEBCAM APPLICATION��6   $��Center=�6-   $��30(�6J   $��0,10,0,0q�6X   $	��Top=�6j   $	��330��6�   $��45��6�   5       5!      