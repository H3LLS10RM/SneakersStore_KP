BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "role" (
	"id"	INTEGER NOT NULL UNIQUE,
	"role_name"	TEXT NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "status" (
	"id"	INTEGER NOT NULL UNIQUE,
	"status_name"	TEXT NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "discount" (
	"id"	INTEGER NOT NULL UNIQUE,
	"disc_name"	TEXT NOT NULL,
	"value"	INTEGER NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "category" (
	"id"	INTEGER NOT NULL UNIQUE,
	"name"	INTEGER NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "product" (
	"id"	INTEGER NOT NULL UNIQUE,
	"category_id"	INTEGER NOT NULL,
	"name"	TEXT NOT NULL,
	"cost"	INTEGER NOT NULL,
	"amount"	INTEGER NOT NULL,
	"discount_id"	INTEGER,
	FOREIGN KEY("category_id") REFERENCES "category"("id") ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY("discount_id") REFERENCES "discount"("id") ON UPDATE CASCADE ON DELETE CASCADE,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "users" (
	"id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	"surname"	TEXT NOT NULL,
	"role"	TEXT NOT NULL,
	"sex"	TEXT NOT NULL,
	"status"	TEXT NOT NULL,
	"login"	TEXT NOT NULL,
	"password"	TEXT NOT NULL,
	FOREIGN KEY("status") REFERENCES "status"("id") ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY("role") REFERENCES "role"("id") ON UPDATE CASCADE ON DELETE CASCADE,
	PRIMARY KEY("id" AUTOINCREMENT)
);
INSERT INTO "role" ("id","role_name") VALUES (1,'Админ'),
 (2,'Модератор'),
 (3,'Пользователь');
INSERT INTO "status" ("id","status_name") VALUES (1,'Активен'),
 (2,'В ожидании'),
 (3,'Не подтвержден');
INSERT INTO "discount" ("id","disc_name","value") VALUES (1,'Скидка новому пользователю',1),
 (2,'Скидка с промокодом',2),
 (3,'Скидка в честь праздника',3);
INSERT INTO "category" ("id","name") VALUES (1,'Категория1'),
 (2,'Категория2'),
 (3,'Категория3'),
 (4,'Категория4'),
 (5,'Категория5');
INSERT INTO "product" ("id","category_id","name","cost","amount","discount_id") VALUES (1,1,'Имя продукта1',1,1,3),
 (2,2,'Имя продукта2',2,2,2),
 (3,3,'Имя продукта3',3,3,1),
 (4,4,'Имя продукта4',4,4,NULL),
 (5,5,'Имя продукта5',5,5,NULL);
INSERT INTO "users" ("id","name","surname","role","sex","status","login","password") VALUES (1,'Алексей','Кренделев','1','Муж','1','AlendK','258852'),
 (2,'Александр','Ломоносов','1','Муж','1','npocto_cah93','1234'),
 (3,'Елена','Соснина','3','Жен','3','LenO4ka','7777'),
 (4,'Admin','Admin','1','Муж','1','Admin','Admin'),
 (5,'Moder','Moder','2','Жен','1','Moder','Moder');
COMMIT;
