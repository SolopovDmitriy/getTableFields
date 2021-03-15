using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteORM
{
    class SQLiteColumn
    {
        private long _cid;
        private string _name;
        private string _dataType;// enum change string
        private bool _notNull;
        private string _defValue;
        private bool _pk;

        public SQLiteColumn(long cid, string name, string dataType, bool notNull, string defValue, bool pk)
        {
           
            _cid = cid;
            _name = name;
            _dataType = dataType;
            _notNull = notNull;
            _defValue = defValue;
            _pk = pk;
        }
        //---------------------------------------------------------------------------------------------- mycode  start
        public override string ToString()
        {
            return $"cid = {_cid}, name = {_name}, dataType = {_dataType}, notNul = {_notNull}, defValue = {_defValue}, pk = {_pk} ";
        }

        //---------------------------------------------------------------------------------------------- mycode  finish

    }
}
