/ * 
   *   S o u r c e   c o d e   f r o m :   h t t p : / / w w w . t u x e n c o n s u l t i n g . d k / l u a . z i p 
   * /
 
 u s i n g   S y s t e m ; 
 u s i n g   S y s t e m . R u n t i m e . I n t e r o p S e r v i c e s ; 
 
 n a m e s p a c e   M A I N 
 { 
 	 p u b l i c   s t a t i c   c l a s s   L u a 
 	 { 
 	 	 p r i v a t e   c o n s t   s t r i n g   D L L F I L E   =   " l i b l u a j i t . s o " ; 
 	 	 / *   m a r k   f o r   p r e c o m p i l e d   c o d e   ( ` < e s c > L u a ' )   * / 
 	 	 p u b l i c   c o n s t   s t r i n g   L U A _ S I G N A T U R E   =   " \ 0 3 3 L u a " ; 
 
 	 	 / *   o p t i o n   f o r   m u l t i p l e   r e t u r n s   i n   ` l u a _ p c a l l '   a n d   ` l u a _ c a l l '   * / 
 	 	 p u b l i c   c o n s t   i n t   L U A _ M U L T R E T   =   ( - 1 ) ; 
 
 	 	 / * 
 	 	 * *   p s e u d o - i n d i c e s 
 	 	 * / 
 	 	 p u b l i c   c o n s t   i n t   L U A _ R E G I S T R Y I N D E X   =   ( - 1 0 0 0 0 ) , 
 	 	 	 	 L U A _ E N V I R O N I N D E X   =   ( - 1 0 0 0 1 ) , 
 	 	 	 	 L U A _ G L O B A L S I N D E X   =   ( - 1 0 0 0 2 ) ; 
 
 	 	 / *   t h r e a d   s t a t u s ;   0   i s   O K   * / 
 	 	 p u b l i c   c o n s t   i n t   L U A _ Y I E L D   =   1 , 
 	 	 	 	 L U A _ E R R R U N   =   2 , 
 	 	 	 	 L U A _ E R R S Y N T A X   =   3 , 
 	 	 	 	 L U A _ E R R M E M   =   4 , 
 	 	 	 	 L U A _ E R R E R R   =   5 ; 
 
 	 	 / * 
 	 	   *     t y p e d e f   i n t   ( * l u a _ C F u n c t i o n )   ( l u a _ S t a t e   * L ) ; 
 	 	   * / 
 	 	 [ U n m a n a g e d F u n c t i o n P o i n t e r ( S y s t e m . R u n t i m e . I n t e r o p S e r v i c e s . C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   d e l e g a t e   i n t   L u a F u n c t i o n ( I n t P t r   l u a _ S t a t e ) ; 
 
 	 	 / * 
 	 	 * *   b a s i c   t y p e s 
 	 	 * / 
 	 	 p u b l i c   c o n s t   i n t   L U A _ T N O N E   =   ( - 1 ) , 
 	 	 	 	 L U A _ T N I L   =   0 , 
 	 	 	 	 L U A _ T B O O L E A N   =   1 , 
 	 	 	 	 L U A _ T L I G H T U S E R D A T A   =   2 , 
 	 	 	 	 L U A _ T N U M B E R   =   3 , 
 	 	 	 	 L U A _ T S T R I N G   =   4 , 
 	 	 	 	 L U A _ T T A B L E   =   5 , 
 	 	 	 	 L U A _ T F U N C T I O N   =   6 , 
 	 	 	 	 L U A _ T U S E R D A T A   =   7 , 
 	 	 	 	 L U A _ T T H R E A D   =   8 , 
 	 	 	 	 / *   m i n i m u m   L u a   s t a c k   a v a i l a b l e   t o   a   C   f u n c t i o n   * / 
 	 	 	 	 L U A _ M I N S T A C K   =   2 0 ; 
 
 
 	 	 / * 
 	 	 * *   s t a t e   m a n i p u l a t i o n 
 	 	 * / 
 	 	 / / L U A _ A P I   l u a _ S t a t e   * ( l u a _ n e w s t a t e )   ( l u a _ A l l o c   f ,   v o i d   * u d ) ; 
 	 	 / / L U A _ A P I   v o i d               ( l u a _ c l o s e )   ( l u a _ S t a t e   * L ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ c l o s e ( I n t P t r   l u a _ S t a t e ) ; 
 
 	 	 / / L U A _ A P I   l u a _ S t a t e   * ( l u a _ n e w t h r e a d )   ( l u a _ S t a t e   * L ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   I n t P t r   l u a _ n e w t h r e a d ( I n t P t r   l u a _ S t a t e ) ; 
 
 	 	 / / L U A _ A P I   l u a _ C F u n c t i o n   ( l u a _ a t p a n i c )   ( l u a _ S t a t e   * L ,   l u a _ C F u n c t i o n   p a n i c f ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   L u a F u n c t i o n   l u a _ a t p a n i c ( I n t P t r   l u a _ S t a t e ,   L u a F u n c t i o n   p a n i c f ) ; 
 
 	 	 / * 
 	 	 * *   b a s i c   s t a c k   m a n i p u l a t i o n 
 	 	 * / 
 	 	 / / L U A _ A P I   i n t       ( l u a _ g e t t o p )   ( l u a _ S t a t e   * L ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   i n t   l u a _ g e t t o p ( I n t P t r   l u a _ S t a t e ) ; 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ s e t t o p )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ s e t t o p ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ p u s h v a l u e )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ p u s h v a l u e ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ r e m o v e )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ r e m o v e ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ i n s e r t )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ i n s e r t ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ r e p l a c e )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ r e p l a c e ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   i n t       ( l u a _ c h e c k s t a c k )   ( l u a _ S t a t e   * L ,   i n t   s z ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ c h e c k s t a c k ( I n t P t r   l u a _ S t a t e ) ; 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ x m o v e )   ( l u a _ S t a t e   * f r o m ,   l u a _ S t a t e   * t o ,   i n t   n ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ x m o v e ( I n t P t r   l u a _ S t a t e _ F r o m ,   I n t P t r   l u a _ S t a t e _ T o ) ; 
 
 	 	 / * 
 	 	 * *   a c c e s s   f u n c t i o n s   ( s t a c k   - >   C ) 
 	 	 * / 
 	 	 / / L U A _ A P I   i n t                           ( l u a _ i s n u m b e r )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   i n t   l u a _ i s n u m b e r ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   i n t                           ( l u a _ i s s t r i n g )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   i n t   l u a _ i s s t r i n g ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   i n t                           ( l u a _ i s c f u n c t i o n )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   i n t   l u a _ i s c f u n c t i o n ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   i n t                           ( l u a _ i s u s e r d a t a )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   i n t   l u a _ i s u s e r d a t a ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   i n t                           ( l u a _ t y p e )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   i n t   l u a _ t y p e ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   c o n s t   c h a r           * ( l u a _ t y p e n a m e )   ( l u a _ S t a t e   * L ,   i n t   t p ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   s t r i n g   l u a _ t y p e n a m e ( I n t P t r   l u a _ S t a t e ,   i n t   t p ) ; 
 	 	 / / L U A _ A P I   i n t                         ( l u a _ e q u a l )   ( l u a _ S t a t e   * L ,   i n t   i d x 1 ,   i n t   i d x 2 ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ e q u a l ( I n t P t r   l u a _ S t a t e ,   i n t   i d x 1 ,   i n t   i d x 2 ) ; 
 	 	 / / L U A _ A P I   i n t                         ( l u a _ r a w e q u a l )   ( l u a _ S t a t e   * L ,   i n t   i d x 1 ,   i n t   i d x 2 ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ r a w e q u a l ( I n t P t r   l u a _ S t a t e ,   i n t   i d x 1 ,   i n t   i d x 2 ) ; 
 	 	 / / L U A _ A P I   i n t                         ( l u a _ l e s s t h a n )   ( l u a _ S t a t e   * L ,   i n t   i d x 1 ,   i n t   i d x 2 ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ l e s s t h a n ( I n t P t r   l u a _ S t a t e ,   i n t   i d x 1 ,   i n t   i d x 2 ) ; 
 	 	 / / L U A _ A P I   l u a _ N u m b e r             ( l u a _ t o n u m b e r )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   d o u b l e   l u a _ t o n u m b e r ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   l u a _ I n t e g e r           ( l u a _ t o i n t e g e r )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   i n t   l u a _ t o i n t e g e r ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   i n t                           ( l u a _ t o b o o l e a n )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   i n t   l u a _ t o b o o l e a n ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   c o n s t   c h a r           * ( l u a _ t o l s t r i n g )   ( l u a _ S t a t e   * L ,   i n t   i d x ,   s i z e _ t   * l e n ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   s t r i n g   l u a _ t o l s t r i n g ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ,   U I n t P t r   l e n ) ; 
 	 	 / / L U A _ A P I   s i z e _ t                     ( l u a _ o b j l e n )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   i n t   l u a _ o b j l e n ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   l u a _ C F u n c t i o n       ( l u a _ t o c f u n c t i o n )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   L u a F u n c t i o n   l u a _ t o c f u n c t i o n ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   v o i d 	               * ( l u a _ t o u s e r d a t a )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   I n t P t r   l u a _ t o u s e r d a t a ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   l u a _ S t a t e             * ( l u a _ t o t h r e a d )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   I n t P t r   l u a _ t o t h r e a d ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   c o n s t   v o i d           * ( l u a _ t o p o i n t e r )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   I n t P t r   l u a _ t o p o i n t e r ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 
 	 	 / * 
 	 	 * *   p u s h   f u n c t i o n s   ( C   - >   s t a c k ) 
 	 	 * / 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ p u s h n i l )   ( l u a _ S t a t e   * L ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ p u s h n i l ( I n t P t r   l u a _ S t a t e ) ; 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ p u s h n u m b e r )   ( l u a _ S t a t e   * L ,   l u a _ N u m b e r   n ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ p u s h n u m b e r ( I n t P t r   l u a _ S t a t e ,   d o u b l e   n ) ; 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ p u s h i n t e g e r )   ( l u a _ S t a t e   * L ,   l u a _ I n t e g e r   n ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ p u s h i n t e g e r ( I n t P t r   l u a _ S t a t e ,   i n t   n ) ; 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ p u s h l s t r i n g )   ( l u a _ S t a t e   * L ,   c o n s t   c h a r   * s ,   s i z e _ t   l ) ; 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ p u s h s t r i n g )   ( l u a _ S t a t e   * L ,   c o n s t   c h a r   * s ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ p u s h s t r i n g ( I n t P t r   l u a _ S t a t e ,   s t r i n g   s ) ; 
 	 	 / / L U A _ A P I   c o n s t   c h a r   * ( l u a _ p u s h v f s t r i n g )   ( l u a _ S t a t e   * L ,   c o n s t   c h a r   * f m t , 
 	 	 / /                                                                                                             v a _ l i s t   a r g p ) ; 
 	 	 / / L U A _ A P I   c o n s t   c h a r   * ( l u a _ p u s h f s t r i n g )   ( l u a _ S t a t e   * L ,   c o n s t   c h a r   * f m t ,   . . . ) ; 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ p u s h c c l o s u r e )   ( l u a _ S t a t e   * L ,   l u a _ C F u n c t i o n   f n ,   i n t   n ) ; 
 	 	 / / [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n = C a l l i n g C o n v e n t i o n . C d e c l ,   C h a r S e t = C h a r S e t . A n s i ) ] 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ p u s h c c l o s u r e ( I n t P t r   l u a _ S t a t e ,   [ M a r s h a l A s ( U n m a n a g e d T y p e . F u n c t i o n P t r ) ]   L u a F u n c t i o n   f u n c ,   i n t   n ) ; 
 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ p u s h b o o l e a n )   ( l u a _ S t a t e   * L ,   i n t   b ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ p u s h b o o l e a n ( I n t P t r   l u a _ S t a t e ,   i n t   b ) ; 
 	 	 p u b l i c   s t a t i c   v o i d   l u a _ p u s h b o o l e a n ( I n t P t r   l u a _ S t a t e ,   b o o l   b ) 
 	 	 { 
 	 	 	 l u a _ p u s h b o o l e a n ( l u a _ S t a t e ,   b   ?   1   :   0 ) ; 
 	 	 } 
 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ p u s h l i g h t u s e r d a t a )   ( l u a _ S t a t e   * L ,   v o i d   * p ) ; 
 	 	 / / L U A _ A P I   i n t       ( l u a _ p u s h t h r e a d )   ( l u a _ S t a t e   * L ) ; 
 
 	 	 / * 
 	 	 * *   g e t   f u n c t i o n s   ( L u a   - >   s t a c k ) 
 	 	 * / 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ g e t t a b l e )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ g e t t a b l e ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ g e t f i e l d )   ( l u a _ S t a t e   * L ,   i n t   i d x ,   c o n s t   c h a r   * k ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ g e t f i e l d ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ,   s t r i n g   s ) ; 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ r a w g e t )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ r a w g e t ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ r a w g e t i )   ( l u a _ S t a t e   * L ,   i n t   i d x ,   i n t   n ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ r a w g e t i ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ,   i n t   n ) ; 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ c r e a t e t a b l e )   ( l u a _ S t a t e   * L ,   i n t   n a r r ,   i n t   n r e c ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ c r e a t e t a b l e ( I n t P t r   l u a _ S t a t e ,   i n t   n a r r ,   i n t   n r e c ) ; 
 	 	 / / L U A _ A P I   v o i d   * ( l u a _ n e w u s e r d a t a )   ( l u a _ S t a t e   * L ,   s i z e _ t   s z ) ; 
 	 	 / / L U A _ A P I   i n t       ( l u a _ g e t m e t a t a b l e )   ( l u a _ S t a t e   * L ,   i n t   o b j i n d e x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   i n t   l u a _ g e t m e t a t a b l e ( I n t P t r   l u a _ S t a t e ,   i n t   o b j i n d e x ) ; 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ g e t f e n v )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ g e t f e n v ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 
 	 	 / * 
 	 	 * *   s e t   f u n c t i o n s   ( s t a c k   - >   L u a ) 
 	 	 * / 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ s e t t a b l e )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ s e t t a b l e ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ s e t f i e l d )   ( l u a _ S t a t e   * L ,   i n t   i d x ,   c o n s t   c h a r   * k ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ s e t f i e l d ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ,   s t r i n g   s ) ; 
 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ r a w s e t )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ r a w s e t ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ r a w s e t i )   ( l u a _ S t a t e   * L ,   i n t   i d x ,   i n t   n ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ r a w s e t i ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ,   i n t   n ) ; 
 	 	 / / L U A _ A P I   i n t       ( l u a _ s e t m e t a t a b l e )   ( l u a _ S t a t e   * L ,   i n t   o b j i n d e x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   i n t   l u a _ s e t m e t a t a b l e ( I n t P t r   l u a _ S t a t e ,   i n t   o b j i n d e x ) ; 
 	 	 / / L U A _ A P I   i n t       ( l u a _ s e t f e n v )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   i n t   l u a _ s e t f e n v ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 
 	 	 / * 
 	 	 * *   ` l o a d '   a n d   ` c a l l '   f u n c t i o n s   ( l o a d   a n d   r u n   L u a   c o d e ) 
 	 	 * / 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ c a l l )   ( l u a _ S t a t e   * L ,   i n t   n a r g s ,   i n t   n r e s u l t s ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ c a l l ( I n t P t r   l u a _ S t a t e ,   i n t   n a r g s ,   i n t   n r e s u l t s ) ; 
 	 	 / / L U A _ A P I   i n t       ( l u a _ p c a l l )   ( l u a _ S t a t e   * L ,   i n t   n a r g s ,   i n t   n r e s u l t s ,   i n t   e r r f u n c ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   i n t   l u a _ p c a l l ( I n t P t r   l u a _ S t a t e ,   i n t   n a r g s ,   i n t   n r e s u l t s ,   i n t   e r r f u n c ) ; 
 
 	 	 / / L U A _ A P I   i n t   ( l u a _ g c )   ( l u a _ S t a t e   * L ,   i n t   w h a t ,   i n t   d a t a ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   i n t   l u a _ g c ( I n t P t r   l u a _ S t a t e ,   i n t   w h a t ,   i n t   d a t a ) ; 
 
 	 	 / * 
 	 	 * *   m i s c e l l a n e o u s   f u n c t i o n s 
 	 	 * / 
 	 	 / / L U A _ A P I   i n t       ( l u a _ e r r o r )   ( l u a _ S t a t e   * L ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   i n t   l u a _ e r r o r ( I n t P t r   l u a _ S t a t e ) ; 
 	 	 / / L U A _ A P I   i n t       ( l u a _ n e x t )   ( l u a _ S t a t e   * L ,   i n t   i d x ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   i n t   l u a _ n e x t ( I n t P t r   l u a _ S t a t e ,   i n t   i d x ) ; 
 
 	 	 / / L U A _ A P I   v o i d     ( l u a _ c o n c a t )   ( l u a _ S t a t e   * L ,   i n t   n ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a _ c o n c a t ( I n t P t r   l u a _ S t a t e ,   i n t   n ) ; 
 	 	 / / L U A _ A P I   l u a _ A l l o c   ( l u a _ g e t a l l o c f )   ( l u a _ S t a t e   * L ,   v o i d   * * u d ) ; 
 	 	 / / L U A _ A P I   v o i d   l u a _ s e t a l l o c f   ( l u a _ S t a t e   * L ,   l u a _ A l l o c   f ,   v o i d   * u d ) ; 
 
 	 	 / *   
 	 	 * *   = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
 	 	 * *   s o m e   u s e f u l   m a c r o s 
 	 	 * *   = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
 	 	 * / 
 
 	 	 / / # d e f i n e   l u a _ p o p ( L , n ) 	 	 l u a _ s e t t o p ( L ,   - ( n ) - 1 ) 
 	 	 p u b l i c   s t a t i c   v o i d   l u a _ p o p ( I n t P t r   l u a _ S t a t e ,   i n t   a m o u n t ) 
 	 	 { 
 	 	 	 l u a _ s e t t o p ( l u a _ S t a t e ,   - ( a m o u n t )   -   1 ) ; 
 	 	 } 
 
 	 	 / / # d e f i n e   l u a _ n e w t a b l e ( L ) 	 	 l u a _ c r e a t e t a b l e ( L ,   0 ,   0 ) 
 	 	 p u b l i c   s t a t i c   v o i d   l u a _ n e w t a b l e ( I n t P t r   l u a _ S t a t e ) 
 	 	 { 
 	 	 	 l u a _ c r e a t e t a b l e ( l u a _ S t a t e ,   0 ,   0 ) ; 
 	 	 } 
 
 	 	 / / # d e f i n e   l u a _ r e g i s t e r ( L , n , f )   ( l u a _ p u s h c f u n c t i o n ( L ,   ( f ) ) ,   l u a _ s e t g l o b a l ( L ,   ( n ) ) ) 
 	 	 p u b l i c   s t a t i c   v o i d   l u a _ r e g i s t e r ( I n t P t r   l u a _ S t a t e ,   s t r i n g   n ,   L u a F u n c t i o n   f u n c ) 
 	 	 { 
 	 	 	 l u a _ p u s h c f u n c t i o n ( l u a _ S t a t e ,   f u n c ) ; 
 	 	 	 l u a _ s e t g l o b a l ( l u a _ S t a t e ,   n ) ; 
 	 	 } 
 
 	 	 / / # d e f i n e   l u a _ p u s h c f u n c t i o n ( L , f ) 	 l u a _ p u s h c c l o s u r e ( L ,   ( f ) ,   0 ) 
 	 	 p u b l i c   s t a t i c   v o i d   l u a _ p u s h c f u n c t i o n ( I n t P t r   l u a _ S t a t e ,   L u a F u n c t i o n   f u n c ) 
 	 	 { 
 	 	 	 l u a _ p u s h c c l o s u r e ( l u a _ S t a t e ,   f u n c ,   0 ) ; 
 	 	 } 
 
 	 	 / / # d e f i n e   l u a _ s t r l e n ( L , i ) 	 	 l u a _ o b j l e n ( L ,   ( i ) ) 
 	 	 p u b l i c   s t a t i c   i n t   l u a _ s t r l e n ( I n t P t r   l u a _ S t a t e ,   i n t   i ) 
 	 	 { 
 	 	 	 r e t u r n   l u a _ o b j l e n ( l u a _ S t a t e ,   i ) ; 
 	 	 } 
 
 	 	 / / # d e f i n e   l u a _ i s f u n c t i o n ( L , n ) 	 ( l u a _ t y p e ( L ,   ( n ) )   = =   L U A _ T F U N C T I O N ) 
 	 	 p u b l i c   s t a t i c   b o o l   l u a _ i s f u n c t i o n ( I n t P t r   l u a _ S t a t e ,   i n t   n ) 
 	 	 { 
 	 	 	 r e t u r n   l u a _ t y p e ( l u a _ S t a t e ,   n )   = =   L U A _ T F U N C T I O N   ?   t r u e   :   f a l s e ; 
 	 	 } 
 	 	 / / # d e f i n e   l u a _ i s t a b l e ( L , n ) 	 ( l u a _ t y p e ( L ,   ( n ) )   = =   L U A _ T T A B L E ) 
 	 	 p u b l i c   s t a t i c   b o o l   l u a _ i s t a b l e ( I n t P t r   l u a _ S t a t e ,   i n t   n ) 
 	 	 { 
 	 	 	 r e t u r n   l u a _ t y p e ( l u a _ S t a t e ,   n )   = =   L U A _ T T A B L E   ?   t r u e   :   f a l s e ; 
 	 	 } 
 	 	 / / # d e f i n e   l u a _ i s l i g h t u s e r d a t a ( L , n ) 	 ( l u a _ t y p e ( L ,   ( n ) )   = =   L U A _ T L I G H T U S E R D A T A ) 
 	 	 / / # d e f i n e   l u a _ i s n i l ( L , n ) 	 	 ( l u a _ t y p e ( L ,   ( n ) )   = =   L U A _ T N I L ) 
 	 	 p u b l i c   s t a t i c   b o o l   l u a _ i s n i l ( I n t P t r   l u a _ S t a t e ,   i n t   n ) 
 	 	 { 
 	 	 	 r e t u r n   l u a _ t y p e ( l u a _ S t a t e ,   n )   = =   L U A _ T N I L   ?   t r u e   :   f a l s e ; 
 	 	 } 
 	 	 / / # d e f i n e   l u a _ i s b o o l e a n ( L , n ) 	 ( l u a _ t y p e ( L ,   ( n ) )   = =   L U A _ T B O O L E A N ) 
 	 	 p u b l i c   s t a t i c   b o o l   l u a _ i s b o o l e a n ( I n t P t r   l u a _ S t a t e ,   i n t   n ) 
 	 	 { 
 	 	 	 r e t u r n   l u a _ t y p e ( l u a _ S t a t e ,   n )   = =   L U A _ T B O O L E A N   ?   t r u e   :   f a l s e ; 
 	 	 } 
 	 	 / / # d e f i n e   l u a _ i s t h r e a d ( L , n ) 	 ( l u a _ t y p e ( L ,   ( n ) )   = =   L U A _ T T H R E A D ) 
 	 	 / / # d e f i n e   l u a _ i s n o n e ( L , n ) 	 	 ( l u a _ t y p e ( L ,   ( n ) )   = =   L U A _ T N O N E ) 
 	 	 p u b l i c   s t a t i c   b o o l   l u a _ i s n o n e ( I n t P t r   l u a _ S t a t e ,   i n t   n ) 
 	 	 { 
 	 	 	 r e t u r n   l u a _ t y p e ( l u a _ S t a t e ,   n )   = =   L U A _ T N O N E   ?   t r u e   :   f a l s e ; 
 	 	 } 
 	 	 / / # d e f i n e   l u a _ i s n o n e o r n i l ( L ,   n ) 	 ( l u a _ t y p e ( L ,   ( n ) )   < =   0 ) 
 	 	 p u b l i c   s t a t i c   b o o l   l u a _ i s n o n e o r n i l ( I n t P t r   l u a _ S t a t e ,   i n t   n ) 
 	 	 { 
 	 	 	 r e t u r n   l u a _ t y p e ( l u a _ S t a t e ,   n )   < =   0   ?   t r u e   :   f a l s e ; 
 	 	 } 
 
 	 	 / / # d e f i n e   l u a _ p u s h l i t e r a l ( L ,   s ) 	 \ 
 	 	 / /         l u a _ p u s h l s t r i n g ( L ,   " "   s ,   ( s i z e o f ( s ) / s i z e o f ( c h a r ) ) - 1 ) 
 
 	 	 / / # d e f i n e   l u a _ s e t g l o b a l ( L , s ) 	 l u a _ s e t f i e l d ( L ,   L U A _ G L O B A L S I N D E X ,   ( s ) ) 
 	 	 p u b l i c   s t a t i c   v o i d   l u a _ s e t g l o b a l ( I n t P t r   l u a _ S t a t e ,   s t r i n g   s ) 
 	 	 { 
 	 	 	 l u a _ s e t f i e l d ( l u a _ S t a t e ,   L U A _ G L O B A L S I N D E X ,   s ) ; 
 	 	 } 
 
 	 	 / / # d e f i n e   l u a _ g e t g l o b a l ( L , s ) 	 l u a _ g e t f i e l d ( L ,   L U A _ G L O B A L S I N D E X ,   ( s ) ) 
 	 	 p u b l i c   s t a t i c   v o i d   l u a _ g e t g l o b a l ( I n t P t r   l u a _ S t a t e ,   s t r i n g   s ) 
 	 	 { 
 	 	 	 l u a _ g e t f i e l d ( l u a _ S t a t e ,   L U A _ G L O B A L S I N D E X ,   s ) ; 
 	 	 } 
 
 	 	 / / # d e f i n e   l u a _ t o s t r i n g ( L , i ) 	 l u a _ t o l s t r i n g ( L ,   ( i ) ,   N U L L ) 
 	 	 p u b l i c   s t a t i c   s t r i n g   l u a _ t o s t r i n g ( I n t P t r   l u a _ S t a t e ,   i n t   i ) 
 	 	 { 
 	 	 	 r e t u r n   l u a _ t o l s t r i n g ( l u a _ S t a t e ,   i ,   U I n t P t r . Z e r o ) ; 
 	 	 } 
 
 	 	 / * 
 	 	 * *   c o m p a t i b i l i t y   m a c r o s   a n d   f u n c t i o n s 
 	 	 * / 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   I n t P t r   l u a L _ n e w s t a t e ( ) ; 
 
 	 	 / / # d e f i n e   l u a _ o p e n ( ) 	 l u a L _ n e w s t a t e ( ) 
 	 	 p u b l i c   s t a t i c   I n t P t r   l u a _ o p e n ( ) 
 	 	 { 
 	 	 	 r e t u r n   l u a L _ n e w s t a t e ( ) ; 
 	 	 } 
 
 	 	 / *   o p e n   a l l   p r e v i o u s   l i b r a r i e s   * / 
 	 	 / / L U A L I B _ A P I   v o i d   ( l u a L _ o p e n l i b s )   ( l u a _ S t a t e   * L ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a L _ o p e n l i b s ( I n t P t r   l u a _ S t a t e ) ; 
 
 	 	 / / # d e f i n e   l u a _ g e t r e g i s t r y ( L ) 	 l u a _ p u s h v a l u e ( L ,   L U A _ R E G I S T R Y I N D E X ) 
 
 	 	 / / # d e f i n e   l u a _ g e t g c c o u n t ( L ) 	 l u a _ g c ( L ,   L U A _ G C C O U N T ,   0 ) 
 
 	 	 / / # d e f i n e   l u a _ C h u n k r e a d e r 	 	 l u a _ R e a d e r 
 	 	 / / # d e f i n e   l u a _ C h u n k w r i t e r 	 	 l u a _ W r i t e r 
 
 	 	 / *   h a c k   * / 
 	 	 / / L U A _ A P I   v o i d   l u a _ s e t l e v e l 	 ( l u a _ S t a t e   * f r o m ,   l u a _ S t a t e   * t o ) ; 
 
 	 	 / / L U A L I B _ A P I   i n t   ( l u a L _ l o a d s t r i n g )   ( l u a _ S t a t e   * L ,   c o n s t   c h a r   * s ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   i n t   l u a L _ l o a d s t r i n g ( I n t P t r   l u a _ S t a t e ,   s t r i n g   s ) ; 
 
 	 	 / / # d e f i n e   l u a L _ d o s t r i n g ( L ,   s )   \ 
 	 	 / / ( l u a L _ l o a d s t r i n g ( L ,   s )   | |   l u a _ p c a l l ( L ,   0 ,   L U A _ M U L T R E T ,   0 ) ) 
 	 	 p u b l i c   s t a t i c   i n t   l u a L _ d o s t r i n g ( I n t P t r   l u a _ S t a t e ,   s t r i n g   s ) 
 	 	 { 
 	 	 	 i f   ( l u a L _ l o a d s t r i n g ( l u a _ S t a t e ,   s )   ! =   0 ) 
 	 	 	 	 r e t u r n   1 ; 
 	 	 	 r e t u r n   l u a _ p c a l l ( l u a _ S t a t e ,   0 ,   L U A _ M U L T R E T ,   0 ) ; 
 	 	 } 
 
 	 	 / / L U A L I B _ A P I   i n t   ( l u a L _ l o a d f i l e )   ( l u a _ S t a t e   * L ,   c o n s t   c h a r   * f i l e n a m e ) ; 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   i n t   l u a L _ l o a d f i l e ( I n t P t r   l u a _ S t a t e ,   s t r i n g   s ) ; 
 
 	 	 / / # d e f i n e   l u a L _ d o f i l e ( L ,   f n )   \ 
 	 	 / / ( l u a L _ l o a d f i l e ( L ,   f n )   | |   l u a _ p c a l l ( L ,   0 ,   L U A _ M U L T R E T ,   0 ) ) 
 	 	 p u b l i c   s t a t i c   i n t   l u a L _ d o f i l e ( I n t P t r   l u a _ S t a t e ,   s t r i n g   s ) 
 	 	 { 
 	 	 	 i f   ( l u a L _ l o a d f i l e ( l u a _ S t a t e ,   s )   ! =   0 ) 
 	 	 	 	 r e t u r n   1 ; 
 	 	 	 r e t u r n   l u a _ p c a l l ( l u a _ S t a t e ,   0 ,   L U A _ M U L T R E T ,   0 ) ; 
 	 	 } 
 	 	 
 	 	 [ D l l I m p o r t ( D L L F I L E ,   C a l l i n g C o n v e n t i o n   =   C a l l i n g C o n v e n t i o n . C d e c l ) ] 
 	 	 p u b l i c   s t a t i c   e x t e r n   v o i d   l u a L _ e r r o r ( I n t P t r   l u a _ S t a t e ,   s t r i n g   s ) ; 
 	 	 
 	 } 
 } 
