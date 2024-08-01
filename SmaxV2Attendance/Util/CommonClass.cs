using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SmaxV2Attendance.Util
{
    public class CommonClass
    {
        string SqlConnectionString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];


        public int ExecuteSQL(string sqlstr)
        {
            SqlConnection con = new SqlConnection(SqlConnectionString);
            con.Open();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandText = sqlstr;
            sqlcmd.Connection = con;
            sqlcmd.CommandTimeout = 600;
            return sqlcmd.ExecuteNonQuery();

        }

        public DataSet GetDataSet(string sqlstr)
        {
            SqlConnection con = new SqlConnection(SqlConnectionString);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlstr, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();
            con = null;
            return ds;
        }

        public DataTable GetDataTable(string sqlstr)
        {
            SqlConnection con = new SqlConnection(SqlConnectionString);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlstr, con);
            adapter.SelectCommand.CommandTimeout = 0;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();
            con = null;
            return ds.Tables[0];
        }

        public bool ExecCommand(string sqlstr)
        {
            try
            {
                SqlConnection con = new SqlConnection(SqlConnectionString);
                con.Open();
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = con;
                sqlcmd.CommandText = sqlstr;
                sqlcmd.ExecuteNonQuery();
                con.Close();
                con = null;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
            return true;
        }
    }
}