CREATE DATABASE IF NOT EXISTS lab2_csharp;

CREATE TABLE IF NOT EXISTS `lab2_csharp`.`urls` (
    `id` INT UNSIGNED AUTO_INCREMENT,
    `url` VARCHAR(255) DEFAULT '' NOT NULL,
    `clicks` INT UNSIGNED DEFAULT '0' NOT NULL,
    PRIMARY KEY(`id`)
) ENGINE=INNODB;
