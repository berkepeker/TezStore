-- Insert data for PackagingTypes
INSERT INTO "PackagingTypes" ("Name", "Description", "PackageFactor") VALUES 
('Glass Bottle', 'Glass bottle for beverages like soda or wine.', '1.20'),
('Plastic Bottle', 'PET plastic bottle for water or soft drinks.', '1.00'),
('Aluminum Can', 'Aluminum can for carbonated drinks and beers.', '0.90'),
('Steel Can', 'Steel can for energy drinks or canned beverages.', '1.10'),
('Tetra Pack', 'Carton package for milk and juices.', '1.30'),
('Plastic Pouch', 'Plastic pouch for liquid syrups or drinks.', '0.80'),
('Bag-in-Box', 'Box with an inner pouch for wine or juices.', '2.00'),
('Carton Bottle', 'Eco-friendly carton bottle for beverages.', '1.40'),
('Mini Glass Bottle', 'Small glass bottle for juices or flavored drinks.', '0.70'),
('Reusable Bottle', 'Reusable glass or plastic bottle for water or energy drinks.', '1.50');

-- Insert data for Products
INSERT INTO "Products" ("ProductName", "PackagingTypeId", "SizeFactor") VALUES 
('Coca-Cola Classic', 2, '0.4'),
('Sour Cherry Nectar 200ml', 5, '0.20'),
('Apricot Nectar 200ml', 5, '0.20'),
('Peach Nectar 200ml', 5, '0.20'),
('Orange Nectar 200ml', 5, '0.20'),
('Avşar Cool Lime 200ml', 1, '0.20'),
('Fruit Mix Nectar 200ml', 5, '0.20'),
('dimes 100% mix', 5, '1.00'),
('7/24 Drink 200ml', 5, '0.20'),
('Banana Strawberry Drink 200ml', 5, '0.20'),
('Pineapple Coconut Drink 200ml', 5, '0.20'),
('Pineapple Drink 200ml', 5, '0.20'),
('Hayat Drinking Water 5L', 2, '5.00');

-- Insert data for SellableItems
INSERT INTO "SellableItems" ("Name", "Description", "Price", "ImagePath") VALUES 
('Askıda Ekmek', 'İhtiyaç sahiplerine ulaştırılmak üzere bağışlanan ekmek.(Adet)', 10, '/images/c41370b5-6515-4986-86d0-0da628496446.jpg'),
('Askıda Bebek Bezi', 'İhtiyaç sahibi ailelere destek amacıyla bağışlanan bebek bezi. (Paket)', 250, '/images/0d1728ec-0c6c-44de-91f3-b1ba27a81da9.jpg'),
('Ekmek', 'Adet ekmek.', 10, '/images/be768048-fa51-41b9-99ec-61a75aa68a77.jpg'),
('Makarna (500gr)', 'Uzun raf ömrüne sahip, pratik ve doyurucu bir gıda.', 24, '/images/965aef7b-c87f-47e2-a892-1dbf52cde6f3.jpg'),
('Pirinç (1kg)', 'Pilav ve çeşitli yemekler için temel bir malzeme.', 30, '/images/7b66d7ad-8def-426f-a706-4ad6c5740792.jpg'),
('Un (2kg)', 'Ekmek, börek ve tatlılar için vazgeçilmez.', 60, '/images/02e4da12-ecad-4b4b-a0ef-528c2ec75e36.jpg'),
('Ayçiçek Yağı (1L)', 'Yemeklerinizi lezzetlendirecek temel yağ çeşidi.', 90, '/images/1b07cc80-1071-476d-b575-913455434a88.jpg'),
('Domates Salçası (650gr)', 'Yemeklere renk ve tat katan geleneksel lezzet.', 70, '/images/92d82f26-a3b0-42db-afff-267dcf88a2eb.jpg'),
('Şeker (1kg)', 'Tatlılar ve çay için temel ihtiyaç.', 50, '/images/cd9de057-fde4-40e1-b796-f161228b84d5.jpg'),
('Öğütme Sofra Tuzu (250gr)', 'Her yemeğin olmazsa olmazı.', 45, '/images/c42f09fa-7904-4436-ae7f-449afafa876b.jpg'),
('Siyah Çay (400g)', 'Türk kültürünün vazgeçilmez içeceği.', 80, '/images/cd97fe47-ce1e-46e7-a20c-698162cad199.jpg'),
('Organik Süt (1L)', 'Kahvaltıların ve tariflerin temel bileşeni.', 50, '/images/619ce116-3d72-4c52-bca2-dc72e08aae7d.jpg');

-- Insert data for Users
INSERT INTO "Users" ("TelefonNo", "Sifre", "Ad", "Soyad", "DogumTarihi", "Cinsiyet", "KayitTarihi", "Durum", "Balance") VALUES 
('5536699927', '158358', 'Berke', 'Peker', '1111-11-11 00:00:00', 'Erkek', '2024-12-23 01:35:07.656446', 'Aktif', '8.12'),
('5555555555', '123456', 'Admin', 'User', '1111-11-11 00:00:00', 'Erkek', '2024-12-24 02:17:34.18556', 'Admin', '38434.0'),
('1111111111', '111111', 'sdadsa', 'sadas', '1111-11-11 00:00:00', 'Erkek', '2025-05-08 00:59:55.358487', 'Aktif', '0.0');

-- Insert data for __EFMigrationsHistory
INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES 
('20250227103554_InitialCreate', '8.0.10'),
('20241222223353_InitialCreate', '8.0.10'); 