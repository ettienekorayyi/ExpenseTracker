   M S B A M L   `   `   `  ����  ExpenseTracker    ExpenseTracker.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6F     � Title$  Expense Management System��5   	   $	��590��6+   $��651.3��68   .��5      �� -   5         ��Name$ CreateRecord��5      ���	#FF86B5BD$��0,0,2,2q�6:   .+�5   
   �� ���#47565A$	��236��6#    �� Left$ 80��60   
���   �
 �� Top$ 14��6P   $	��492��6`   �� 5   
   $��Create New Record��6   $	 248��6,   $ 25��6>   $	��158��6N   
�������$
��Bold"�6m   $��Arial)�6   $��16(�6�   �� 5	   
   $��25��6   $	 215��6    $��0q�62   $ 41��6=   $	��224��6M   �� 5   
   -   5         ��Name$ itemLbl��5      $��Item:��6!   $	 126��61   $ 72��6C   
�������6S   �� 5   
   -   5         ��Name$ itemTxt��5      ���#47565A$��23��68   $	 182��6D    �� TextWrapping$
 Wrap=�6V   $ 64��6j   $	��120��6z   $��0,0,0,1q�6�   
�������
�������6�   �� 5   
   -   5        $ qtyLbl��5      $
��Qty:��6    $	 126��6/   $ 93��6A   
�������6Q   �� 5   
   -   5        $ qtyTxt��5      ���#47565A$��23��67   $	 182��6C   $
 Wrap=�6U   $ 90��6i   $	��120��6y   $��0,0,0,1q�6�   
�������
�������6�   �� 5   
   -   5        $ 	amountLbl��5      $��Amount:��6#   $	 126��65   $	 119��6G   
�������6X   �� 5   
   -   5        $ 	amountTxt��5      ���#47565A$��23��6:   $	 183��6F   $
 Wrap=�6X   $	 116��6l   $	��120��6}   $��0,0,0,1q�6�   
�������
�������6�   �� 5   
   -   5        $ totalLbl��5      $��Total:��6"   $	 126��63   $	 145��6E   
�������6V   �� 5   
   -	   5        $ totalTxt��5      ���#47565A$��23��69   $	 182��6E   $
 Wrap=�6W   $	 142��6k   $	��120��6|   $��0,0,0,1q�6�   
�������
�������{�. 6�   �� 5   
   -
   5        $ cashLbl��5      $��Cash:��6!   $	 347��61   $ 67��6C   
�������6S   �� 5   
   -   5        $ cashTxt��5      ���#47565A$��23��68   $	 403��6D   $
 Wrap=�6V   $ 65��6j   $	��120��6z   $��0,0,0,1q�6�   
�������
�������6�   �� 5   
   -   5        $ 	changeLbl��5      $��Change:��6#   $	 347��65   $ 93��6G   
�������6W   �� 5   
   -   5        $ 	changeTxt��5      ���#47565A$��23��6:   $	 403��6F   $
 Wrap=�6X   $ 91��6l   $	��120��6|   $��0,0,0,1q�6�   
�������
�������{�. 6�   �� 5   
   -   5        $ taxLbl��5      $
��Tax:��6    $	 347��6/   $	 121��6A   
�������6R   �� 5   
   -   5        $ taxTxt��5      ���#47565A$��23��67   $	 403��6C   $
 Wrap=�6U   $	 119��6i   $	��120��6z   $��0,0,0,1q�6�   
�������
�������6�   �� 5    
   -   5        $ dateLbl��5       $��Date:��6!   $	 347��61   $	 149��6C   
�������6T   (  "System.Windows.Controls.DatePicker  5!   
   -   5          Name$ datePckr��5!      $	 403��6'   $	 148��69   6J   �� 5#   
   -   5        $ establishmentLbl��5#      $��Establishment:��6*   $	 129��6C   $	 175��6U   
�������6f   �� 5$   
   -   5         ��Name$ establishmentCbox��5$      $	 220��6.   $	 177��6@   $	��120��6Q   6   �� 5%   
   -   5        $	��New��5%      $	 345��6   $	 179��61   $��53��6B   $��20��6M   6Y   �� 5'   
   -   5        	 ��Name$	 	btnCreate��5'      $��Create Record��6$   $	 251��6<   $	 213��6N   $	��106��6_   $��25��6k   $
��Bold"�6w   6�   �� 5)   
   ���#47565A$	��251��6#   $ 80��60   
���   �$	 265��6P   $	��492��6a   �� 5*   
   -   5        $ establishmentsLbl��5*      $��Transaction History��6+   $	 252��6I   $	 271��6[   
�������
 J� RenderTransformOrigin$
 10.056,0.096&�6   $
��Bold"�6�   $��16(�6�   �� 5+   
   $��25��6   $	 213��6    $��0q�62   $	 285��6=   $	��224��6N   �� 5-   
   -   5         ��Name$ historyListView��5-      $	��130��6,   $	 126��69   $	 310��6K   $	��397��6\    �� View 5.      �� 5/      .P�50      �� -   5        lvPk��$��Id��52       �� DisplayMemberBinding 53      �� *
primaryKey+�� 55      -   5        
lvItem��$
��Item��57       58      �� *
itemName+�� 5:      -   5        
lvQuantity��$	��Qty��5<       5=      �� *qty+�� 5?      -   5        lvAmount��$��Amount��5A       5B      �� *amount+�� 5D      -   5        
lvCash��$
��Cash��5F       5G      �� *cash+�� 5I      -   5        	lvTax��$	��Tax��5K       5L      �� *tax+�� 5N      -   5        lvTransDate��$
��Date��5P       5Q      �� *	transDate+�� 5S      -   5        	lvEstName��$��	Est. Name��5U       5V      �� *	estName+5X      5Z      �� 5[   
   -    5        $	 	btnUpdate��5\      $��Update��5]      $	 183��5^      $	 468��5_      $	��106��5`      $��25��6   $
��Bold"�5a      {�.  5b   "   �� 5c   
   -!   5        $	 	btnDelete��5d      $��Delete��5e      $	 331��5f      $	 468��5g      $	��106��5h      $��25��5i      $
��Bold"�5j      {�.  5k   "   5m      5n      