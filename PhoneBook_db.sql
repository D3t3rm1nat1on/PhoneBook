PGDMP         4                x            PhoneBook_DB    10.11    10.11     �
           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                       false            �
           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                       false            �
           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                       false            �
           1262    16539    PhoneBook_DB    DATABASE     �   CREATE DATABASE "PhoneBook_DB" WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Russian_Russia.1251' LC_CTYPE = 'Russian_Russia.1251';
    DROP DATABASE "PhoneBook_DB";
             postgres    false                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
             postgres    false            �
           0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                  postgres    false    3                        3079    12924    plpgsql 	   EXTENSION     ?   CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;
    DROP EXTENSION plpgsql;
                  false            �
           0    0    EXTENSION plpgsql    COMMENT     @   COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';
                       false    1            �            1259    16542 	   phonebook    TABLE       CREATE TABLE public.phonebook (
    id integer NOT NULL,
    name character varying(120) NOT NULL,
    phone character varying(5) NOT NULL,
    mobile character varying(20) NOT NULL,
    "position" character varying(50) NOT NULL,
    department character varying(50) NOT NULL
);
    DROP TABLE public.phonebook;
       public         postgres    false    3            �            1259    16540    phonebook_id_seq    SEQUENCE     �   CREATE SEQUENCE public.phonebook_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.phonebook_id_seq;
       public       postgres    false    3    197            �
           0    0    phonebook_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.phonebook_id_seq OWNED BY public.phonebook.id;
            public       postgres    false    196            n
           2604    16545    phonebook id    DEFAULT     l   ALTER TABLE ONLY public.phonebook ALTER COLUMN id SET DEFAULT nextval('public.phonebook_id_seq'::regclass);
 ;   ALTER TABLE public.phonebook ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    197    196    197            �
          0    16542 	   phonebook 
   TABLE DATA               T   COPY public.phonebook (id, name, phone, mobile, "position", department) FROM stdin;
    public       postgres    false    197          �
           0    0    phonebook_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.phonebook_id_seq', 10, true);
            public       postgres    false    196            q
           2606    16548    phonebook phonebook_pk 
   CONSTRAINT     T   ALTER TABLE ONLY public.phonebook
    ADD CONSTRAINT phonebook_pk PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.phonebook DROP CONSTRAINT phonebook_pk;
       public         postgres    false    197            o
           1259    16546    phonebook_id_uindex    INDEX     N   CREATE UNIQUE INDEX phonebook_id_uindex ON public.phonebook USING btree (id);
 '   DROP INDEX public.phonebook_id_uindex;
       public         postgres    false    197            �
   �  x��S�j1}���.�տ�c�&4��@��PJ��m�5��ڿ0����c�Iqh��j5sΜ3���?r�ΰ���>���Z�%i�MՇ� �B��LA��;�)ϸI�>��'^r��MƚhF�_
"w��x�m���a�i��g��2���.�
�[�o	HWR4�,���5�%���dE��/�ꠧ�Cko���.��Gٹy��x��>_�c�s�ld!F�����*ޣ��@�,��*}�Z6�bQ|�	�^Yp0M��Nmk�D+,@���4�/B�Ł�PBbP�ۍE��s�!�[���7�4P��`��;:$�b8i{�Je�����*�E:M;���j���E	-�)�RQ��q��Z:!@�wPa����T�F��R��Q;2�;b�B�}�	�P�ȕ���������=vI     