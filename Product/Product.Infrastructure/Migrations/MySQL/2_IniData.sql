INSERT INTO category(name, description) 
VALUES ('computers', 'computers'),('cloth and shoes', 'cloth and shoes');

INSERT INTO product(name,picture_url, description,unit,currency,price,category_id) 
VALUES ('adidas black t-shirt', 'https://hfuentesstorage.blob.core.windows.net/images/adidas_black_tshirt.jpg','didas black t-shirt',10,840,10,LAST_INSERT_ID()),
('adidas white t-shirt', 'https://hfuentesstorage.blob.core.windows.net/images/adidas_white_tshirt.jpg','adidas white t-shirt',10,840,15,LAST_INSERT_ID()),
('nike black and white t-shirt', 'https://hfuentesstorage.blob.core.windows.net/images/nike_back_and_white_tshirt.jpg','nike black and white t-shirt',10,840,50,LAST_INSERT_ID()),
('adidas red sport shoes', 'https://hfuentesstorage.blob.core.windows.net/images/nike_red_sport_shoes.jpeg','adidas red sport shoes',10,840,60,LAST_INSERT_ID()),
('puma blue sport shoes', 'https://hfuentesstorage.blob.core.windows.net/images/puma_blue_shoes.jpg','puma blue sport shoes',10,840,90,LAST_INSERT_ID()),
('puma grey t-shirt', 'https://hfuentesstorage.blob.core.windows.net/images/puma_grey_tshirt.jpg','puma gray t-shirt',10,840,30,LAST_INSERT_ID()),
('reebok blue t-shirt', 'https://hfuentesstorage.blob.core.windows.net/images/reebok_blue_tshirt.jpg','reebok blue t-shirt',10,840,20,LAST_INSERT_ID()),
('reebok white sport shoes', 'https://hfuentesstorage.blob.core.windows.net/images/reebok_white_sport_shoes.jpg','reebok white sport shoes',10,840,100,LAST_INSERT_ID())
;