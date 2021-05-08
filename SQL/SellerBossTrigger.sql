CREATE OR ALTER TRIGGER SELLER_BOSS_TRIGGER ON Sellers AFTER Insert, Update AS 
BEGIN 
    UPDATE Sellers SET seller_boss_id = seller_id WHERE seller_boss_id IS NULL

    IF EXISTS (SELECT * FROM Sellers as S WHERE dbo.CYCLE_CHECKER(S.seller_id) = 1)
	BEGIN
		RAISERROR('Seller bosses can not form a cycle!', 16, 1);
	END
END
GO