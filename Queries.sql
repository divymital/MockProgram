create table t_sales
(
    SalesID int primary key,
	salesmanName varchar(50),
	SalesDate datetime,
	SalesValue decimal(10,3),
	Location varchar(100)
)

Go

CREATE procedure sp_SalesCRUD
@CrudType int = null,
@SalesmanName varchar(50) = null,
@SalesDate datetime = null ,
@SalesValue decimal(10,3) = null,
@Location varchar(100) = null

As 
Begin

if @CrudType = 1
Begin
	-- Select
	select SalesmanName,count(*) as Sales from t_sales group by salesmanName
End
if @CrudType = 2
Begin
	-- Insert
	Declare @SalesID int
	select @SalesID = max(SalesID) from t_sales
	if @SalesID is null
	   Set @SalesID = 1
	else
	Begin
		Set @SalesID = @SalesID + 1
	ENd
	insert into t_sales values(@SalesID,@SalesmanName,@SalesDate,@SalesValue,@Location)
End

END

