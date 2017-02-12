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
-- Table structure for `character`
-- ----------------------------
DROP TABLE IF EXISTS `character`;
CREATE TABLE `character` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `AccountId` varchar(127) DEFAULT NULL,
  `ActiveGroupID` int(11) NOT NULL,
  `Admin` int(11) NOT NULL,
  `Bank` int(11) NOT NULL,
  `Cash` int(11) NOT NULL,
  `JobId` int(11) NOT NULL,
  `LastLoginDate` datetime NOT NULL,
  `Level` int(11) NOT NULL,
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
  PRIMARY KEY (`Id`),
  KEY `IX_Character_AccountId` (`AccountId`),
  CONSTRAINT `FK_Character_AspNetUsers_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `aspnetusers` (`Id`) ON DELETE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for `groupdivision`
-- ----------------------------
DROP TABLE IF EXISTS `groupdivision`;
CREATE TABLE `groupdivision` (
  `DivisionID` int(11) NOT NULL AUTO_INCREMENT,
  `GroupID` int(11) NOT NULL,
  `Name` longtext,
  PRIMARY KEY (`DivisionID`),
  KEY `IX_GroupDivision_GroupID` (`GroupID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
-- Table structure for `grouprank`
-- ----------------------------
DROP TABLE IF EXISTS `grouprank`;
CREATE TABLE `grouprank` (
  `RankID` int(11) NOT NULL AUTO_INCREMENT,
  `GroupID` int(11) NOT NULL,
  `Name` longtext,
  `RankNumber` int(11) NOT NULL,
  PRIMARY KEY (`RankID`),
  KEY `IX_GroupRank_GroupID` (`GroupID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;