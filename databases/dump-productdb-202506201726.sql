PGDMP                      }         	   productdb    17.5 (Debian 17.5-1.pgdg120+1)    17.0     +           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            ,           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            -           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            .           1262    16384 	   productdb    DATABASE     t   CREATE DATABASE productdb WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.utf8';
    DROP DATABASE productdb;
                     postgres    false            �            1259    16389    categoriestable    TABLE     u   CREATE TABLE public.categoriestable (
    id uuid NOT NULL,
    name text NOT NULL,
    delete_state_code integer
);
 #   DROP TABLE public.categoriestable;
       public         heap r       postgres    false            �            1259    16398    productstable    TABLE     ]  CREATE TABLE public.productstable (
    id uuid NOT NULL,
    name text NOT NULL,
    description text,
    price numeric(10,2) NOT NULL,
    category_id uuid,
    quantity integer DEFAULT 1,
    delete_state_code integer,
    discount_price numeric(10,2),
    protein integer,
    fats integer DEFAULT 0,
    carbs integer,
    calories integer
);
 !   DROP TABLE public.productstable;
       public         heap r       postgres    false            '          0    16389    categoriestable 
   TABLE DATA           F   COPY public.categoriestable (id, name, delete_state_code) FROM stdin;
    public               postgres    false    217   L       (          0    16398    productstable 
   TABLE DATA           �   COPY public.productstable (id, name, description, price, category_id, quantity, delete_state_code, discount_price, protein, fats, carbs, calories) FROM stdin;
    public               postgres    false    218   
       �           2606    16397 #   categoriestable categories_name_key 
   CONSTRAINT     ^   ALTER TABLE ONLY public.categoriestable
    ADD CONSTRAINT categories_name_key UNIQUE (name);
 M   ALTER TABLE ONLY public.categoriestable DROP CONSTRAINT categories_name_key;
       public                 postgres    false    217            �           2606    16395    categoriestable categories_pkey 
   CONSTRAINT     ]   ALTER TABLE ONLY public.categoriestable
    ADD CONSTRAINT categories_pkey PRIMARY KEY (id);
 I   ALTER TABLE ONLY public.categoriestable DROP CONSTRAINT categories_pkey;
       public                 postgres    false    217            �           2606    16405    productstable products_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public.productstable
    ADD CONSTRAINT products_pkey PRIMARY KEY (id);
 E   ALTER TABLE ONLY public.productstable DROP CONSTRAINT products_pkey;
       public                 postgres    false    218            �           2606    16406 '   productstable products_category_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.productstable
    ADD CONSTRAINT products_category_id_fkey FOREIGN KEY (category_id) REFERENCES public.categoriestable(id);
 Q   ALTER TABLE ONLY public.productstable DROP CONSTRAINT products_category_id_fkey;
       public               postgres    false    218    3218    217            '   �   x�m�1�0���.��E��$5.����h��HE9��Yj����{���Bj��̓�2�RZ]RZ.��J�P�p�ow���C��ht��5'6穇"
0��
�x�75��녇"
0\������\�y(� ��n5h�ۮ��(�p���g��-h��z{a5�� 54x      (   z  x��X]o�}��}�S1�|��v]'��pP��evg�& �.Ev�,;HS؍�(�"E����"K6eK�_X����]J����Z\�K�u��{.�6KM��׎��1�u�U�s��Vڑ�wӃ��d#V�fa+�i$��'�~�f��Gx��;�����i�t�ͻ���_<Y�vo�Wx<��Tx��.v��n�H��%�z�iy�#��wx<%�HkG�6:�X�F���(#��yʄa�pɨԐ�a;�����w�ΪO��^����^�r���{�xVu'}@K�9��x�\��^㆓n�xv
�����i؈����k�]��P�ZK]r��$Z�9��A�N�\��Hr�b�N�u~���~�UX<*���Q�u�\'��04( )V[�[E�j�0��a4Tk�}��Ґ��'a�*�دH����ށ���͛�
՘��3�'����I_�����ѣY˹մ:R嘠�1ImJ.�\g�]�����/�7Ø��O/N�>^?_���j1P�Pܔ3iUm���Z%�K4X'�aN�)��*En���%���F�ʽ�����49.����u�|�X=��F^'ayt4�" �N ����Mk�wa-�r���������Y�o%�}�K��Vݼ*�w3_�A��_!z�b8�9���3!���7h ����'�����ʶ�z�6"���Ժ�ܠ+�Z�*�w
�}@���BU�����|
�&\��2�Џ
�~�1&YkF��P>/�[W#�)E-XSI�mo���8lT��d�t� ���{<D�|�H�y��Un8�.w�#���%����_��|_��%Y�zjT��*���:0�����$Q�x�!&O3�E�Y�\ ��R������w�Nn9�@���Zɚ5��!'��@��hBl�HrR5)i��m��H���`���_���
��;ǧ���\�x���"¬��BB���[�b
�Dͭku��@n��x��]e���뒹yw�����Q��H�C����҉an��眖GO�� 3j���Z$n@���
��VĨ���NEo'7!�u����A�N�9��nn�yi�>��7Q|�C�s��E�ڐ�"Q�<���r�/�mΐ[;�Ҵ� S�}�2|/R�^����Ȯ��1���qel4̷Ze�Uy4[c)�H���V6�Y���� ��?����\����.����Y�V���ܭ����ԅ��ye)5�k)�Z 7A1�`� �yk�$��V��O����R���S�	�4Cy3�W����s�K��3<R��:�%=�8qظ�:X����`�"�L��1г:�ɽ٘�L��ы~�d�E�O��x��'�t�m5����8U��Ã�0�C�`2���(��{m@i�c�1r��`oO63��V1���E���O�*�vH�]H��QA	�j��#���vƃ�^H��o�x�A�Io�ATL#J	L}�
��į�K�3\�=r��[�(*���+�頱��Ll/� 7w6f�[�&�Yuu�yo#�?`���0 �gi�Ix��9�ť3x���ՄJ7R��u����X�!M��!y���)�����V��L&�34Si���E�e��A!�I�t���>�0;��5 ���[����0�S���5��E��i�kR�^W�tk<C�W����M��yVV�������g������g���Q=�<�)f��w��0g�4�E�:1x.�h-ۖ�V�C�d��aV]ٙf��v6M�EYߠ[�����NQ�W��_�P�\�[��!/;B�B���/�!���Vp��<��&���\�q9�f9�e�@��~�t��I�{<C��e{�rpj�<�1ˀ������c�sCݨ�r;���4ݜT7R�����,����YV�eϕB��g)6<gJ�۩�>�D�Q�a5���4���J�C3���0���9|Yj���
�h�m=S�s������J��]a�� 3UY���rS3�K�Ј�U䬽n��3�խ����5Cv�}b���H�E_+�=��)�P�T�m�n�Bl�	��F�Ϥ�P�l!����G�D�����_���<,���gFg�UJ1�ڗ@����0�k8�B���s�_�`���u0�c�l ���9�u�}pq�Yڞq!!�8�du���.��_�}~i4�S5�Z     