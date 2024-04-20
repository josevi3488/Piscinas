/************************************************************

USUARIOS

*************************************************************/

--INSERTAR USUARIOS
CREATE PROC SP_REGISTRARUSUARIO(
@Usuario varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Clave varchar(50),
@IdTRol int,
@Estado bit,
@IdUsuarioResultado int output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @IdUsuarioResultado = 0
	SET @Mensaje = ''


	IF NOT EXISTS (SELECT * FROM USUARIOS WHERE Usuario = @Usuario) 
	BEGIN
		INSERT INTO USUARIOS (Usuario,NombreCompleto,Correo,Clave,IdTRol,Estado) VALUES
		(@Usuario,@NombreCompleto,@Correo,@Clave,@IdTRol,@Estado)

		SET @IdUsuarioResultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'No se puede repetir el usuario'
END

--Prueba de creacion de usuarios
declare @IdUsuarioResultado int
declare @Mensaje varchar(500)
exec SP_REGISTRARUSUARIO 'prueba','prueba','prueba@gmail.com','123',1,1,@IdUsuarioResultado output,@Mensaje output
select @IdUsuarioResultado
select @Mensaje


GO

--EDITAR USUARIOS
CREATE PROC SP_EDITARUSUARIO(
@IdTUsuario int,
@Usuario varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Clave varchar(50),
@IdTRol int,
@Estado bit,
@Respuesta bit output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''


	IF NOT EXISTS (SELECT * FROM USUARIOS WHERE Usuario = @Usuario and IdTUsuario != @IdTUsuario) 
	BEGIN
		UPDATE USUARIOS SET
		Usuario = @Usuario,
		NombreCompleto = @NombreCompleto,
		Correo = @Correo,
		Clave = @Clave,
		IdTRol = @IdTRol,
		Estado = @Estado
		WHERE IdTUsuario = @IdTUsuario
		
		SET @Respuesta = 1
	END
	ELSE
		SET @Mensaje = 'No se puede repetir el usuario'
END

GO

-- ELIMINAR USUARIOS
CREATE PROC SP_ELIMINARUSUARIO(
@IdTUsuario int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''
	declare @correcto bit = 1

	IF EXISTS (SELECT * FROM INVENTARIO I
	INNER JOIN USUARIOS U ON U.IdTUsuario = I.IdTUsuario
	WHERE U.IdTUsuario = @IdTUsuario)

	BEGIN
		SET @correcto = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'Hay registros de inventario relacionados con este usuario. No se puede eliminar, en su lugar puede inactivarlo\n' 
	END

	IF (@correcto = 1)
	BEGIN
		DELETE FROM USUARIOS WHERE IdTUsuario = @IdTUsuario
		SET @Respuesta = 1
	END

END



GO




/************************************************************

CATEGORÍAS

*************************************************************/
GO

--INSERTAR CATEGORIA
CREATE PROC SP_REGISTRARCATEGORIA(
@Descripcion varchar(100),
@Estado bit,
@IdCategoriaResultado int output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @IdCategoriaResultado = 0
	SET @Mensaje = ''


	IF NOT EXISTS (SELECT * FROM CATEGORIA_PRODUCTOS WHERE Descripcion = @Descripcion) 
	BEGIN
		INSERT INTO CATEGORIA_PRODUCTOS(Descripcion,Estado) VALUES
		(@Descripcion,@Estado)

		SET @IdCategoriaResultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'No se puede repetir la categoria'
END

GO

--Prueba de creacion de categorías
declare @IdCategoriaResultado int
declare @Mensaje varchar(500)
exec SP_REGISTRARCATEGORIA'Accesorios',1,@IdCategoriaResultado output,@Mensaje output
exec SP_REGISTRARCATEGORIA'Articulos de limpieza',1,@IdCategoriaResultado output,@Mensaje output
exec SP_REGISTRARCATEGORIA'Bombas',1,@IdCategoriaResultado output,@Mensaje output
exec SP_REGISTRARCATEGORIA'Calefacción',1,@IdCategoriaResultado output,@Mensaje output
exec SP_REGISTRARCATEGORIA'Escalera',1,@IdCategoriaResultado output,@Mensaje output
exec SP_REGISTRARCATEGORIA'Espumaderas (Skimmer)',1,@IdCategoriaResultado output,@Mensaje output
exec SP_REGISTRARCATEGORIA'Filtros',1,@IdCategoriaResultado output,@Mensaje output
exec SP_REGISTRARCATEGORIA'Iluminación',1,@IdCategoriaResultado output,@Mensaje output
exec SP_REGISTRARCATEGORIA'Productos Químicos',1,@IdCategoriaResultado output,@Mensaje output
select @IdCategoriaResultado
select @Mensaje

select * from CATEGORIA_PRODUCTOS

GO

--EDITAR CATEGORIAS
CREATE PROC SP_EDITARCATEGORIA(
@IdTCategoria int,
@Descripcion varchar(50),
@Respuesta bit output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''


	IF NOT EXISTS (SELECT * FROM CATEGORIA_PRODUCTOS WHERE Descripcion = @Descripcion and @IdTCategoria != @IdTCategoria) 
	BEGIN
		UPDATE CATEGORIA_PRODUCTOS SET
		Descripcion = @Descripcion
		WHERE IdTCategoria = @IdTCategoria
		
		SET @Respuesta = 1
	END
	ELSE
		SET @Mensaje = 'No se puede repetir la descripcion de la categoria'
END

GO

-- ELIMINAR CATEGORIAS
CREATE PROC SP_ELIMINARCATEGORIA(
@IdTCategoria int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''
	declare @correcto bit = 1

	IF EXISTS (SELECT * FROM CATEGORIA_PRODUCTOS CP
	INNER JOIN PRODUCTOS P ON P.IdTCategoria = CP.IdTCategoria
	WHERE CP.IdTCategoria = @IdTCategoria)

	BEGIN
		SET @correcto = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'Hay registros de productos relacionados con esta categoría. No se puede eliminar\n' 
	END

	IF (@correcto = 1)
	BEGIN
		DELETE TOP (1) FROM CATEGORIA_PRODUCTOS WHERE IdTCategoria = @IdTCategoria
		SET @Respuesta = 1
	END

END



GO







/************************************************************

PRODUCTOS

*************************************************************/


GO

--REGISTRAR PRODUCTO
CREATE PROC SP_REGISTRARPRODUCTO(
@CodigoProducto varchar(10),
@NombreProducto varchar(100),
@IdTCategoria int,
@StockMinimo decimal(10,2),
@UltimoPrecioCompra decimal(10,2),
@CodigoBarra varchar(20),
@Estado bit,
@IdProductoResultado int output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @IdProductoResultado = 0
	SET @Mensaje = ''


	IF NOT EXISTS (SELECT * FROM PRODUCTOS WHERE CodigoProducto = @CodigoProducto) 
	BEGIN
		INSERT INTO PRODUCTOS(CodigoProducto,NombreProducto,IdTCategoria,StockMinimo,UltimoPrecioCompra,CodigoBarra,Estado) VALUES
		(@CodigoProducto,@NombreProducto,@IdTCategoria,@StockMinimo,@UltimoPrecioCompra,@CodigoBarra,@Estado)

		SET @IdProductoResultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'No se puede repetir el producto'
END

GO

--Prueba de creacion de PRODUCTOS
declare @IdProductoResultado int
declare @Mensaje varchar(500)
exec SP_REGISTRARPRODUCTO 'PT00000001','Dispensador de cloro flotante',1,20,0,'',1,@IdProductoResultado output,@Mensaje output
exec SP_REGISTRARPRODUCTO 'PT00000002','Aspiradora de piscina',2,20,0,'',1,@IdProductoResultado output,@Mensaje output
exec SP_REGISTRARPRODUCTO 'PT00000003','Cepillo para piscina',2,20,0,'',1,@IdProductoResultado output,@Mensaje output
exec SP_REGISTRARPRODUCTO 'PT00000004','Limpiafondos automático',2,20,0,'',1,@IdProductoResultado output,@Mensaje output
exec SP_REGISTRARPRODUCTO 'PT00000005','Manguera de aspiradora de piscina',2,20,0,'',1,@IdProductoResultado output,@Mensaje output
exec SP_REGISTRARPRODUCTO 'PT00000006','Red para hojas y escombros',2,20,0,'',1,@IdProductoResultado output,@Mensaje output
exec SP_REGISTRARPRODUCTO 'PT00000007','Robot limpiador de piscina',2,20,0,'',1,@IdProductoResultado output,@Mensaje output
exec SP_REGISTRARPRODUCTO 'PT00000008','Bomba de calor',3,20,0,'',1,@IdProductoResultado output,@Mensaje output
exec SP_REGISTRARPRODUCTO 'PT00000009','Bomba de piscina',3,20,0,'',1,@IdProductoResultado output,@Mensaje output
exec SP_REGISTRARPRODUCTO 'PT00000010','Calentador de piscina',4,20,0,'',1,@IdProductoResultado output,@Mensaje output
exec SP_REGISTRARPRODUCTO 'PT00000011','Escalera de piscina',5,20,0,'',1,@IdProductoResultado output,@Mensaje output
exec SP_REGISTRARPRODUCTO 'PT00000012','Skimmer de superficie',6,20,0,'',1,@IdProductoResultado output,@Mensaje output
exec SP_REGISTRARPRODUCTO 'PT00000013','Filtro de piscina',7,20,0,'',1,@IdProductoResultado output,@Mensaje output
exec SP_REGISTRARPRODUCTO 'PT00000014','Luz de piscina',8,20,0,'',1,@IdProductoResultado output,@Mensaje output
exec SP_REGISTRARPRODUCTO 'PT00000015','Alguicida',9,20,0,'',1,@IdProductoResultado output,@Mensaje output
exec SP_REGISTRARPRODUCTO 'PT00000016','Cloro en tabletas',9,20,0,'',1,@IdProductoResultado output,@Mensaje output
exec SP_REGISTRARPRODUCTO 'PT00000017','Cloro líquido',9,20,0,'',1,@IdProductoResultado output,@Mensaje output
exec SP_REGISTRARPRODUCTO 'PT00000018','Desinfectante para piscinas',9,20,0,'',1,@IdProductoResultado output,@Mensaje output
exec SP_REGISTRARPRODUCTO 'PT00000019','Floculante',9,20,0,'',1,@IdProductoResultado output,@Mensaje output
exec SP_REGISTRARPRODUCTO 'PT00000020','Kit de prueba de agua',9,20,0,'',1,@IdProductoResultado output,@Mensaje output
select @IdProductoResultado
select @Mensaje

select * from PRODUCTOS


GO

--EDITAR PRODUCTOS
CREATE PROC SP_EDITARPRODUCTOS(
@IdTProducto int,
@CodigoProducto varchar(10),
@NombreProducto varchar(100),
@IdTCategoria int,
@StockMinimo decimal(10,2),
@UltimoPrecioCompra decimal(10,2),
@CodigoBarra varchar(20),
@Estado bit,
@Respuesta bit output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''


	IF NOT EXISTS (SELECT * FROM PRODUCTOS WHERE NombreProducto = @NombreProducto and @IdTProducto != @IdTProducto) 
	BEGIN
		UPDATE PRODUCTOS SET
		NombreProducto = @NombreProducto,
		IdTCategoria = @IdTCategoria,
		StockMinimo =@StockMinimo,
		UltimoPrecioCompra = @UltimoPrecioCompra,
		CodigoBarra = @CodigoBarra,
		Estado = @Estado
		WHERE @IdTProducto = @IdTProducto
		
		SET @Respuesta = 1
	END
	ELSE
		SET @Mensaje = 'No se puede repetir la descripcion de la categoria'
END

GO

-- ELIMINAR PRODUCTOS
CREATE PROC SP_ELIMINARPRODUCTOS(
@IdTProducto int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''
	declare @correcto bit = 1

	IF EXISTS (SELECT * FROM INVENTARIO I
	INNER JOIN PRODUCTOS P ON P.IdTProducto = I.IdTProducto
	WHERE I.IdTProducto = @IdTProducto)

	BEGIN
		SET @correcto = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'Hay registros de productos relacionados con el inventario. No se puede eliminar\n' 
	END

	IF (@correcto = 1)
	BEGIN
		DELETE TOP (1) FROM PRODUCTOS WHERE IdTProducto = @IdTProducto
		SET @Respuesta = 1
	END

END



GO

/************************************************************

ALMACENES

*************************************************************/

--REGISTRAR ALMACENES
CREATE PROC SP_REGISTRARALMACENES(
@CodigoAlmacen varchar(10),
@Descripcion varchar(50),
@Estado bit,
@IdAlmacenResultado int output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @IdAlmacenResultado = 0
	SET @Mensaje = ''

	IF NOT EXISTS (SELECT * FROM ALMACENES WHERE CodigoAlmacen = @CodigoAlmacen) 
	BEGIN
		INSERT INTO ALMACENES(CodigoAlmacen,Descripcion,Estado) VALUES
		(@CodigoAlmacen,@Descripcion,@Estado)

		SET @IdAlmacenResultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'No se puede repetir el almacén'
END

GO

--Prueba de creacion de ALMACENES
declare @IdAlmacenResultado int
declare @Mensaje varchar(500)
exec SP_REGISTRARALMACENES 'ALM-PT001','ALMACEN PRODUCTOS TERMINADOS',1,@IdAlmacenResultado output,@Mensaje output
exec SP_REGISTRARALMACENES 'ALM-DES001','ALMACEN PRODUCTOS PARA DESTRUCCION',1,@IdAlmacenResultado output,@Mensaje output
select @IdAlmacenResultado
select @Mensaje

select * from ALMACENES

GO



--EDITAR ALMACENES
CREATE PROC SP_EDITARALMACENES(
@IdTAlmacen int,
@CodigoAlmacen varchar(10),
@Descripcion varchar(50),
@Estado bit,
@Respuesta bit output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''


	IF NOT EXISTS (SELECT * FROM ALMACENES WHERE Descripcion = @Descripcion and IdTAlmacen != @IdTAlmacen) 
	BEGIN
		UPDATE ALMACENES SET
		Descripcion = @Descripcion,
		Estado = @Estado
		WHERE IdTAlmacen = @IdTAlmacen
		
		SET @Respuesta = 1
	END
	ELSE
		SET @Mensaje = 'No se puede repetir la descripcion del almacen'
END


GO

-- ELIMINAR ALMACENES
CREATE PROC SP_ELIMINARALMACENES(
@IdTAlmacen int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''
	declare @correcto bit = 1

	IF EXISTS (SELECT A.IdTAlmacen,A.CodigoAlmacen,A.Descripcion,A.Estado FROM ALMACENES A
	INNER JOIN INVENTARIO I ON  A.IdTAlmacen=I.IdTAlmacen
	WHERE A.IdTAlmacen = @IdTAlmacen)

	

	BEGIN
		SET @correcto = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'Hay registros de productos relacionados con el inventario. No se puede eliminar\n' 
	END

	IF (@correcto = 1)
	BEGIN
		DELETE TOP (1) FROM ALMACENES WHERE IdTAlmacen = @IdTAlmacen
		SET @Respuesta = 1
	END

END




GO





/************************************************************

TIPO DE MOVIMIENTOS

*************************************************************/

--REGISTRAR TIPOS DE MOVIMIENTOS
CREATE PROC SP_REGISTRARTIPOSMOVIMIENTOS(
@Descripcion varchar(100),
@Movimiento char(1),
@IdTMovimientoResultado int output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @IdTMovimientoResultado = 0
	SET @Mensaje = ''

	IF NOT EXISTS (SELECT * FROM TIPOS_MOVIMIENTOS WHERE Descripcion = @Descripcion) 
	BEGIN
		INSERT INTO TIPOS_MOVIMIENTOS(Descripcion,Movimiento) VALUES
		(@Descripcion,@Movimiento)

		SET @IdTMovimientoResultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'No se puede repetir el tipo de movimiento'
END

GO

--Prueba de creacion de tipos de movimientos
declare @IdTMovimientoResultado int
declare @Mensaje varchar(500)
exec SP_REGISTRARTIPOSMOVIMIENTOS 'AJUSTE POSITIVO','E', @IdTMovimientoResultado output,@Mensaje output
exec SP_REGISTRARTIPOSMOVIMIENTOS 'AJUSTE NEGATIVO','S', @IdTMovimientoResultado output,@Mensaje output
exec SP_REGISTRARTIPOSMOVIMIENTOS 'COMPRA','E', @IdTMovimientoResultado output,@Mensaje output
exec SP_REGISTRARTIPOSMOVIMIENTOS 'CONSUMO','S', @IdTMovimientoResultado output,@Mensaje output
select @IdTMovimientoResultado
select @Mensaje

select * from TIPOS_MOVIMIENTOS

GO


--EDITAR TIPOS DE MOVIMIENTOS
CREATE PROC SP_EDITARTIPOSMOVIMIENTOS(
@IdTTipoMov int,
@Descripcion varchar(10),
@Movimiento char(1),
@Respuesta bit output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''


	IF NOT EXISTS (SELECT * FROM TIPOS_MOVIMIENTOS WHERE Descripcion = @Descripcion and IdTTipoMov != @IdTTipoMov) 
	BEGIN
		UPDATE TIPOS_MOVIMIENTOS SET
		Descripcion = @Descripcion,
		Movimiento = @Movimiento
		WHERE IdTTipoMov = @IdTTipoMov
		
		SET @Respuesta = 1
	END
	ELSE
		SET @Mensaje = 'No se puede repetir el tipo de movimiento'
END


GO



-- ELIMINAR TIPOS DE MOVIMIENTOS
CREATE PROC SP_ELIMINARTIPOSMOVIMIENTOS(
@IdTTipoMov int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''
	declare @correcto bit = 1

	IF EXISTS (SELECT TP.IdTTipoMov,TP.Descripcion,TP.Movimiento FROM TIPOS_MOVIMIENTOS TP
	INNER JOIN INVENTARIO I ON  I.IdTTipoMov=TP.IdTTipoMov
	WHERE TP.IdTTipoMov =  @IdTTipoMov)

	BEGIN
		SET @correcto = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'Hay registros de inventario relacionados con este tipo de movimiento. No se puede eliminar\n' 
	END

	IF (@correcto = 1)
	BEGIN
		DELETE TOP (1) FROM TIPOS_MOVIMIENTOS WHERE IdTTipoMov = @IdTTipoMov
		SET @Respuesta = 1
	END

END


GO









/************************************************************

INVENTARIOS

*************************************************************/


--REGISTRAR INVENTARIOS

CREATE PROC SP_REGISTRARINVENTARIO(
@IdTAlmacen int,
@IdTTipoMov int,
@IdTProducto int,
@Cantidad decimal(10,2),
@RefDocumento varchar(20),
@IdTUsuario int,
@IdTLoteProducto int,
@IdInventarioResultado int output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @IdInventarioResultado = 0
	SET @Mensaje = ''


	IF NOT EXISTS (SELECT * FROM INVENTARIO WHERE RefDocumento = @RefDocumento) 
	BEGIN
		INSERT INTO INVENTARIO(IdTAlmacen,IdTTipoMov,IdTProducto,Cantidad,RefDocumento,IdTUsuario,IdTLoteProducto) VALUES
		(@IdTAlmacen,@IdTTipoMov,@IdTProducto,@Cantidad,@RefDocumento,@IdTUsuario,@IdTLoteProducto)

		SET @IdInventarioResultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'El documento de referencia ya existe'
END







/************************************************************

LOTES

*************************************************************/



--REGISTRAR LOTES DE PRODUCTOS
alter PROC SP_REGISTRARLOTES(
@IdTProducto int,
@Lote varchar(10),
@FechaFabricacion date,
@FechaVencimiento date,
@IdLoteResultado int output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @IdLoteResultado = 0
	SET @Mensaje = ''

	IF NOT EXISTS (SELECT * FROM LOTE_PRODUCTO WHERE IdTProducto = @IdTProducto and lote =@Lote) 
	BEGIN
		INSERT INTO LOTE_PRODUCTO(IdTProducto,Lote,FechaFabricacion,FechaVencimiento) VALUES
		(@IdTProducto,@Lote,@FechaFabricacion,@FechaVencimiento)

		SET @IdLoteResultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'El lote para el producto ya existe'
END

GO


--Prueba de creacion de LOTES
declare @IdLoteResultado int
declare @Mensaje varchar(500)
exec SP_REGISTRARLOTES 1,'L-PT01','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output
exec SP_REGISTRARLOTES 2,'L-PT02','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output
exec SP_REGISTRARLOTES 3,'L-PT03','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output
exec SP_REGISTRARLOTES 4,'L-PT04','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output
exec SP_REGISTRARLOTES 5,'L-PT05','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output
exec SP_REGISTRARLOTES 6,'L-PT06','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output
exec SP_REGISTRARLOTES 7,'L-PT07','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output
exec SP_REGISTRARLOTES 8,'L-PT08','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output
exec SP_REGISTRARLOTES 9,'L-PT09','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output
exec SP_REGISTRARLOTES 10,'L-PT10','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output
exec SP_REGISTRARLOTES 11,'L-PT11','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output
exec SP_REGISTRARLOTES 12,'L-PT12','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output
exec SP_REGISTRARLOTES 13,'L-PT13','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output
exec SP_REGISTRARLOTES 14,'L-PT14','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output
exec SP_REGISTRARLOTES 15,'L-PT15','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output
exec SP_REGISTRARLOTES 16,'L-PT16','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output
exec SP_REGISTRARLOTES 17,'L-PT17','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output
exec SP_REGISTRARLOTES 18,'L-PT18','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output
exec SP_REGISTRARLOTES 19,'L-PT19','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output
exec SP_REGISTRARLOTES 20,'L-PT20','2023-10-01','2026-10-01', @IdLoteResultado output,@Mensaje output

select @IdLoteResultado
select @Mensaje

select * from LOTE_PRODUCTO




GO


--EDITAR LOTE DE PRODUCTOS
CREATE PROC SP_EDITARLOTES(
@IdTLoteProducto int,
@IdTProducto int,
@Lote varchar(10),
@FechaFabricacion date,
@FechaVencimiento date,
@Respuesta bit output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''


	IF NOT EXISTS (SELECT * FROM LOTE_PRODUCTO WHERE Lote = @Lote and IdTLoteProducto != @IdTLoteProducto) 
	BEGIN
		UPDATE LOTE_PRODUCTO SET
		FechaFabricacion = @FechaFabricacion,
		FechaVencimiento = @FechaVencimiento
		WHERE IdTLoteProducto = @IdTLoteProducto
		
		SET @Respuesta = 1
	END
	ELSE
		SET @Mensaje = 'No se puede repetir el lote'
END


GO

-- ELIMINAR LOTES
CREATE PROC SP_ELIMINARLOTES(
@IdTLoteProducto int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''
	declare @correcto bit = 1

	IF EXISTS (SELECT LP.IdTLoteProducto,LP.Lote,LP.IdTProducto,LP.FechaFabricacion,LP.FechaVencimiento FROM LOTE_PRODUCTO LP
	INNER JOIN PRODUCTOS P ON P.IdTProducto = LP.IdTProducto
	WHERE LP.IdTLoteProducto = @IdTLoteProducto)

	

	BEGIN
		SET @correcto = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'Hay registros de productos relacionados con este lote. No se puede eliminar\n' 
	END

	IF (@correcto = 1)
	BEGIN
		DELETE TOP (1) FROM LOTE_PRODUCTO WHERE IdTLoteProducto = @IdTLoteProducto
		SET @Respuesta = 1
	END

END


GO





/******************************** CONSULTAS SQL ******************************************************/

--STOCK

SELECT A.CodigoProducto [Código Producto],A.Producto,A.Almacen,A.StockMinimo,SUM(A.Stock) Disponible
FROM
(
SELECT	P.CodigoProducto CodigoProducto,P.NombreProducto Producto,A.CodigoAlmacen + ' - ' + A.Descripcion Almacen,P.StockMinimo StockMinimo,CASE WHEN TM.Movimiento = 'E' THEN Cantidad	WHEN TM.Movimiento = 'S' THEN Cantidad *(-1) END Stock
FROM INVENTARIO I
LEFT OUTER JOIN ALMACENES A ON A.IdTAlmacen = I.IdTAlmacen
LEFT OUTER JOIN TIPOS_MOVIMIENTOS TM ON TM.IdTTipoMov = I.IdTTipoMov
LEFT OUTER JOIN PRODUCTOS P ON P.IdTProducto = I.IdTProducto
LEFT OUTER JOIN CATEGORIA_PRODUCTOS CP ON P.IdTCategoria = CP.IdTCategoria
LEFT OUTER JOIN USUARIOS U ON U.IdTUsuario = I.IdTUsuario
LEFT OUTER JOIN LOTE_PRODUCTO LP ON LP.IdTLoteProducto = I.IdTLoteProducto
) A 
GROUP BY A.CodigoProducto,A.Producto,A.Almacen,A.StockMinimo


SELECT A.CodigoProducto,A.Producto,A.Almacen,A.StockMinimo,A.Stock,CASE WHEN A.StockMinimo >= A.Stock THEN 'Reposición' ELSE '' END Accion
FROM
(
SELECT P.CodigoProducto, P.NombreProducto Producto,P.StockMinimo, ISNULL(A.CodigoAlmacen + ' - ' + A.Descripcion,'') Almacen,SUM(ISNULL(CASE WHEN TM.Movimiento = 'E' THEN Cantidad	WHEN TM.Movimiento = 'S' THEN Cantidad *(-1) END,0)) Stock
FROM PRODUCTOS P 
LEFT OUTER JOIN INVENTARIO I ON P.IdTProducto = I.IdTProducto
LEFT OUTER JOIN ALMACENES A ON A.IdTAlmacen = I.IdTAlmacen
LEFT OUTER JOIN TIPOS_MOVIMIENTOS TM ON TM.IdTTipoMov =I.IdTTipoMov
WHERE P.StockMinimo > 0
GROUP BY P.CodigoProducto, P.NombreProducto,P.StockMinimo,A.CodigoAlmacen,A.Descripcion
) A 

GO

-- PROCEDIMIENTO ALMACENADO PARA CONSULTA DE REPORTES

CREATE PROC SP_REPORTEINVENTARIO(
@Producto int
)
AS 
BEGIN
	SELECT A.IdTProducto, A.CodigoProducto,A.Producto,A.Almacen,A.StockMinimo,A.Stock,CASE WHEN A.StockMinimo >= A.Stock THEN 'Reposición' ELSE '' END Accion
	FROM
	(
		SELECT P.IdTProducto, P.CodigoProducto, P.NombreProducto Producto,P.StockMinimo, ISNULL(A.CodigoAlmacen + ' - ' + A.Descripcion,'') Almacen,SUM(ISNULL(CASE WHEN TM.Movimiento = 'E' THEN Cantidad	WHEN TM.Movimiento = 'S' THEN Cantidad *(-1) END,0)) Stock
		FROM PRODUCTOS P 
		LEFT OUTER JOIN INVENTARIO I ON P.IdTProducto = I.IdTProducto
		LEFT OUTER JOIN ALMACENES A ON A.IdTAlmacen = I.IdTAlmacen
		LEFT OUTER JOIN TIPOS_MOVIMIENTOS TM ON TM.IdTTipoMov =I.IdTTipoMov
		WHERE P.StockMinimo > 0
		GROUP BY P.IdTProducto,P.CodigoProducto, P.NombreProducto,P.StockMinimo,A.CodigoAlmacen,A.Descripcion
	) A
	WHERE A.IdTProducto = iif(@Producto=0,A.IdTProducto,@Producto) 
END

EXEC SP_REPORTEINVENTARIO 0 





/************************************************************
ROLES
*************************************************************/
--REGISTRAR 
CREATE PROC SP_REGISTRARROLES(
@IdTRol int,
@Descripcion varchar(50),
@IdRolResultado int output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @IdRolResultado = 0
	SET @Mensaje = ''

	IF NOT EXISTS (SELECT * FROM ROLES WHERE Descripcion = @Descripcion) 
	BEGIN
		INSERT INTO ROLES(IdTRol,Descripcion) VALUES
		(@IdTRol,@Descripcion)

		SET @IdRolResultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'El Rol ya existe'
END

GO


--EDITAR 
CREATE PROC SP_EDITARROLES(
@IdTRol int,
@Descripcion varchar(50),
@Respuesta bit output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''

	IF NOT EXISTS (SELECT * FROM ROLES WHERE Descripcion = @Descripcion and IdTRol != @IdTRol) 
	BEGIN
		UPDATE ROLES SET
		Descripcion = @Descripcion
		WHERE IdTRol = @IdTRol
		
		SET @Respuesta = 1
	END
	ELSE
		SET @Mensaje = 'No se puede repetir el rol'
END


GO

-- ELIMINAR
CREATE PROC SP_ELIMINARROLES(
@IdTRol int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''
	declare @correcto bit = 1

	IF EXISTS (
		SELECT R.Descripcion,U.Usuario FROM ROLES R 
		INNER JOIN USUARIOS U ON U.IdTRol = R.IdTRol		WHERE R.IdTRol=@IdTRol
	)
	
	BEGIN
		SET @correcto = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'Hay registros de usuarios relacionados con este rol. No se puede eliminar\n' 
	END

	IF (@correcto = 1)
	BEGIN
		DELETE TOP (1) FROM ROLES WHERE IdTRol = @IdTRol
		SET @Respuesta = 1
	END
END

GO


/************************************************************
PERMISOS
*************************************************************/


CREATE PROC SP_REGISTRARPERMISOS(
@IdTPermiso int,
@IdTRol int,
@NombreMenu varchar(100),
@IdResultado int output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @IdResultado = 0
	SET @Mensaje = ''

	IF NOT EXISTS (SELECT * FROM PERMISOS WHERE IdTRol=@IdTRol and NombreMenu = @NombreMenu) 
	BEGIN
		INSERT INTO PERMISOS(IdTPermiso,IdTRol,NombreMenu) VALUES
		(@IdTPermiso,@IdTRol,@NombreMenu)

		SET @IdResultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'El Permiso ya existe'
END

GO


--EDITAR 
CREATE PROC SP_EDITARPERMISOS(
@IdTPermiso int,
@IdTRol int,
@NombreMenu varchar(100),
@Respuesta bit output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''

	IF NOT EXISTS (SELECT * FROM PERMISOS WHERE NombreMenu = @NombreMenu and IdTPermiso != @IdTPermiso) 
	BEGIN
		UPDATE PERMISOS SET
		NombreMenu = @NombreMenu
		WHERE IdTPermiso = @IdTPermiso
		
		SET @Respuesta = 1
	END
	ELSE
		SET @Mensaje = 'No se puede repetir el permiso'
END


GO

-- ELIMINAR
CREATE PROC SP_ELIMINARPERMISOS(
@IdTPermiso int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje = ''
	declare @correcto bit = 1

	IF EXISTS (
		SELECT * FROM PERMISOS PE
		INNER JOIN ROLES R ON PE.IdTRol=R.IdTRol
		WHERE PE.IdTPermiso = @IdTPermiso
	)
	
	BEGIN
		SET @correcto = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'Hay registros de roles relacionados con este permiso. No se puede eliminar\n' 
	END

	IF (@correcto = 1)
	BEGIN
		DELETE TOP (1) FROM PERMISOS WHERE IdTPermiso = @IdTPermiso
		SET @Respuesta = 1
	END
END

GO