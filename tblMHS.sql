CREATE TABLE mahasiswa(nim bigint unique primary key not null, firstname varchar(50) not null, lastname varchar(50) not null, phone varchar(20))
;

INSERT INTO mahasiswa(nim, firstname, lastname)
    VALUES
        ('123456789012', 'Nama Depan', 'Nama Belakang')
;
