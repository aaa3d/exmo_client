using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Configuration;
using System.Data.SqlServerCe;

namespace exmo_client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("https://api.exmo.com/v1/trades/?pair=BTC_USD");
            StreamReader sr = new StreamReader(stream);
            String jsonString = sr.ReadToEnd();

            



            trades _t = JsonConvert.DeserializeObject<trades>(jsonString);

            var connString = ConfigurationManager.ConnectionStrings["exmo_client.Properties.Settings.MyDatabase_1ConnectionString"].ConnectionString;
            
            var conn = new SqlCeConnection(connString);
            
            conn.Open();
            var cmd = new SqlCeCommand(@"SELECT trade_id from trades where trade_id = @trade_id", conn);
            var cmd_insert = new SqlCeCommand(
                @"INSERT INTO trades (pair, trade_id, type, quantity, price, amount, date)
                    VALUES(@pair, @trade_id, @type, @quantity, @price, @amount, @date);", conn);
            

            cmd.Prepare();
            cmd_insert.Prepare();

            foreach (trade t in  _t.BTC_USD)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@trade_id", t.trade_id);
                var result = cmd.ExecuteScalar();
                if (result == null){
                    cmd_insert.Parameters.Clear();
                    cmd_insert.Parameters.AddWithValue("@pair","BTC_USD");
                    cmd_insert.Parameters.AddWithValue("@trade_id",t.trade_id);
                    cmd_insert.Parameters.AddWithValue("@type", t.type);
                    cmd_insert.Parameters.AddWithValue("@quantity", t.quantity);
                    cmd_insert.Parameters.AddWithValue("@price", t.price);
                    cmd_insert.Parameters.AddWithValue("@amount", t.amount);
                    cmd_insert.Parameters.AddWithValue("@date", (new DateTime(TimeSpan.FromSeconds(long.Parse(t.date)).Ticks)).AddYears(1969));
                        
                    cmd_insert.ExecuteNonQuery();
                }
            }

            conn.Close();
            
            
            

            

            textBox1.Text = jsonString;

            stream.Close();
        }

        private void myDatabase1DataSetBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }
    }
}
