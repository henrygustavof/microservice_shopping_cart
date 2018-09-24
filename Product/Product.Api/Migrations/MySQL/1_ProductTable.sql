CREATE TABLE product (
  product_id int(11) NOT NULL AUTO_INCREMENT,
  name varchar(50) NOT NULL,
  picture_url longtext NOT NULL,
  description longtext NOT NULL,
  price decimal(10,2) NOT NULL,
  currency varchar(3) NOT NULL,
  PRIMARY KEY (product_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
