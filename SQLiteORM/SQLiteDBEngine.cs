using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteORM
{
    class SQLiteDBEngine
    {
        private List<string> _tables;
        public SQLiteDBEngine(string dbPath, SQLIteMode mode)
        {
            _tables = new List<string>();
            switch (mode)
            {
                case SQLIteMode.EXISTS:
                    {
                        SQLiteConnector.PathToDataBase = dbPath;
                        init();
                        break;
                    }
                case SQLIteMode.NEW:
                    {
                        SQLiteConnector.CreateDatabaseSource(dbPath);
                        //развернуть дб по заранее заложенному алгоритму
                        break;
                    }
            }
        }

        private void init()
        {
            getTableNamesExists();
        }

        private void getTableNamesExists()
        {
            string queryGetTablesName = "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%'";
            SQLiteConnector.Connection.Open();
            SQLiteCommand sQLiteCommand = new SQLiteCommand(queryGetTablesName, SQLiteConnector.Connection);
            SQLiteDataReader qLiteDataReader = sQLiteCommand.ExecuteReader();
            foreach (DbDataRecord rows in qLiteDataReader)
            {
                _tables.Add(rows.GetString(0));
            }
            SQLiteConnector.Connection.Close();
        }
        //public void getTableFields(string table)
        //{
        //    if (DoesTableExists(table))
        //    {
        //        string queryGetTableFields = $"PRAGMA table_info('{table}')";
        //        SQLiteConnector.Connection.Open();
        //        SQLiteCommand sQLiteCommand = new SQLiteCommand(queryGetTableFields, SQLiteConnector.Connection);
        //        SQLiteDataReader qLiteDataReader = sQLiteCommand.ExecuteReader();
        //        foreach (DbDataRecord rows in qLiteDataReader)
        //        {
        //            for (int i = 0; i < rows.FieldCount; i++)
        //            {
        //                Console.WriteLine(rows.GetValue(i).ToString());
        //            }
        //            Console.WriteLine();
        //        }
        //        SQLiteConnector.Connection.Close();
        //    }
        //}

//---------------------------------------------------------------------------------------------- mycode  start

        public SQLiteRow getTableFields(string tableName)//создаем публичный метод  getTableFields ()  класса SQLiteDBEngine, 
          //SQLiteRow - тип возвращаемого значения -  return sqliteRow, а если не возвращается пишем void;
        {
            if (!DoesTableExists(tableName))//проверяем, что если таблица с таким именем не существует, то выход из метода, 
            {
                return null;
            }
            string queryGetTableFields = $"PRAGMA table_info('{tableName}')";//текст sql запроса, для получения информации о таблице
            SQLiteConnector.Connection.Open();// открываем подключение к базе данных test.db
            SQLiteCommand sQLiteCommand = new SQLiteCommand(queryGetTableFields, SQLiteConnector.Connection);//передаем текст запроса и подключение
            SQLiteDataReader qLiteDataReader = sQLiteCommand.ExecuteReader();//запуск sql запроса, qLiteDataReader - целая таблица

            SQLiteRow sqliteRow = new SQLiteRow();//создаем sqliteRow 

            foreach (DbDataRecord row in qLiteDataReader)// берем поочереди рядки таблицы
            {
                // Console.WriteLine(row.GetValue(2).GetType());

                long cid = row.GetInt64(0);
                string name = row.GetString(1);
                string dataType = row.GetString(2);
                // SQLiteDataTypes dataType = (SQLiteDataTypes)Enum.Parse(typeof(SQLiteDataTypes), row.GetString(2), true);
                bool notNull = Convert.ToBoolean(row.GetInt64(3));  
                string defaultValue = "NULL";//если это не  string , то останется NULL
                if (row.GetValue(4) is string)
                {
                    defaultValue = row.GetString(4);
                }
                bool pk = Convert.ToBoolean(row.GetInt64(5));
                SQLiteColumn column = new SQLiteColumn(cid, name, dataType, notNull, defaultValue, pk);
                sqliteRow.AddColumn(column);//добавляем столбец в список столбцов
            }
            SQLiteConnector.Connection.Close();                                
            return sqliteRow;           
            
        }


        //---------------------------------------------------------------------------------------------- mycode  finish

        public bool DoesTableExists(string tableName)
        {
            if(_tables.Count > 0)
            { 
                foreach (string table in _tables)
                {
                    if (table.ToLower().Equals(tableName.ToLower()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public List<string> Tables
        {
            get
            {
                return _tables;
            }
        }

    }
}
