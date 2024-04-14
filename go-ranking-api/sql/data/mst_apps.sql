SET CHARACTER_SET_CLIENT = utf8;
SET CHARACTER_SET_CONNECTION = utf8;

INSERT INTO mst_apps VALUES
 (1001,    'SampleApp',  'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx',  '2024-04-01 00:00:00',  '2024-04-01 00:00:00')
ON DUPLICATE KEY UPDATE
 `name` = VALUES(`name`),
 `client_key` = VALUES(`client_key`);

SELECT * FROM mst_apps;
