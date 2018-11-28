select * from (select p.*, row_number() over (order by id) rn, '||v_count||' total_lines 
        from pokemon p) where rn between 1 and 10

select * from (select id, name, experience, height, weight
row_number() over ())
* from pokemon where rownum < 10 order by id desc

select * from 
(select id, name, experience, rownum rn from pokemon where name like '%ch%')
where rn between 0 and 20;

select p.*, rownum from pokemon p

create SEQUENCE sq_pokemon
  INCREMENT BY 1 MAXVALUE 9999999999 CYCLE;

create table pokemon(
    id number(10) primary key,
    name varchar(100),
    experience number(10),
    height number(10),
    weight number(10)
);

select count(1) from pokemon

insert into pokemon (id, name, experience, height, weight) values (289, 'slaking', 252, 20, 1305);

create or replace procedure list_pokemon (
    p_result    IN OUT SYS_REFCURSOR,
    p_text varchar2,
    p_order_field varchar2,
    p_order_type varchar2,
    p_start number,
    p_length number
) is
v_sql varchar2(4000);
v_count number(10);
v_filtered number(10);
begin 
    select count(1) into v_count from pokemon;
    select count(1) into v_filtered from pokemon where p_text is null or name like '%'||p_text||'%';
    v_sql := 'select * from (select p.*, row_number() over (order by '||p_order_field||' '||p_order_type||') rn, '||v_count||' total_lines, '||v_filtered||' filtered from pokemon p where :p_1 is null or name like :p_2 ) where rn between '||p_start||' and '||p_length;
    open p_result for v_sql using p_text, '%'||p_text||'%';
end;

