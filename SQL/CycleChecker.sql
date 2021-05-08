CREATE FUNCTION CYCLE_CHECKER(@sellerId int) RETURNS int
AS
BEGIN
	declare @bossId int;
	set @bossId = (select seller_boss_id from sellers where seller_id = @sellerId);

	if @sellerId = @bossId RETURN 0

	declare @nextBossId int;

	set @nextBossId = (select seller_boss_id from sellers where seller_id = @bossId);

	while @nextBossId != @bossId
	BEGIN
		if @nextBossId = @sellerId RETURN 1
		set @bossId = @nextBossId;
		set @nextBossId = (select seller_boss_id from sellers where seller_id = @bossId);
	END

	RETURN 0
END