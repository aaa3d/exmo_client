using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace exmo_client
{
    class trade
    {
        public string trade_id;
        public string type;
        public string price;
        public string quantity;
        public string amount;
        public string date;

    }
    class trades
    {
        public List<trade> BTC_USD = new List<trade>();

        /*
        public DataSet asDataset()
        {
            DataSet result = new DataSet();
            result.Tables.Add("trades");
            result.Tables["trades"].Columns.Add("trade_id");
            result.Tables["trades"].Columns.Add("type");
            result.Tables["trades"].Columns.Add("price");
            result.Tables["trades"].Columns.Add("quantity");
            result.Tables["trades"].Columns.Add("amount");
            result.Tables["trades"].Columns.Add("date");


            foreach (trade t in BTC_USD)
            {
                DataRow row = result.Tables["trades"].NewRow();
                row["trade_id"] = t.trade_id;
                row["trade_id"] = t.type;
                row["trade_id"] = t.price;
                row["trade_id"] = t.quantity;
                row["trade_id"] = t.amount;
                row["trade_id"] = DateTime.FromFileTimeUtc(int.Parse(t.date)).ToString();
            }

        }*/

    }
}
