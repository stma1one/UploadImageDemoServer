Create Database ProfileDemoDB
Go

use profileDemoDB;


Create Table Users (
	Email nvarchar(100) PRIMARY KEY,
	[Password] nvarchar(20) NOT NULL,
	PhoneNumber nvarchar(20) NULL,
	BirthDate DATETIME NULL,
	[Status] int NULL,
	[Name] nvarchar(50) NOT NULL,
	[Image] nvarchar(250)

)
Go

INSERT INTO dbo.Users VALUES ('talsi@talsi.com', '1234', '+972506665835','20-may-1976',1,'Tal Simon',null)
Go

--scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=ProfileDemoDB;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Encrypt=False;Trust Server Certificate=False;Command Timeout=0" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context LoginDemoDbContext -DataAnnotations -force