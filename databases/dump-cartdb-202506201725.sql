PGDMP  7                    }            cartdb    17.5 (Debian 17.5-1.pgdg120+1)    17.0     +           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            ,           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            -           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            .           1262    16384    cartdb    DATABASE     q   CREATE DATABASE cartdb WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.utf8';
    DROP DATABASE cartdb;
                     postgres    false            �            1259    16398 	   carttable    TABLE       CREATE TABLE public.carttable (
    id uuid NOT NULL,
    user_id uuid,
    product_id uuid,
    quantity integer DEFAULT 1,
    status_id uuid,
    delete_state_code integer,
    created_on date,
    is_active boolean DEFAULT false NOT NULL,
    cart_id uuid NOT NULL
);
    DROP TABLE public.carttable;
       public         heap r       postgres    false            �            1259    16389    statustable    TABLE     s   CREATE TABLE public.statustable (
    id uuid NOT NULL,
    name text NOT NULL,
    "delete_state code" integer
);
    DROP TABLE public.statustable;
       public         heap r       postgres    false            (          0    16398 	   carttable 
   TABLE DATA           �   COPY public.carttable (id, user_id, product_id, quantity, status_id, delete_state_code, created_on, is_active, cart_id) FROM stdin;
    public               postgres    false    218   �       '          0    16389    statustable 
   TABLE DATA           D   COPY public.statustable (id, name, "delete_state code") FROM stdin;
    public               postgres    false    217   �       �           2606    16403    carttable carttable_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.carttable
    ADD CONSTRAINT carttable_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY public.carttable DROP CONSTRAINT carttable_pkey;
       public                 postgres    false    218            �           2606    16397     statustable statustable_name_key 
   CONSTRAINT     [   ALTER TABLE ONLY public.statustable
    ADD CONSTRAINT statustable_name_key UNIQUE (name);
 J   ALTER TABLE ONLY public.statustable DROP CONSTRAINT statustable_name_key;
       public                 postgres    false    217            �           2606    16395    statustable statustable_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.statustable
    ADD CONSTRAINT statustable_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.statustable DROP CONSTRAINT statustable_pkey;
       public                 postgres    false    217            �           2606    16404 "   carttable carttable_status_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.carttable
    ADD CONSTRAINT carttable_status_id_fkey FOREIGN KEY (status_id) REFERENCES public.statustable(id);
 L   ALTER TABLE ONLY public.carttable DROP CONSTRAINT carttable_status_id_fkey;
       public               postgres    false    218    217    3218            (   6  x�����d1D�׹�G|�D�8 A�����XC �VI��D��e,�2��i0pƱ�S9�&���C8�kmƾ剺�~�+��ѭU`|m�q�ڠ����x�zV��I� ~��y��.�2^�tM/jUo��O$8T�/ه|�$�9�!	"#��fб%��K��|�<G��;���Z��Mɚ�6u~ȗ�`�ݛ���Qp�Tѡ���3���T,�0h��[b��Z�i���#j ��!4�-�L��MG���0?>�|�a��#k��� �<G0c���OWd8��{�B���zM�-��������8j��      '   �   x�5˻�0 �:���s���`�4��sP �3 3�D  ��y#��Xgh*�u��	x�*f�	��/��6r�W��Un��<�;�Eӊ���#E��DmZpΔɥA����Oy�,�7j�sH��fn�-D�\kS�L��Q�k��㿍[��}	@�     