USE [StoreDB]
GO

/****** Object:  Table [dbo].[VentasDetalle]    Script Date: 2/08/2022 19:20:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE [dbo].[Productos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
	[Stock] [int] NOT NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Ventas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](5) NOT NULL,
	[Cliente] [varchar](100) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Ventas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[VentasDetalle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductoId] [int] NOT NULL,
	[VentaId] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_VentasDetalle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

GO

CREATE PROCEDURE [dbo].[ObtenerTodosProductos]
AS
BEGIN
	SELECT Id, 
	       Descripcion, 
		   Stock
	FROM Productos
END

GO

CREATE PROCEDURE [dbo].[ObtenerProductoXId]
	@id int
AS
BEGIN
	SELECT Id, 
	       Descripcion, 
		   Stock
	FROM Productos
	WHERE Id = @id
END

GO

CREATE PROCEDURE [dbo].[InsertarProducto]
	@descripcion varchar(100),
	@stock int,
	@id int OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Productos(Descripcion, Stock)
	VALUES (@descripcion, @stock)

	SELECT @id = SCOPE_IDENTITY()
END

GO

CREATE PROCEDURE [dbo].[ActualizarProducto]
    @id int,
	@descripcion varchar(100),
	@stock int
AS
BEGIN
	UPDATE Productos
	SET Descripcion = @descripcion, 
		Stock = @stock
	WHERE Id = @id 
END

GO

CREATE PROCEDURE [dbo].[EliminarProducto]
    @id int
AS
BEGIN
	DELETE FROM Productos
	WHERE Id = @id
END

GO

CREATE PROCEDURE [dbo].[InsertarVenta]
	@codigo varchar(5),
	@cliente varchar(100),
	@fecha datetime,
	@total decimal(18,2),
	@id int OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Ventas(Codigo, Cliente, Fecha, Total)
	VALUES (@codigo, @cliente, @fecha, @total)

	SELECT @id = SCOPE_IDENTITY()
END

GO

CREATE PROCEDURE [dbo].[InsertarVentaDetalle]
	@productoId int,
	@ventaId int,
	@cantidad int,
	@precio decimal(18,2),
	@total decimal(18,2),
	@id int OUTPUT
AS
BEGIN

	INSERT INTO VentasDetalle(ProductoId, VentaId, Cantidad, Precio, Total)
	VALUES (@productoId, @ventaId, @cantidad, @precio, @total)

	SELECT @id = SCOPE_IDENTITY()
END