PGDMP                      }         	   articledb    17.5 (Debian 17.5-1.pgdg120+1)    17.0                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false                        0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            !           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            "           1262    16384 	   articledb    DATABASE     t   CREATE DATABASE articledb WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.utf8';
    DROP DATABASE articledb;
                     postgres    false            �            1259    16389    articlestable    TABLE     �   CREATE TABLE public.articlestable (
    id uuid NOT NULL,
    name text NOT NULL,
    description text,
    delete_state_code integer
);
 !   DROP TABLE public.articlestable;
       public         heap r       postgres    false                      0    16389    articlestable 
   TABLE DATA           Q   COPY public.articlestable (id, name, description, delete_state_code) FROM stdin;
    public               postgres    false    217   �       �           2606    16395     articlestable articlestable_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public.articlestable
    ADD CONSTRAINT articlestable_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY public.articlestable DROP CONSTRAINT articlestable_pkey;
       public                 postgres    false    217               ?   x�3NK�0M33�5574�5153�M2NK�5J�43N33KLK4�,.)��K�442�4������ �L�     