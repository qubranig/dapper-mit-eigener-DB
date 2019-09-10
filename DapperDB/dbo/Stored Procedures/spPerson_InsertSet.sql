-- add (on folder) - storage Procedures
CREATE PROCEDURE [dbo].[spPerson_InsertSet] -- name von der storageProcedure
	@people BasicUDT readonly  -- variable die man händisch eingibt Parameter
	-- UDT ist User Defined Table Type - datentyp hier angelegt in der DapperDB Mappe
AS
BEGIN  -- was sie macht, starte...
	INSERT INTO dbo.Person(FirstName, LastName)
	SELECT [FirstName], [LastName]
	FROM @people;
end
