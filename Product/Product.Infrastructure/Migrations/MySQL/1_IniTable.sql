CREATE TABLE category (
  category_id int(11) NOT NULL AUTO_INCREMENT,
  name varchar(45) NOT NULL,
  description varchar(45) DEFAULT NULL,
  PRIMARY KEY (category_id)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE product (
  product_id int(11) NOT NULL AUTO_INCREMENT,
  name varchar(50) NOT NULL,
  picture_url longtext NOT NULL,
  description longtext NOT NULL,
  price decimal(10,2) NOT NULL,
  currency varchar(3) NOT NULL,
  unit int(11)  NOT NULL,
  category_id int(11) DEFAULT NULL,
  PRIMARY KEY(product_id),
  CONSTRAINT product_category_id FOREIGN KEY (category_id) REFERENCES category (category_id)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
