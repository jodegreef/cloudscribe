
SET TERM ^ ;
ALTER PROCEDURE MP_ROLES_INSERT (
    SITEID Integer,
    ROLENAME Varchar(50),
    DISPLAYNAME Varchar(50),
    SITEGUID Char(36),
    ROLEGUID Char(36) )
RETURNS (
    ROLEID Integer )
AS
BEGIN
 ROLEID = NEXT VALUE FOR mp_Roles_seq;

INSERT INTO 	MP_ROLES
(				
                ROLEID,
                SITEID,
                ROLENAME,
                DISPLAYNAME,
		SITEGUID,
                ROLEGUID
) 
VALUES 
(				
               :ROLEID,
               :SITEID,
               :ROLENAME,
               :DISPLAYNAME,
	       :SITEGUID,
               :ROLEGUID
);

END^
SET TERM ; ^

GRANT EXECUTE
 ON PROCEDURE MP_ROLES_INSERT TO SYSDBA;




SET TERM ^ ;
ALTER PROCEDURE MP_SCHEMASCRIPTHISTORY_INSERT (
    APPLICATIONID Char(36),
    SCRIPTFILE Varchar(255),
    "RUNTIME" Timestamp,
    ERROROCCURRED Smallint,
    ERRORMESSAGE Blob sub_type 1,
    SCRIPTBODY Blob sub_type 1 )
RETURNS (
    ID Integer )
AS
BEGIN
 ID = NEXT VALUE FOR mp_SchemaScriptHistory_seq;

INSERT INTO 	MP_SCHEMASCRIPTHISTORY
(				
                ID,
                APPLICATIONID,
                SCRIPTFILE,
                RUNTIME,
                ERROROCCURRED,
                ERRORMESSAGE,
                SCRIPTBODY
) 
VALUES 
(				
               :ID,
               :APPLICATIONID,
               :SCRIPTFILE,
               :RUNTIME,
               :ERROROCCURRED,
               :ERRORMESSAGE,
               :SCRIPTBODY
);

END^
SET TERM ; ^

GRANT EXECUTE
 ON PROCEDURE MP_SCHEMASCRIPTHISTORY_INSERT TO SYSDBA;




SET TERM ^ ;
ALTER PROCEDURE MP_SITES_INSERT (
    SITEGUID Char(36),
    SITENAME Varchar(255),
    SKIN Varchar(100),
    LOGO Varchar(50),
    ICON Varchar(50),
    ALLOWUSERSKINS Smallint,
    ALLOWPAGESKINS Smallint,
    ALLOWHIDEMENUONPAGES Smallint,
    ALLOWNEWREGISTRATION Smallint,
    USESECUREREGISTRATION Smallint,
    USESSLONALLPAGES Smallint,
    DEFAULTPAGEKEYWORDS Varchar(255),
    DEFAULTPAGEDESCRIPTION Varchar(255),
    DEFAULTPAGEENCODING Varchar(255),
    DEFAULTADDITIONALMETATAGS Varchar(255),
    ISSERVERADMINSITE Smallint,
    USELDAPAUTH Smallint,
    AUTOCREATELDAPUSERONFIRSTLOGIN Smallint,
    LDAPSERVER Varchar(255),
    LDAPPORT Integer,
    LDAPDOMAIN Varchar(255),
    LDAPROOTDN Varchar(255),
    LDAPUSERDNKEY Varchar(10),
    REALLYDELETEUSERS Smallint,
    USEEMAILFORLOGIN Smallint,
    ALLOWUSERFULLNAMECHANGE Smallint,
    EDITORSKIN Varchar(50),
    DEFAULTFRIENDLYURLPATTERNENUM Varchar(50),
    ALLOWPASSWORDRETRIEVAL Smallint,
    ALLOWPASSWORDRESET Smallint,
    REQUIRESQUESTIONANDANSWER Smallint,
    MAXINVALIDPASSWORDATTEMPTS Integer,
    PASSWORDATTEMPTWINDOWMINUTES Integer,
    REQUIRESUNIQUEEMAIL Smallint,
    PASSWORDFORMAT Integer,
    MINREQUIREDPASSWORDLENGTH Integer,
    MINREQNONALPHACHARS Integer,
    PWDSTRENGTHREGEX Blob sub_type 1,
    DEFAULTEMAILFROMADDRESS Varchar(100),
    ENABLEMYPAGEFEATURE Smallint,
    EDITORPROVIDER Varchar(255),
    DATEPICKERPROVIDER Varchar(255),
    CAPTCHAPROVIDER Varchar(255),
    RECAPTCHAPRIVATEKEY Varchar(255),
    RECAPTCHAPUBLICKEY Varchar(255),
    WORDPRESSAPIKEY Varchar(255),
    WINDOWSLIVEAPPID Varchar(255),
    WINDOWSLIVEKEY Varchar(255),
    ALLOWOPENIDAUTH Smallint,
    ALLOWWINDOWSLIVEAUTH Smallint,
    GMAPAPIKEY Varchar(255),
    APIKEYEXTRA1 Varchar(255),
    APIKEYEXTRA2 Varchar(255),
    APIKEYEXTRA3 Varchar(255),
    APIKEYEXTRA4 Varchar(255),
    APIKEYEXTRA5 Varchar(255),
    DISABLEDBAUTH smallint )
RETURNS (
    SITEID Integer )
AS
BEGIN
 SITEID = NEXT VALUE FOR mp_Sites_seq;

INSERT INTO 	MP_SITES
(				
                SITEID,
                SITEGUID,
                SITENAME,
                SKIN,
                LOGO,
                ICON,
                ALLOWUSERSKINS,
                ALLOWPAGESKINS,
                ALLOWHIDEMENUONPAGES,
                ALLOWNEWREGISTRATION,
                USESECUREREGISTRATION,
                USESSLONALLPAGES,
                DEFAULTPAGEKEYWORDS,
                DEFAULTPAGEDESCRIPTION,
                DEFAULTPAGEENCODING,
                DEFAULTADDITIONALMETATAGS,
                ISSERVERADMINSITE,
                USELDAPAUTH,
                AUTOCREATELDAPUSERONFIRSTLOGIN,
                LDAPSERVER,
                LDAPPORT,
                LDAPDOMAIN,
                LDAPROOTDN,
                LDAPUSERDNKEY,
                REALLYDELETEUSERS,
                USEEMAILFORLOGIN,
                ALLOWUSERFULLNAMECHANGE,
                EDITORSKIN,
                DEFAULTFRIENDLYURLPATTERNENUM,
                ALLOWPASSWORDRETRIEVAL,
                ALLOWPASSWORDRESET,
                REQUIRESQUESTIONANDANSWER,
                MAXINVALIDPASSWORDATTEMPTS,
                PASSWORDATTEMPTWINDOWMINUTES,
                REQUIRESUNIQUEEMAIL,
                PASSWORDFORMAT,
                MINREQUIREDPASSWORDLENGTH,
                MINREQNONALPHACHARS,
                PWDSTRENGTHREGEX,
                DEFAULTEMAILFROMADDRESS,
                ENABLEMYPAGEFEATURE,
                EDITORPROVIDER,
                DATEPICKERPROVIDER,
                CAPTCHAPROVIDER,
                RECAPTCHAPRIVATEKEY,
                RECAPTCHAPUBLICKEY,
                WORDPRESSAPIKEY,
                WINDOWSLIVEAPPID,
                WINDOWSLIVEKEY,
                ALLOWOPENIDAUTH,
                ALLOWWINDOWSLIVEAUTH,
		GMAPAPIKEY,
		APIKEYEXTRA1,
		APIKEYEXTRA2,
		APIKEYEXTRA3,
		APIKEYEXTRA4,
                APIKEYEXTRA5,
				DisableDbAuth
) 
VALUES 
(				
               :SITEID,
               :SITEGUID,
               :SITENAME,
               :SKIN,
               :LOGO,
               :ICON,
               :ALLOWUSERSKINS,
               :ALLOWPAGESKINS,
               :ALLOWHIDEMENUONPAGES,
               :ALLOWNEWREGISTRATION,
               :USESECUREREGISTRATION,
               :USESSLONALLPAGES,
               :DEFAULTPAGEKEYWORDS,
               :DEFAULTPAGEDESCRIPTION,
               :DEFAULTPAGEENCODING,
               :DEFAULTADDITIONALMETATAGS,
               :ISSERVERADMINSITE,
               :USELDAPAUTH,
               :AUTOCREATELDAPUSERONFIRSTLOGIN,
               :LDAPSERVER,
               :LDAPPORT,
               :LDAPDOMAIN,
               :LDAPROOTDN,
               :LDAPUSERDNKEY,
               :REALLYDELETEUSERS,
               :USEEMAILFORLOGIN,
               :ALLOWUSERFULLNAMECHANGE,
               :EDITORSKIN,
               :DEFAULTFRIENDLYURLPATTERNENUM,
               :ALLOWPASSWORDRETRIEVAL,
               :ALLOWPASSWORDRESET,
               :REQUIRESQUESTIONANDANSWER,
               :MAXINVALIDPASSWORDATTEMPTS,
               :PASSWORDATTEMPTWINDOWMINUTES,
               :REQUIRESUNIQUEEMAIL,
               :PASSWORDFORMAT,
               :MINREQUIREDPASSWORDLENGTH,
               :MINREQNONALPHACHARS,
               :PWDSTRENGTHREGEX,
               :DEFAULTEMAILFROMADDRESS,
               :ENABLEMYPAGEFEATURE,
               :EDITORPROVIDER,
                :DATEPICKERPROVIDER,
                :CAPTCHAPROVIDER,
                :RECAPTCHAPRIVATEKEY,
                :RECAPTCHAPUBLICKEY,
                :WORDPRESSAPIKEY,
                :WINDOWSLIVEAPPID,
                :WINDOWSLIVEKEY,
                :ALLOWOPENIDAUTH,
                :ALLOWWINDOWSLIVEAUTH,
	       :GMAPAPIKEY,
	       :APIKEYEXTRA1,
	       :APIKEYEXTRA2,
	       :APIKEYEXTRA3,
	       :APIKEYEXTRA4,
               :APIKEYEXTRA5,
			   :DisableDbAuth
);

END^
SET TERM ; ^

GRANT EXECUTE
 ON PROCEDURE MP_SITES_INSERT TO SYSDBA;


SET TERM ^ ;
ALTER PROCEDURE MP_USERROLES_INSERT (
    USERID Integer,
    ROLEID Integer,
    USERGUID Char(36),
    ROLEGUID Char(36) )
RETURNS (
    ID Integer )
AS
BEGIN
 ID = NEXT VALUE FOR mp_UserRoles_seq;

INSERT INTO 	MP_USERROLES
(				
                ID,
                USERID,
                ROLEID,
                USERGUID,
                ROLEGUID
) 
VALUES 
(				
               :ID,
               :USERID,
               :ROLEID,
               :USERGUID,
               :ROLEGUID
);

END^
SET TERM ; ^

GRANT EXECUTE
 ON PROCEDURE MP_USERROLES_INSERT TO SYSDBA;


SET TERM ^ ;
ALTER PROCEDURE MP_USERS_INSERT (
    SITEID Integer,
    NAME Varchar(100),
    LOGINNAME Varchar(50),
    EMAIL Varchar(100),
    LOWEREDEMAIL Varchar(100),
    "PASSWORD" Varchar(128),
    PASSWORDQUESTION Varchar(255),
    PASSWORDANSWER Varchar(255),
    GENDER Char(10),
    PROFILEAPPROVED Smallint,
    REGISTERCONFIRMGUID Char(36),
    APPROVEDFORFORUMS Smallint,
    TRUSTED Smallint,
    DISPLAYINMEMBERLIST Smallint,
    WEBSITEURL Varchar(100),
    COUNTRY Varchar(100),
    STATE Varchar(100),
    OCCUPATION Varchar(100),
    INTERESTS Varchar(100),
    MSN Varchar(50),
    YAHOO Varchar(50),
    AIM Varchar(50),
    ICQ Varchar(50),
    TOTALPOSTS Integer,
    AVATARURL Varchar(255),
    TIMEOFFSETHOURS Integer,
    SIGNATURE Varchar(255),
    DATECREATED Timestamp,
    USERGUID Char(36),
    SKIN Varchar(100),
    ISDELETED Smallint,
    FAILEDPASSWORDATTEMPTCOUNT Integer,
    FAILEDPWDANSWERATTEMPTCOUNT Integer,
    ISLOCKEDOUT Smallint,
    MOBILEPIN Varchar(16),
    PASSWORDSALT Varchar(128),
    "COMMENT" Blob sub_type 1,
    SITEGUID Char(36),
    MUSTCHANGEPWD integer,
    FIRSTNAME varchar(100),
    LASTNAME varchar(100),
    EMAILCHANGEGUID char(36),
    TIMEZONEID varchar(32),
    DATEOFBIRTH timestamp,
    PWDFORMAT integer,
    EMAILCONFIRMED integer,
    PASSWORDHASH blob sub_type 1,
    SECURITYSTAMP blob sub_type 1,
    PHONENUMBER varchar(50),
    PHONENUMBERCONFIRMED integer,
    TWOFACTORENABLED integer,
    LOCKOUTENDDATEUTC timestamp	)
RETURNS (
    USERID Integer )
AS
BEGIN
 USERID = NEXT VALUE FOR mp_Users_seq;

INSERT INTO 	MP_USERS
(				
                USERID,
                SITEID,
                NAME,
                LOGINNAME,
                EMAIL,
                LOWEREDEMAIL,
                "PASSWORD",
                PASSWORDQUESTION,
                PASSWORDANSWER,
                GENDER,
                PROFILEAPPROVED,
                REGISTERCONFIRMGUID,
                APPROVEDFORFORUMS,
                TRUSTED,
                DISPLAYINMEMBERLIST,
                WEBSITEURL,
                COUNTRY,
                STATE,
                OCCUPATION,
                INTERESTS,
                MSN,
                YAHOO,
                AIM,
                ICQ,
                TOTALPOSTS,
                AVATARURL,
                TIMEOFFSETHOURS,
                SIGNATURE,
                DATECREATED,
                USERGUID,
                SKIN,
                ISDELETED,
                FAILEDPASSWORDATTEMPTCOUNT,
                FAILEDPWDANSWERATTEMPTCOUNT,
                ISLOCKEDOUT,
                MOBILEPIN,
                PASSWORDSALT,
                COMMENT,
                SITEGUID,
				TOTALREVENUE,
				MustChangePwd,
				FIRSTNAME,
				LASTNAME,
				EMAILCHANGEGUID,
				TIMEZONEID,
				PasswordResetGuid,
				RolesChanged,
				DateOfBirth,
				EmailConfirmed,
				PwdFormat,
				PasswordHash,
				SecurityStamp,
				PhoneNumber,
				PhoneNumberConfirmed,
				TwoFactorEnabled,
				LockoutEndDateUtc
) 
VALUES 
(				
               :USERID,
               :SITEID,
               :NAME,
               :LOGINNAME,
               :EMAIL,
               :LOWEREDEMAIL,
               :"PASSWORD",
               :PASSWORDQUESTION,
               :PASSWORDANSWER,
               :GENDER,
               :PROFILEAPPROVED,
               :REGISTERCONFIRMGUID,
               :APPROVEDFORFORUMS,
               :TRUSTED,
               :DISPLAYINMEMBERLIST,
               :WEBSITEURL,
               :COUNTRY,
               :STATE,
               :OCCUPATION,
               :INTERESTS,
               :MSN,
               :YAHOO,
               :AIM,
               :ICQ,
               :TOTALPOSTS,
               :AVATARURL,
               :TIMEOFFSETHOURS,
               :SIGNATURE,
               :DATECREATED,
               :USERGUID,
               :SKIN,
               :ISDELETED,
               :FAILEDPASSWORDATTEMPTCOUNT,
               :FAILEDPWDANSWERATTEMPTCOUNT,
               :ISLOCKEDOUT,
               :MOBILEPIN,
               :PASSWORDSALT,
               :COMMENT,
               :SITEGUID,
			   0,
				:MustChangePwd,
				:FIRSTNAME,
				:LASTNAME,
				:EMAILCHANGEGUID,
				:TIMEZONEID,
				'00000000-0000-0000-0000-000000000000',
				0,
				:DateOfBirth,
				:EmailConfirmed,
				:PwdFormat,
				:PasswordHash,
				:SecurityStamp,
				:PhoneNumber,
				:PhoneNumberConfirmed,
				:TwoFactorEnabled,
				:LockoutEndDateUtc
				
				
);

END^
SET TERM ; ^

GRANT EXECUTE
 ON PROCEDURE MP_USERS_INSERT TO SYSDBA;



GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MP_CURRENCY TO SYSDBA WITH GRANT OPTION;

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MP_GEOCOUNTRY TO SYSDBA WITH GRANT OPTION;

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MP_GEOZONE TO SYSDBA WITH GRANT OPTION;


GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MP_LANGUAGE TO SYSDBA WITH GRANT OPTION;


ALTER TABLE MP_ROLES ADD CONSTRAINT FK_ROLES_SITES
  FOREIGN KEY (SITEID) REFERENCES MP_SITES (SITEID);
CREATE INDEX IDX_ROLES_SITEID ON MP_ROLES (SITEID);
GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MP_ROLES TO SYSDBA WITH GRANT OPTION;


ALTER TABLE MP_SCHEMASCRIPTHISTORY ADD CONSTRAINT FK_SCHEMASCRIPTHISTORY_SV
  FOREIGN KEY (APPLICATIONID) REFERENCES MP_SCHEMAVERSION (APPLICATIONID);
CREATE INDEX IDX_SCHEMASCRIPTHX_APPID ON MP_SCHEMASCRIPTHISTORY (APPLICATIONID);
GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MP_SCHEMASCRIPTHISTORY TO SYSDBA WITH GRANT OPTION;

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MP_SCHEMAVERSION TO SYSDBA WITH GRANT OPTION;


GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MP_SITEFOLDERS TO SYSDBA WITH GRANT OPTION;

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MP_SITEHOSTS TO SYSDBA WITH GRANT OPTION;


GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MP_SITES TO SYSDBA WITH GRANT OPTION;

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MP_SITESETTINGSEX TO SYSDBA WITH GRANT OPTION;

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MP_SITESETTINGSEXDEF TO SYSDBA WITH GRANT OPTION;


GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MP_USERLOCATION TO SYSDBA WITH GRANT OPTION;

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MP_USERPROPERTIES TO SYSDBA WITH GRANT OPTION;

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MP_USERROLES TO SYSDBA WITH GRANT OPTION;

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MP_USERS TO SYSDBA WITH GRANT OPTION;

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MP_SYSTEMLOG TO  SYSDBA WITH GRANT OPTION;
 
 GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MP_USERCLAIMS TO  SYSDBA WITH GRANT OPTION;
 
 GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MP_USERLOGINS TO  SYSDBA WITH GRANT OPTION;
 
 SET TERM ^ ;
CREATE PROCEDURE MP_SYSTEMLOG_INSERT (
    LOGDATE timestamp,
    IPADDRESS varchar(50),
    CULTURE varchar(10),
    URL blob sub_type 1,
    SHORTURL varchar(255),
    THREAD varchar(255),
    LOGLEVEL varchar(20),
    LOGGER varchar(255),
    "MESSAGE" blob sub_type 1 )
RETURNS (
    ID integer )
AS
BEGIN
 ID = NEXT VALUE FOR mp_SystemLog_seq;

INSERT INTO 	MP_SYSTEMLOG
(				
                ID,
                LOGDATE,
                IPADDRESS,
                CULTURE,
                URL,
                SHORTURL,
                THREAD,
                LOGLEVEL,
                LOGGER,
                MESSAGE
) 
VALUES 
(				
               :ID,
               :LOGDATE,
               :IPADDRESS,
               :CULTURE,
               :URL,
               :SHORTURL,
               :THREAD,
               :LOGLEVEL,
               :LOGGER,
               :MESSAGE
);

END^
SET TERM ; ^

GRANT EXECUTE
 ON PROCEDURE MP_SYSTEMLOG_INSERT TO  SYSDBA;
 
 SET TERM ^ ;
CREATE PROCEDURE MP_USERCLAIMS_INSERT (
    USERID varchar(128),
    CLAIMTYPE blob sub_type 1,
    CLAIMVALUE blob sub_type 1 )
RETURNS (
    ID integer )
AS
BEGIN
 ID = NEXT VALUE FOR mp_UserClaims_seq;

INSERT INTO 	mp_USERCLAIMS
(				
                ID,
                USERID,
                CLAIMTYPE,
                CLAIMVALUE
) 
VALUES 
(				
               :ID,
               :USERID,
               :CLAIMTYPE,
               :CLAIMVALUE
);

END^
SET TERM ; ^

GRANT EXECUTE
 ON PROCEDURE MP_USERCLAIMS_INSERT TO  SYSDBA;
 