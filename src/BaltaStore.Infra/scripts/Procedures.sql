CREATE PROCEDURE spCheckDocument
	@Document CHAR(11)
AS
	SELECT CASE WHEN EXISTS (
		SELECT [Id]
		FROM [Customer]
		WHERE [Document] = @Document
	)
	THEN CAST(1 AS BIT)
	ELSE CAST(0 AS BIT) 
END

GO

CREATE PROCEDURE spCheckEmail
	@Email VARCHAR(160)
AS
	SELECT CASE WHEN EXISTS (
		SELECT [Id]
		FROM [Customer]
		WHERE [Email] = @Email
	)
	THEN CAST(1 AS BIT)
	ELSE CAST(0 AS BIT) 
END

GO

CREATE PROCEDURE spCreateAddress
    @Id UNIQUEIDENTIFIER,
	@CustomerId UNIQUEIDENTIFIER,
	@Number VARCHAR(10),
	@Complement VARCHAR(40),
	@District VARCHAR(60),
	@City VARCHAR(60),
	@State CHAR(2),
	@Country CHAR(2),
	@ZipCode CHAR(8),
	@Type INT
AS
    INSERT INTO [Address] (
        [Id],
        [CustomerId],
        [Number],
        [Complement],
        [District],
        [City],
        [State],
        [Country],
        [ZipCode],
        [Type]
    ) VALUES (
        @Id,
        @CustomerId,
        @Number,
        @Complement,
        @District,
        @City,
        @State,
        @Country,
        @ZipCode,
        @Type
    )

GO

CREATE PROCEDURE spCreateCustomer
    @Id UNIQUEIDENTIFIER,
    @FirstName VARCHAR(40),
    @LastName VARCHAR(40),
    @Document CHAR(11),
    @Email VARCHAR(160),
    @Phone VARCHAR(13)
AS
    INSERT INTO [Customer] (
        [Id], 
        [FirstName], 
        [LastName], 
        [Document], 
        [Email], 
        [Phone]
    ) VALUES (
        @Id,
        @FirstName,
        @LastName,
        @Document,
        @Email,
        @Phone
    )