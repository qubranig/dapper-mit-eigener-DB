using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient; // für die Sql Connection - Connection String
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper; //Dapper über NuGet (Pakete (Projekt...)) besorgen
using HelperLibrary.Models; //datenstruktur der db in cs code erfassen als class
using static HelperLibrary.Tools; //der connection string steht da drinnen

// 1. SQL Server Data Tools zum erstellen einer lokalen DB Instanz
// => https://www.youtube.com/watch?v=ijDcHGxyqE4 (ähnlich/gleich  LocalDB)
// => DB erzeugung direkt aus einen skript heraus

//deutsches tut https://www.infoworld.com/article/3025784/how-to-use-the-dapper-orm-in-c.html
// engl video https://www.youtube.com/watch?v=eKkh5Xm0OlU

namespace Demo1_BasicDapper
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicRead(); //einfache abfrage - holt sich daten...

            // der rest ist aus einem dapper tutorial:
            //BasicWithoutModel();
            //ReadOnlyPart();
            //ReadWithParameters("Smith");
            //ReadWithAnonymousParameters("Corey");
            //BasicWrite("Penny", "Brown");
            //ReadWithStoredProcedure("or");
            //GetWriteCount();
            //WriteSet(GetPeople());
            Console.ReadLine();
        }

        private static void BasicRead()
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString())) //das using hier gillt nur für den eingegrenzten bereich...
            { // die funktion die den connection string holt ist in einer extra Libary (/Projekt)
                string sql = "SELECT * FROM dbo.tbl_test1"; //deine query

                //auf den connectionString eine Abfrage starten... (übergib die abfrage...)
                var people = cnn.Query<tbl_test1>(sql); //es muss eine liste sein da mehrere datensätze gefunden werden ...

                foreach (var datensatz in people) //durchlaufe die liste und gib alles auf die konsole aus...
                {
                    Console.WriteLine($"testID\ttest_spalte1\ttest_spalte2\ttest_spalte_date\n{ datensatz.test_ID }\t{ datensatz.test_spalte1 }\t{datensatz.test_spalte2 }\t{datensatz.test_spalte_date }"); 
                    //der macht automatisch ein \n durch den sql string
                }
            } // bedenke das die datenstruktur der tbl als class definiert sein muss
        }

        private static void BasicWithoutModel()
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                string sql = "select * from dbo.Person";

                var people = cnn.Query(sql);

                foreach (var person in people)
                {
                    Console.WriteLine($"{ person.FirstName } { person.LastName }");
                }
            }
        }

        private static void ReadOnlyPart()
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                string sql = "select FirstName from dbo.Person";

                var people = cnn.Query<tbl_test1>(sql);

                foreach (var person in people)
                {
                    //Console.WriteLine($"{ person.FirstName } { person.LastName }");
                }
            }
        }

        private static void ReadWithParameters(string lastName)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                var p = new DynamicParameters();
                p.Add("@LastName", lastName);

                string sql = "select * from dbo.Person where LastName = @LastName";

                var people = cnn.Query<tbl_test1>(sql, p);

                foreach (var person in people)
                {
                   // Console.WriteLine($"{ person.FirstName } { person.LastName }");
                }
            }
        }

        private static void ReadWithAnonymousParameters(string lastName)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                var p = new
                {
                    LastName = lastName
                };

                string sql = "select * from dbo.Person where LastName = @LastName";

                var people = cnn.Query<tbl_test1>(sql, p);

                foreach (var person in people)
                {
                  // Console.WriteLine($"{ person.FirstName } { person.LastName }");
                }
            }
        }

        private static void ReadWithStoredProcedure(string searchTerm)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                var p = new DynamicParameters();
                p.Add("@SearchTerm", searchTerm);

                string sql = "dbo.spPerson_Search";

                var people = cnn.Query<tbl_test1>(sql, p, 
                    commandType: CommandType.StoredProcedure);

                foreach (var person in people)
                {
                  //  Console.WriteLine($"{ person.FirstName } { person.LastName }");
                }
            }
        }

        private static void BasicWrite(string firstName, string lastName)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", firstName);
                p.Add("@LastName", lastName);

                string sql = $@"insert into dbo.Person (FirstName, LastName) 
                                values (@FirstName, @LastName)";

                cnn.Execute(sql, p);

                ReadWithParameters(lastName);
            }
        }

        private static void GetWriteCount()
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                string sql = @"update dbo.Person
                               set LastName = UPPER(LastName);";

                var rowsAffected = cnn.Execute(sql);

                Console.WriteLine($"Rows Affected: { rowsAffected }");
            }
        }

        private static void WriteSet(List<tbl_test1> people)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                string sql = $@"insert into dbo.Person (FirstName, LastName) 
                                values (@FirstName, @LastName)";

                cnn.Execute(sql, people);

                BasicRead();
            }
        }

        private static List<tbl_test1> GetPeople()
        {
            var output = new List<tbl_test1>();

          //  output.Add(new PersonModel { FirstName = "Mary", LastName = "Kilborn" });
          //  output.Add(new PersonModel { FirstName = "Wayne", LastName = "Decker" });
          //  output.Add(new PersonModel { FirstName = "Beth", LastName = "Tasker" });
          //  output.Add(new PersonModel { FirstName = "Luke", LastName = "Riker" });
          //  output.Add(new PersonModel { FirstName = "Owen", LastName = "Parker" });

            return output;
        }
    }
}
