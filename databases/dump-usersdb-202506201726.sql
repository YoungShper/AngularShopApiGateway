PGDMP  ,                    }            usersdb    17.5 (Debian 17.5-1.pgdg120+1)    17.0     "           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            #           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            $           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            %           1262    16384    usersdb    DATABASE     r   CREATE DATABASE usersdb WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.utf8';
    DROP DATABASE usersdb;
                     postgres    false            �            1259    16397 
   userstable    TABLE     !  CREATE TABLE public.userstable (
    id uuid NOT NULL,
    last_name text,
    first_name text,
    address text,
    postal_code character varying(20),
    is_admin boolean DEFAULT false,
    delete_state_code integer,
    city text,
    password text NOT NULL,
    mail text NOT NULL
);
    DROP TABLE public.userstable;
       public         heap r       postgres    false                      0    16397 
   userstable 
   TABLE DATA           �   COPY public.userstable (id, last_name, first_name, address, postal_code, is_admin, delete_state_code, city, password, mail) FROM stdin;
    public               postgres    false    217   �       �           2606    16406    userstable userstable_mail_key 
   CONSTRAINT     Y   ALTER TABLE ONLY public.userstable
    ADD CONSTRAINT userstable_mail_key UNIQUE (mail);
 H   ALTER TABLE ONLY public.userstable DROP CONSTRAINT userstable_mail_key;
       public                 postgres    false    217            �           2606    16404    userstable userstable_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.userstable
    ADD CONSTRAINT userstable_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.userstable DROP CONSTRAINT userstable_pkey;
       public                 postgres    false    217               �   x�36JK42�0�MJ13�5I34�ML3��51646LKL54�0�4Ss ��93��Js��s3s���s9-�8�88/̿�xaׅ}6q�%���8f��dee;����d���yxZT�x�y�Ey�9g�����zT�fb��+F��� h7�     