using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteORM
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                /*SQLiteConnector.CreateDatabaseSource("test.db");
                SQLiteConnector.Connection.Open();
                string createStudentTableQuery = "CREATE TABLE IF NOT EXISTS students (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, fio VARCHAR(128) NOT NULL, age INTEGER )";
                    SQLiteCommand  sQLiteCommand= new SQLiteCommand(createStudentTableQuery, SQLiteConnector.Connection);
                sQLiteCommand.ExecuteNonQuery();
                SQLiteConnector.Connection.Close();*/

                string pathTofile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "database", "test.db");
                SQLiteDBEngine dBEngine = new SQLiteDBEngine(pathTofile, SQLIteMode.EXISTS);
                //foreach (var item in dBEngine.Tables)
                //{
                //    Console.WriteLine(item);
                //}
 //------------------------------------------------------------------------------------------------------ mycode  start
                SQLiteRow row = dBEngine.getTableFields("student");// передаем название  таблицы student в параметры функции getTableFields, 
                // которая дает полную информацию про столбцы таблицы student
                Console.WriteLine(row);
 //---------------------------------------------------------------------------------------------- mycode  finish





            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
