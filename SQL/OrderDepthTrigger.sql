CREATE OR ALTER TRIGGER ORDER_DEPTH_TRIGGER ON Orders AFTER Insert, Update AS
BEGIN
	DECLARE @parents TABLE (id int)

	INSERT INTO @parents
	SELECT order_parent_id as id 
	FROM Orders WHERE order_parent_id IS NOT NULL;

	IF EXISTS (SELECT * FROM @parents as P JOIN Orders as O on O.order_id = P.id WHERE O.order_parent_id IS NOT NULL)
	BEGIN
		RAISERROR('Order hierarchy cannot be deeper than 2 levels!', 16, 1);
	END
END
GO