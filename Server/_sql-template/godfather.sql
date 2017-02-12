/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50505
Source Host           : localhost:3306
Source Database       : godfather

Target Server Type    : MYSQL
Target Server Version : 50505
File Encoding         : 65001

Date: 2017-02-05 18:30:41
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `aspnetroleclaims`
-- ----------------------------
DROP TABLE IF EXISTS `aspnetroleclaims`;
CREATE TABLE `aspnetroleclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  `RoleId` varchar(127) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of aspnetroleclaims
-- ----------------------------

-- ----------------------------
-- Table structure for `aspnetroles`
-- ----------------------------
DROP TABLE IF EXISTS `aspnetroles`;
CREATE TABLE `aspnetroles` (
  `Id` varchar(127) NOT NULL,
  `ConcurrencyStamp` longtext,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of aspnetroles
-- ----------------------------

-- ----------------------------
-- Table structure for `aspnetuserclaims`
-- ----------------------------
DROP TABLE IF EXISTS `aspnetuserclaims`;
CREATE TABLE `aspnetuserclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  `UserId` varchar(127) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of aspnetuserclaims
-- ----------------------------

-- ----------------------------
-- Table structure for `aspnetuserlogins`
-- ----------------------------
DROP TABLE IF EXISTS `aspnetuserlogins`;
CREATE TABLE `aspnetuserlogins` (
  `UserId` varchar(127) NOT NULL,
  `LoginProvider` varchar(127) NOT NULL,
  `ProviderDisplayName` longtext,
  `ProviderKey` varchar(127) NOT NULL,
  PRIMARY KEY (`UserId`),
  UNIQUE KEY `AK_AspNetUserLogins_LoginProvider_ProviderKey` (`LoginProvider`,`ProviderKey`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of aspnetuserlogins
-- ----------------------------

-- ----------------------------
-- Table structure for `aspnetuserroles`
-- ----------------------------
DROP TABLE IF EXISTS `aspnetuserroles`;
CREATE TABLE `aspnetuserroles` (
  `UserId` varchar(127) NOT NULL,
  `RoleId` varchar(127) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of aspnetuserroles
-- ----------------------------

-- ----------------------------
-- Table structure for `aspnetusers`
-- ----------------------------
DROP TABLE IF EXISTS `aspnetusers`;
CREATE TABLE `aspnetusers` (
  `Id` varchar(127) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  `ConcurrencyStamp` longtext,
  `Email` varchar(256) DEFAULT NULL,
  `EmailConfirmed` bit(1) NOT NULL,
  `Ip` varchar(16) DEFAULT NULL,
  `LastLoginDate` datetime NOT NULL,
  `LockoutEnabled` bit(1) NOT NULL,
  `LockoutEnd` datetime DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  `Online` bit(1) NOT NULL,
  `PasswordHash` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` bit(1) NOT NULL,
  `RegisterDate` datetime NOT NULL,
  `SecurityStamp` longtext,
  `SessionID` varchar(25) DEFAULT NULL,
  `SocialClub` varchar(24) DEFAULT NULL,
  `TwoFactorEnabled` bit(1) NOT NULL,
  `UpdateDate` datetime NOT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of aspnetusers
-- ----------------------------
INSERT INTO `aspnetusers` VALUES ('13d20275-7c89-43ae-9c3d-023e79248813', '0', '92a784fb-ada3-42d4-8bc4-53c63f4f0816', 'jingles', '', null, '2017-02-05 18:29:27', '', null, 'J@J.COM', 'J@J.COM', '', 'AQAAAAEAACcQAAAAEHHSiyNmzKtGxv1V/uWOfCj5ETEvA5uCMBetq+FrwyLoFk71x6+Ku2HMdmJ9BYHBsQ==', null, '', '0001-01-01 00:00:00', '138e34b4-ccb4-48cd-9978-ed72316db845', 'vJwjYPK5vdST5YymaGwrJCGyN', 'Spijkervet', '', '0001-01-01 00:00:00', 'j@j.com');

-- ----------------------------
-- Table structure for `aspnetusertokens`
-- ----------------------------
DROP TABLE IF EXISTS `aspnetusertokens`;
CREATE TABLE `aspnetusertokens` (
  `UserId` varchar(127) NOT NULL,
  `LoginProvider` varchar(127) NOT NULL,
  `Name` varchar(127) NOT NULL,
  `Value` longtext,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of aspnetusertokens
-- ----------------------------

-- ----------------------------
-- Table structure for `ban`
-- ----------------------------
DROP TABLE IF EXISTS `ban`;
CREATE TABLE `ban` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `AccountId` varchar(127) DEFAULT NULL,
  `Active` bit(1) NOT NULL,
  `BanDate` datetime NOT NULL,
  `BanReason` varchar(128) DEFAULT NULL,
  `ExpiryDate` datetime NOT NULL,
  `Ip` varchar(16) DEFAULT NULL,
  `IsSocialClubBanned` bit(1) NOT NULL,
  `IssuerId` int(11) NOT NULL,
  `SocialClub` varchar(24) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Ban_AccountId` (`AccountId`),
  CONSTRAINT `FK_Ban_AspNetUsers_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `aspnetusers` (`Id`) ON DELETE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of ban
-- ----------------------------

-- ----------------------------
-- Table structure for `character`
-- ----------------------------
DROP TABLE IF EXISTS `character`;
CREATE TABLE `character` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `AccountId` varchar(127) DEFAULT NULL,
  `ActiveGroupID` int(11) NOT NULL,
  `Admin` int(11) NOT NULL,
  `AttackXP` int(11) NOT NULL,
  `Bank` int(11) NOT NULL,
  `BodyguardXP` int(11) NOT NULL,
  `Cash` int(11) NOT NULL,
  `CookingXP` int(11) NOT NULL,
  `CraftingXP` int(11) NOT NULL,
  `DealingXP` int(11) NOT NULL,
  `DetectiveXP` int(11) NOT NULL,
  `FarmingXP` int(11) NOT NULL,
  `FiremakingXP` int(11) NOT NULL,
  `FishingXP` int(11) NOT NULL,
  `HuntingXP` int(11) NOT NULL,
  `JobID` int(11) NOT NULL,
  `LastLoginDate` datetime NOT NULL,
  `LawyerXP` int(11) NOT NULL,
  `Level` int(11) NOT NULL,
  `LifeXP` int(11) NOT NULL,
  `MechanicXP` int(11) NOT NULL,
  `MiningXP` int(11) NOT NULL,
  `Model` int(11) NOT NULL,
  `ModelName` longtext,
  `Name` varchar(32) DEFAULT NULL,
  `Online` bit(1) NOT NULL,
  `PosX` float NOT NULL,
  `PosY` float NOT NULL,
  `PosZ` float NOT NULL,
  `RegisterDate` datetime NOT NULL,
  `RegistrationStep` int(11) NOT NULL,
  `Rot` float NOT NULL,
  `StrengthXP` int(11) NOT NULL,
  `TaxiDriverXP` int(11) NOT NULL,
  `ThiefingXP` int(11) NOT NULL,
  `TruckingXP` int(11) NOT NULL,
  `WhoreXP` int(11) NOT NULL,
  `WoodcuttingXP` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Character_AccountId` (`AccountId`),
  CONSTRAINT `FK_Character_AspNetUsers_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `aspnetusers` (`Id`) ON DELETE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of character
-- ----------------------------
INSERT INTO `character` VALUES ('2', '13d20275-7c89-43ae-9c3d-023e79248813', '0', '9', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '2017-02-05 18:29:28', '0', '0', '0', '0', '0', '-872673803', '-872673803', 'Sam_Jingles', '', '-1904.78', '-572.29', '19.0972', '2017-02-03 17:51:54', '-1', '-65.3063', '0', '0', '0', '0', '0', '0');

-- ----------------------------
-- Table structure for `command`
-- ----------------------------
DROP TABLE IF EXISTS `command`;
CREATE TABLE `command` (
  `CommandID` int(11) NOT NULL AUTO_INCREMENT,
  `AdminLevel` int(11) NOT NULL,
  `Help` longtext,
  `Name` varchar(32) DEFAULT NULL,
  `Parameters` varchar(32) DEFAULT NULL,
  `Type` varchar(32) DEFAULT NULL,
  PRIMARY KEY (`CommandID`)
) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of command
-- ----------------------------
INSERT INTO `command` VALUES ('1', '0', 'View your character\'s statistics', '/stats', null, 'Character');
INSERT INTO `command` VALUES ('2', '0', 'Brings you back to the account login screen.', '/logout', null, 'Character');
INSERT INTO `command` VALUES ('3', '0', 'Express something.', '/me', '[Text]', 'Chat');
INSERT INTO `command` VALUES ('5', '0', 'Express an action.', '/do', '[Text]', 'Chat');
INSERT INTO `command` VALUES ('6', '0', 'Same as /do, but smaller range.', '/dolow', '[Text]', 'Chat');
INSERT INTO `command` VALUES ('7', '0', 'Shout.', '/s', '[Text]', 'Chat');
INSERT INTO `command` VALUES ('8', '0', 'Whisper to a player.', '/w', '[Name/ID] [Text]', 'Chat');
INSERT INTO `command` VALUES ('9', '0', 'Talk in OOC chat.', '/b', '[Text]', 'Chat');
INSERT INTO `command` VALUES ('10', '0', 'Talk in Global OOC chat.', '/o', '[Text]', 'Chat');
INSERT INTO `command` VALUES ('12', '0', 'Send a PM to another player.', '/pm', '[Name/ID] [Text]', 'Chat');
INSERT INTO `command` VALUES ('13', '0', 'Same as /me, but smaller range.', '/melow', '[Text]', 'Chat');
INSERT INTO `command` VALUES ('14', '1', 'Teleport to a player.', '/gotoid', '[Name/ID]', 'Administrator');
INSERT INTO `command` VALUES ('15', '1', 'Teleport a player to you.', '/gethere', '[Name/ID]', 'Admin');
INSERT INTO `command` VALUES ('16', '1', 'Teleport to a property, job or a place on the map.', '/goto', '[Property/Job/Place] [ID]', 'Admin');
INSERT INTO `command` VALUES ('17', '0', 'Switch your active group.', '/switchgroup', '[GroupID]', 'Character');
INSERT INTO `command` VALUES ('18', '5', 'Create a group', '/creategroup', '[name]', 'Admin, Groups');
INSERT INTO `command` VALUES ('19', '5', 'Edit a group', 'editgroup', '[GroupID]', 'Admin, Groups');
INSERT INTO `command` VALUES ('20', '5', 'Create a property for a player or a group.', '/createproperty', '[player/group] [Name/ID]', 'Admin, Properties');
INSERT INTO `command` VALUES ('21', '5', 'Edit a property.', '/setproperty', '[ID] [Exterior/Interior/IPL]', 'Admin, Properties');
INSERT INTO `command` VALUES ('22', '5', 'Edit a job.', '/editjob', '[ID]', 'Admin, Jobs');
INSERT INTO `command` VALUES ('23', '5', 'Set a player\'s money.', '/setmoney', '[Name/ID]', 'Admin');
INSERT INTO `command` VALUES ('24', '1', 'Give a gun to a player.', '/givegun', '[Name/ID]', 'Admin');
INSERT INTO `command` VALUES ('25', '4', 'Create a vehicle for a player or a group.', '/createvehicle', '[player/group] [model] [color]', 'Admin');
INSERT INTO `command` VALUES ('26', '1', 'Set a player\'s skin.', '/setskin', '[Name/ID] [Model]', 'Admin');
INSERT INTO `command` VALUES ('27', '1', 'Set a player\'s health', '/sethealth', '[Name/ID] [Health]', 'Admin');
INSERT INTO `command` VALUES ('28', '1', 'Set a player\'s armor', '/setarmor', '[Name/ID] [Armor]', 'Admin');
INSERT INTO `command` VALUES ('29', '0', 'Drop an item.', '/drop', '[item] [all/amount]', 'Character');
INSERT INTO `command` VALUES ('30', '0', 'Smoke a sigarette.', '/smoke', null, 'Character');
INSERT INTO `command` VALUES ('31', '0', 'List all groups.', '/listgroups', null, 'Groups');
INSERT INTO `command` VALUES ('32', '4', 'Create a job.', '/createjob', '[type] [level] [group (optional]', 'Admin, Jobs');
INSERT INTO `command` VALUES ('33', '0', 'Get a job at a job point.', '/join', null, 'Jobs');
INSERT INTO `command` VALUES ('34', '0', 'Quit your job.', '/quitjob', null, 'Jobs');
INSERT INTO `command` VALUES ('35', '0', 'Buy a product from a business.', '/buy', null, 'Properties');
INSERT INTO `command` VALUES ('36', '0', 'Store or spawn your vehicle.', '/vstorage', null, 'Vehicles');

-- ----------------------------
-- Table structure for `group`
-- ----------------------------
DROP TABLE IF EXISTS `group`;
CREATE TABLE `group` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ExtraType` int(11) NOT NULL,
  `Name` varchar(32) DEFAULT NULL,
  `Type` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of group
-- ----------------------------
INSERT INTO `group` VALUES ('1', '0', 'LSPD', '1');
INSERT INTO `group` VALUES ('2', '0', 'FBI', '2');
INSERT INTO `group` VALUES ('3', '0', 'National Guard', '3');
INSERT INTO `group` VALUES ('4', '0', 'Fire Medical Department', '4');
INSERT INTO `group` VALUES ('5', '0', 'Correctional Facility', '5');
INSERT INTO `group` VALUES ('6', '0', 'Hitman Agency', '6');
INSERT INTO `group` VALUES ('7', '0', 'Taxi Cab Company', '7');
INSERT INTO `group` VALUES ('8', '0', 'News Network', '8');
INSERT INTO `group` VALUES ('9', '0', 'Criminal Organization', '9');
INSERT INTO `group` VALUES ('10', '0', 'Detective Co.', '10');
INSERT INTO `group` VALUES ('11', '2', 'Truckie', '10');
INSERT INTO `group` VALUES ('12', '3', 'Morningwood Ammunation', '10');
INSERT INTO `group` VALUES ('13', '3', 'Paleto Bay Ammunation', '10');
INSERT INTO `group` VALUES ('14', '4', 'Davis Gasoline Station', '10');
INSERT INTO `group` VALUES ('15', '4', 'El Burro Gasoline Station', '10');
INSERT INTO `group` VALUES ('16', '4', 'Vinewood Gas', '10');
INSERT INTO `group` VALUES ('17', '2', 'Banham Food Mart', '10');
INSERT INTO `group` VALUES ('18', '1', 'Edeka', '10');
INSERT INTO `group` VALUES ('19', '0', 'Jane_Doe', '7');
INSERT INTO `group` VALUES ('20', '0', 'Austin Babe', '6');
INSERT INTO `group` VALUES ('21', '0', 'The Ritz Carl Family', '9');

-- ----------------------------
-- Table structure for `groupdivision`
-- ----------------------------
DROP TABLE IF EXISTS `groupdivision`;
CREATE TABLE `groupdivision` (
  `DivisionID` int(11) NOT NULL AUTO_INCREMENT,
  `GroupID` int(11) NOT NULL,
  `Name` longtext,
  PRIMARY KEY (`DivisionID`),
  KEY `IX_GroupDivision_GroupID` (`GroupID`),
  CONSTRAINT `FK_GroupDivision_Group_GroupID` FOREIGN KEY (`GroupID`) REFERENCES `group` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of groupdivision
-- ----------------------------

-- ----------------------------
-- Table structure for `groupmember`
-- ----------------------------
DROP TABLE IF EXISTS `groupmember`;
CREATE TABLE `groupmember` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CharacterId` int(11) DEFAULT NULL,
  `GroupDivisionDivisionID` int(11) DEFAULT NULL,
  `GroupId` int(11) DEFAULT NULL,
  `GroupRankRankID` int(11) DEFAULT NULL,
  `Leader` bit(1) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_GroupMember_CharacterId` (`CharacterId`),
  KEY `IX_GroupMember_GroupDivisionDivisionID` (`GroupDivisionDivisionID`),
  KEY `IX_GroupMember_GroupId` (`GroupId`),
  KEY `IX_GroupMember_GroupRankRankID` (`GroupRankRankID`),
  CONSTRAINT `FK_GroupMember_Character_CharacterId` FOREIGN KEY (`CharacterId`) REFERENCES `character` (`Id`) ON DELETE NO ACTION,
  CONSTRAINT `FK_GroupMember_GroupDivision_GroupDivisionDivisionID` FOREIGN KEY (`GroupDivisionDivisionID`) REFERENCES `groupdivision` (`DivisionID`) ON DELETE NO ACTION,
  CONSTRAINT `FK_GroupMember_GroupRank_GroupRankRankID` FOREIGN KEY (`GroupRankRankID`) REFERENCES `grouprank` (`RankID`) ON DELETE NO ACTION,
  CONSTRAINT `FK_GroupMember_Group_GroupId` FOREIGN KEY (`GroupId`) REFERENCES `group` (`Id`) ON DELETE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of groupmember
-- ----------------------------

-- ----------------------------
-- Table structure for `grouprank`
-- ----------------------------
DROP TABLE IF EXISTS `grouprank`;
CREATE TABLE `grouprank` (
  `RankID` int(11) NOT NULL AUTO_INCREMENT,
  `GroupID` int(11) NOT NULL,
  `Name` longtext,
  `RankNumber` int(11) NOT NULL,
  PRIMARY KEY (`RankID`),
  KEY `IX_GroupRank_GroupID` (`GroupID`),
  CONSTRAINT `FK_GroupRank_Group_GroupID` FOREIGN KEY (`GroupID`) REFERENCES `group` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of grouprank
-- ----------------------------

-- ----------------------------
-- Table structure for `item`
-- ----------------------------
DROP TABLE IF EXISTS `item`;
CREATE TABLE `item` (
  `ItemID` int(11) NOT NULL AUTO_INCREMENT,
  `Amount` int(11) NOT NULL,
  `CharacterId` int(11) DEFAULT NULL,
  `PropertyID` int(11) DEFAULT NULL,
  `TypeID` int(11) NOT NULL,
  `VehicleId` int(11) DEFAULT NULL,
  PRIMARY KEY (`ItemID`),
  KEY `IX_Item_CharacterId` (`CharacterId`),
  KEY `IX_Item_PropertyID` (`PropertyID`),
  KEY `IX_Item_VehicleId` (`VehicleId`),
  CONSTRAINT `FK_Item_Character_CharacterId` FOREIGN KEY (`CharacterId`) REFERENCES `character` (`Id`) ON DELETE NO ACTION,
  CONSTRAINT `FK_Item_Property_PropertyID` FOREIGN KEY (`PropertyID`) REFERENCES `property` (`PropertyID`) ON DELETE NO ACTION,
  CONSTRAINT `FK_Item_Vehicle_VehicleId` FOREIGN KEY (`VehicleId`) REFERENCES `vehicle` (`Id`) ON DELETE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of item
-- ----------------------------

-- ----------------------------
-- Table structure for `job`
-- ----------------------------
DROP TABLE IF EXISTS `job`;
CREATE TABLE `job` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `GroupId` int(11) DEFAULT NULL,
  `Level` int(11) NOT NULL,
  `PosX` float NOT NULL,
  `PosY` float NOT NULL,
  `PosZ` float NOT NULL,
  `Type` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Job_GroupId` (`GroupId`),
  CONSTRAINT `FK_Job_Group_GroupId` FOREIGN KEY (`GroupId`) REFERENCES `group` (`Id`) ON DELETE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of job
-- ----------------------------
INSERT INTO `job` VALUES ('1', '10', '1', '-874.187', '-2570.49', '13.9868', '1');
INSERT INTO `job` VALUES ('2', '7', '1', '-965.848', '-2607.39', '13.981', '9');
INSERT INTO `job` VALUES ('3', null, '1', '-1853.91', '-1229.32', '13.0173', '11');

-- ----------------------------
-- Table structure for `log`
-- ----------------------------
DROP TABLE IF EXISTS `log`;
CREATE TABLE `log` (
  `LogID` int(11) NOT NULL AUTO_INCREMENT,
  `AccountIP` varchar(16) DEFAULT NULL,
  `Id` longtext,
  `TargetIP` varchar(16) DEFAULT NULL,
  `TargetId` longtext,
  `Text` varchar(256) DEFAULT NULL,
  `TimeStamp` datetime NOT NULL,
  `Type` varchar(32) DEFAULT NULL,
  PRIMARY KEY (`LogID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of log
-- ----------------------------

-- ----------------------------
-- Table structure for `mining`
-- ----------------------------
DROP TABLE IF EXISTS `mining`;
CREATE TABLE `mining` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MiningType` int(11) NOT NULL,
  `RequiredLevel` int(11) NOT NULL,
  `XPGain` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of mining
-- ----------------------------

-- ----------------------------
-- Table structure for `npc`
-- ----------------------------
DROP TABLE IF EXISTS `npc`;
CREATE TABLE `npc` (
  `NPCId` int(11) NOT NULL AUTO_INCREMENT,
  `Model` int(11) NOT NULL,
  `PosX` float NOT NULL,
  `PosY` float NOT NULL,
  `PosZ` float NOT NULL,
  `Rot` float NOT NULL,
  `TotalLevel` int(11) NOT NULL,
  PRIMARY KEY (`NPCId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of npc
-- ----------------------------

-- ----------------------------
-- Table structure for `property`
-- ----------------------------
DROP TABLE IF EXISTS `property`;
CREATE TABLE `property` (
  `PropertyID` int(11) NOT NULL AUTO_INCREMENT,
  `CharacterId` int(11) DEFAULT NULL,
  `Enterable` bit(1) NOT NULL,
  `ExtPosX` float NOT NULL,
  `ExtPosY` float NOT NULL,
  `ExtPosZ` float NOT NULL,
  `GroupId` int(11) DEFAULT NULL,
  `IPL` varchar(48) DEFAULT NULL,
  `IntPosX` float NOT NULL,
  `IntPosY` float NOT NULL,
  `IntPosZ` float NOT NULL,
  `Name` longtext,
  `Stock` int(11) NOT NULL,
  `Type` int(11) NOT NULL,
  PRIMARY KEY (`PropertyID`),
  KEY `IX_Property_CharacterId` (`CharacterId`),
  KEY `IX_Property_GroupId` (`GroupId`),
  CONSTRAINT `FK_Property_Character_CharacterId` FOREIGN KEY (`CharacterId`) REFERENCES `character` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Property_Group_GroupId` FOREIGN KEY (`GroupId`) REFERENCES `group` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of property
-- ----------------------------
INSERT INTO `property` VALUES ('1', '2', '', '-698.663', '46.8655', '44.0338', null, 'apa_v_mp_h_01_a', '-786.866', '315.764', '217.639', 'Sammy\'s Place', '0', '1');
INSERT INTO `property` VALUES ('2', null, '', '434.054', '-981.909', '30.7094', '1', null, '-1392.67', '-480.474', '72.0422', 'Mission Row Police Station', '0', '3');
INSERT INTO `property` VALUES ('3', null, '', '-1315.37', '-390.206', '36.5453', '12', null, '0', '0', '0', 'Morningwood Gun Store', '0', '3');
INSERT INTO `property` VALUES ('4', null, '', '-963.152', '-2622.1', '13.981', '7', 'ex_dt1_02_office_01a', '-141.497', '-620.829', '168.82', 'Taxi Cab Co.', '0', '3');
INSERT INTO `property` VALUES ('5', null, '', '-1091.72', '-808.667', '19.2699', '1', null, '0', '0', '0', 'Vespucci Police Station', '0', '3');
INSERT INTO `property` VALUES ('6', null, '', '826.383', '-1289.96', '28.2407', '1', null, '0', '0', '0', 'La Mesa Police Station', '0', '3');
INSERT INTO `property` VALUES ('7', null, '', '360.771', '-1584.3', '29.292', '1', null, '0', '0', '0', 'Davis Sheriff\'s Police Station', '0', '3');
INSERT INTO `property` VALUES ('8', null, '', '-561.92', '-131.256', '38.4319', '1', null, '0', '0', '0', 'Rockford Hills Police Station', '0', '3');
INSERT INTO `property` VALUES ('9', null, '', '-660.452', '-76.6299', '38.7958', '4', null, '0', '0', '0', 'Fire Department Headquarters', '0', '3');
INSERT INTO `property` VALUES ('10', null, '', '639.437', '1.20649', '82.7864', '1', null, '0', '0', '0', 'Vinewood Police Station', '0', '3');
INSERT INTO `property` VALUES ('11', null, '', '1855.62', '3682.72', '34.2675', '1', null, '0', '0', '0', 'Sandy Shores Sheriff\'s Station', '0', '3');
INSERT INTO `property` VALUES ('12', null, '', '1839.42', '3672.66', '34.2767', '4', null, '0', '0', '0', 'Sandy Shores Medical Center', '0', '3');
INSERT INTO `property` VALUES ('13', null, '', '-442.955', '6016.91', '31.7122', '1', null, '0', '0', '0', 'Paleto Bay Police Station', '0', '3');
INSERT INTO `property` VALUES ('14', null, '', '-324.641', '6075.63', '31.2446', '13', null, '0', '0', '0', 'Paleto Bay Ammunation', '0', '3');
INSERT INTO `property` VALUES ('15', null, '', '1765.75', '2565.82', '45.565', '5', null, '1737.15', '2623.16', '45.5695', 'Prison Block #1', '0', '2');
INSERT INTO `property` VALUES ('16', null, '', '1729.35', '2562.72', '45.5649', '5', null, '1728.5', '2578.46', '45.5695', 'Prison Food Court', '0', '2');
INSERT INTO `property` VALUES ('17', null, '', '1846.08', '2586.01', '45.6721', '5', null, '0', '0', '0', 'Bolingbroke Penitentiary', '0', '3');
INSERT INTO `property` VALUES ('18', null, '', '359.919', '-585.104', '28.8194', '4', null, '0', '0', '0', 'Pillsbury Hills Medical Center', '0', '3');
INSERT INTO `property` VALUES ('19', null, '', '342.301', '-1397.51', '32.5092', '4', null, '0', '0', '0', 'Central Los Santos Medical Center', '0', '3');
INSERT INTO `property` VALUES ('20', null, '', '1151.5', '-1529.33', '35.3654', '5', null, '0', '0', '0', 'St. Fiacre Hospital', '0', '3');
INSERT INTO `property` VALUES ('21', null, '', '-675.769', '313.056', '83.0841', '4', null, '0', '0', '0', 'Eclipse Medical Tower', '0', '3');
INSERT INTO `property` VALUES ('22', null, '', '-53.3834', '-1757.24', '29.4396', '14', '', '0', '0', '0', 'Davis Gasoline Station', '0', '3');
INSERT INTO `property` VALUES ('23', null, '', '1211.29', '-1389.54', '35.3769', '15', '', '0', '0', '0', 'El Burro Gasoline Station', '0', '3');
INSERT INTO `property` VALUES ('24', null, '', '645.782', '267.513', '103.237', '16', '', '0', '0', '0', 'Vinewood Gas', '0', '3');
INSERT INTO `property` VALUES ('25', null, '', '-2974.35', '390.912', '15.0332', '17', '', '0', '0', '0', 'Banham Food Mart', '0', '3');
INSERT INTO `property` VALUES ('27', null, '', '-2963.55', '432.144', '15.2762', '1', null, '0', '0', '0', null, '0', '3');

-- ----------------------------
-- Table structure for `propertyproduct`
-- ----------------------------
DROP TABLE IF EXISTS `propertyproduct`;
CREATE TABLE `propertyproduct` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(32) DEFAULT NULL,
  `Price` int(11) NOT NULL,
  `PropertyID` int(11) NOT NULL,
  `TypeID` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_PropertyProduct_PropertyID` (`PropertyID`),
  CONSTRAINT `FK_PropertyProduct_Property_PropertyID` FOREIGN KEY (`PropertyID`) REFERENCES `property` (`PropertyID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of propertyproduct
-- ----------------------------
INSERT INTO `propertyproduct` VALUES ('1', 'Sigar', '10', '25', '1');
INSERT INTO `propertyproduct` VALUES ('2', 'Sigar', '10', '25', '1');
INSERT INTO `propertyproduct` VALUES ('3', 'Sigar', '10', '25', '1');
INSERT INTO `propertyproduct` VALUES ('4', '1', '1', '25', '1');
INSERT INTO `propertyproduct` VALUES ('5', 'Big Smoke', '1', '25', '2');
INSERT INTO `propertyproduct` VALUES ('6', 'test', '1', '25', '1');

-- ----------------------------
-- Table structure for `skillsite`
-- ----------------------------
DROP TABLE IF EXISTS `skillsite`;
CREATE TABLE `skillsite` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Dimension` int(11) NOT NULL,
  `PosY` float NOT NULL,
  `PosZ` float NOT NULL,
  `Pox` float NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of skillsite
-- ----------------------------

-- ----------------------------
-- Table structure for `vehicle`
-- ----------------------------
DROP TABLE IF EXISTS `vehicle`;
CREATE TABLE `vehicle` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CharacterId` int(11) DEFAULT NULL,
  `Color1` int(11) NOT NULL,
  `Color2` int(11) NOT NULL,
  `GroupId` int(11) DEFAULT NULL,
  `JobId` int(11) DEFAULT NULL,
  `Model` int(11) NOT NULL,
  `PosX` float NOT NULL,
  `PosY` float NOT NULL,
  `PosZ` float NOT NULL,
  `Respawnable` bit(1) NOT NULL,
  `Rot` float NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Vehicle_CharacterId` (`CharacterId`),
  KEY `IX_Vehicle_GroupId` (`GroupId`),
  KEY `IX_Vehicle_JobId` (`JobId`),
  CONSTRAINT `FK_Vehicle_Character_CharacterId` FOREIGN KEY (`CharacterId`) REFERENCES `character` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Vehicle_Group_GroupId` FOREIGN KEY (`GroupId`) REFERENCES `group` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Vehicle_Job_JobId` FOREIGN KEY (`JobId`) REFERENCES `job` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=48 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of vehicle
-- ----------------------------
INSERT INTO `vehicle` VALUES ('1', null, '88', '140', '7', null, '-956048545', '-952.833', '-2595.49', '13.848', '', '58.6362');
INSERT INTO `vehicle` VALUES ('2', null, '88', '140', '7', null, '-956048545', '-954.624', '-2598.44', '13.848', '', '59.1007');
INSERT INTO `vehicle` VALUES ('3', null, '88', '140', '7', null, '-956048545', '-956.395', '-2601.34', '13.848', '', '58.3649');
INSERT INTO `vehicle` VALUES ('4', null, '88', '140', '7', null, '-956048545', '-958.094', '-2604.15', '13.848', '', '59.4334');
INSERT INTO `vehicle` VALUES ('5', null, '88', '140', '7', null, '-956048545', '-959.874', '-2607.16', '13.9455', '', '52.7922');
INSERT INTO `vehicle` VALUES ('11', null, '27', '27', '4', null, '1938952078', '-641.318', '-77.0024', '40.1016', '', '-6.20724');
INSERT INTO `vehicle` VALUES ('12', null, '27', '27', '4', null, '1938952078', '-633.873', '-78.3704', '40.2767', '', '-4.56542');
INSERT INTO `vehicle` VALUES ('13', null, '27', '27', '4', null, '1938952078', '-625.551', '-78.5022', '40.3697', '', '-7.34117');
INSERT INTO `vehicle` VALUES ('14', null, '27', '27', '4', null, '1938952078', '-657.411', '-101.843', '37.8702', '', '120.95');
INSERT INTO `vehicle` VALUES ('15', null, '0', '111', '1', null, '2046537925', '-548.663', '-141.932', '38.2802', '', '-125.515');
INSERT INTO `vehicle` VALUES ('16', null, '111', '1', '1', null, '2046537925', '-552.146', '-143.2', '38.2244', '', '-121.242');
INSERT INTO `vehicle` VALUES ('17', null, '111', '1', '1', null, '2046537925', '-556.62', '-144.765', '38.2017', '', '-121.834');
INSERT INTO `vehicle` VALUES ('18', null, '111', '1', '1', null, '2046537925', '-560.501', '-146.586', '38.138', '', '-126.054');
INSERT INTO `vehicle` VALUES ('19', null, '111', '1', '1', null, '2046537925', '470.168', '-1024.55', '28.2024', '', '-86.1809');
INSERT INTO `vehicle` VALUES ('20', null, '111', '1', '1', null, '2046537925', '844.691', '-1352.05', '26.0856', '', '-111.216');
INSERT INTO `vehicle` VALUES ('21', null, '111', '1', '1', null, '2046537925', '844.118', '-1346.22', '26.0663', '', '-123.51');
INSERT INTO `vehicle` VALUES ('22', null, '111', '1', '1', null, '2046537925', '843.578', '-1340.29', '26.0496', '', '-118.252');
INSERT INTO `vehicle` VALUES ('23', null, '111', '1', '1', null, '-1973172295', '871.455', '-1349.99', '26.3093', '', '85.9374');
INSERT INTO `vehicle` VALUES ('24', null, '111', '1', '1', null, '-1973172295', '828.872', '-1351.54', '26.097', '', '60.7163');
INSERT INTO `vehicle` VALUES ('25', null, '111', '1', '1', null, '-1973172295', '826.812', '-1344.94', '26.097', '', '60.499');
INSERT INTO `vehicle` VALUES ('26', null, '111', '1', '1', null, '-1627000575', '828.68', '-1339.32', '26.0945', '', '62.9932');
INSERT INTO `vehicle` VALUES ('27', null, '111', '1', '1', null, '-1627000575', '833.411', '-1257.17', '26.335', '', '170.596');
INSERT INTO `vehicle` VALUES ('28', null, '111', '1', '1', null, '-1627000575', '627.184', '24.3385', '87.7861', '', '-102.633');
INSERT INTO `vehicle` VALUES ('29', null, '111', '1', '1', null, '-1627000575', '615.589', '28.6616', '89.0009', '', '-104.605');
INSERT INTO `vehicle` VALUES ('30', null, '111', '111', '1', null, '-1973172295', '1854.19', '3675.57', '33.7362', '', '-147.09');
INSERT INTO `vehicle` VALUES ('31', null, '111', '0', '1', null, '-1973172295', '1851.01', '3673.73', '33.7614', '', '-150.942');
INSERT INTO `vehicle` VALUES ('32', null, '0', '111', '1', null, '-1973172295', '1847.83', '3671.49', '33.703', '', '-153.319');
INSERT INTO `vehicle` VALUES ('33', null, '0', '111', '1', null, '-1973172295', '1861', '3679.64', '33.6805', '', '-156.434');
INSERT INTO `vehicle` VALUES ('34', null, '111', '0', '1', null, '-1683328900', '-434.642', '6030.91', '31.3405', '', '26.1246');
INSERT INTO `vehicle` VALUES ('35', null, '111', '0', '1', null, '-1683328900', '-438.765', '6028.91', '31.3405', '', '28.3675');
INSERT INTO `vehicle` VALUES ('36', null, '111', '0', '1', null, '-1683328900', '-452.382', '6049.98', '31.3405', '', '-142.779');
INSERT INTO `vehicle` VALUES ('37', null, '111', '0', '1', null, '1922257928', '-449.461', '6052.23', '31.3405', '', '-146.753');
INSERT INTO `vehicle` VALUES ('38', null, '111', '0', '1', null, '1922257928', '-445.083', '6054.1', '31.3405', '', '-145.341');
INSERT INTO `vehicle` VALUES ('39', null, '111', '1', '1', null, '353883353', '-475.239', '5988.13', '31.3367', '', '-49.6876');
INSERT INTO `vehicle` VALUES ('40', null, '61', '61', '5', null, '-2007026063', '1639.31', '2605.22', '45.5649', '', '-90.3149');
INSERT INTO `vehicle` VALUES ('41', null, '61', '61', '5', null, '-2007026063', '1638.71', '2598.66', '45.5649', '', '-91.7364');
INSERT INTO `vehicle` VALUES ('47', '2', '0', '0', null, null, '-1757836725', '-695.86', '40.5705', '43.2067', '', '109.417');

-- ----------------------------
-- Table structure for `weapon`
-- ----------------------------
DROP TABLE IF EXISTS `weapon`;
CREATE TABLE `weapon` (
  `WeaponID` int(11) NOT NULL AUTO_INCREMENT,
  `Ammo` int(11) NOT NULL,
  `ItemID` int(11) NOT NULL,
  `Model` int(11) NOT NULL,
  `Tint` int(11) NOT NULL,
  PRIMARY KEY (`WeaponID`),
  KEY `IX_Weapon_ItemID` (`ItemID`),
  CONSTRAINT `FK_Weapon_Item_ItemID` FOREIGN KEY (`ItemID`) REFERENCES `item` (`ItemID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of weapon
-- ----------------------------

-- ----------------------------
-- Table structure for `weaponcomponent`
-- ----------------------------
DROP TABLE IF EXISTS `weaponcomponent`;
CREATE TABLE `weaponcomponent` (
  `WeaponComponentID` int(11) NOT NULL AUTO_INCREMENT,
  `CompontentHash` int(11) NOT NULL,
  `WeaponID` int(11) NOT NULL,
  PRIMARY KEY (`WeaponComponentID`),
  KEY `IX_WeaponComponent_WeaponID` (`WeaponID`),
  CONSTRAINT `FK_WeaponComponent_Weapon_WeaponID` FOREIGN KEY (`WeaponID`) REFERENCES `weapon` (`WeaponID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of weaponcomponent
-- ----------------------------

-- ----------------------------
-- Table structure for `xpdata`
-- ----------------------------
DROP TABLE IF EXISTS `xpdata`;
CREATE TABLE `xpdata` (
  `Level` int(11) NOT NULL AUTO_INCREMENT,
  `XP` int(11) NOT NULL,
  PRIMARY KEY (`Level`)
) ENGINE=InnoDB AUTO_INCREMENT=127 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of xpdata
-- ----------------------------
INSERT INTO `xpdata` VALUES ('1', '0');
INSERT INTO `xpdata` VALUES ('2', '83');
INSERT INTO `xpdata` VALUES ('3', '174');
INSERT INTO `xpdata` VALUES ('4', '276');
INSERT INTO `xpdata` VALUES ('5', '388');
INSERT INTO `xpdata` VALUES ('6', '512');
INSERT INTO `xpdata` VALUES ('7', '650');
INSERT INTO `xpdata` VALUES ('8', '801');
INSERT INTO `xpdata` VALUES ('9', '969');
INSERT INTO `xpdata` VALUES ('10', '1154');
INSERT INTO `xpdata` VALUES ('11', '1358');
INSERT INTO `xpdata` VALUES ('12', '1584');
INSERT INTO `xpdata` VALUES ('13', '1833');
INSERT INTO `xpdata` VALUES ('14', '2107');
INSERT INTO `xpdata` VALUES ('15', '2411');
INSERT INTO `xpdata` VALUES ('16', '2746');
INSERT INTO `xpdata` VALUES ('17', '3115');
INSERT INTO `xpdata` VALUES ('18', '3523');
INSERT INTO `xpdata` VALUES ('19', '3973');
INSERT INTO `xpdata` VALUES ('20', '4470');
INSERT INTO `xpdata` VALUES ('21', '5018');
INSERT INTO `xpdata` VALUES ('22', '5624');
INSERT INTO `xpdata` VALUES ('23', '6291');
INSERT INTO `xpdata` VALUES ('24', '7028');
INSERT INTO `xpdata` VALUES ('25', '7842');
INSERT INTO `xpdata` VALUES ('26', '8740');
INSERT INTO `xpdata` VALUES ('27', '9730');
INSERT INTO `xpdata` VALUES ('28', '10824');
INSERT INTO `xpdata` VALUES ('29', '12031');
INSERT INTO `xpdata` VALUES ('30', '13363');
INSERT INTO `xpdata` VALUES ('31', '14833');
INSERT INTO `xpdata` VALUES ('32', '16456');
INSERT INTO `xpdata` VALUES ('33', '18247');
INSERT INTO `xpdata` VALUES ('34', '20224');
INSERT INTO `xpdata` VALUES ('35', '22406');
INSERT INTO `xpdata` VALUES ('36', '24815');
INSERT INTO `xpdata` VALUES ('37', '27473');
INSERT INTO `xpdata` VALUES ('38', '30408');
INSERT INTO `xpdata` VALUES ('39', '33648');
INSERT INTO `xpdata` VALUES ('40', '37224');
INSERT INTO `xpdata` VALUES ('41', '41171');
INSERT INTO `xpdata` VALUES ('42', '45529');
INSERT INTO `xpdata` VALUES ('43', '50339');
INSERT INTO `xpdata` VALUES ('44', '55649');
INSERT INTO `xpdata` VALUES ('45', '61512');
INSERT INTO `xpdata` VALUES ('46', '67983');
INSERT INTO `xpdata` VALUES ('47', '75127');
INSERT INTO `xpdata` VALUES ('48', '83014');
INSERT INTO `xpdata` VALUES ('49', '91721');
INSERT INTO `xpdata` VALUES ('50', '101333');
INSERT INTO `xpdata` VALUES ('51', '111945');
INSERT INTO `xpdata` VALUES ('52', '123660');
INSERT INTO `xpdata` VALUES ('53', '136594');
INSERT INTO `xpdata` VALUES ('54', '150872');
INSERT INTO `xpdata` VALUES ('55', '166636');
INSERT INTO `xpdata` VALUES ('56', '184040');
INSERT INTO `xpdata` VALUES ('57', '203254');
INSERT INTO `xpdata` VALUES ('58', '224466');
INSERT INTO `xpdata` VALUES ('59', '247886');
INSERT INTO `xpdata` VALUES ('60', '273742');
INSERT INTO `xpdata` VALUES ('61', '302288');
INSERT INTO `xpdata` VALUES ('62', '333804');
INSERT INTO `xpdata` VALUES ('63', '368599');
INSERT INTO `xpdata` VALUES ('64', '407015');
INSERT INTO `xpdata` VALUES ('65', '449428');
INSERT INTO `xpdata` VALUES ('66', '496254');
INSERT INTO `xpdata` VALUES ('67', '547953');
INSERT INTO `xpdata` VALUES ('68', '605032');
INSERT INTO `xpdata` VALUES ('69', '668051');
INSERT INTO `xpdata` VALUES ('70', '737627');
INSERT INTO `xpdata` VALUES ('71', '814445');
INSERT INTO `xpdata` VALUES ('72', '899257');
INSERT INTO `xpdata` VALUES ('73', '992895');
INSERT INTO `xpdata` VALUES ('74', '1096278');
INSERT INTO `xpdata` VALUES ('75', '1210421');
INSERT INTO `xpdata` VALUES ('76', '1336443');
INSERT INTO `xpdata` VALUES ('77', '1475581');
INSERT INTO `xpdata` VALUES ('78', '1629200');
INSERT INTO `xpdata` VALUES ('79', '1798808');
INSERT INTO `xpdata` VALUES ('80', '1986068');
INSERT INTO `xpdata` VALUES ('81', '2192818');
INSERT INTO `xpdata` VALUES ('82', '2421087');
INSERT INTO `xpdata` VALUES ('83', '2673114');
INSERT INTO `xpdata` VALUES ('84', '2951373');
INSERT INTO `xpdata` VALUES ('85', '3258594');
INSERT INTO `xpdata` VALUES ('86', '3597792');
INSERT INTO `xpdata` VALUES ('87', '3972294');
INSERT INTO `xpdata` VALUES ('88', '4385776');
INSERT INTO `xpdata` VALUES ('89', '4842295');
INSERT INTO `xpdata` VALUES ('90', '5346332');
INSERT INTO `xpdata` VALUES ('91', '5902831');
INSERT INTO `xpdata` VALUES ('92', '6517253');
INSERT INTO `xpdata` VALUES ('93', '7195629');
INSERT INTO `xpdata` VALUES ('94', '7944614');
INSERT INTO `xpdata` VALUES ('95', '8771558');
INSERT INTO `xpdata` VALUES ('96', '9684577');
INSERT INTO `xpdata` VALUES ('97', '10692629');
INSERT INTO `xpdata` VALUES ('98', '11805606');
INSERT INTO `xpdata` VALUES ('99', '13034431');
INSERT INTO `xpdata` VALUES ('100', '14391160');
INSERT INTO `xpdata` VALUES ('101', '15889109');
INSERT INTO `xpdata` VALUES ('102', '17542976');
INSERT INTO `xpdata` VALUES ('103', '19368992');
INSERT INTO `xpdata` VALUES ('104', '21385073');
INSERT INTO `xpdata` VALUES ('105', '23611006');
INSERT INTO `xpdata` VALUES ('106', '26068632');
INSERT INTO `xpdata` VALUES ('107', '28782069');
INSERT INTO `xpdata` VALUES ('108', '31777943');
INSERT INTO `xpdata` VALUES ('109', '35085654');
INSERT INTO `xpdata` VALUES ('110', '38737661');
INSERT INTO `xpdata` VALUES ('111', '42769801');
INSERT INTO `xpdata` VALUES ('112', '47221641');
INSERT INTO `xpdata` VALUES ('113', '52136869');
INSERT INTO `xpdata` VALUES ('114', '57563718');
INSERT INTO `xpdata` VALUES ('115', '63555443');
INSERT INTO `xpdata` VALUES ('116', '70170840');
INSERT INTO `xpdata` VALUES ('117', '77474828');
INSERT INTO `xpdata` VALUES ('118', '85539082');
INSERT INTO `xpdata` VALUES ('119', '94442737');
INSERT INTO `xpdata` VALUES ('120', '104273167');
INSERT INTO `xpdata` VALUES ('121', '115126838');
INSERT INTO `xpdata` VALUES ('122', '127110260');
INSERT INTO `xpdata` VALUES ('123', '140341028');
INSERT INTO `xpdata` VALUES ('124', '154948977');
INSERT INTO `xpdata` VALUES ('125', '171077457');
INSERT INTO `xpdata` VALUES ('126', '188884740');
