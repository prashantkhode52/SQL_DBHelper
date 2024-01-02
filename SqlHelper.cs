using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MasterSoft
{
    namespace OBEDataAcessLayer
    {

        public class SqlHelper
        {
            private string _constr = string.Empty;      //connection string

            public SqlHelper(string connectionstring)
            {
                _constr = connectionstring;
            }
            public SqlHelper()
            {

            }
            public string Getcon()
            {
                string ConnectionString = string.Empty;
                return ConnectionString = ConfigurationManager.ConnectionStrings["DB_Con"].ConnectionString;

            }
        
       
            public object ExecuteNonQuerySP(String query, SqlParameter[] parameters, bool flag)
            {

                SqlConnection cnn = new SqlConnection(Getcon());
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.CommandTimeout = 2000;
                if (query.StartsWith("INSERT") | query.StartsWith("insert") | query.StartsWith("UPDATE") | query.StartsWith("update") | query.StartsWith("DELETE") | query.StartsWith("delete"))
                    cmd.CommandType = CommandType.Text;
                else
                    cmd.CommandType = CommandType.StoredProcedure;

                int i;
                for (i = 0; i < parameters.Length; i++)
                    cmd.Parameters.Add(parameters[i]);

                object retval = null;
                try
                {
                    cnn.Open();
                    retval = cmd.ExecuteNonQuery();

                    if (flag == true)
                        retval = cmd.Parameters[parameters.Length - 1].Value;
                }
                catch (Exception ex)
                {
                    retval = null;

                }
                finally
                {
                    if (cnn.State == ConnectionState.Open) cnn.Close();
                }
                return retval;
            }

            public DataSet ExecuteDataSetSP(String query, SqlParameter[] parameters)
            {
                DataSet ds = null;
                try
                {
                    SqlConnection cnn = new SqlConnection(Getcon());
                    SqlCommand cmd = new SqlCommand(query, cnn);
                    if (query.StartsWith("SELECT") | query.StartsWith("select") | query.StartsWith("INSERT") | query.StartsWith("insert") | query.StartsWith("UPDATE") | query.StartsWith("update") | query.StartsWith("DELETE") | query.StartsWith("delete"))
                        cmd.CommandType = CommandType.Text;
                    else
                        cmd.CommandType = CommandType.StoredProcedure;

                    int i;
                    for (i = 0; i < parameters.Length; i++)
                        cmd.Parameters.Add(parameters[i]);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    cmd.CommandTimeout = 180;
                    ds = new DataSet();
                    da.Fill(ds);

                }
                catch (Exception ex)
                {
                    ds = null;

                }
                return ds;
            }

            public SqlDataReader ExecuteDataReaderSP(String query, SqlParameter[] parameters)
            {

                SqlDataReader dr = null;
                using (SqlConnection cnn = new SqlConnection(Getcon()))
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(query, cnn))
                        {
                            if (query.StartsWith("SELECT") | query.StartsWith("select"))
                                cmd.CommandType = CommandType.Text;
                            else
                                cmd.CommandType = CommandType.StoredProcedure;

                            int i;
                            for (i = 0; i < parameters.Length; i++)
                                cmd.Parameters.Add(parameters[i]);

                            cnn.Open();
                            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                            return dr;
                        }
                    }
                    catch (Exception ex)
                    {
                        dr = null;

                        throw;
                    }

                }

            }

            public Object ExecuteScalarSP(String query, SqlParameter[] parameters)
            {
                Object retval;
                using (SqlConnection cnn = new SqlConnection(Getcon()))
                {
                    using (SqlCommand cmd = new SqlCommand(query, cnn))
                    {
                        if (query.StartsWith("SELECT") | query.StartsWith("select"))
                            cmd.CommandType = CommandType.Text;
                        else
                            cmd.CommandType = CommandType.StoredProcedure;



                        int i;
                        for (i = 0; i < parameters.Length; i++)
                            cmd.Parameters.Add(parameters[i]);




                        try
                        {
                            cnn.Open();
                            retval = cmd.ExecuteScalar();



                        }
                        catch (Exception ex)
                        {
                            retval = null;



                        }
                        finally
                        {
                            if (cnn.State == ConnectionState.Open) cnn.Close();
                        }
                    }
                }
                return retval;
            }

            public SqlDataReader ExecuteReaderSP(String query, SqlParameter[] parameters)
            {
                SqlDataReader dr = null;
                try
                {
                    SqlConnection cnn = new SqlConnection(Getcon());
                    SqlCommand cmd = new SqlCommand(query, cnn);
                    if (query.StartsWith("SELECT") | query.StartsWith("select"))
                        cmd.CommandType = CommandType.Text;
                    else
                        cmd.CommandType = CommandType.StoredProcedure;

                    int i;
                    for (i = 0; i < parameters.Length; i++)
                        cmd.Parameters.Add(parameters[i]);

                    cnn.Open();
                    cmd.CommandTimeout = 180;
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (Exception ex)
                {
                    dr = null;

                }
                return dr;
            }

         
          


        }
    }
}