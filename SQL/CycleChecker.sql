CREATE OR ALTER FUNCTION CYCLE_CHECKER(@sellerId int) RETURNS int
AS
BEGIN
	declare @bossId int;
	set @bossId = (select seller_boss_id from sellers where seller_id = @sellerId);

	if @sellerId = @bossId RETURN 0

	declare @nextBossId int;

	set @nextBossId = (select seller_boss_id from sellers where seller_id = @bossId);

	declare @countdown int;

	set @countdown = (select count(*) from sellers) + 1;

	while @nextBossId != @bossId
	BEGIN
		if @countdown < 0 RETURN 1;
		set @countdown = @countdown - 1;
		if @nextBossId = @sellerId RETURN 1;
		set @bossId = @nextBossId;
		set @nextBossId = (select seller_boss_id from sellers where seller_id = @bossId);
	END

	RETURN 0
END
