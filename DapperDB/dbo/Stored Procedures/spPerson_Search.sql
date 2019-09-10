-- add (on folder) - storage Procedures
CREATE PROCEDURE [dbo].[spPerson_Search] -- name von der storageProcedure
	@searchTerm VARCHAR(50) -- variable die man händisch eingibt Parameter
AS
begin -- was sie macht, starte...

-- Bewirkt, dass die Meldung bezüglich der Anzahl der von einer Transact-SQL-Anweisung oder gespeicherten Prozedur betroffenen Zeilen nicht mehr als Teil des Resultsets zurückgegeben wird.-- https://docs.microsoft.com/de-de/sql/t-sql/statements/set-nocount-transact-sql?view=sql-server-2017

set nocount on; -- kann eine erhebliche Leistungssteigerung bewirken, da der Netzwerkverkehr stark reduziert wird.

	select [Id], [FirstName], [LastName] -- der SQL Befehl...
	from dbo.Person
	WHERE FirstName LIKE '%' + @searchTerm + '%' 
		OR LastName LIKE '%' + @searchTerm + '%';
end
