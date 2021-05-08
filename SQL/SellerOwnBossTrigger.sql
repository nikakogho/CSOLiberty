CREATE OR ALTER TRIGGER SELLER_OWN_BOSS_TRIGGER ON Sellers AFTER Insert AS 
BEGIN 
	IF EXISTS (SELECT * FROM Sellers WHERE seller_boss_id IS NULL) 
	BEGIN
		UPDATE Sellers SET seller_boss_id = seller_id WHERE seller_boss_id IS NULL
	END
END
GO
