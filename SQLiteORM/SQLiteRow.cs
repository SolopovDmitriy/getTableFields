using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteORM
{
    class SQLiteRow : IEnumerable
    {
        private List<SQLiteColumn> _columns;
        public SQLiteRow(int fieldCount)
        {
            if (fieldCount <= 0) throw new ArgumentOutOfRangeException();
            _columns = new List<SQLiteColumn>(fieldCount);
        }

        public SQLiteRow()//конструктор
        {
            _columns = new List<SQLiteColumn>();//инициализируем список
        
        }

        public void AddColumn(SQLiteColumn column)
        {
            _columns.Add(column);
        }


        public SQLiteColumn GetColumn(int index)
        {
            throw new NotImplementedException();
        }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public object this[int index]
        {
            get
            {
                return _columns[index];
            }
        }

        //---------------------------------------------------------------------------------------------- mycode  start
        public override string ToString()
        {
            string s = "";
            foreach (SQLiteColumn column in _columns)
            {
                s += column + "\n";
            }
            return s;
        }
        //---------------------------------------------------------------------------------------------- mycode  finish

    }
}

